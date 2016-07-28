using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Task.Run(async () =>
        {
            String no = await Dcinside.Article.WriteArticle("game_classic", "asd", "123123", "dadslad11skjadsld", "ㅇdaslㅇㅇㅇㅇ?");
            await Dcinside.Comment.WriteComment("game_classic",no, "ㅇㅇ", "12312", "123132312");
            Console.WriteLine("끝");
        }); 
       
        Console.Read();
    }
}
