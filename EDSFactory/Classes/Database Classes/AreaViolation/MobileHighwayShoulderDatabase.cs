using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory
{
    partial class DatabaseOperation
    {
        public class MobileHighwayShoulder : Database
        {
            private static MobileHighwayShoulder m_do;

            public static MobileHighwayShoulder Singleton()
            {
                if (m_do == null)
                    m_do = new MobileHighwayShoulder();

                return m_do;
            }
            public async Task<List<DateTime>> AsycSelect(string plate)
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    List<DateTime> exitDates = new List<DateTime>();

                    try
                    {
                        await conn.OpenAsync();

                        string selectsyncFileNames = "SELECT Date,Hour  FROM MobileHighwayShoulder where Plate = @Plate";

                        SQLiteCommand command = new SQLiteCommand();
                        command.Connection = conn;
                        command.CommandText = selectsyncFileNames;
                        command.Parameters.AddWithValue("@Plate", plate);


                        DbDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            exitDates.Add(DateTime.Parse(reader["Date"].ToString() + " " + reader["Hour"].ToString()));
                        }

                        reader.Dispose();
                        command.Dispose();

                        return exitDates;
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP AsycSelect");
                        return exitDates;
                    }
                }
            }
            public DataTable FillDataSet()
            {
                DataTable dt = new DataTable("MobileHighwayShoulder");
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        string selectMobileHighwayShoulder = "SELECT * FROM MobileHighwayShoulder";
                        SQLiteDataAdapter sqliteDataAdapterMobileHighwayShoulder = new SQLiteDataAdapter(selectMobileHighwayShoulder, conn);
                        sqliteDataAdapterMobileHighwayShoulder.Fill(dt);
                        long autoIncrementSeedMobileHighwayShoulder = GetNextAutoincrementValue(conn, "MobileHighwayShoulder");

                        dt.Columns["ID"].AutoIncrement = true;
                        dt.Columns["ID"].AutoIncrementSeed = autoIncrementSeedMobileHighwayShoulder;
                        dt.Columns["ID"].AutoIncrementStep = 1;

                        DataColumn[] keysMobileHighwayShoulder = new DataColumn[1];
                        keysMobileHighwayShoulder[0] = dt.Columns["ID"];
                        dt.PrimaryKey = keysMobileHighwayShoulder;

                        sqliteDataAdapterMobileHighwayShoulder.Dispose();

                        return dt;
                    }
                    catch
                    {
                        MessageBox.Show("Bilgiler Veritabanından Okunamadı!", "Uyarı");
                        return dt;
                    }
                }
            }

            public override int Insert(List<string> value)
            {
                int recordedRow = -1;

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        conn.Open();
                        SQLiteCommand command = new SQLiteCommand("insert into MobileHighwayShoulder (Plate, Date, Hour, FirstImageName, SecondImageName, ImagePath) values (@Plate, @Date, @Hour, @FirstImageName, @SecondImageName, @ImagePath)",
                  conn);
                        command.Parameters.AddWithValue("@Plate", value[0]);
                        command.Parameters.AddWithValue("@Date", value[1]);
                        command.Parameters.AddWithValue("@Hour", value[2]);
                        command.Parameters.AddWithValue("@FirstImageName", value[3]);
                        command.Parameters.AddWithValue("@SecondImageName", value[4]);
                        command.Parameters.AddWithValue("@ImagePath", value[5]);
                        recordedRow = command.ExecuteNonQuery();

                        command.Dispose();

                        return recordedRow;
                    }
                    catch
                    {
                        return recordedRow;
                    }
                }
            }

            public async Task<int> AsyncInsert(List<string> value)
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    int result = 0; 

                    try
                    {
                        await conn.OpenAsync();

                        SQLiteCommand command = new SQLiteCommand("insert into MobileHighwayShoulder (Plate, Date, Hour, FirstImageName, SecondImageName, ImagePath) values (@Plate, @Date, @Hour, @FirstImageName, @SecondImageName, @ImagePath)",
                 conn);
                        command.Parameters.AddWithValue("@Plate", value[0]);
                        command.Parameters.AddWithValue("@Date", value[1]);
                        command.Parameters.AddWithValue("@Hour", value[2]);
                        command.Parameters.AddWithValue("@FirstImageName", value[3]);
                        command.Parameters.AddWithValue("@SecondImageName", value[4]);
                        command.Parameters.AddWithValue("@ImagePath", value[5]);
                        result = await command.ExecuteNonQueryAsync();

                        command.Dispose();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "fizedhighway async");
                        return result;
                    }
                }
            }

            //public int Update(List<string> value)
            //{
            //    DataRow newRow = MainForm.m_dsEDSType.Tables["MobileHighwayShoulder"].Rows.Find(value[0]);
            //    newRow.BeginEdit();
            //    newRow["Plate"] = value[1];
            //    newRow["FirstImageName"] = value[2];
            //    newRow["SecondImageName"] = value[3];
            //    newRow.EndEdit();


            //    ConnectionString.cnn.Open();
            //    SQLiteDataAdapter sqliteDataAdapter = new SQLiteDataAdapter();
            //    sqliteDataAdapter.UpdateCommand = new SQLiteCommand("update MobileHighwayShoulder set Plate=@Plate , FirstImageName = @FirstImageName, SecondImageName = @SecondImageName where ID = @ID",
            //        ConnectionString.cnn);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@ID", value[0]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@Plate", value[1]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@FirstImageName", value[2]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@SecondImageName", value[3]);
            //    int recordedRow = sqliteDataAdapter.Update(MainForm.m_dsEDSType, "MobileHighwayShoulder");
            //    ConnectionString.cnn.Close();

            //    return recordedRow;
            //}

            public override int Delete()
            {
                int deletedRow = -1;
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        conn.Open();
                        SQLiteCommand command = new SQLiteCommand("DELETE FROM MobileHighwayShoulder", conn);
                        deletedRow = command.ExecuteNonQuery();
                        command.Dispose();

                        return deletedRow;
                    }
                    catch
                    {
                        return deletedRow;
                    }
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
                        SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM MobileHighwayShoulder", conn);
                        result = await deleteCommand.ExecuteNonQueryAsync();

                        deleteCommand.Dispose();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP Delete");
                        return result;
                    }
                }
            }

        }
    }
}
