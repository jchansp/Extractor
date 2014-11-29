using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using CsQuery;
using Persistence;

namespace Extractor
{
    public static class Program
    {
        private static readonly string Scheme = ConfigurationManager.AppSettings["Scheme"];
        private static readonly string Host = ConfigurationManager.AppSettings["Host"];
        //public static List<System.Uri> Links { get; set; }
        //public static List<Article> Articles { get; set; }
        //public static DatabaseEntities DatabaseEntities;
        private static CQ Dom { get; set; }

        private static void Main()
        {
            var url = new Uri(Scheme + "://" + Host);
            //using (var writer = new StreamWriter("Links.txt", false))
            //{
            //    writer.WriteLine();
            //}
            //using (var writer = new StreamWriter("Articles.txt", false))
            //{
            //    writer.WriteLine();
            //}
            //Links = new List<System.Uri>();
            //Articles = new List<Article>();
            //DatabaseEntities = new DatabaseEntities();
            FindArticles(url);
        }

        private static void FindArticles(Uri url)
        {
            //if (url == null || Links.Exists(link => link == url) || url.Host != Host)
            if (url == null || Uris.Exists(url) || url.Host != Host)
                return;
            //using (var writer = new StreamWriter("Links.txt", true))
            //{
            //    writer.WriteLine(url);
            //}
            Debug.WriteLine(url);
            //DatabaseEntities.Uris.Add(new Uri {AbsoluteUri = url.ToString()});
            Uris.Add(url);
            //DatabaseEntities.SaveChanges();
            try
            {
                Dom = CQ.CreateFromUrl(url.ToString());
                if (Dom.Find(".opz_product").Length == 1)
                {
                    //using (var writer = new StreamWriter("Articles.txt", true))
                    //{
                    //    writer.WriteLine(url);
                    //    writer.WriteLine(ParseUri(Dom.Select(".opz_product img").Get(0).GetAttribute("src")));
                    //    writer.WriteLine(Dom.Select(".opz_product .opz_title").Get(0).InnerHTML);
                    //    writer.WriteLine(
                    //        Convert.ToDecimal(
                    //            Dom.Select(".opz_product .lzr_preciofichasart_iva").Get(0).InnerHTML.Split(' ')[0],
                    //            new CultureInfo("es-ES")));
                    //    writer.WriteLine(Convert.ToInt32(Dom.Select(".opz_product .textounidadesf").Get(0).InnerHTML));
                    //    Dom.Select("[rel='category tag']").Each(o => writer.WriteLine(o.InnerHTML));
                    //}
                    //var article = /*new
                    //Articles.Add(*/ new Article
                    //    {
                    //        Link = url.ToString(),
                    //        //Picture = ParseUri(Dom.Select(".opz_product img").Get(0).GetAttribute("src")).ToString(),
                    //        Title = Dom.Select(".opz_product .opz_title").Get(0).InnerHTML,
                    //        Price =
                    //            Convert.ToDecimal(
                    //                Dom.Select(".opz_product .lzr_preciofichasart_iva").Get(0).InnerHTML.Split(' ')[0],
                    //                new CultureInfo("es-ES")),
                    //        //Units = Convert.ToInt32(Dom.Select(".opz_product .textounidadesf").Get(0).InnerHTML),
                    //        //Units = Convert.ToByte(Dom.Select(".opz_product .textounidadesf").Get(0).InnerHTML),
                    //        //Categories = new List<string>(Dom.Select("[rel='category tag']").Map(o => o.InnerHTML))
                    //    } /*);*/;
                    //DatabaseEntities.Articles.Add(article);
                    //DatabaseEntities.SaveChanges();
                }
                Dom.Select("a")
                    .Each(link => FindArticles(ParseUri(link.HasAttribute("href") ? link.GetAttribute("href") : "")));
            }
            catch (WebException e)
            {
                Debug.WriteLine(e);
            }
        }

        private static Uri ParseUri(string href)
        {
            if (Uri.IsWellFormedUriString(href, UriKind.Absolute)) return new Uri(href);
            if (!href.StartsWith("/"))
            {
                href = "/" + href;
            }
            if (Uri.IsWellFormedUriString(href, UriKind.Relative))
            {
                href = Scheme + "://" + Host + href;
            }
            else
            {
                return null;
            }
            return new Uri(href);
        }
    }
}