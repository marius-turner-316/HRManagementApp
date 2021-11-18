using Application.Common.Interfaces;
using System;
using System.Configuration;

namespace Infrastructure.Services
{
    public class Configuration : IConfiguration
    {
        public string ConnectionString { get; private set; }
        public int TotalResultsPerPage { get; private set; }

        public Configuration()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            TotalResultsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["TotalResultsPerPage"]);
        }
    }
}
