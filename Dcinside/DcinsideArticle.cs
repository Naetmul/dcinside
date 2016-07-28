using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static partial class Dcinside
{
    public static class Article
    {
        //id=game_classic&w_subject=1312312&w_memo=132312312&w_filter=&mode=write_verify

        private async static Task<String> getBlockKey(String gall, String title, String content)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://m.dcinside.com/_option_write.php");

            request.Headers.Host = "m.dcinside.com";
            request.Headers.Referrer = new Uri("http://m.dcinside.com/write.php?id=" + gall + "&mode=write");
            request.Headers.TryAddWithoutValidation("X-Requested-With", "\"XMLHttpRequest\"");
            request.Headers.TryAddWithoutValidation("\"Accept\"", "*/*");
            request.Headers.TryAddWithoutValidation("User-Agent",
                        "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "\"sdch\"");
            request.Headers.TryAddWithoutValidation("Accept-Language", "ko-KR,ko;q=0.8,en-US;q=0.6,en;q=0.4");
            StringBuilder postData = new StringBuilder();
            postData.AddPostData("id", "game_classic");
            postData.AddPostData("w_subject", title);
            postData.AddPostData("w_memo", content);
            postData.AddPostData("w_filter", "");
            postData.AddPostData("mode", "write_verify");

            request.Content = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            String response = await (await client.SendAsync(request)).Content.ReadAsStringAsync();
            Regex regex = new Regex("{\"msg\":\"\\d\",\"data\":\"([a-zA-Z0-9]+)\"}");
            return regex.Match(response).Groups[1].Value;
        }

        private async static Task<String> getCodeKey(String gall)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://m.dcinside.com/write.php?id=" + gall + "&mode=write");

            request.Headers.Host = "m.dcinside.com";
            request.Headers.Referrer = new Uri("http://m.dcinside.com/list.php?id=" + gall);
            request.Headers.TryAddWithoutValidation("X-Requested-With", "\"XMLHttpRequest\"");
            request.Headers.TryAddWithoutValidation("\"Accept\"", "*/*");
            request.Headers.TryAddWithoutValidation("User-Agent",
                        "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "\"sdch\"");
            request.Headers.TryAddWithoutValidation("Accept-Language", "ko-KR,ko;q=0.8,en-US;q=0.6,en;q=0.4");
            StringBuilder postData = new StringBuilder();

            String response = await (await client.SendAsync(request)).Content.ReadAsStringAsync();

            Regex regex = new Regex("name=\"code\" value=\"([a-zA-Z0-9]+)\"");
            return regex.Match(response).Groups[1].Value;
        }
        /// <summary>
        /// 글쓰는 프로그램
        /// </summary>
        /// <param name="gall">갤러리</param>
        /// <param name="name">이름</param>
        /// <param name="pw">비번</param>
        /// <param name="title">제목</param>
        /// <param name="content">내용</param>
        /// <returns></returns>
        public async static Task<String> WriteArticle(String gall, String name, String pw, String title, String content)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://upload.dcinside.com/g_write.php");
            var blockKey = getBlockKey(gall, title, content);
            var codekey = getCodeKey(gall);
            request.Headers.Host = "upload.dcinside.com";
            request.Headers.Referrer = new Uri("http://m.dcinside.com/write.php?id="+gall+"&mode=write");
            request.Headers.TryAddWithoutValidation("X-Requested-With", "\"XMLHttpRequest\"");
            request.Headers.TryAddWithoutValidation("\"Accept\"", "*/*");
            request.Headers.TryAddWithoutValidation("User-Agent",
                        "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "\"sdch\"");
            request.Headers.TryAddWithoutValidation("Accept-Language", "ko-KR,ko;q=0.8,en-US;q=0.6,en;q=0.4");

            String guidkey = String.Format("{0:N}", Guid.NewGuid());
            MultipartFormDataContent postData = new MultipartFormDataContent(guidkey);

            postData.Add(new StringContent(name), "\"name\"");
            postData.Add(new StringContent(pw), "\"password\"");
            postData.Add(new StringContent(title), "\"subject\"");
            postData.Add(new StringContent(content), "\"memo\"");
            postData.Add(new StringContent(""), "\"page\"");
            postData.Add(new StringContent("write"), "\"mode\"");
            postData.Add(new StringContent(gall), "\"id\"");
            
            String code = await codekey;
            //Console.WriteLine("CODE:"+code);
            postData.Add(new StringContent(code), "\"code\"");
            postData.Add(new StringContent(""), "\"no\"");
            postData.Add(new StringContent("mobile_nomember"), "\"mobile_key\"");
            postData.Add(new StringContent(""), "\"t_ch2\"");
            postData.Add(new StringContent(""), "\"FL_DATA\"");
            postData.Add(new StringContent(""), "\"OFL_DATA\"");
            postData.Add(new StringContent(""), "\"delcheck\"");
            String key = await blockKey;
            //Console.WriteLine("BLOCK"+key);
            postData.Add(new StringContent(key), "\"Block_key\"");
            postData.Add(new StringContent(""), "\"filter\"");
            postData.Add(new StringContent(""), "\"wikiTag\"");
            request.Content = postData;

            //Console.WriteLine(await request.Content.ReadAsStringAsync());
            //Console.WriteLine(await request.Content.ReadAsStringAsync()) ;
            var response = await client.SendAsync(request);
            Regex regex = new Regex("url=([^\"]+)");
            return regex.Match(await response.Content.ReadAsStringAsync()).Groups[1].Value;
        }
    }
}
