using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.Common;

namespace Com.ACBC.Framework.Database
{
    public class MysqlAccess : IDatabaseOperation
    {
        private MySqlCommand transCommandMySql;
        private MySqlConnection transConnectionMySql;
        private MySqlTransaction transMySql;
        private string _connectionString = "";
        private bool _isTransaction = false;
        private ArrayList al = new ArrayList();


        /// <summary>
        /// 获取或设置连接字符串的值
        /// </summary>
        public bool IsTransaction
        {
            get
            {
                return _isTransaction;
            }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conn">连接字符串</param>
        public MysqlAccess(string conn)
        {
            _connectionString = conn;
        }

        public MysqlAccess()
        {
            
        }
        #endregion


        /// <summary>
        /// 打开数据库
        /// </summary>
        public void openDatabase()
        {
            if (transConnectionMySql == null)
            {
                transConnectionMySql = new MySqlConnection(_connectionString);
                (transConnectionMySql).Open();
            }
            else if (transConnectionMySql.State == ConnectionState.Closed)
            {

                transConnectionMySql = new MySqlConnection(_connectionString);
                (transConnectionMySql).Open();
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void closeDatabase()
        {
            if (transConnectionMySql != null)
            {
                if (transConnectionMySql.State == ConnectionState.Open)
                {

                    (transConnectionMySql).Close();

                }
            }
        }

        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <returns></returns>
        public string getConnectString()
        {
            return this._connectionString;
        }

        /// <summary>
        /// 设置数据库连接串
        /// </summary>
        /// <returns></returns>
        public void setConnectString(string str)
        {
            this._connectionString = str;
        }

        /// <summary>
        /// 返回DATASET类型数据
        /// </summary>
        /// <returns></returns>
        public DataSet getDataSet(string sql, string tableName)
        {
            try
            {
                this.openDatabase();
                MySqlDataAdapter oda = new MySqlDataAdapter(sql, transConnectionMySql);
                DataSet dsData = new DataSet();
                oda.Fill(dsData, tableName);
                this.closeDatabase();
                return dsData;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), sql);
                throw e;
            }
            finally
            {
                if (!IsTransaction)
                {
                    this.closeDatabase();
                }
            }
        }

        /// <summary>
        /// 返回XML类型数据
        /// </summary>
        /// <returns></returns>
        public string getXML(string sql, string tableName)
        {
            DataSet ds;
            try
            {
                ds = this.getDataSet(sql, tableName);
                return ds.GetXml();
            }
            catch (PException em)
            {
                throw em;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), "XML_ERROR");
                throw e;
            }
        }

        /// <summary>
        /// 返回DATAREADER类型数据
        /// </summary>
        /// <returns></returns>
        public DbDataReader getDataReader(string commandText)
        {
            try
            {
                this.openDatabase();
                transCommandMySql = new MySqlCommand();
                transCommandMySql.Connection = transConnectionMySql;
                transCommandMySql.CommandText = commandText;
                transCommandMySql.Transaction = transMySql;
                transCommandMySql.CommandType = CommandType.Text;

                MySqlDataReader dataReader;
                dataReader = transCommandMySql.ExecuteReader(CommandBehavior.CloseConnection);

                return dataReader;
            }
            catch(Exception ex)
            {
                if (!IsTransaction)
                {
                    this.closeDatabase();
                }
                PException e = new PException(ex, ex.GetType(), commandText);
                throw e;
            }
        }

