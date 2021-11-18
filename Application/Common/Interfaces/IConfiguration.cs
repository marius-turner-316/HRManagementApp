namespace Application.Common.Interfaces
{
    public interface IConfiguration
    {
        string ConnectionString { get; }
        int TotalResultsPerPage { get; }
    }
}
