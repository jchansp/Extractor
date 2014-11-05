using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using CsQuery;

namespace Extractor
{
    public class Program
    {
        public static string Scheme = ConfigurationManager.AppSettings["Scheme"];
        public static string Host = ConfigurationManager.AppSettings["Host"];
        public static CQ Dom { get; set; }
        public static List<Uri> Links { get; set; }
        public static List<Article> Articles { get; set; }

        private static void Main()
        {
            var url = new Uri(Scheme + "://" + Host);
            using (var writer = new StreamWriter("Links.txt", false))
            {
                writer.WriteLine();
            }
            using (var writer = new StreamWriter("Articles.txt", false))
            {
                writer.WriteLine();
            }
            Links = new List<Uri>();
            Articles = new List<Article>();
            FindArticles(url);
        }

        private static void FindArticles(Uri url)
        {
            if (url == null || Links.Exists(link => link == url) || url.Host != Host)
                return;
            using (var writer = new StreamWriter("Links.txt", true))
            {
                writer.WriteLine(url);
            }
            Debug.WriteLine(url);
            Links.Add(url);
            try
            {
                Dom = CQ.CreateFromUrl(url.ToString());
                if (Dom.Find(".opz_product").Length == 1)
                {
                    using (var writer = new StreamWriter("Articles.txt", true))
                    {
                        writer.WriteLine(url);
                        writer.WriteLine(ParseUri(Dom.Select(".opz_product img").Get(0).GetAttribute("src")));
                        writer.WriteLine(Dom.Select(".opz_product .opz_title").Get(0).InnerHTML);
                        writer.WriteLine(
                            Convert.ToDecimal(
                                Dom.Select(".opz_product .lzr_preciofichasart_iva").Get(0).InnerHTML.Split(' ')[0],
                                new CultureInfo("es-ES")));
                        writer.WriteLine(Convert.ToInt32(Dom.Select(".opz_product .textounidadesf").Get(0).InnerHTML));
                        Dom.Select("[rel='category tag']").Each(o => writer.WriteLine(o.InnerHTML));
                    }
                    Articles.Add(new Article
                        {
                            Link = url,
                            Picture = ParseUri(Dom.Select(".opz_product img").Get(0).GetAttribute("src")),
                            Title = Dom.Select(".opz_product .opz_title").Get(0).InnerHTML,
                            Price =
                                Convert.ToDecimal(
                                    Dom.Select(".opz_product .lzr_preciofichasart_iva").Get(0).InnerHTML.Split(' ')[0],
                                    new CultureInfo("es-ES")),
                            Units = Convert.ToInt32(Dom.Select(".opz_product .textounidadesf").Get(0).InnerHTML),
                            Categories = new List<string>(Dom.Select("[rel='category tag']").Map(o => o.InnerHTML))
                        });
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