        /// <summary>
        /// DML操作
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlString)
        {
            try
            {
                transCommandMySql = new MySqlCommand(sqlString, transConnectionMySql);
                if (_isTransaction)
                    transCommandMySql.Transaction = transMySql;
                if (transCommandMySql.CommandText.Trim() != "")
                {
                    return transCommandMySql.ExecuteNonQuery();
                }
                return 0;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), sqlString);
                throw e;
            }
        }

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <returns></returns>
        public int executeInsert(string sql)
        {
            try
            {
                this.al.Clear();
                this.al.Add(sql);
                this.executeBatch();
                return 1;
            }
            catch(Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), "INSERT");
                throw e;
            }
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <returns></returns>
        public int executeUpdate(string sql)
        {
            try
            {
                this.al.Clear();
                this.al.Add(sql);
                this.executeBatch();
                return 1;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), "UPDATE");
                throw e;
            }
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <returns></returns>
        public int executeDelete(string sql)
        {
            try
            {
                this.al.Clear();
                this.al.Add(sql);
                this.executeBatch();
                return 1;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), "DELETE");
                throw e;
            }
        }

        /// <summary>
        /// 按事物执行DML操作
        /// </summary>
        /// <returns></returns>
        public bool executeBatch(ArrayList als)
        {
            this.al = als;
            int i = 0;
            try
            {
                this.BeginTransaction();
                for (i = 0; i < this.al.Count; i++)
                {
                    this.ExecuteNonQuery(al[i].ToString());
                }
                this.Commit();
                return true;
            }
            catch(Exception ex)
            {
                this.RollBack();
                PException e = new PException(ex, ex.GetType(), this.al[i].ToString());
                throw e;
            }
            finally
            {
                if (!IsTransaction)
                {
                    this.closeDatabase();
                }
                al = new ArrayList();
            }
            
        }

        /// <summary>
        /// 按事物执行DML操作
        /// </summary>
        /// <returns></returns>
        public bool executeBatch()
        {
            int i = 0;
            try
            {
                this.BeginTransaction();
                for (i = 0; i < this.al.Count; i++)
                {
                    this.ExecuteNonQuery(al[i].ToString());
                }
                this.Commit();
                return true;
            }
            catch (Exception ex)
            {
                this.RollBack();
                PException e = new PException(ex, ex.GetType(), this.al[i].ToString());
                throw e;
            }
            finally
            {
                if (!IsTransaction)
                {
                    this.closeDatabase();
                }
            }

        }

        /// <summary>
        /// 增加DML查询语句
        /// </summary>
        /// <param name="str"></param>
        public void addDmlSql(string str)
        {
            al.Add(str);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            this.openDatabase();
            transMySql = transConnectionMySql.BeginTransaction();
            _isTransaction = true;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public int Commit()
        {
            try
            {
                transMySql.Commit();
                _isTransaction = false;
                this.closeDatabase();
            }
            catch
            {

            }
            return 1;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public int RollBack()
        {
            try
            {
                (transMySql).Rollback();
                _isTransaction = false;
                this.closeDatabase();
            }
            catch
            {

            }
            return 1;
        }




        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public bool executeProc(ref ProcWorker pw)
        {
            try
            {
                ArrayList al = new ArrayList();
                this.openDatabase();
                transCommandMySql = new MySqlCommand();
                transCommandMySql.Connection = transConnectionMySql;
                transCommandMySql.CommandText = pw.ProcName;
                transCommandMySql.Transaction = transMySql;
                transCommandMySql.CommandType = CommandType.StoredProcedure;

                MySqlDbType myT = MySqlDbType.VarChar;

                for (int i = 0; i < pw.LP.Count; i++)
                {
                    Para p = pw.LP[i];
                    switch (p.Dtp)
                    {
                        case DBDataType.Number:
                            myT = MySqlDbType.Float;
                            break;
                        case DBDataType.Varchar:
                            myT = MySqlDbType.VarChar;
                            break;
                        default:
                            PException e = new PException(null, null, "数据类型未定义");
                            pw.Mp = e;
                            pw.IsSuccess = false;
                            return false;
                    }

                    transCommandMySql.Parameters.Add(p.Pname, myT, p.Size);

                    switch (p.Pt)
                    {
                        case ProcType.IN:
                            transCommandMySql.Parameters[i].Direction = ParameterDirection.Input;
                            transCommandMySql.Parameters[i].Value = p.Value;
                            break;
                        case ProcType.INOUT:
                            transCommandMySql.Parameters[i].Direction = ParameterDirection.InputOutput;
                            transCommandMySql.Parameters[i].Value = p.Value;
                            al.Add(i);
                            break;
                        case ProcType.OUT:
                            transCommandMySql.Parameters[i].Direction = ParameterDirection.Output;
                            transCommandMySql.Parameters[i].Value = p.Value;
                            al.Add(i);
                            break;
                        default:
                            PException e = new PException(null, null, "参数类型未定义");
                            pw.Mp = e;
                            pw.IsSuccess = false;
                            return false;
                    }
                }

                MySqlDataReader dataReader = null;
                DataSet ds = null;
                dataReader = transCommandMySql.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.HasRows)
                {
                    ds = new DataSet(pw.ProcName);
                    DataTable dt = new DataTable();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        dt.Columns.Add(dataReader.GetName(i));
                    }

                    while (dataReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            dr[i] = (dataReader.GetValue(i));
                        }
                        dt.Rows.Add(dr);
                    }

                    ds.Tables.Add(dt);

                    while (dataReader.NextResult())
                    {
                        DataTable dts = null;
                        dts = new DataTable();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            dts.Columns.Add(dataReader.GetName(i));
                        }

                        while (dataReader.Read())
                        {
                            DataRow dr = dts.NewRow();
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                dr[i] = (dataReader.GetValue(i));
                            }
                            dts.Rows.Add(dr);
                        }

                        ds.Tables.Add(dts);
                    }
                }

                dataReader.Close();

                ArrayList alre = new ArrayList();

                for (int i = 0; i < al.Count; i++)
                {
                    int index = Convert.ToInt32(al[i]);
                    alre.Add(transCommandMySql.Parameters[index].Value);
                }

                pw.setReturn(alre, (ds == null) ? null : ds);

                return pw.IsSuccess;
            }
            catch (Exception ex)
            {
                if (!IsTransaction)
                {
                    this.closeDatabase();
                }
                PException e = new PException(ex, ex.GetType(), pw.ProcName);
                pw.Mp = e;
                pw.IsSuccess = false;
                return pw.IsSuccess;
            }
        }





    }
}
