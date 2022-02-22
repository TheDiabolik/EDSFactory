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

        public class MobileParking : Database
        {
            private static MobileParking m_do;

            public static MobileParking Singleton()
            {
                if (m_do == null)
                    m_do = new MobileParking();

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

                        string selectsyncFileNames = "SELECT ExitDate,ExitHour  FROM MobileParking where Plate = @Plate";

                        SQLiteCommand command = new SQLiteCommand();
                        command.Connection = conn;
                        command.CommandText = selectsyncFileNames;
                        command.Parameters.AddWithValue("@Plate", plate);


                        DbDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            exitDates.Add(DateTime.Parse(reader["ExitDate"].ToString() + " " + reader["ExitHour"].ToString()));
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
                DataTable dt = new DataTable("MobileParking");

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        string selectMobileParking = "SELECT * FROM MobileParking";
                        SQLiteDataAdapter sqliteDataAdapterMobileParking = new SQLiteDataAdapter(selectMobileParking, conn);
                        sqliteDataAdapterMobileParking.Fill(dt);
                        long autoIncrementSeedMobileParking = GetNextAutoincrementValue(conn, "MobileParking");


                        dt.Columns["ID"].AutoIncrement = true;
                        dt.Columns["ID"].AutoIncrementSeed = autoIncrementSeedMobileParking;
                        dt.Columns["ID"].AutoIncrementStep = 1;

                        DataColumn[] keysMobileParking = new DataColumn[1];
                        keysMobileParking[0] = dt.Columns["ID"];
                        dt.PrimaryKey = keysMobileParking;
                        
                        sqliteDataAdapterMobileParking.Dispose();

                        return dt;
                    }
                    catch
                    {
                        MessageBox.Show("Bilgiler Veritabanından Okunamadı!", "Uyarı");
                        return dt;
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

                        SQLiteCommand insertCommand = new SQLiteCommand("insert into MobileParking (Plate, EntryDate, EntryHour, ExitDate, ExitHour, EntryNarrowImageName, EntryWideImageName, ExitNarrowImageName, ExitWideImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @EntryNarrowImageName, @EntryWideImageName, @ExitNarrowImageName, @ExitWideImageName, @ImagePath)", conn);
                        insertCommand.Parameters.AddWithValue("@Plate", value[0]);
                        insertCommand.Parameters.AddWithValue("@EntryDate", value[1]);
                        insertCommand.Parameters.AddWithValue("@EntryHour", value[2]);
                        insertCommand.Parameters.AddWithValue("@ExitDate", value[3]);
                        insertCommand.Parameters.AddWithValue("@ExitHour", value[4]);
                        insertCommand.Parameters.AddWithValue("@EntryNarrowImageName", value[5]);
                        insertCommand.Parameters.AddWithValue("@EntryWideImageName", value[6]);
                        insertCommand.Parameters.AddWithValue("@ExitNarrowImageName", value[7]);
                        insertCommand.Parameters.AddWithValue("@ExitWideImageName", value[8]);
                        insertCommand.Parameters.AddWithValue("@ImagePath", value[9]);

                        result = await insertCommand.ExecuteNonQueryAsync();
                        insertCommand.Dispose();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "fizedhighway async");
                        return result;
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
                        SQLiteCommand insertCommand = new SQLiteCommand("insert into MobileParking (Plate, EntryDate, EntryHour, ExitDate, ExitHour, EntryNarrowImageName, EntryWideImageName, ExitNarrowImageName, ExitWideImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @EntryNarrowImageName, @EntryWideImageName, @ExitNarrowImageName, @ExitWideImageName, @ImagePath)",
                            conn);
                        insertCommand.Parameters.AddWithValue("@Plate", value[0]);
                        insertCommand.Parameters.AddWithValue("@EntryDate", value[1]);
                        insertCommand.Parameters.AddWithValue("@EntryHour", value[2]);
                        insertCommand.Parameters.AddWithValue("@ExitDate", value[3]);
                        insertCommand.Parameters.AddWithValue("@ExitHour", value[4]);
                        insertCommand.Parameters.AddWithValue("@EntryNarrowImageName", value[5]);
                        insertCommand.Parameters.AddWithValue("@EntryWideImageName", value[6]);
                        insertCommand.Parameters.AddWithValue("@ExitNarrowImageName", value[7]);
                        insertCommand.Parameters.AddWithValue("@ExitWideImageName", value[8]);
                        insertCommand.Parameters.AddWithValue("@ImagePath", value[9]);

                        recordedRow = insertCommand.ExecuteNonQuery();
                        insertCommand.Dispose();

                        return recordedRow;
                    }
                    catch
                    {
                        return recordedRow;
                    }
                }  
            }

            //public int Update(List<string> value)
            //{
            //    DataRow newRow = MainForm.m_dsEDSType.Tables["MobileParking"].Rows.Find(value[0]);
            //    newRow.BeginEdit();
            //    newRow["Plate"] = value[1];
            //    newRow["EntryNarrowImageName"] = value[2];
            //    newRow["EntryWideImageName"] = value[3];
            //    newRow["ExitNarrowImageName"] = value[4];
            //    newRow["ExitWideImageName"] = value[5];
            //    newRow.EndEdit();

            //    ConnectionString.cnn.Open();
            //    SQLiteDataAdapter sqliteDataAdapter = new SQLiteDataAdapter();
            //    sqliteDataAdapter.UpdateCommand = new SQLiteCommand("update MobileParking set Plate = @Plate, EntryNarrowImageName = @EntryNarrowImageName, EntryWideImageName = @EntryWideImageName, ExitNarrowImageName = @ExitNarrowImageName, ExitWideImageName = @ExitWideImageName where ID=@ID",
            //        ConnectionString.cnn);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@ID", value[0]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@Plate", value[1]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@EntryNarrowImageName", value[2]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@EntryWideImageName", value[3]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@ExitNarrowImageName", value[4]);
            //    sqliteDataAdapter.UpdateCommand.Parameters.AddWithValue("@ExitWideImageName", value[5]);

            //    int recordedRow = sqliteDataAdapter.Update(MainForm.m_dsEDSType, "MobileParking");
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
                        SQLiteCommand command = new SQLiteCommand("DELETE FROM MobileParking", conn);
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
                        SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM MobileParking", conn);
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
