using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace KimotilkaV3.Models
{
    public static class DbMethods
    {
        public static IConfiguration Configuration;
        
        public static IDbConnection OpenConnection()
        {
            return new SqlConnection(AppSettings.ConnectionString);
        }
        
        
        public static async Task<Url> GetUrlByHash(string hash)
        {
            using var conn = OpenConnection();
            var p = new DynamicParameters();
            p.Add("@Hash", dbType: DbType.String, value: hash);
            Url url = await conn.QuerySingleOrDefaultAsync<Url>("dbo.[Url.GetUrlByHash]",p, commandType: CommandType.StoredProcedure );
            return url;
        }

        public static async  Task Deactivate(string hash)
        {
            using var conn = OpenConnection();
            var p = new DynamicParameters();
            p.Add("@Hash", dbType: DbType.String, value: hash);
            await conn.ExecuteAsync("[Url.Deactivate]",p, commandType: CommandType.StoredProcedure );
        }
        
        public static async Task<bool> IsHashExists(string hash)
        {
            using var conn = OpenConnection();
            var p = new DynamicParameters();
            p.Add("@Hash", dbType: DbType.String, value: hash);
            return await conn.ExecuteScalarAsync<bool>("select dbo.[fn.Url.IsHashExists](@Hash);", p);
        }

        public static async Task CreateUrl(string hash, string url, DateTime? startDate = null, DateTime? endDate = null)
        {
            using var conn = OpenConnection();
            var p = new DynamicParameters();
            p.Add("@Hash", dbType: DbType.String, size: 8, value: hash);
            p.Add("@Url", dbType: DbType.String, size: 1024, value: url);
            p.Add("@StartDate", dbType: DbType.DateTime, value: startDate);
            p.Add("@ExpireDate", dbType: DbType.DateTime, value: endDate);
            conn.Execute("[Url.Create]", p, commandType: CommandType.StoredProcedure);
        }
        
        
        
    }
}