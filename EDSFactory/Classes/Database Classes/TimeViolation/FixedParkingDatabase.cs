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
        public class FixedParking : Database
        {
            private static FixedParking m_do;

            public static FixedParking Singleton()
            {
                if (m_do == null)
                    m_do = new FixedParking();

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

                        string selectsyncFileNames = "SELECT ExitDate,ExitHour  FROM FixedParking where Plate = @Plate";

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
                DataTable dt = new DataTable("FixedParking");

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        string selectFixedParking = "SELECT * FROM FixedParking";
                        SQLiteDataAdapter sqliteDataAdapterFixedParking = new SQLiteDataAdapter(selectFixedParking, conn);
                        sqliteDataAdapterFixedParking.Fill(dt);
                        long autoIncrementSeedFixedParking = GetNextAutoincrementValue(conn, "FixedParking");

                        dt.Columns["ID"].AutoIncrement = true;
                        dt.Columns["ID"].AutoIncrementSeed = autoIncrementSeedFixedParking;
                        dt.Columns["ID"].AutoIncrementStep = 1;

                        DataColumn[] keysFixedParking = new DataColumn[1];
                        keysFixedParking[0] = dt.Columns["ID"];
                        dt.PrimaryKey = keysFixedParking;

                        sqliteDataAdapterFixedParking.Dispose();

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

                        SQLiteCommand insertCommand = new SQLiteCommand("insert into FixedParking (Plate, EntryDate, EntryHour, ExitDate, ExitHour, EntryNarrowImageName, EntryWideImageName, ExitNarrowImageName, ExitWideImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @EntryNarrowImageName, @EntryWideImageName, @ExitNarrowImageName, @ExitWideImageName, @ImagePath)", conn);
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
                          SQLiteCommand insertCommand  = new SQLiteCommand("insert into FixedParking (Plate, EntryDate, EntryHour, ExitDate, ExitHour, EntryNarrowImageName, EntryWideImageName, ExitNarrowImageName, ExitWideImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @EntryNarrowImageName, @EntryWideImageName, @ExitNarrowImageName, @ExitWideImageName, @ImagePath)",
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

            public override int Delete()
            {
                int deletedRow = -1;
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        conn.Open();
                        SQLiteCommand command = new SQLiteCommand("DELETE FROM FixedParking", conn);
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
                        SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM FixedParking", conn);
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
