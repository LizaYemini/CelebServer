using System;
using System.Collections.Generic;
using CelebContracts;
using DIContracts;
using InfraContracts;

namespace CelebService
{
    [Register(Policy.Singleton, typeof(ICelebService))]
    public class CelebServiceImpl : ICelebService
    {
        private readonly ICelebDal _dalService;
        private readonly ICelebWebScrapper _webScrapperService;
        public CelebServiceImpl(ICelebDal dalService, ICelebWebScrapper webScrapperService)
        {
            _dalService = dalService;
            _webScrapperService = webScrapperService;
        }

        public Response ResetDb()
        {
            Dictionary<string, CelebDto> celebsDictionary = new Dictionary<string, CelebDto>();
            List<CelebDto> celebs;
            try
            {
                celebs = _webScrapperService.GetCelebsFromPage("https://www.imdb.com/list/ls052283250");
            }
            catch (Exception e)
            {
                return new AppResponseError(e.Message);
            }

            foreach (CelebDto celeb in celebs)
            {
                if (!celebsDictionary.ContainsKey(celeb.Name))
                {
                    celebsDictionary.Add(celeb.Name, celeb);
                }
            }

            try
            {
                _dalService.ResetDb(celebsDictionary);
                return GetAllCelebs();
            }
            catch (Exception e)
            {
                return new AppResponseError(e.Message);
            }
        }

        public bool DbExists()
        {
            return _dalService.DbExists();
        }

        public Response GetAllCelebs()
        {
            try
            {
                Dictionary<string, CelebDto> celebDict = _dalService.GetAll();
                List<CelebDto> celebList = new List<CelebDto>();
                foreach (var value in celebDict.Values)
                {
                    celebList.Add(value);
                }
                return new GetAllCelebsResponseOk
                {
                    Celebs = celebList.ToArray()
                };
            }
            catch (Exception e)
            {
                return new AppResponseError(e.Message);
            }
        }

        public Response Remove(string key)
        {
            try
            {
                _dalService.Remove(key);
                return GetAllCelebs();
            }
            catch (Exception e)
            {
                return new AppResponseError(e.Message);
            }
        }
        
        public Response RemoveAll()
        {
            try
            {
                _dalService.RemoveAll();
                return GetAllCelebs();
            }
            catch (Exception e)
            {
                return new AppResponseError(e.Message);
            }
        }
    }
}
