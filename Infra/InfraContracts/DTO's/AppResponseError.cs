namespace InfraContracts
{
    public class AppResponseError : Response
    {
        public AppResponseError(string msg)
        {
            Message = msg;
        }

        public string Message { get; }
    }
}