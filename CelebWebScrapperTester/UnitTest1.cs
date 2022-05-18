using System.Collections.Generic;
using CelebContracts;
using CelebWebScrapper;
using NUnit.Framework;

namespace CelebWebScrapperTester
{
    public class Tests
    {
        private ICelebWebScrapper _webScrapper;
        [SetUp]
        public void Setup()
        {
            _webScrapper = new CelebWebScrapperImpl();
        }

        [Test]
        public void Test1()
        {
            List<CelebDto> celebs = _webScrapper.GetCelebsFromPage("https://www.imdb.com/list/ls052283250");
            Assert.AreEqual(100, celebs.Count);
        }
    }
}