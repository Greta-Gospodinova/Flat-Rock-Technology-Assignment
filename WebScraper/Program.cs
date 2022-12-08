using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"C:\Users\gospo\OneDrive\Desktop\Flat Rock\WebScraperSolution\WebScraper\index.html");

            List<Product> products = new List<Product>();
            List<HtmlNode> productItems = doc.DocumentNode.SelectNodes("div").ToList();

            foreach (HtmlNode item in productItems)
            {
                string name = item.SelectSingleNode("div/h4/a").InnerText;
                name = DecodeHtmlEntity(name);
                string price = item.SelectSingleNode("div/div/p/span/span").InnerText;
                price = price.Remove(0, 1);
                string rating = item.GetAttributeValue("rating", "0");
                rating = (double.Parse(rating) / 2).ToString();

                Product product = new Product(name, price, rating);

                products.Add(product);
            }
            string result = JsonConvert.SerializeObject(products, Formatting.Indented);
            Console.WriteLine(result);
        }


        public static string DecodeHtmlEntity(string text)
        {
            StringWriter decodedString = new StringWriter();

            if (text.Contains("&"))
            {
                int htmlEntityIndex = text.IndexOf("&");

                string toDecode = text.Substring(htmlEntityIndex, 5);
                HttpUtility.HtmlDecode(toDecode, decodedString);

                text = text.Remove(htmlEntityIndex, 5);
                text = text.Insert(htmlEntityIndex, decodedString.ToString());

            }
            return text.ToString();
        }
    }
}
