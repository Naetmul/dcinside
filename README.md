# dcinside
C# 디시 자동화 프로그램 
Task.Run(async () =>
        {
            String no = await Dcinside.Article.WriteArticle("game_classic", "asd", "123123", "dadslad11skjadsld", "ㅇdaslㅇㅇㅇㅇ?");
            await Dcinside.Comment.WriteComment("game_classic",no, "ㅇㅇ", "12312", "123132312");
            Console.WriteLine("끝");
        }); 
ㅇㅣ게 글쓰고 글쓴 게시글에 대해서 댓글달기.
