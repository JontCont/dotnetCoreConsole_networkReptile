using HtmlAgilityPack;

namespace networkReptile
{



    internal class Program
    {
        static async Task Main(string[] args)
        {
            //設定爬的網站
            string url = "https://udn.com/news/cate/2/6644";
            
            //取得當前 html 字串
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            //設定response Body
            HtmlDocument doc = new ();
            doc.LoadHtml(responseBody);

            Console.WriteLine($"!! ----- 即時新聞 ------ !!");

            //取得想要的內容
            for (int i = 1; i<10; i++)
            {
                string xpath = @$"/html/body/main/div/section[2]/section[2]/div[1]/div[{i}]/div[2]/h2/a";
                HtmlNodeCollection content = doc.DocumentNode.SelectNodes(xpath);
                if(content == null) { continue; }
                foreach (HtmlNode node in content)
                {
                    string href = doc.DocumentNode.SelectNodes(xpath+ @"/@href").FirstOrDefault().Attributes.FirstOrDefault().Value.ToString();
                    Console.WriteLine($"{i} - {node.InnerText} (https://udn.com/{href})");
                    break;
                }//foreach (HtmlNode node in content)
            }//for()

        }//main()
    }


}