using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
namespace NewsReader
{
    public class NewsObject
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }
    public abstract class NewsReader
    {
        protected string RootUrl { get; set; }
        protected string TrimHtml(string value)
        {
            return value.Replace("\t", "").Replace("\n", "");
        }
        public abstract List<NewsObject> NewestNews(int page);
    }
    public class TinhTe:NewsReader
    {
        public TinhTe()
        {
            RootUrl = "https://tinhte.vn/";
        }

        public override List<NewsObject> NewestNews(int page)
        {
            string htmlUrl = RootUrl;
            if (page > 1)
            {
                htmlUrl = RootUrl + "?wpage=" + page;
            }
            List<NewsObject> results = new List<NewsObject>();
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };
            HtmlDocument document = htmlWeb.Load(htmlUrl);
            var threadItems = document.DocumentNode.QuerySelectorAll("div.recentNews").ToList();
            foreach (var item in threadItems)
            {
                NewsObject news = new NewsObject();
                var linkNode = item.QuerySelector("h2.subHeading");
                var link = linkNode.QuerySelector("a").Attributes["href"].Value;
                news.Link = RootUrl + link;
                news.Text = TrimHtml(linkNode.InnerText);
                results.Add(news);
            }
            return results;
        }
    }
}
