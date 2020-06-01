using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Collections;
using Oracle.ManagedDataAccess.Client;

namespace Com.ACBC.Framework.Database
{
    /// <summary>
    /// 接口方法的实现(Oracle数据库下的处理)
    /// </summary>
    public class OracleAccess : IDatabaseOperation
    {
       
        private OracleConnection oracleConnection;

        private OracleCommand oracleCommand;

        private OracleDataReader OraDr;
       
       // private OracleCommand transCommandMySql;

        private OracleTransaction oracleTransaction;
        private string _connectionString = "";
        private bool _isTransaction = false;
        private ArrayList al = new ArrayList();


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public OracleAccess(string conn)
        {
            _connectionString = conn;
        }

        
        public OracleAccess()
        {

        }

        #endregion 

        #region 析构函数
        /// <summary>
        /// 析构函数(垃圾自动回收)不可调用
        /// </summary>
        public void Dispose()
        {

            //if (OraCon != null)
            //{
            //    if (OraCon.State == ConnectionState.Open)//关闭连接
            //        OraCon.Close();
            //    OraCon.Dispose();
            //}

            //if (OraDad != null)
            //{
            //    if (OraDad.SelectCommand != null)
            //    {

            //        OraDad.SelectCommand.Dispose();
            //    }
            //    if (OraDad.InsertCommand != null)
            //    {

            //        OraDad.InsertCommand.Dispose();
            //    }
            //    if (OraDad.DeleteCommand != null)
            //    {

            //        OraDad.DeleteCommand.Dispose();
            //    }
            //    if (OraDad.UpdateCommand != null)
            //    {

            //        OraDad.UpdateCommand.Dispose();
            //    }
            //    OraDad.Dispose();
            //    OraDad = null;
            //}

            //if (OraCom != null)
            //{
            //    if (OraCom.Connection != null)
            //    {
            //        OraCom.Connection.Dispose();
            //    }
            //    OraCom.Dispose();
            //}

            GC.SuppressFinalize(true);
        }
        #endregion

       

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

        /// <summary>
        /// 打开数据库
        /// </summary>
        public void openDatabase()
        {
            //if (OraCon != null)
            //{
            //    if (OraCon.State == ConnectionState.Open)
            //    {
            //        OraCon.Close();
            //        OraCon.Open();
            //    }
            //    else
            //    {
            //        OraCon.Open();
            //    }
            //}



            if (oracleConnection == null)
            {
                oracleConnection = new OracleConnection(_connectionString);
                (oracleConnection).Open();
            }
            else if (oracleConnection.State == ConnectionState.Closed)
            {

                oracleConnection = new OracleConnection(_connectionString);
                (oracleConnection).Open();
            }

        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void closeDatabase()
        {

            if (oracleConnection != null)
            {
                if (oracleConnection.State == ConnectionState.Open)
                {
                    oracleConnection.Close();
                }
                else
                {
                    oracleConnection.Close();
                }
            }

            //if (oracleConnection != null)
            //{
            //    if (oracleConnection.State == ConnectionState.Open)
            //    {
            //        OraCon.Close();
            //    }
            //    else
            //    {
            //        OraCon.Close();
            //    }
            //}
        }

        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <returns></returns>
        public string getConnectString()
        {
            //return OraCon.ConnectionString.ToString();
            return _connectionString;
        }


        /// <summary>
        /// 设置数据库连接串
        /// </summary>
        /// <returns></returns>
        public void setConnectString(string con)
        {
            //OraCon.ConnectionString = con;
            _connectionString = con;
        }


        /// <summary>
        /// 返回DATASET类型数据
        /// </summary>
        /// <param name="strSql">数据库查询语句strSql</param>
        /// <param name="tableName">数据集里表名tableName</param>
        /// <returns>数据库结果集ds1</returns>
        public DataSet getDataSet(string strSql, string tableName)
        {
            try
            {
                this.openDatabase();
                OracleDataAdapter oda = new OracleDataAdapter(strSql, oracleConnection);
                DataSet dsData = new DataSet();
                oda.Fill(dsData, tableName);
                this.closeDatabase();
                return dsData;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), strSql);
                throw e;
            }
            finally
            {
                if (!IsTransaction)
                {
                    this.closeDatabase();
                }
            }

