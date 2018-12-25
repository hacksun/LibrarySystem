using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// 定时器任务，主要包括定时短信提醒和清理过期预定
/// </summary>
public class TimerTasks
{
    public static void Task()
    {
        System.Timers.Timer timer = new System.Timers.Timer(3000)
        {
            //AutoReset 属性为 true 时，每隔指定时间循环一次
            //如果为 false，则只执行一次
            AutoReset = true,
            Enabled = true
        };
        timer.Elapsed += new System.Timers.ElapsedEventHandler(Task_Email);
        timer.Elapsed += new System.Timers.ElapsedEventHandler(Task_ClearReservation);
    }
    static void Task_Email(object sender, EventArgs e)
    {
        //超期未还罚款期限
        int overdueDuration = int.Parse(ConfigurationManager.AppSettings["OverdueDuration"].ToString());
        //发送提醒邮件时间
        int noticeDuration = int.Parse(ConfigurationManager.AppSettings["NoticeDuration"].ToString());
        TimeSpan notice_time = TimeSpan.FromDays(noticeDuration);
        TimeSpan overdue_time = TimeSpan.FromDays(overdueDuration);
        DateTime time_now = DateTime.Now;

        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        MySqlConnection conn2 = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        conn2.Open();
        try
        {
            string querySql = "SELECT * FROM IssueRecords WHERE " +
                            "ReturnTime is null ORDER BY IssueTime ASC";
            MySqlCommand queryCmd = new MySqlCommand(querySql, conn);
            MySqlDataReader reader = queryCmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    //emailNoticeStatus表示已经向该订单用户发送邮件的数量
                    string bookbarcode = reader["BookBarcode"].ToString();
                    int emailNoticeStatus = (int)reader["EmailNoticeStatus"];
                    int returnStatus = (int)reader["Status"];
                    DateTime time_notice = (DateTime)reader["IssueTime"] + notice_time;
                    DateTime time_overdue = (DateTime)reader["IssueTime"] + overdue_time;
                    //DateTime.Compare前一个时间早于后一个时间时返回结果小于0
                    if (emailNoticeStatus < 2 && DateTime.Compare(time_notice, time_now) < 0)
                    {
                        //System.Diagnostics.Debug.WriteLine(time_overdue);
                        //FOR UPDATE加锁
                        string queryReaderEmailSql = "SELECT Email FROM Readers WHERE ReaderId = " +
                            reader["ReaderId"] + " FOR UPDATE";
                        MySqlCommand emailCmd = new MySqlCommand(queryReaderEmailSql, conn2);
                        MySqlDataReader reader2 = emailCmd.ExecuteReader();
                        reader2.Read();
                        String emailReceiver = null;
                        if (reader2.HasRows)
                            emailReceiver = (string)reader2["Email"];
                        reader2.Close();
                        //发送提醒邮件
                        if (emailNoticeStatus == 0)
                        {
                            SendEmail.Send(emailReceiver, "OnlineLibraryManagement书籍即将逾期提醒 Book Will Be Overdue Reminder",
                            "尊敬的用户，您在本馆所借书籍还有3天将逾期，请尽快归还，谢谢合作！\n"
                            + "Dear user, the books you borrowed in the library will be overdue 3 days later. Please return it as soon as possible. Thanks for your cooperation!");
                        }
                        // 发送逾期邮件
                        else if (emailNoticeStatus == 1 && DateTime.Compare(time_overdue, time_now) < 0)
                        {
                            SendEmail.Send(emailReceiver, "OnlineLibraryManagement书籍逾期提醒 Book Overdue Reminder",
                            "尊敬的用户，您在本馆所借书籍已经逾期未归还，请尽快归还，谢谢合作！\n"
                            + "Dear user, the books you borrowed in the library have been overdue and have not been returned. Please return it as soon as possible. Thanks for your cooperation!");
                        }
                        string updateEmailStatusSql = "UPDATE IssueRecords SET EmailNoticeStatus = "
                            + (emailNoticeStatus + 1) + " WHERE (RecordId = " + reader["RecordId"] + ");COMMIT WORK;";
                        MySqlCommand cmd2 = new MySqlCommand(updateEmailStatusSql, conn2);
                        cmd2.ExecuteNonQuery();
                    }
                    if (DateTime.Compare(time_overdue, time_now) < 0 && (returnStatus == 0 || returnStatus == 3))
                    {
                        MySqlConnection conn3 = new MySqlConnection(OLMSDBConnectionString);
                        try
                        {
                            conn3.Open();
                            TimeSpan ts = time_now - time_overdue;
                            int overduelength = ts.Days;
                            System.Diagnostics.Debug.WriteLine(overduelength);
                            double finePerDay = Convert.ToDouble(ConfigurationManager.AppSettings["OverdueFinePerDay"].ToString());
                            double fine = overduelength * finePerDay;
                            double deposit = Convert.ToDouble(ConfigurationManager.AppSettings["Deposit"].ToString());
                            //罚款不超过押金
                            if (fine > deposit)
                                fine = deposit;
                            //更改判断罚金逻辑，只有未归还的才需要判断罚金
                            string overdue_sql = "update IssueRecords, set Status=3, OverdueLength=?overduelength,Fine=?fine where BookBarcode=?bookbarcode and Status<>1 and Status<>2";
                            MySqlCommand cmd2 = new MySqlCommand(overdue_sql, conn3);
                            cmd2.Parameters.AddWithValue("?overduelength", overduelength);
                            cmd2.Parameters.AddWithValue("?fine", fine);
                            cmd2.Parameters.AddWithValue("?bookbarcode", bookbarcode);
                            int result = cmd2.ExecuteNonQuery();
                            System.Diagnostics.Debug.WriteLine(result);
                        }
                        catch (MySqlException ex)
                        {
                            System.Diagnostics.Debug.Write(ex.Message);
                        }
                        finally
                        {
                            conn3.Close();
                        }
                    }
                }
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            System.Diagnostics.Debug.Write(ex.Message);
        }
        finally
        {
            conn.Close();
            conn2.Close();
        }
    }

    //清除过期预定
    static void Task_ClearReservation(object sender, EventArgs e)
    {
        int overdueReservationDuration = int.Parse(ConfigurationManager.AppSettings["OverdueReservationDuration"].ToString());
        TimeSpan overdueRes_time = TimeSpan.FromHours(overdueReservationDuration);
        DateTime time_now = DateTime.Now;

        string OLMSDBConnectionString = ConfigurationManager.ConnectionStrings["OLMSDB"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(OLMSDBConnectionString);
        MySqlConnection conn2 = new MySqlConnection(OLMSDBConnectionString);
        conn.Open();
        conn2.Open();
        try
        {
            string querySql = "SELECT * FROM BookBarcodes WHERE Status = 2";
            MySqlCommand queryCmd = new MySqlCommand(querySql, conn);
            MySqlDataReader reader = queryCmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    DateTime time_overdueRes = (DateTime)reader["ReservingTime"] + overdueRes_time;
                    if (DateTime.Compare(time_overdueRes, time_now) < 0)
                    {
                        string ClearSql = "UPDATE BookBarcodes SET Status = 0, ReservingTime = NULL, ReservingReaderId = NULL WHERE(BookBarcode = ?bookBarcode);";
                        MySqlCommand ClearCmd = new MySqlCommand(ClearSql, conn2);
                        ClearCmd.Parameters.AddWithValue("?bookBarcode", reader["BookBarcode"]);
                        int result = ClearCmd.ExecuteNonQuery();
                        System.Diagnostics.Debug.WriteLine("ClearReservation: " + result);
                    }
                }
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            System.Diagnostics.Debug.Write(ex.Message);
        }
        finally
        {
            conn.Close();
            conn2.Close();
        }
    }
}