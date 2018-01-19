using Microsoft.Practices.EnterpriseLibrary.Data;
using NLog;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace EIS.DataAccess
{
	public class SysDatabase
	{
		private static Database database_0;

		private static Logger fileLogger;

		static SysDatabase()
		{
			SysDatabase.fileLogger = LogManager.GetCurrentClassLogger();
			SysDatabase.database_0 = DatabaseFactory.CreateDatabase("sysDataBase");
		}

		public SysDatabase()
		{
		}

		public static void AddInParameter(DbCommand command, string name, DbType dbType, object value)
		{
			SysDatabase.database_0.AddInParameter(command, name, dbType, value);
		}

		public static DbConnection CreateConnection()
		{
			return SysDatabase.database_0.CreateConnection();
		}

		public static DataSet ExecuteDataSet(DbCommand command)
		{
			return SysDatabase.database_0.ExecuteDataSet(command);
		}

		public static DataSet ExecuteDataSet(string command)
		{
			DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
			return SysDatabase.database_0.ExecuteDataSet(sqlStringCommand);
		}

		public static int ExecuteNonQuery(DbCommand command)
		{
			int num;
			SysDatabase.fileLogger.Debug("ExecuteNonQuery:Command=[{0}]", command.CommandText);
			try
			{
				int num1 = 0;
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				num1 = SysDatabase.database_0.ExecuteNonQuery(command);
				stopwatch.Stop();
				if (stopwatch.ElapsedMilliseconds > (long)10000)
				{
					SysDatabase.fileLogger.Error<string, string>("执行超过10秒钟 ExecuteNonQuery:{0},参数列表：{1}", command.CommandText, SysDatabase.smethod_0(command));
				}
				num = num1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string, string>("发生错误 ExecuteNonQuery:{0},{1},参数列表：{2}", command.CommandText, exception.Message, SysDatabase.smethod_0(command));
				throw exception;
			}
			return num;
		}

		public static int ExecuteNonQuery(DbCommand command, DbTransaction transaction)
		{
			int num;
			SysDatabase.fileLogger.Debug("ExecuteNonQuery:Command=[{0}]", command.CommandText);
			try
			{
				num = SysDatabase.database_0.ExecuteNonQuery(command, transaction);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (DbParameter parameter in command.Parameters)
				{
					stringBuilder.AppendFormat("{0}={1}", parameter.ParameterName, parameter.Value);
				}
				SysDatabase.fileLogger.Error<string, string, StringBuilder>("发生错误 ExecuteNonQuery:{0},{1},参数列表：{2}", command.CommandText, exception.Message, stringBuilder);
				throw exception;
			}
			return num;
		}

		public static int ExecuteNonQuery(string command, DbTransaction transaction)
		{
			int num;
			SysDatabase.fileLogger.Debug("ExecuteNonQuery:Command=[{0}]", command);
			try
			{
				DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
				num = SysDatabase.database_0.ExecuteNonQuery(sqlStringCommand, transaction);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteNonQuery:{0},{1}", command, exception.Message);
				throw exception;
			}
			return num;
		}

		public static int ExecuteNonQuery(string command)
		{
			int num;
			SysDatabase.fileLogger.Debug("ExecuteNonQuery:Command=[{0}]", command);
			try
			{
				DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
				num = SysDatabase.database_0.ExecuteNonQuery(sqlStringCommand);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteNonQuery:{0},{1}", command, exception.Message);
				throw exception;
			}
			return num;
		}

		public static DbDataReader ExecuteReader(string command)
		{
			DbDataReader dbDataReaders;
			SysDatabase.fileLogger.Debug("ExecuteReader:Command=[{0}]", command);
			try
			{
				DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
				dbDataReaders = (DbDataReader)SysDatabase.database_0.ExecuteReader(sqlStringCommand);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteReader:{0},{1}", command, exception.Message);
				throw exception;
			}
			return dbDataReaders;
		}

		public static object ExecuteScalar(DbCommand command)
		{
			object obj;
			SysDatabase.fileLogger.Debug("ExecuteScalar:Command=[{0}]", command.CommandText);
			try
			{
				obj = SysDatabase.database_0.ExecuteScalar(command);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteScalar:{0},{1}", command.CommandText, exception.Message);
				throw exception;
			}
			return obj;
		}

		public static object ExecuteScalar(string command)
		{
			object obj;
			SysDatabase.fileLogger.Debug("ExecuteScalar:Command=[{0}]", command);
			try
			{
				DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
				obj = SysDatabase.database_0.ExecuteScalar(sqlStringCommand);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteScalar:{0},{1}", command, exception.Message);
				throw exception;
			}
			return obj;
		}

		public static object ExecuteScalar(string command, DbTransaction transaction)
		{
			object obj;
			SysDatabase.fileLogger.Debug("ExecuteScalar:Command=[{0}]", command);
			try
			{
				DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
				obj = SysDatabase.database_0.ExecuteScalar(sqlStringCommand, transaction);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteScalar:{0},{1}", command, exception.Message);
				throw exception;
			}
			return obj;
		}

		public static object ExecuteScalar(DbCommand dbCommand, DbTransaction transaction)
		{
			object obj;
			SysDatabase.fileLogger.Debug("ExecuteScalar:Command=[{0}]", dbCommand.CommandText);
			try
			{
				obj = SysDatabase.database_0.ExecuteScalar(dbCommand, transaction);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteScalar:{0},{1}", dbCommand.CommandText, exception.Message);
				throw exception;
			}
			return obj;
		}

		public static DataTable ExecuteTable(DbCommand command)
		{
			DataTable item;
			SysDatabase.fileLogger.Debug("ExecuteTable:Command=[{0}]", command.CommandText);
			try
			{
				item = SysDatabase.database_0.ExecuteDataSet(command).Tables[0];
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteTable:{0},{1}", command.CommandText, exception.Message);
				throw exception;
			}
			return item;
		}

		public static DataTable ExecuteTable(string command)
		{
			DataTable item;
			SysDatabase.fileLogger.Debug("ExecuteTable:Command=[{0}]", command);
			try
			{
				DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
				item = SysDatabase.database_0.ExecuteDataSet(sqlStringCommand).Tables[0];
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteTable:{0},{1}", command, exception.Message);
				throw exception;
			}
			return item;
		}

		public static DataTable ExecuteTable(string cmdText, int first, int last)
		{
			int i;
			DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(cmdText);
			DataTable dataTable = new DataTable();
			DbDataReader dbDataReaders = (DbDataReader)SysDatabase.database_0.ExecuteReader(sqlStringCommand);
			try
			{
				int fieldCount = dbDataReaders.FieldCount;
				for (i = 0; i < fieldCount; i++)
				{
					DataColumn dataColumn = new DataColumn()
					{
						ColumnName = dbDataReaders.GetName(i),
						DataType = dbDataReaders.GetFieldType(i)
					};
					dataTable.Columns.Add(dataColumn);
				}
				int num = 0;
				while (dbDataReaders.Read())
				{
					num++;
					if (num > last)
					{
						break;
					}
					if ((num < first ? true : num > last))
					{
						continue;
					}
					DataRow dataRow = dataTable.NewRow();
					for (i = 0; i < fieldCount; i++)
					{
						dataRow[i] = dbDataReaders[i];
					}
					dataTable.Rows.Add(dataRow);
				}
				sqlStringCommand.Cancel();
				dbDataReaders.Close();
				dbDataReaders.Dispose();
			}
			finally
			{
				if (dbDataReaders != null)
				{
					((IDisposable)dbDataReaders).Dispose();
				}
			}
			return dataTable;
		}

		public static DataTable ExecuteTable(string cmdText, int first, int last, ref int total)
		{
			int i;
			DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(cmdText);
			DataTable dataTable = new DataTable();
			DbDataReader dbDataReaders = (DbDataReader)SysDatabase.database_0.ExecuteReader(sqlStringCommand);
			try
			{
				int fieldCount = dbDataReaders.FieldCount;
				for (i = 0; i < fieldCount; i++)
				{
					DataColumn dataColumn = new DataColumn()
					{
						ColumnName = dbDataReaders.GetName(i),
						DataType = dbDataReaders.GetFieldType(i)
					};
					dataTable.Columns.Add(dataColumn);
				}
				int num = 0;
				while (dbDataReaders.Read())
				{
					num++;
					if ((num < first ? true : num > last))
					{
						continue;
					}
					DataRow dataRow = dataTable.NewRow();
					for (i = 0; i < fieldCount; i++)
					{
						dataRow[i] = dbDataReaders[i];
					}
					dataTable.Rows.Add(dataRow);
				}
				total = num;
				sqlStringCommand.Cancel();
				dbDataReaders.Close();
				dbDataReaders.Dispose();
			}
			finally
			{
				if (dbDataReaders != null)
				{
					((IDisposable)dbDataReaders).Dispose();
				}
			}
			return dataTable;
		}

		public static DataTable ExecuteTable(string command, DbTransaction transaction)
		{
			DataTable item;
			SysDatabase.fileLogger.Debug("ExecuteTable:Command=[{0}]", command);
			try
			{
				DbCommand sqlStringCommand = SysDatabase.database_0.GetSqlStringCommand(command);
				item = SysDatabase.database_0.ExecuteDataSet(sqlStringCommand, transaction).Tables[0];
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteTable:{0},{1}", command, exception.Message);
				throw exception;
			}
			return item;
		}

		public static DataTable ExecuteTable(DbCommand command, DbTransaction transaction)
		{
			DataTable item;
			SysDatabase.fileLogger.Debug("ExecuteTable:Command=[{0}]", command.CommandText);
			try
			{
				item = SysDatabase.database_0.ExecuteDataSet(command, transaction).Tables[0];
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				SysDatabase.fileLogger.Error<string, string>("发生错误 ExecuteTable:{0},{1}", command.CommandText, exception.Message);
				throw exception;
			}
			return item;
		}

		public static DbCommand GetSqlStringCommand(string query)
		{
			return SysDatabase.database_0.GetSqlStringCommand(query);
		}

        public static DbCommand GetStoredProcCommand(string query)
        {
            return SysDatabase.database_0.GetStoredProcCommand(query);
        }

		private static string smethod_0(DbCommand dbCommand_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbParameter parameter in dbCommand_0.Parameters)
			{
				stringBuilder.AppendFormat("{0}={1},", parameter.ParameterName, parameter.Value);
			}
			return stringBuilder.ToString();
		}
	}
}