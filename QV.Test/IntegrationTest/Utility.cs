using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace QV.Test.IntegrationTest
{
    public class Utility
    {

        // Use this approach if you plan to build a special Database for testing.
        // Besure to comment out the Database Init code in the QV21 Context so it does not rebuild the db.
        // You can point all of your testing to another database connection for testing to avoid the above issue.
        public static void CreateSeededTestDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MasterDbConnection"].ConnectionString;

            var path = Environment.CurrentDirectory.Replace("bin\\Debug", "Sql\\MakeDatabaseScriptFile.sql");
            var file = new FileInfo(path);
            var script = file.OpenText().ReadToEnd();

            using (var connection = new SqlConnection(connectionString))
            {
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(script);
            }

        }
    }
}
