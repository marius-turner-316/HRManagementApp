using Application.Domain;
using Application.Common.Interfaces;
using Application.Common.Models;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HumanResourceRepository : IHumanResourceRepository
    {
        private readonly IConfiguration _config;
        private readonly ISystemClock _systemClock;

        public HumanResourceRepository(
            IConfiguration config, 
            ISystemClock systemClock)
        {
            _config = config;  
            _systemClock = systemClock;
        }

        public async Task<int> AddAsync(HumanResource humanResource)
        {
            humanResource.AddedOn = _systemClock.Now;
            var sql = @"INSERT INTO HumanResource (FirstName,Surname,Email,DOB,Department,Status,EmployeeNumber,AddedOn) 
                        VALUES (@FirstName,@Surname,@Email,@DOB,@Department,@Status,@EmployeeNumber,@AddedOn)";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, humanResource);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int humanResourceId)
        {
            var sql = @"UPDATE HumanResource 
                        SET DeletedOn = @DeletedOn
                        WHERE HumanResourceId = @HumanResourceId";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, new { HumanResourceId = humanResourceId, DeletedOn = _systemClock.Now });
                return result;
            }
        }

        public async Task<PaginatedList<HumanResource>> GetAllAsync(int? status = null, string department = null, int pageNumber = 1)
        {
            var sql = @"SELECT HumanResourceId,FirstName,Surname,Email,DOB,Department,Status,EmployeeNumber 
                        FROM HumanResource
                        WHERE (@Status IS NULL OR Status = @Status) AND
                              (@Department IS NULL OR Department LIKE CONCAT('%',@Department,'%')) AND
                              (DeletedOn IS NULL)
                        ORDER BY FirstName, Surname
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY

                        SELECT COUNT(*)
                        FROM HumanResource
                        WHERE (@Status IS NULL OR Status = @Status) AND
                              (@Department IS NULL OR Department LIKE '%@Department%') AND
                              (DeletedOn IS NULL)";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                await connection.OpenAsync();
                using (var multi = await connection.QueryMultipleAsync(sql, new
                {
                    Status = status,
                    Department = department,
                    Offset = (pageNumber - 1) * _config.TotalResultsPerPage,
                    PageSize = _config.TotalResultsPerPage
                }))
                {
                    var results = new PaginatedList<HumanResource>(
                    multi.Read<HumanResource>().ToList(),
                    multi.ReadFirst<int>(),
                    pageNumber,
                    _config.TotalResultsPerPage);

                    return results;
                }
            }
        }

        public async Task<HumanResource> GetByIdAsync(int humanResourceId)
        {
            var sql = @"SELECT HumanResourceId,FirstName,Surname,Email,DOB,Department,Status,EmployeeNumber 
                        FROM HumanResource
                        WHERE HumanResourceId = @HumanResourceId";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<HumanResource>(sql, new { HumanResourceId = humanResourceId });
                return result;
            }
        }

        public async Task<int> UpdateAsync(HumanResource entity)
        {
            entity.ModifiedOn = _systemClock.Now;
            var sql = @"UPDATE HumanResource 
                        SET FirstName = @FirstName, Surname = @Surname, Email = @Email, DOB = @DOB, Department = @Department, Status = @Status, EmployeeNumber = @EmployeeNumber, ModifiedOn = @ModifiedOn 
                        WHERE HumanResourceId = @HumanResourceId";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public int? CheckEmailExists(string email)
        {
            if (email is null) return null;

            var sql = @"SELECT TOP 1 HumanResourceId
                        FROM HumanResource
                        WHERE Email = @Email";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();
                var result = connection.ExecuteScalar<int?>(sql, new { Email = email });
                return result;
            }
        }

        public int? CheckEmployeeNumberExists(int employeeNumber)
        {
            var sql = @"SELECT TOP 1 HumanResourceId
                        FROM HumanResource
                        WHERE EmployeeNumber = @EmployeeNumber";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();
                var result = connection.ExecuteScalar<int?>(sql, new { EmployeeNumber = employeeNumber });
                return result;
            }
        }
    }
}
