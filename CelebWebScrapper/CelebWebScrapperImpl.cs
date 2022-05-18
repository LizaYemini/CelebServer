using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CelebContracts;
using DIContracts;
using HtmlAgilityPack;

namespace CelebWebScrapper
{
    [Register(Policy.Singleton, typeof(ICelebWebScrapper))]
    public class CelebWebScrapperImpl : ICelebWebScrapper
    {
        private readonly HtmlWeb _web;
        public CelebWebScrapperImpl()
        {
            _web = new HtmlWeb();
        }
        public List<CelebDto> GetCelebsFromPage(string url)
        {
            List<CelebDto> celebs = new List<CelebDto>();
            HtmlDocument doc = _web.Load(url);
            foreach (HtmlNode nameNode in doc.DocumentNode.SelectNodes("//h3[@class='lister-item-header']//a"))
            {
                string celebUrl = GetAbsoluteUrlString(url, nameNode.Attributes["href"].Value);
                CelebDto celeb = GetMissingInfoOnSpecificCeleb(celebUrl);
                celeb.Name = nameNode.InnerText.Replace("\n", "");
                celebs.Add(celeb);
                // Debug information
                Console.WriteLine("{0}: celeb number {1} added", DateTime.Now.ToString(), celebs.Count);
            }
            return celebs;
        }

        private CelebDto GetMissingInfoOnSpecificCeleb(string url)
        {
            CelebDto celeb = new CelebDto();
            HtmlDocument celebDoc = _web.Load(url);
            // Image Url handle
            HtmlNode imgUrlNode = celebDoc.DocumentNode.SelectNodes("//img[@id='name-poster']")[0];
            celeb.ImgUrl = imgUrlNode.Attributes["src"].Value;
            // Date Of Birth handle
            HtmlNodeCollection time = celebDoc.DocumentNode.SelectNodes("//time//a");
            string monthDay = time[0].InnerText;
            string year = time[1].InnerText;
            celeb.DateOfBirth = monthDay + ", " + year;
            // Gender handle
            HtmlNodeCollection bio = celebDoc.DocumentNode.SelectNodes("//div[@id='name-bio-text']");
            string bioText = bio[0].InnerText;
            //celeb.Gender = bioText.Contains("She") || bioText.Contains("she")  ? "Female" : "Male";
            celeb.Gender = Regex.IsMatch(bioText, @"\bShe\b") ||
                           Regex.IsMatch(bioText, @"\bshe\b") || 
                           Regex.IsMatch(bioText, @"\bHer\b") ||
                           Regex.IsMatch(bioText, @"\bher\b") ? "Female" : "Male";
            // Role handle
            HtmlNodeCollection roles = celebDoc.DocumentNode.SelectNodes("//div//a//span[@class='itemprop']");
            celeb.Role = roles[0].InnerText.Replace("\n","");
            return celeb;
        }
        static string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(baseUrl), uri);
            return uri.ToString();
        }
    }
}
