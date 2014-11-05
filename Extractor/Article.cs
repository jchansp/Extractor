using System;
using System.Collections.Generic;

namespace Extractor
{
    public class Article
    {
        public Article(Uri link, string thumb, string title, decimal price)
        {
            Link = link;
            Thumb = thumb;
            Title = title;
            Price = price;
        }

        public Article()
        {
        }

        public List<string> Categories { get; set; }
        public Uri Link { get; set; }
        public Uri Picture { get; set; }
        public decimal Price { get; set; }
        public string Thumb { get; set; }
        public string Title { get; set; }
        public int Units { get; set; }
    }
}