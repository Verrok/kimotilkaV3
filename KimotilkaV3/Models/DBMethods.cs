using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using KimotilkaV2.Models;
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
        
        
        public static Url GetUrlByHash(string hash)
        {
            using var conn = OpenConnection();
            var p = new DynamicParameters();
            p.Add("@Hash", dbType: DbType.String, size: 8, value: hash);
            Url url = conn.QuerySingle<Url>("dbo.[Url.GetUrlByHash]",p, commandType: CommandType.StoredProcedure );
            return url;
        }

        public static void DeleteByHash(string hash)
        {
            using var conn = OpenConnection();
            var p = new DynamicParameters();
            p.Add("@Hash", dbType: DbType.String, size: 8, value: hash);
            conn.Execute("[Url.DeleteByHash]",p, commandType: CommandType.StoredProcedure );
        }
        
        public static bool CheckHash(string hash)
        {
            using var conn = OpenConnection();
            var p = new DynamicParameters();
            p.Add("@Hash", dbType: DbType.String, size: 8, value: hash);
            p.Add("@Exists", DbType.Binary, direction: ParameterDirection.Output);
            conn.Execute("[Url.CheckHash]", p, commandType: CommandType.StoredProcedure);
            return Convert.ToBoolean( p.Get<int>("Exists"));
        }

        public static void CreateUrl(string hash, string url, DateTime? startDate = null, DateTime? endDate = null)
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