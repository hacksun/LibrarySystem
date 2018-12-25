using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// BookInfoQuery 的摘要说明
/// </summary>
public static class BookInfoQuery
{
    public static Book GetByISBN(string ISBN)
    {
        string bookInfo = GetBookInfoJson(ISBN);

        if (bookInfo == null)
        {
            return null;
        }

        Book book = JsonConvert.DeserializeObject<Book>(bookInfo);
        
        return book;
    }

    private static string GetBookInfoJson(string ISBN)
    {
        var request = WebRequest.Create("https://api.douban.com/v2/book/isbn/:" + ISBN) as HttpWebRequest;
        request.Method = "GET";

        HttpWebResponse response;
        try
        {
            response = request.GetResponse() as HttpWebResponse;
        }
        catch (Exception)
        {
            return null;
        }
            Stream responseStream = response.GetResponseStream();
            var responseReader = new StreamReader(responseStream);
            string bookInfo = responseReader.ReadToEnd();
            responseReader.Close();
            responseStream.Close();

            return bookInfo;
    }
}

[Serializable]
public class Book
{
    public string subtitle { get; set; }               //
    public List<String> author { get; set; }           //
    public string pubdate { get; set; }                //
    public string origin_title { get; set; }           //
    public string image { get; set; }                  //
    public string binding { get; set; }                //
    public List<String> translator { get; set; }       //
    public string catalog { get; set; }                //
    public string pages { get; set; }                  //
    public string publisher { get; set; }              //
    public string isbn10 { get; set; }                 //
    public string isbn13 { get; set; }                 //
    public string title { get; set; }                  //
    public string author_intro { get; set; }           //
    public string summary { get; set; }                //
    public string price { get; set; }                  //
    public tags[] tags { get; set; }
}
[Serializable]
public class tags
{
    public string count { get; set; }//
    public string name { get; set; }//
    public string title { get; set; }//
}