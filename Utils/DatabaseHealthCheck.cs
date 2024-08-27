using Back.Db;
using Metrics;
using Metrics.Core;
using Microsoft.EntityFrameworkCore;

namespace Back.Utils
{
    public class DatabaseHealthCheck(MySqlContext database) : HealthCheck("DatabaseHealthCheck")
    {
        private readonly MySqlContext _db = database;

        protected override HealthCheckResult Check()
        {
            try
            {
                _db.Database.ExecuteSqlRaw("SELECT 1");
                return HealthCheckResult.Healthy($"Database works fine, last check at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Database doesn't work, last check at: {DateTime.Now}");
            }
        }
    }
}
