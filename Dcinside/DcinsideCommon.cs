using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static partial class Dcinside
{
    private static void ParseURL(String url, out String gall, out String no)
    {
        Regex regex = new Regex("id=([^\\&]+)\\&no=(\\d+)");
        Match match = regex.Match(url);
        gall = match.Groups[1].Value;
        no = match.Groups[2].Value;
    }

    //댓글
    private static void AddPostData(this StringBuilder post, String name, String value)
    {
        if (post.Length > 0)
        {
            post.Append("&");
        }
        post.Append(name).Append("=").Append(value);
    }



    /// <summary>
    /// //글쓸때
    ///-----------------------------19050650628927
    ///Content-Disposition: form-data; name="name"

    ///value
    ///이런구조다
    /// </summary>
    /// <param name="post"></param>
    /// <param name="guid"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    private static void AddMultipartPostData(this MultipartFormDataContent post, String guid, String name, String value)
    {
        post.Add(new StringContent(value), name);
        //post.Append("-----------------------------").Append(guid).Append("\r\n");
        //post.Append("Content-Disposition: form-data; name=\"").Append(name).Append("\"\r\n\r\n");
        //post.Append(value).Append("\r\n");
    }

    //private static void EndMultipartPostData(this MultipartFormDataContent post, String guid)
    //{
    //    post.Append("\r\n").Append("-----------------------------").Append(guid).Append("--");
    //}

}
