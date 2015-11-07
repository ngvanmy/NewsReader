using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewsReaderApi.Controllers
{
    public class TinhTeController : ApiController
    {
        NewsReader.TinhTe _reader = new NewsReader.TinhTe();
        public List<NewsReader.NewsObject> GetNews()
        {
           return _reader.NewestNews(1);
        }
        public List<NewsReader.NewsObject> GetNewsWithPage(int page)
        {
            return _reader.NewestNews(page);
        }
    }
}