            //if (OraDad == null)
            //{
            //    throw new System.ObjectDisposedException(GetType().FullName);
            //}
            //if (OraCom == null)
            //{
            //    OraCom = new OracleCommand();
            //    OraCom.Connection = OraCon;
            //}
            //OraCom.CommandType = CommandType.Text;
            //OraCom.CommandText = strSql;
            //OraDad.SelectCommand = OraCom;
            //DataSet ds = new DataSet();
            //try
            //{
            //    OraDad.Fill(ds, tableName);
            //}
            //catch (OracleException e)
            //{
            //    OraCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "操作数据库出错，查询失败");
            //    throw pException;
            //}
            //OraCon.Close();
            //return ds;
        }


        ///// <summary>
        ///// 返回XML类型数据
        ///// </summary>
        ///// <returns></returns>
        //public string getXML(DataSet ds)
        //{
        //   //ds.WriteXml(path);
        //   //ds.ReadXml(path);
        //   //return ds.GetXml();
        //    DataSet ds = getDataSet(strSql, tableName);

        //    string oleDbXML = ds.GetXml();

        //    return oleDbXML;

        //}


        /// <summary>
        /// 返回XML类型数据
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="tableName">DataTable名</param>
        /// <returns>XML</returns>
        public string getXML(string strSql, string tableName)
        {
            DataSet ds = getDataSet(strSql, tableName);

            string XML = ds.GetXml();

            return XML;
        }


        /// <summary>
        /// 返回DATAREADER类型数据
        /// </summary>
        /// <returns></returns>
        public System.Data.Common.DbDataReader getDataReader(string strSql)
        {

            try
            {
                this.openDatabase();
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = oracleConnection;
                oracleCommand.CommandText = strSql;
                oracleCommand.Transaction = oracleTransaction;
                oracleCommand.CommandType = CommandType.Text;

                System.Data.Common.DbDataReader dataReader;
                dataReader = oracleCommand.ExecuteReader(CommandBehavior.CloseConnection);

                return dataReader;
            }
            catch (Exception ex)
            {
                if (!IsTransaction)
                {
                    this.closeDatabase();
                }
                PException e = new PException(ex, ex.GetType(), strSql);
                throw e;
            }

            //if (OraCom == null)
            //{
            //    OraCom = new OracleCommand();
            //    OraCom.Connection = OraCon;
            //}
            //try
            //{
            //    if (OraCom.Connection.State != ConnectionState.Open)
            //    {
            //        OraCom.Connection.Open();
            //    }
            //}
            //catch (OracleException e)
            //{
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}

            //OraCom.CommandType = CommandType.Text;
            //OraCom.CommandText = strSql;

