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
        public class CorridorSpeedWide : Database
        {
            private static CorridorSpeedWide m_do;

            public static CorridorSpeedWide Singleton()
            {
                if (m_do == null)
                    m_do = new CorridorSpeedWide();

                return m_do;
            }

            public async Task<List<DateTime>> AsycSelect(string plate, string minViolationDay, string minViolationHour, string maxViolationDay, string maxViolationHour)
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    List<DateTime> exitDates = new List<DateTime>();

                    try
                    {
                        await conn.OpenAsync();

                        string selectsyncFileNames = "SELECT ExitDate,ExitHour FROM CorridorSpeedWide WHERE (ExitDate >= @MinViolationDay AND ExitHour  >= @MinViolationHour) and  (ExitDate <= @MaxViolationDay AND ExitHour  <= @MaxViolationHour) and Plate = @Plate";
 
                        //string selectsyncFileNames = "SELECT ExitDate,ExitHour  FROM CorridorSpeedWide where Plate = @Plate";

                        SQLiteCommand command = new SQLiteCommand();
                        command.Connection = conn;
                        command.CommandText = selectsyncFileNames;
                        command.Parameters.AddWithValue("@Plate", plate);
                        command.Parameters.AddWithValue("@MinViolationDay", minViolationDay);
                        command.Parameters.AddWithValue("@MinViolationHour", minViolationHour);
                        command.Parameters.AddWithValue("@MaxViolationDay", maxViolationDay);
                        command.Parameters.AddWithValue("@MaxViolationHour", maxViolationHour);


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
                DataTable dt = new DataTable("CorridorSpeedWide");
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        string selectSpeedCorridorWide = "SELECT * FROM CorridorSpeedWide";
                        SQLiteDataAdapter sqliteDataAdapterSpeedCorridorWide = new SQLiteDataAdapter(selectSpeedCorridorWide, conn);
                        sqliteDataAdapterSpeedCorridorWide.Fill(dt);
                        long autoIncrementSeedSpeedCorridorWide = GetNextAutoincrementValue(conn, "CorridorSpeedWide");

                        dt.Columns["ID"].AutoIncrement = true;
                        dt.Columns["ID"].AutoIncrementSeed = autoIncrementSeedSpeedCorridorWide;
                        dt.Columns["ID"].AutoIncrementStep = 1;

                        DataColumn[] keysSpeedCorridorWide = new DataColumn[1];
                        keysSpeedCorridorWide[0] = dt.Columns["ID"];
                        dt.PrimaryKey = keysSpeedCorridorWide;

                        sqliteDataAdapterSpeedCorridorWide.Dispose();

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
                        SQLiteCommand command = new SQLiteCommand("insert into CorridorSpeedWide (Plate, EntryDate, EntryHour, ExitDate, ExitHour, SpeedLimit, SpeedTolerance, Speed, EntryNarrowImageName, EntryWideImageName, ExitNarrowImageName, ExitWideImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @SpeedLimit, @SpeedTolerance, @Speed, @EntryNarrowImageName, @EntryWideImageName, @ExitNarrowImageName, @ExitWideImageName, @ImagePath)",
                    conn);
                        command.Parameters.AddWithValue("@Plate", value[0]);
                        command.Parameters.AddWithValue("@EntryDate", value[1]);
                        command.Parameters.AddWithValue("@EntryHour", value[2]);
                        command.Parameters.AddWithValue("@ExitDate", value[3]);
                        command.Parameters.AddWithValue("@ExitHour", value[4]);
                        command.Parameters.AddWithValue("@SpeedLimit", value[5]);
                        command.Parameters.AddWithValue("@SpeedTolerance", value[6]);
                        command.Parameters.AddWithValue("@Speed", value[7]);
                        command.Parameters.AddWithValue("@EntryNarrowImageName", value[8]);
                        command.Parameters.AddWithValue("@EntryWideImageName", value[9]);
                        command.Parameters.AddWithValue("@ExitNarrowImageName", value[10]);
                        command.Parameters.AddWithValue("@ExitWideImageName", value[11]);
                        command.Parameters.AddWithValue("@ImagePath", value[12]);
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
                        SQLiteCommand command = new SQLiteCommand("insert into CorridorSpeedWide (Plate, EntryDate, EntryHour, ExitDate, ExitHour, SpeedLimit, SpeedTolerance, Speed, EntryNarrowImageName, EntryWideImageName, ExitNarrowImageName, ExitWideImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @SpeedLimit, @SpeedTolerance, @Speed, @EntryNarrowImageName, @EntryWideImageName, @ExitNarrowImageName, @ExitWideImageName, @ImagePath)",
                   conn);
                        command.Parameters.AddWithValue("@Plate", value[0]);
                        command.Parameters.AddWithValue("@EntryDate", value[1]);
                        command.Parameters.AddWithValue("@EntryHour", value[2]);
                        command.Parameters.AddWithValue("@ExitDate", value[3]);
                        command.Parameters.AddWithValue("@ExitHour", value[4]);
                        command.Parameters.AddWithValue("@SpeedLimit", value[5]);
                        command.Parameters.AddWithValue("@SpeedTolerance", value[6]);
                        command.Parameters.AddWithValue("@Speed", value[7]);
                        command.Parameters.AddWithValue("@EntryNarrowImageName", value[8]);
                        command.Parameters.AddWithValue("@EntryWideImageName", value[9]);
                        command.Parameters.AddWithValue("@ExitNarrowImageName", value[10]);
                        command.Parameters.AddWithValue("@ExitWideImageName", value[11]);
                        command.Parameters.AddWithValue("@ImagePath", value[12]);
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

            public override int Delete()
            {
                int deletedRow = -1;
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        conn.Open();
                        SQLiteCommand command = new SQLiteCommand("DELETE FROM CorridorSpeedWide", conn);
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
                        SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM CorridorSpeedWide", conn);
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
