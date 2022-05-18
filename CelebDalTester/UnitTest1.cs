using System;
using System.Collections.Generic;
using CelebContracts;
using CelebDal;
using NUnit.Framework;

namespace CelebDalTester
{
    public class Tests
    {
        private Dictionary<string, CelebDto> _celebs;
        private ICelebDal _celebDal;
        [SetUp]
        public void Setup()
        {
            _celebDal = new CelebJsonDal();
            CelebDto celeb1 = new CelebDto
            {
                Name = "Johnny Depp",
                DateOfBirth = "June 9, 1963",
                Gender = "Male",
                Role = "Actor",
                ImgUrl =
                    "https://m.media-amazon.com/images/M/MV5BMTM0ODU5Nzk2OV5BMl5BanBnXkFtZTcwMzI2ODgyNQ@@._V1_UY209_CR3,0,140,209_AL_.jpg"
            };

            CelebDto celeb2 = new CelebDto
            {
                Name = "Arnold Schwarzenegger",
                DateOfBirth = "July 30, 1947",
                Gender = "Male",
                Role = "Actor",
                ImgUrl =
                    "https://m.media-amazon.com/images/M/MV5BMTI3MDc4NzUyMV5BMl5BanBnXkFtZTcwMTQyMTc5MQ@@._V1_UY209_CR13,0,140,209_AL_.jpg"
            };

            _celebs = new Dictionary<string, CelebDto>
            {
                { celeb1.Name, celeb1 },
                { celeb2.Name, celeb2 }
            };
        }

        [Test]
        public void CreateTest()
        {
            _celebDal.ResetDb(_celebs);
            Assert.AreEqual(_celebs.Count, _celebDal.GetAll().Count);
        }

        [Test]
        public void ResetTest()
        {
            _celebDal.ResetDb(_celebs);
            _celebDal.RemoveAll();
            Assert.AreEqual(0, _celebDal.GetAll().Count);
        }

        [Test]
        public void RemoveTest()
        {
            _celebDal.ResetDb(_celebs);
            _celebDal.Remove("Arnold Schwarzenegger");
            Assert.AreEqual(1, _celebDal.GetAll().Count);
        }
    }
}