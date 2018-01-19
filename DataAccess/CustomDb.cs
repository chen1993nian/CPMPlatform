using DDTek.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using NLog;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace EIS.DataAccess
{
	public class CustomDb
	{
		private Database database_0 = null;

		private DbConnection dbConnection_0 = null;

		private string string_0 = "";

		public string dbType = "";

		private static Logger fileLogger;

		static CustomDb()
		{
			CustomDb.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public CustomDb()
		{
			this.database_0 = DatabaseFactory.CreateDatabase();
		}

		public CustomDb(string dbName)
		{
			ConnectionStringSettings item = ConfigurationManager.ConnectionStrings[dbName];
			if (item == null)
			{
				throw new Exception(string.Concat("配置文件ConnectionString节中没有找到名称为【", dbName, "】的数据源"));
			}
			if (!item.ProviderName.Contains("Oracle"))
			{
				this.database_0 = DatabaseFactory.CreateDatabase(dbName);
			}
			else
			{
				this.string_0 = item.ConnectionString;
				this.dbType = "oracle";
			}
		}

		public void AddInParameter(DbCommand command, string name, DbType dbType, object value)
		{
			if (this.dbType != "oracle")
			{
				this.database_0.AddInParameter(command, name, dbType, value);
			}
			else
			{
				DbParameter dbParameter = command.CreateParameter();
				dbParameter.ParameterName = name;
				dbParameter.DbType = dbType;
				dbParameter.Value = value;
				command.Parameters.Add(dbParameter);
			}
		}

		public DbConnection CreateConnection()
		{
			DbConnection dbConnection0;
			if (this.dbType != "oracle")
			{
				if (this.dbConnection_0 == null)
				{
					this.dbConnection_0 = this.database_0.CreateConnection();
				}
				dbConnection0 = this.dbConnection_0;
			}
			else
			{
				if (this.dbConnection_0 == null)
				{
					//this.dbConnection_0 = new OracleConnection(this.string_0);
				}
				dbConnection0 = this.dbConnection_0;
			}
			return dbConnection0;
		}

		public void CreateDatabaseByCode(string dsCode)
		{
			string str = string.Concat("select top 1 ConnString,DsType from T_E_Sys_Connection where dsCode='", dsCode, "'");
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			if (dataTable.Rows.Count > 0)
			{
				this.string_0 = dataTable.Rows[0]["ConnString"].ToString();
				this.dbType = dataTable.Rows[0]["DsType"].ToString().ToLower();
				if (this.dbType.Equals("sqlsever"))
				{
					this.database_0 = new SqlDatabase(this.string_0);
				}
			}
		}

		public void CreateDatabaseByConnectionId(string ConnectionId)
		{
			string str = string.Concat("select ConnString,DsType from T_E_Sys_Connection where _autoid='", ConnectionId, "'");
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			if (dataTable.Rows.Count > 0)
			{
				this.string_0 = dataTable.Rows[0]["ConnString"].ToString();
				this.dbType = dataTable.Rows[0]["DsType"].ToString().ToLower();
				if (this.dbType.Equals("sqlsever"))
				{
					this.database_0 = new SqlDatabase(this.string_0);
				}
			}
		}

		public DataSet ExecuteDataSet(DbCommand command)
		{
			return this.database_0.ExecuteDataSet(command);
		}

		public DataSet ExecuteDataSet(string command)
		{
			DataSet dataSet;
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				dataSet = this.database_0.ExecuteDataSet(sqlStringCommand);
			}
			else
			{
				OracleConnection oracleConnection = new OracleConnection(this.string_0);
				OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(new OracleCommand(command, oracleConnection));
				DataSet dataSet1 = new DataSet();
				oracleDataAdapter.Fill(dataSet1);
				dataSet = dataSet1;
			}
			return dataSet;
		}

		public int ExecuteNonQuery(DbCommand command)
		{
			return this.database_0.ExecuteNonQuery(command);
		}

		public int ExecuteNonQuery(DbCommand command, DbTransaction transaction)
		{
			int num;
			num = (this.dbType != "oracle" ? this.database_0.ExecuteNonQuery(command, transaction) : command.ExecuteNonQuery());
			return num;
		}

		public int ExecuteNonQuery(string command, DbTransaction transaction)
		{
			DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
			return this.database_0.ExecuteNonQuery(sqlStringCommand, transaction);
		}

		public int ExecuteNonQuery(string command)
		{
			int num;
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				num = this.database_0.ExecuteNonQuery(sqlStringCommand);
			}
			else
			{
				int num1 = -1;
				OracleConnection oracleConnection = new OracleConnection(this.string_0);
				try
				{
					OracleCommand oracleCommand = new OracleCommand(command, oracleConnection);
					oracleConnection.Open();
					num1 = oracleCommand.ExecuteNonQuery();
					oracleConnection.Close();
				}
				finally
				{
					if (oracleConnection != null)
					{
						oracleConnection.Dispose();
					}
				}
				num = num1;
			}
			return num;
		}

		public DbDataReader ExecuteReader(string command)
		{
			DbDataReader dbDataReaders=null;
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				dbDataReaders = (DbDataReader)this.database_0.ExecuteReader(sqlStringCommand);
			}
			else
			{
                //OracleConnection oracleConnection = new OracleConnection(this.string_0);
                //OracleCommand oracleCommand = new OracleCommand(command, oracleConnection);
                //oracleConnection.Open();
                //dbDataReaders = oracleCommand.ExecuteReader();
			}
			return dbDataReaders;
		}

		public object ExecuteScalar(string command)
		{
			object obj;
			object obj1;
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				obj1 = this.database_0.ExecuteScalar(sqlStringCommand);
			}
			else
			{
				OracleConnection oracleConnection = new OracleConnection(this.string_0);
				try
				{
					OracleCommand oracleCommand = new OracleCommand(command, oracleConnection);
					oracleConnection.Open();
					obj = oracleCommand.ExecuteScalar();
					oracleConnection.Close();
				}
				finally
				{
					if (oracleConnection != null)
					{
						oracleConnection.Dispose();
					}
				}
				obj1 = obj;
			}
			return obj1;
		}

		public object ExecuteScalar(string command, DbTransaction transaction)
		{
			DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
			return this.database_0.ExecuteScalar(sqlStringCommand, transaction);
		}

		public object ExecuteScalar(DbCommand command)
		{
			return this.database_0.ExecuteScalar(command);
		}

		public DataTable ExecuteTable(DbCommand command)
		{
			return this.database_0.ExecuteDataSet(command).Tables[0];
		}

		public DataTable ExecuteTable(DbCommand command, DbTransaction tran)
		{
			DataTable item = this.database_0.ExecuteDataSet(command, tran).Tables[0];
			return item;
		}

		public DataTable ExecuteTable(string command)
		{
			DataTable item;
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				item = this.database_0.ExecuteDataSet(sqlStringCommand).Tables[0];
			}
			else
			{
				OracleConnection oracleConnection = new OracleConnection(this.string_0);
				OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(new OracleCommand(command, oracleConnection));
				DataTable dataTable = new DataTable();
				oracleDataAdapter.Fill(dataTable);
				item = dataTable;
			}
			return item;
		}

		public DataTable ExecuteTable(string command, DbTransaction tran)
		{
			DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
			DataTable item = this.database_0.ExecuteDataSet(sqlStringCommand, tran).Tables[0];
			return item;
		}

		public DataTable ExecuteTable(string command, int timeout)
		{
			DataTable item;
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				sqlStringCommand.CommandTimeout = timeout;
				item = this.database_0.ExecuteDataSet(sqlStringCommand).Tables[0];
			}
			else
			{
				OracleCommand oracleCommand = new OracleCommand(command, new OracleConnection(this.string_0))
				{
					CommandTimeout = timeout
				};
				OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
				DataTable dataTable = new DataTable();
				oracleDataAdapter.Fill(dataTable);
				item = dataTable;
			}
			return item;
		}

		public DataTable ExecuteTable(string command, int first, int last)
		{
			DbDataReader dbDataReaders;
			int fieldCount;
			int i;
			int num;
			DataColumn dataColumn;
			DataRow dataRow;
			DataTable dataTable = new DataTable();
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				dbDataReaders = (DbDataReader)this.database_0.ExecuteReader(sqlStringCommand);
				try
				{
					fieldCount = dbDataReaders.FieldCount;
					for (i = 0; i < fieldCount; i++)
					{
						dataColumn = new DataColumn()
						{
							ColumnName = dbDataReaders.GetName(i),
							DataType = dbDataReaders.GetFieldType(i)
						};
						dataTable.Columns.Add(dataColumn);
					}
					num = 0;
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
						dataRow = dataTable.NewRow();
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
			}
			else
			{
				OracleConnection oracleConnection = new OracleConnection(this.string_0);
                OracleDataReader OracleDataReaders = null;
				try
				{
					OracleCommand oracleCommand = new OracleCommand(command, oracleConnection);
					oracleConnection.Open();
                    OracleDataReaders = oracleCommand.ExecuteReader();
                    fieldCount = OracleDataReaders.FieldCount;
					for (i = 0; i < fieldCount; i++)
					{
						dataColumn = new DataColumn()
						{
                            ColumnName = OracleDataReaders.GetName(i),
                            DataType = OracleDataReaders.GetFieldType(i)
						};
						dataTable.Columns.Add(dataColumn);
					}
					num = 0;
                    while (OracleDataReaders.Read())
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
						dataRow = dataTable.NewRow();
						for (i = 0; i < fieldCount; i++)
						{
                            dataRow[i] = OracleDataReaders[i];
						}
						dataTable.Rows.Add(dataRow);
					}
					oracleCommand.Cancel();
                    OracleDataReaders.Close();
                    OracleDataReaders.Dispose();
				}
				finally
				{
					if (oracleConnection != null)
					{
						oracleConnection.Dispose();
					}
				}
			}
			return dataTable;
		}

		public DataTable ExecuteTable(string command, int first, int last, ref int total)
		{
			DbDataReader dbDataReaders;
			int fieldCount;
			int i;
			int num;
			DataColumn dataColumn;
			DataRow dataRow;
			DataTable dataTable = new DataTable();
			if (this.dbType != "oracle")
			{
				DbCommand sqlStringCommand = this.database_0.GetSqlStringCommand(command);
				dbDataReaders = (DbDataReader)this.database_0.ExecuteReader(sqlStringCommand);
				try
				{
					fieldCount = dbDataReaders.FieldCount;
					for (i = 0; i < fieldCount; i++)
					{
						dataColumn = new DataColumn()
						{
							ColumnName = dbDataReaders.GetName(i),
							DataType = dbDataReaders.GetFieldType(i)
						};
						dataTable.Columns.Add(dataColumn);
					}
					num = 0;
					while (dbDataReaders.Read())
					{
						num++;
						if ((num < first ? true : num > last))
						{
							continue;
						}
						dataRow = dataTable.NewRow();
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
			}
			else
			{
				OracleConnection oracleConnection = new OracleConnection(this.string_0);
                OracleDataReader OracleDataReaders = null;
				try
				{
					OracleCommand oracleCommand = new OracleCommand(command, oracleConnection);
					oracleConnection.Open();
                    OracleDataReaders = oracleCommand.ExecuteReader();
                    fieldCount = OracleDataReaders.FieldCount;
					for (i = 0; i < fieldCount; i++)
					{
						dataColumn = new DataColumn()
						{
                            ColumnName = OracleDataReaders.GetName(i),
                            DataType = OracleDataReaders.GetFieldType(i)
						};
						dataTable.Columns.Add(dataColumn);
					}
					num = 0;
                    while (OracleDataReaders.Read())
					{
						num++;
						if ((num < first ? true : num > last))
						{
							continue;
						}
						dataRow = dataTable.NewRow();
						for (i = 0; i < fieldCount; i++)
						{
                            dataRow[i] = OracleDataReaders[i];
						}
						dataTable.Rows.Add(dataRow);
					}
					total = num;
					oracleCommand.Cancel();
                    OracleDataReaders.Close();
                    OracleDataReaders.Dispose();
				}
				finally
				{
					if (oracleConnection != null)
					{
						oracleConnection.Dispose();
					}
				}
			}
			return dataTable;
		}

		public DbCommand GetSqlStringCommand(string query)
		{
			return this.database_0.GetSqlStringCommand(query);
		}
	}
}