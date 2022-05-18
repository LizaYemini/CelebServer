using System.Collections.Generic;

namespace CelebContracts
{
    public interface ICelebWebScrapper
    {
        List<CelebDto> GetCelebsFromPage(string url);
    }
}