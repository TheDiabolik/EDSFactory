using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    partial class DatabaseOperation
    {
        public class CorridorSpeed : Database
        {
            private static CorridorSpeed m_do;

            public static CorridorSpeed Singleton()
            {
                if (m_do == null)
                    m_do = new CorridorSpeed();

                return m_do;
            }

            private static readonly object m_lockAsyncSelect = new object();
            public async Task<List<DateTime>> AsycSelect(string plate, string minViolationDay, string maxViolationDay)
            {
                Monitor.Enter(m_lockAsyncSelect); 

                List<DateTime> exitDates = new List<DateTime>(); 

                try
                {
                      minViolationDay = minViolationDay.Substring(6, 4) + "-" + minViolationDay.Substring(3, 2) + "-" + minViolationDay.Substring(0, 2);
                      maxViolationDay = maxViolationDay.Substring(6, 4) + "-" + maxViolationDay.Substring(3, 2) + "-" + maxViolationDay.Substring(0, 2);

                    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                    {
                        await conn.OpenAsync();

                        //string selectsyncFileNames = "SELECT ExitDate,ExitHour FROM CorridorSpeed WHERE ExitDate >= @MinViolationDay and  ExitDate <= @MaxViolationDay and Plate = @Plate";
                        string selectsyncFileNames = "SELECT ExitDate,ExitHour FROM CorridorSpeed WHERE date(substr(ExitDate, 7, 4) || '-' || substr(ExitDate, 4, 2) || '-' || substr(ExitDate, 1, 2))>= @MinViolationDay and  date(substr(ExitDate, 7, 4) || '-' || substr(ExitDate, 4, 2) || '-' || substr(ExitDate, 1, 2))<= @MaxViolationDay and Plate = @Plate";
 
                        //string selectsyncFileNames = "SELECT ExitDate,ExitHour  FROM CorridorSpeed where Plate = @Plate";

                        using (SQLiteCommand command = new SQLiteCommand())
                        {
                            command.Connection = conn;
                            command.CommandText = selectsyncFileNames;
                            command.Parameters.AddWithValue("@Plate", plate);
                            command.Parameters.AddWithValue("@MinViolationDay", minViolationDay);
                            command.Parameters.AddWithValue("@MaxViolationDay", maxViolationDay);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    exitDates.Add(DateTime.Parse(reader["ExitDate"].ToString() + " " + reader["ExitHour"].ToString()));
                                }

                                return exitDates;
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    SQLiteConnection.ClearAllPools();
                    Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "NTP AsycSelect");
                    return exitDates;
                }
                finally
                {
                    Monitor.Exit(m_lockAsyncSelect);
                }
            }

            public DataTable FillDataSet()
            {
                DataTable dt = new DataTable("CorridorSpeed");
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                {
                    try
                    {
                        string selectSpeedCorridor = "SELECT * FROM CorridorSpeed";
                        SQLiteDataAdapter sqliteDataAdapterSpeedCorridor = new SQLiteDataAdapter(selectSpeedCorridor, conn);
                        sqliteDataAdapterSpeedCorridor.Fill(dt);
                        long autoIncrementSeedSpeedCorridor = GetNextAutoincrementValue(conn, "CorridorSpeed");

                        dt.Columns["ID"].AutoIncrement = true;
                        dt.Columns["ID"].AutoIncrementSeed = autoIncrementSeedSpeedCorridor;
                        dt.Columns["ID"].AutoIncrementStep = 1;

                        DataColumn[] keysSpeedCorridor = new DataColumn[1];
                        keysSpeedCorridor[0] = dt.Columns["ID"];
                        dt.PrimaryKey = keysSpeedCorridor;

                        sqliteDataAdapterSpeedCorridor.Dispose();

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
                        SQLiteCommand command = new SQLiteCommand("insert into CorridorSpeed (Plate, EntryDate, EntryHour, ExitDate, ExitHour, SpeedLimit, SpeedTolerance, Speed, EntryNarrowImageName, ExitNarrowImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @SpeedLimit, @SpeedTolerance, @Speed, @EntryNarrowImageName , @ExitNarrowImageName , @ImagePath)",
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
                        command.Parameters.AddWithValue("@ExitNarrowImageName", value[9]);
                        command.Parameters.AddWithValue("@ImagePath", value[10]);
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

            private static readonly object m_lockAsyncInsert = new object();
            public async Task<int> AsyncInsert(List<string> value)
            {
                Monitor.Enter(m_lockAsyncInsert); 

                int result = 0;

                try
                {
                    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
                    { 
                        await conn.OpenAsync();

                        using (SQLiteCommand command = new SQLiteCommand("insert into CorridorSpeed (Plate, EntryDate, EntryHour, ExitDate, ExitHour, SpeedLimit, SpeedTolerance, Speed, EntryNarrowImageName, ExitNarrowImageName, ImagePath) values (@Plate, @EntryDate, @EntryHour, @ExitDate, @ExitHour, @SpeedLimit, @SpeedTolerance, @Speed, @EntryNarrowImageName , @ExitNarrowImageName , @ImagePath)",
                conn))
                        {
                            command.Parameters.AddWithValue("@Plate", value[0]);
                            command.Parameters.AddWithValue("@EntryDate", value[1]);
                            command.Parameters.AddWithValue("@EntryHour", value[2]);
                            command.Parameters.AddWithValue("@ExitDate", value[3]);
                            command.Parameters.AddWithValue("@ExitHour", value[4]);
                            command.Parameters.AddWithValue("@SpeedLimit", value[5]);
                            command.Parameters.AddWithValue("@SpeedTolerance", value[6]);
                            command.Parameters.AddWithValue("@Speed", value[7]);
                            command.Parameters.AddWithValue("@EntryNarrowImageName", value[8]);
                            command.Parameters.AddWithValue("@ExitNarrowImageName", value[9]);
                            command.Parameters.AddWithValue("@ImagePath", value[10]);
                            result = await command.ExecuteNonQueryAsync(); 
                       
                            return result;
                        }

                    }
                }
                catch (Exception ex)
                {
                    SQLiteConnection.ClearAllPools();
                    Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "fizedhighway async");
                    return result;
                }
                finally
                {
                    Monitor.Exit(m_lockAsyncInsert);
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
                        SQLiteCommand command = new SQLiteCommand("DELETE FROM CorridorSpeed", conn);
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
                        SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM CorridorSpeed", conn);
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
        }
    }
}
