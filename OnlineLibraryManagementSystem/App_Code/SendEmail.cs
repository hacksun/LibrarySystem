using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// 发送邮件的相关操作
/// </summary>
public class SendEmail
{
    static System.Net.Mail.SmtpClient client = null;
    //接收者邮件地址，邮件主题，邮件正文
    public static void Send(string Receiver, string Subject, string content)
    {
        if (string.IsNullOrEmpty(Receiver) || string.IsNullOrEmpty(Subject)
            || string.IsNullOrEmpty(content))
        {
            throw new ArgumentNullException("SendEmail参数空异常！");
        }
        if (client == null)
        {
            try
            {
                //163发送配置                    
                client = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.163.com",
                    Port = 25,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    UseDefaultCredentials = true,


                    //qq发送配置的参数//切EnableSsl必须设置为true  
                    //client = new System.Net.Mail.SmtpClient();
                    //client.Host = "smtp.qq.com";
                    //client.Port = 25;
                    //client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //client.EnableSsl = true;
                    //client.UseDefaultCredentials = false;

                    Credentials = new System.Net.NetworkCredential("spm_a4@163.com", "SPM12345")
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        try
        {
            System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
            Message.SubjectEncoding = System.Text.Encoding.UTF8;
            Message.BodyEncoding = System.Text.Encoding.UTF8;
            Message.Priority = System.Net.Mail.MailPriority.High;

            Message.From = new System.Net.Mail.MailAddress("spm_a4@163.com", "OnlineLibraryManagement");
            //收件人中加上发送者，防止被当做垃圾邮件（我也不懂为什么加上这个就可以了= =）
            Receiver += ",spm_a4@163.com";
            //添加邮件接收人地址
            string[] receivers = Receiver.Split(new char[] { ',' });
            Array.ForEach(receivers.ToArray(), ToMail => { Message.To.Add(ToMail); });
            //抄送自己一份
            //Message.CC.Add("spm_a4@163.com");
           
            Message.Subject = Subject;
            Message.Body = content;
            Message.IsBodyHtml = true;
            client.Send(Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}