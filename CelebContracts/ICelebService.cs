using InfraContracts;

namespace CelebContracts
{
    public interface ICelebService
    {
        bool DbExists();
        Response ResetDb();
        Response GetAllCelebs();
        Response Remove(string key);
        Response RemoveAll();
    }
}