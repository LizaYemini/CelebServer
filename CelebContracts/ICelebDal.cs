using System.Collections.Generic;

namespace CelebContracts
{
    public interface ICelebDal
    {
        bool DbExists();
        void ResetDb(Dictionary<string, CelebDto> celebs);
        Dictionary<string, CelebDto> GetAll();
        void Remove(string key);
        void RemoveAll();
    }
}