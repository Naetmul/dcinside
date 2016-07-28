using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static partial class Dcinside
{
    public static class Comment
    {
        /// <summary>
        /// 모바일 디시를 통해 댓글을 단다. 
        /// </summary>
        /// <param name="gall">galme_classic 갤러리 아이디 </param>
        /// <param name="no">글번호</param>
        /// <param name="name">닉</param>
        /// <param name="pw">비번</param>
        /// <param name="content">할말</param>
        public static async Task WriteComment(String gall, String no, String name, String pw, String content)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://m.dcinside.com/_option_write.php");

            request.Headers.Host = "m.dcinside.com";
            request.Headers.Referrer = new Uri("http://m.dcinside.com/view.php?id=" + gall + "&no=" + no);
            request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
            request.Headers.TryAddWithoutValidation("Accept", "*/*");
            request.Headers.TryAddWithoutValidation("User-Agent",
                        "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "sdch");
            request.Headers.TryAddWithoutValidation("Accept-Language", "ko-KR,ko;q=0.8,en-US;q=0.6,en;q=0.4");

            StringBuilder postData = new StringBuilder();
            postData.AddPostData("comment_nick", name);
            postData.AddPostData("comment_pw", pw);
            postData.AddPostData("comment_memo", pw);
            postData.AddPostData("mode", "comment_nonmember");
            postData.AddPostData("voice_file", "");
            postData.AddPostData("di_code", "");
            postData.AddPostData("no", no);
            postData.AddPostData("id", gall);
            postData.AddPostData("board_id", "");
            postData.AddPostData("user_no", "");
            postData.AddPostData("date_time", DateTime.Now.ToString("yyyy.MM.dd HH:mm"));
            postData.AddPostData("ip", "");
            postData.AddPostData("best_chk", "");
            request.Content = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            await client.SendAsync(request);
        }

        /// <summary>
        /// 모바일 디시를 통해 댓글을 단다.
        /// </summary>
        /// <param name="url">갤러리 주소http로 시작하는거</param>
        /// <param name="name">닉</param>
        /// <param name="pw">비번</param>
        /// <param name="content">할말</param>
        public static async void WriteComment(String url, String name, String pw, String content)
        {
            String gall;
            String no;
            ParseURL(url, out gall, out no);
            await WriteComment(gall, no, name, pw, content);
        }
    }

    
}
