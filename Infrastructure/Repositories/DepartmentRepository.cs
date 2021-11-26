using Application.Common.Interfaces;
using Application.Domain;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration _config;

        public DepartmentRepository(
            IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            var sql = @"SELECT DepartmentId, Name 
                        FROM Department
                        ORDER BY Name";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Department>(sql);
                return result;
            }
        }
    }
}
