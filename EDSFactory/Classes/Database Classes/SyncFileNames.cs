using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace EDSFactory 
{
     partial class DatabaseOperation
     {
         public class NTP
         {
             private static NTP m_do;
             private readonly object sendSyncRoot = new object();

             public static NTP Singleton()
             {
                 if (m_do == null)
                     m_do = new NTP();
                 
                 return m_do;
             }

             //public MyList<string> Select()
             //{
             //    MyList<string> fileNames = new MyList<string>();

             //    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
             //    {
             //        try
             //        {
             //            conn.Open();

             //            string selectsyncFileNames = "SELECT * FROM NTP";

             //            SQLiteCommand command = new SQLiteCommand();
             //            command.Connection = conn;
             //            command.CommandText = selectsyncFileNames;

             //            DbDataReader reader = command.ExecuteReader();

             //            while (reader.Read())
             //            {
             //                fileNames.Add(reader["FileName"].ToString());
             //            }


             //            command.Dispose();
             //            reader.Dispose();
             //        }
             //        catch
             //        {

             //        }
             //    }

             //    return fileNames;
             //}

             private static readonly object m_lockAsyncSelect = new object();
             public async Task<List<string>> AsycSelect()
             {
                 Monitor.Enter(m_lockAsyncSelect); 

                 List<string> fileNames = new List<string>();

                 try
                 {
                     using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                     {
                         await conn.OpenAsync();

                         string selectsyncFileNames = "SELECT * FROM NTP";

                         using (SQLiteCommand command = new SQLiteCommand())
                         {
                             command.Connection = conn;
                             command.CommandText = selectsyncFileNames;

                             using (DbDataReader reader = await command.ExecuteReaderAsync())
                             {
                                 while (await reader.ReadAsync())
                                 {
                                     fileNames.Add(reader["FileName"].ToString());
                                 }

                                 reader.Dispose();

                                 return fileNames;
                             } 
                         }
                      
                     }
                 }
                 catch (Exception ex)
                 {
                     SQLiteConnection.ClearAllPools();
                     Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP AsycSelect");
                     return fileNames;
                 }
                 finally
                 {
                     Monitor.Exit(m_lockAsyncSelect);
                 }

             }

             int counter = 0;
             int counter1 = 0;
             private static readonly object m_lockAsyncInsert = new object();
             public async Task<int> AsyncInsert(List<string> value)
             {
                 //counter++;

                 Monitor.Enter(m_lockAsyncInsert); 
                 //if (Monitor.TryEnter(m_lockAsyncInsert))
                 //{
                     int result = 0;

                     try
                     {
                         using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                         {
                             await conn.OpenAsync();
                             using (SQLiteCommand insertCommand = new SQLiteCommand("insert into NTP (FileName) values (@FileName)", conn))
                             {
                                 insertCommand.Parameters.AddWithValue("@FileName", value[0]);
                                 result = await insertCommand.ExecuteNonQueryAsync();  

                                 return result; 
                                 //if (result > 0)
                                 //    return true;
                                 //else
                                 //    return false;
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         SQLiteConnection.ClearAllPools();
                         Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP Insert");
                         //counter1++;
                         return result; 
                     } 
                     finally
                     { 
                         Monitor.Exit(m_lockAsyncInsert);
                     }
                  
                 //    return false;
                 //}
                 //else
                 //{
                 //    return false;
                 //}
             }

             //public async Task<int> AsyncInsert(List<string> value)
             //{
             //    //using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
             //    //{
             //        //MessageBox.Show("AsyncInsert");
             //        int result = 0;

             //        try
             //        {
             //            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
             //            {
             //                await conn.OpenAsync();
             //                SQLiteCommand insertCommand = new SQLiteCommand("insert into NTP (FileName) values (@FileName)", conn);
             //                insertCommand.Parameters.AddWithValue("@FileName", value[0]);
             //                result = await insertCommand.ExecuteNonQueryAsync();

             //                insertCommand.Dispose();
             //                //MessageBox.Show("AsyncInsert1");
             //                return result;
             //            }
             //        }


             //        catch (Exception ex)
             //        {
             //            Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP Insert");
             //            return result;
             //        }

             //    //}
             //}

             //public async Task<int> AsyncDelete(string fileName)
             //{
             //    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
             //    {
             //        int result = 0;

             //        try
             //        {

             //            await conn.OpenAsync();
             //            SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM NTP where FileName=@FileName", conn);
             //            deleteCommand.Parameters.AddWithValue("@FileName", fileName);
             //            result = await deleteCommand.ExecuteNonQueryAsync();
             //            deleteCommand.Dispose();
             //            return result;

             //        }
             //        catch (Exception ex)
             //        {
             //            Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP Delete");
             //            return result;
             //        }

             //    }
             //}

             private static readonly object m_lockAsyncDelete = new object();
             public async Task<int> AsyncDelete(string fileName)
             {
                 Monitor.Enter(m_lockAsyncDelete); 

                 int result = 0;

                 try
                 {
                     using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                     {
                         await conn.OpenAsync();

                         using (SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM NTP where FileName=@FileName", conn))
                         {
                             deleteCommand.Parameters.AddWithValue("@FileName", fileName);
                             result = await deleteCommand.ExecuteNonQueryAsync();
              
                             return result;
                         }
                    
                     }
                 }
                 catch (Exception ex)
                 {
                     SQLiteConnection.ClearAllPools();
                     Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP Delete");
                     return result;
                 }
                 finally
                 {
                     Monitor.Exit(m_lockAsyncDelete);
                 }
             }

             
             public async Task<int> AsyncDelete()
             {
                 using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                 {
                     int result = 0;
                     try
                     {
                         await conn.OpenAsync();
                         SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM NTP", conn);
                         result = await deleteCommand.ExecuteNonQueryAsync();

                         deleteCommand.Dispose();

                         return result;
                     }
                     catch (Exception ex)
                     {
                         SQLiteConnection.ClearAllPools();
                         Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP Delete");
                         return result;
                     }
                 }
             }

             //public int Delete()
             //{
             //    int deletedRow = -1;
             //    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
             //    {
             //        try
             //        {
             //            conn.Open();
             //            SQLiteCommand command = new SQLiteCommand("DELETE FROM NTP", conn);
             //            deletedRow = command.ExecuteNonQuery();
             //            command.Dispose();

             //            return deletedRow;
             //        }
             //        catch
             //        {
             //            return deletedRow;
             //        }
             //    }
             //}

         }
     }
    
}
