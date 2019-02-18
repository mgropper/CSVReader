using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Collections;
using System.Data.SqlClient;

namespace readCSV
{
    public class SqliteDataAccess
    {
        public static List<data> Load()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<data>("select * from data", new DynamicParameters());
                return output.ToList();
            }
        }


        public static void Save(data d)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into data (n, unique_views, tag, content) values (@N, @Unique_views, @Tag, @Content)", d);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
