using HtmlAgilityPack;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace networkReptile
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //設定爬的網站
            string url = "https://www.69shu.com/47120/";
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            HtmlWeb web = new();
            web.OverrideEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HtmlDocument doc = web.Load(url);

            string xpath = @$"//*[@id=""catalog""]/ul/li";
            HtmlNodeCollection content = doc.DocumentNode.SelectNodes(xpath);
            for (int index = 1; index < content.Count; index++)
            {

                string strLinkUrl = xpath + @$"[{index}]/a/@href";
                string strLinkName = xpath + @$"[{index}]/a";
                var links = doc.DocumentNode.SelectNodes(strLinkUrl).FirstOrDefault().Attributes.FirstOrDefault().Value.ToString();
                var names = doc.DocumentNode.SelectNodes(strLinkName).Select(x => x.InnerHtml).FirstOrDefault(); //.Select(x => x.Attributes.FirstOrDefault()?.Value);
                Console.WriteLine($"{index} - {names} : ({links}");
                DownloadHtml(links, names);
            }

        }//main()



        static void DownloadHtml(string url, string name)
        {
            WebClient wc = new();
            wc.DownloadFile(url, $"d:\\Temp\\{name}.html");
        }



    }


}