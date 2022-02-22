using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class ConnectionString
    {
          public static SQLiteConnection cnn = new SQLiteConnection("Data Source=EDSCloudComputing.sqlite;Version=3;");


          public static string CnnString
          {
              get
              {
                  return "Data Source=EDSCloudComputing.sqlite;Version=3;Pooling=True;Max Pool Size=500;";
              }
          }

        //public static SQLiteConnection cnn
        //{
        //    SQLiteConnection sqlConnection = new SQLiteConnection(ConnectionString.CnnString);
        //    return sqlConnection;
        //}
    }

    //class ConnectionString
    //{
    //    public static SQLiteConnection cnn = new SQLiteConnection("Data Source=EDSCloudComputing.sqlite;Version=3;");
    //}


    //public static SqlConnection GetConnection()
    //    {
    //        SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString);
    //        return sqlConnection;
    //    }








    //public static SQLiteConnection cnn
    //    {
    //        get
    //        {
    //           return new SQLiteConnection("Data Source=EDSCloudComputing.sqlite;Version=3;Synchronous=Full;");
    //        }
    //    }
}