            ////定义要返回的OracleDataReader对象
            ////OracleDataReader dr = null;
            //System.Data.Common.DbDataReader dr1;
            //try
            //{
            //    OraDr = OraCom.ExecuteReader();
            //    OraCon.Close();
            //}
            //catch (OracleException e)
            //{
            //   OraCon.Close();
            //   // throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "操作数据库出错，删除失败");
            //    throw pException;
            //}
            //dr1 = OraDr;
            //return  dr1;
        }


        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <returns></returns>
        public int executeInsert(string strSql)
        {
            try
            {
                this.al.Clear();
                this.al.Add(strSql);
                this.executeBatch();
                return 1;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), "INSERT");
                throw e;
            }

            //int i;
            //if (OraCom == null)
            //{
            //    OraCom = new OracleCommand();
            //    OraCom.Connection = OraCon;
            //}
            //try
            //{
            //    if (OraCom.Connection.State != ConnectionState.Open)
            //    {
            //        OraCom.Connection.Open();
            //    }
            //}
            //catch (OracleException e)
            //{
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}

            //OraCom.CommandType = CommandType.Text;
            //OraCom.CommandText = strSql;

            //try
            //{
            //    i = OraCom.ExecuteNonQuery();
            //}
            //catch (OracleException e)
            //{
            //    i = 0;
            //    OraCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "操作数据库出错，删除失败");
            //    throw pException;
            //}
            //OraCon.Close();
            //return i;
        }



        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <returns></returns>
        public int executeUpdate(string strSql)
        {
            try
            {
                this.al.Clear();
                this.al.Add(strSql);
                this.executeBatch();
                return 1;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), "UPDATE");
                throw e;
            }

            //int i;
            //if (oracleCommand == null)
            //{
            //    oracleCommand = new OracleCommand();
            //    oracleCommand.Connection = oracleConnection;
            //}
            //try
            //{
            //    if (oracleCommand.Connection.State != ConnectionState.Open)
            //    {
            //        oracleCommand.Connection.Open();
            //    }
            //}
            //catch (OracleException e)
            //{
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}

            //OraCom.CommandType = CommandType.Text;
            //OraCom.CommandText = strSql;

            //try
            //{
            //    i = OraCom.ExecuteNonQuery();
            //}
            //catch (OracleException e)
            //{
            //    i = 0;
            //    OraCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "操作数据库出错，删除失败");
            //    throw pException;
            //}
            //OraCon.Close();
            //return i;
        }


        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <returns></returns>
        public int executeDelete(string strSql)
        {

            try
            {
                this.al.Clear();
                this.al.Add(strSql);
                this.executeBatch();
                return 1;
            }
            catch (Exception ex)
            {
                PException e = new PException(ex, ex.GetType(), "DELETE");
                throw e;
            }

            //int i;
            //if (OraCom == null)
            //{
            //    OraCom = new OracleCommand();
            //    OraCom.Connection = OraCon;
            //}
            //try
            //{
            //    if (OraCom.Connection.State != ConnectionState.Open)
            //    {
            //        OraCom.Connection.Open();
            //    }
            //}
            //catch (OracleException e)
            //{
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}

            //OraCom.CommandType = CommandType.Text;
            //OraCom.CommandText = strSql;

            //try
            //{
            //    i = OraCom.ExecuteNonQuery();
            //}
            //catch (OracleException e)
            //{
            //    i = 0;
            //    OraCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    /// <summary> 异常类实例化对象 </summary>
            //    PException pException = new PException(e, e.GetType(), "操作数据库出错，删除失败");
            //    throw pException;
            //}
            //OraCon.Close();
            //return i;
        }


        /// <summary>
        /// 按事物执行DML操作
        /// </summary>
        /// <returns></returns>
        public bool executeBatch(ArrayList sqlList)
        {
            this.al = sqlList;
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
                al = new ArrayList();
            }


          //if (OraCom == null)
          //  {
          //      OraCom = new OracleCommand();
          //      OraCom.Connection = OraCon;
          //  }
          //  try
          //  {
          //      if (OraCom.Connection.State != ConnectionState.Open)
          //      {
          //          OraCom.Connection.Open();
          //      }
          //  }
          //  catch (OracleException e)
          //  {
          //      //throw new Exception(e.Message.ToString());
          //      /// <summary> 异常类实例化对象 </summary>
          //      PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
          //      throw pException;
          //  }
          //  OraCom.CommandType = CommandType.Text;
          //  // Oracle 事务处理
          //  OracleTransaction oracleTransaction = OraCom.Connection.BeginTransaction();
          //  OraCom.Transaction = oracleTransaction;
          //  string commandSql = "";
          //  try
          //  {
          //      foreach (string strSql in strSqlList)
          //      {
          //          commandSql = strSql;
          //          OraCom.CommandText = strSql;
          //          OraCom.ExecuteNonQuery();
          //      }
          //  }
          //  catch (OracleException e)
          //  {
          //      // 操作发生异常，事务回滚
          //      oracleTransaction.Rollback();
          //      OraCon.Close();
          //      //throw new Exception(e.Message.ToString());
          //      /// <summary> 异常类实例化对象 </summary>
          //      PException pException = new PException(e, e.GetType(), "操作数据库时发生错误，操作失败");
          //      throw pException;
          //  }
          //  // 事务执行
          //  oracleTransaction.Commit();
          //  return true;
        }


        /// <summary>
        /// 调用存储过程(重载)
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public bool executeProc(OracleCommand oracleCommand)
        {
            if (oracleCommand != null)
            {
                oracleCommand.Connection = oracleConnection;
            }
            else
            {
                return false;
            }

            oracleCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                if (oracleCommand.Connection.State != ConnectionState.Open)
                {
                    oracleCommand.Connection.Open();
                }
            }
            catch (OracleException e)
            {
                /// <summary> 异常类实例化对象 </summary>
                PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
                throw pException;
            }

            // Oracle 事务处理
            OracleTransaction oracleTransaction = oracleCommand.Connection.BeginTransaction();
            oracleCommand.Transaction = oracleTransaction;

            try
            {
                oracleCommand.ExecuteNonQuery();
            }

            catch (OracleException e)
            {
                // 操作发生异常，事务回滚
                oracleTransaction.Rollback();
                oracleConnection.Close();
                //throw new Exception("执行存储过程发生错误：" + e.Message.ToString());
                /// <summary> 异常类实例化对象 </summary>
                PException pException = new PException(e, e.GetType(), "操作数据库时发生错误，操作失败");
                throw pException;
            }

            // 事务执行
            oracleTransaction.Commit();
            return true;
        }



        /// <summary>
        /// 调用存储过程(重载)
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public bool executeProc(ref ProcWorker pw)
        {
            try
            {
                ArrayList al = new ArrayList();
                this.openDatabase();
                oracleCommand = new OracleCommand();
                oracleCommand.Connection = oracleConnection;
                oracleCommand.CommandText = pw.ProcName;
                oracleCommand.Transaction = oracleTransaction;
                oracleCommand.CommandType = CommandType.StoredProcedure;

                OracleDbType myT = OracleDbType.Varchar2;

                for (int i = 0; i < pw.LP.Count; i++)
                {
                    Para p = pw.LP[i];
                    switch (p.Dtp)
                    {
                        case DBDataType.Number:
                            myT = OracleDbType.Double;
                            break;
                        case DBDataType.Varchar:
                            myT = OracleDbType.Varchar2;
                            break;
                        case DBDataType.Cursor:
                            myT = OracleDbType.RefCursor;
                            break;
                        default:
                            PException e = new PException(null, null, "参数类型未定义");
                            pw.Mp = e;
                            pw.IsSuccess = false;
                            return false;
                    }
                    oracleCommand.Parameters.Add(p.Pname, myT, p.Size);
                    switch (p.Pt)
                    {
                        case ProcType.IN:
                            oracleCommand.Parameters[i].Direction = ParameterDirection.Input;
                            oracleCommand.Parameters[i].Value = p.Value;
                            break;
                        case ProcType.INOUT:
                            oracleCommand.Parameters[i].Direction = ParameterDirection.InputOutput;
                            oracleCommand.Parameters[i].Value = p.Value;
                            al.Add(i);
                            break;
                        case ProcType.OUT:
                            oracleCommand.Parameters[i].Direction = ParameterDirection.Output;
                            //oracleCommand.Parameters[i].Value = p.Value;
                            al.Add(i);
                            break;
                        default:
                            PException e = new PException(null, null, "参数类型未定义");
                            pw.Mp = e;
                            pw.IsSuccess = false;
                            return false;
                    }
                }
                OracleDataReader dataReader = null;
                DataTable dt = null;
                DataSet ds = null;
                dataReader = oracleCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.HasRows)
                {
                    ds = new DataSet(pw.ProcName);
                    dt = new DataTable();
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
                        DataTable dts = new DataTable();
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
                    alre.Add(oracleCommand.Parameters[index].Value);
                }

                //pw.setReturn(alre, (dt == null) ? null : dt);

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





        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            this.openDatabase();
            oracleTransaction = oracleConnection.BeginTransaction();
            _isTransaction = true;
        }


        /// <summary>
        /// 提交事务
        /// </summary>
        public int Commit()
        {
            try
            {
                oracleTransaction.Commit();
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
                (oracleTransaction).Rollback();
                _isTransaction = false;
                this.closeDatabase();
            }
            catch
            {

            }
            return 1;
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
                oracleCommand = new OracleCommand(sqlString, oracleConnection);
                if (_isTransaction)
                    oracleCommand.Transaction = oracleTransaction;
                if (oracleCommand.CommandText.Trim() != "")
                {
                    return oracleCommand.ExecuteNonQuery();
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




    }
}
