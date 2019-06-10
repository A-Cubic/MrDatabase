/* ================================================================ 
// Copyright (C) 2009-2009 港隆科技有限公司
// All rights reserved.
//
// 作者：yaoronghui       时间：2009/12/1-3       版本：v1.0
//
// 功能描述：Sqlserver数据库访问类文件
=================================================================== */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using Com.ACBC.Framework.Database;

namespace Com.ACBC.Framework.Database
{
	/// <summary>
	/// 接口方法的实现(sqlserver数据库下的处理)
	/// </summary>
	public class SqlAccess : IDatabaseOperation
	{



        private SqlConnection sqlConnection = null;

        private SqlCommand sqlCommand;

        private SqlTransaction sqlTransaction;

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

        /// <summary>
        /// 构造函数
        /// </summary>
		public SqlAccess(string conn)
		{
            _connectionString = conn;
		}

       
        public SqlAccess()
        {
            
        }

		

		
		/// <summary>
		/// 打开数据库
		/// </summary>
		public void openDatabase()
		{
            //if (SqlCon != null)
            //{
            //    if (SqlCon.State == ConnectionState.Open)
            //    {
            //        SqlCon.Close();
            //        SqlCon.Open();
            //    }
            //    else
            //    {
            //        SqlCon.Open();
            //    }
            //}


            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(_connectionString);
                (sqlConnection).Open();
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {

                sqlConnection = new SqlConnection(_connectionString);
                (sqlConnection).Open();
            }

		}


		/// <summary>
		/// 关闭数据库
		/// </summary>
		public void closeDatabase()
		{

            if (sqlConnection != null)
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                else
                {
                    sqlConnection.Close();
                }
            }

            //if (SqlCon != null)
            //{
            //    if (SqlCon.State == ConnectionState.Open)
            //    {
            //        SqlCon.Close();
            //    }
            //    else
            //    {
            //        SqlCon.Close();
            //    }
            //}
		}


		/// <summary>
		/// 获取数据库连接串
		/// </summary>
		/// <returns></returns>
		public string getConnectString()
		{
            return _connectionString;
            //return SqlCon.ConnectionString.ToString();
		}

		/// <summary>
		/// 设置数据库连接串
		/// </summary>
		/// <returns></returns>
		public void setConnectString(string con)
		{
			//SqlCon.ConnectionString = con;
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
                SqlDataAdapter oda = new SqlDataAdapter(strSql, sqlConnection);
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


            //if (SqlDad == null)
            //{
            //    //throw new System.ObjectDisposedException(GetType().FullName);
            //}
            //if (SqlCom == null)
            //{
            //    SqlCom = new SqlCommand();
            //    SqlCom.Connection = SqlCon;
            //}
            //SqlCom.CommandType = CommandType.Text;
            //SqlCom.CommandText = strSql;
            //SqlDad.SelectCommand = SqlCom;
            //DataSet ds = new DataSet();
            //try
            //{
            //    SqlDad.Fill(ds, tableName);
            //}
            //catch (SqlException e)
            //{
            //    //SqlCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "操作数据库出错，查询失败");
            //    throw pException;
            //}
            ////SqlCon.Close();
            //return ds;
		}

		/// <summary>
		/// 返回XML类型数据
		/// </summary>
		/// <returns></returns>
		public string getXML(string sql, string tableName)
		{
            DataSet ds = this.getDataSet(sql, tableName);
			return ds.GetXml();
		}


		/// <summary>
		/// 返回DataReader类型数据
		/// </summary>
		/// <returns></returns>
		public System.Data.Common.DbDataReader getDataReader(string strSql)
		{
            try
            {
                this.openDatabase();
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = strSql;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.CommandType = CommandType.Text;

                System.Data.Common.DbDataReader dataReader;
                dataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

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

            //if (SqlCom == null)
            //{
            //    SqlCom = new SqlCommand();
            //    SqlCom.Connection = SqlCon;
            //}
            //try
            //{
            //    if (SqlCom.Connection.State != ConnectionState.Open)
            //    {
            //        SqlCom.Connection.Open();
            //    }
            //}
            //catch (SqlException e)
            //{
            //    ////throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //}

            //SqlCom.CommandType = CommandType.Text;
            //SqlCom.CommandText = strSql;

            ////定义要返回的SqlDataReader对象
            //SqlDataReader dr = null;
            //System.Data.Common.DbDataReader dr1;
            //try
            //{
            //    dr = SqlCom.ExecuteReader();
            //    SqlCon.Close();
            //}
            //catch (SqlException e)
            //{
            //    //SqlCon.Close();
            //    ////throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}
            //dr1 = dr;
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
            //if (SqlCom == null)
            //{
            //    SqlCom = new SqlCommand();
            //    SqlCom.Connection = SqlCon;
            //}
            //try
            //{
            //    if (SqlCom.Connection.State != ConnectionState.Open)
            //    {
            //        SqlCom.Connection.Open();
            //    }
            //}
            //catch (SqlException e)
            //{
            //    ////throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}

            //SqlCom.CommandType = CommandType.Text;
            //SqlCom.CommandText = strSql;

            //try
            //{
            //    i = SqlCom.ExecuteNonQuery();
            //}
            //catch (SqlException e)
            //{
            //    i = 0;
            //    //SqlCon.Close();
            //    ////throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}
            ////SqlCon.Close();
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
            //if (SqlCom == null)
            //{
            //    SqlCom = new SqlCommand();
            //    SqlCom.Connection = SqlCon;
            //}
            //try
            //{
            //    if (SqlCom.Connection.State != ConnectionState.Open)
            //    {
            //        SqlCom.Connection.Open();
            //    }
            //}
            //catch (SqlException e)
            //{
            //    //throw new Exception(e.Message.ToString());	
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //}

            //SqlCom.CommandType = CommandType.Text;
            //SqlCom.CommandText = strSql;

            //try
            //{
            //    i = SqlCom.ExecuteNonQuery();
            //}
            //catch (SqlException e)
            //{
            //    i = 0;
            //    //SqlCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //}
            ////SqlCon.Close();
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
            //if (SqlCom == null)
            //{
            //    SqlCom = new SqlCommand();
            //    SqlCom.Connection = SqlCon;
            //}
            //try
            //{
            //    if (SqlCom.Connection.State != ConnectionState.Open)
            //    {
            //        SqlCom.Connection.Open();
            //    }
            //}
            //catch (SqlException e)
            //{
            //    //throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}

            //SqlCom.CommandType = CommandType.Text;
            //SqlCom.CommandText = strSql;

            //try
            //{
            //    i = SqlCom.ExecuteNonQuery();
            //}
            //catch (SqlException e)
            //{
            //    i = 0;
            //    //SqlCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //}
            ////SqlCon.Close();
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

            //if (SqlCom == null)
            //{
            //    SqlCom = new SqlCommand();
            //    SqlCom.Connection = SqlCon;
            //}
            //try
            //{
            //    if (SqlCom.Connection.State != ConnectionState.Open)
            //    {
            //        SqlCom.Connection.Open();
            //    }
            //}
            //catch (SqlException e)
            //{
            //    //throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}
            //SqlCom.CommandType = CommandType.Text;
            //// Oracle 事务处理
            //SqlTransaction sqlTransaction = SqlCom.Connection.BeginTransaction();
            //SqlCom.Transaction = sqlTransaction;
            //string commandSql = "";
            //try
            //{
            //    foreach (string strSql in strSqlList)
            //    {
            //        commandSql = strSql;
            //        SqlCom.CommandText = strSql;
            //        SqlCom.ExecuteNonQuery();
            //    }
            //}
            //catch (SqlException e)
            //{
            //    // 操作发生异常，事务回滚
            //    //SqlTransaction.Rollback();
            //    //SqlCon.Close();
            //    //throw new Exception(e.Message.ToString());
            //    PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
            //    throw pException;
            //}
            //// 事务执行
            ////SqlTransaction.Commit();
            ////SqlCon.Close();
            //return true;
		}




		/// <summary>
		/// 调用存储过程
		/// </summary>
		/// <param name="prscName"></param>
		/// <returns></returns>
        public bool executeProc(SqlCommand sqlcommand)
		{
            if (sqlcommand != null)
			{
                sqlcommand.Connection = sqlConnection;
			}
			else
			{
                return false;
			}
            sqlcommand.CommandType = CommandType.StoredProcedure;
            try
            {
                if (sqlcommand.Connection.State != ConnectionState.Open)
                {
                    sqlcommand.Connection.Open();
                }
            }
            catch (SqlException e)
            {
                //
                /// <summary> 异常类实例化对象 </summary>
                PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
                throw pException;
            }

			// sql 事务处理
            SqlTransaction SqlTransaction = sqlcommand.Connection.BeginTransaction();
            sqlcommand.Transaction = SqlTransaction;
			try
			{
                sqlcommand.ExecuteNonQuery();
			}
			catch (SqlException e)
			{
				// 操作发生异常，事务回滚
				SqlTransaction.Rollback();
				sqlConnection.Close();
				//throw new Exception(e.Message.ToString());
				PException pException = new PException(e, e.GetType(), "无法打开数据库，连接出错");
                throw pException;
			}
			// 事务执行
			SqlTransaction.Commit();
			//SqlCon.Close();
			return true;
		}
		



        ///// <summary>
        ///// 实现返回数据集合存储过程
        ///// </summary>
        ///// <param name="p"></param>
        ///// <returns></returns>
        public bool executeProc(ref ProcWorker pw)
        {

            try
            {
                ArrayList al = new ArrayList();
                this.openDatabase();
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = pw.ProcName;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.CommandType = CommandType.StoredProcedure;


                SqlDbType myT = SqlDbType.VarChar;

                for (int i = 0; i < pw.LP.Count; i++)
                {
                    Para p = pw.LP[i];
                    switch (p.Dtp)
                    {
                        case DBDataType.Number:
                            myT = SqlDbType.Float;
                            break;
                        case DBDataType.Varchar:
                            myT = SqlDbType.VarChar;
                            break;
                        default:
                            PException e = new PException(null, null, "参数类型未定义");
                            pw.Mp = e;
                            pw.IsSuccess = false;
                            return false;
                    }

                    sqlCommand.Parameters.Add(p.Pname, myT, p.Size);

                    switch (p.Pt)
                    {
                        case ProcType.IN:
                            sqlCommand.Parameters[i].Direction = ParameterDirection.Input;
                            sqlCommand.Parameters[i].Value = p.Value;
                            break;
                        case ProcType.INOUT:
                            sqlCommand.Parameters[i].Direction = ParameterDirection.InputOutput;
                            sqlCommand.Parameters[i].Value = p.Value;
                            al.Add(i);
                            break;
                        case ProcType.OUT:
                            sqlCommand.Parameters[i].Direction = ParameterDirection.Output;
                            sqlCommand.Parameters[i].Value = p.Value;
                            al.Add(i);
                            break;
                        default:
                            PException e = new PException(null, null, "参数类型未定义");
                            pw.Mp = e;
                            pw.IsSuccess = false;
                            return false;
                    }
                }

              
                SqlDataReader dataReader = null;
                DataSet ds = null;
                //DataTable dt = null;
                dataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //dataReader = transCommandMySql.ExecuteReader();

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
                    alre.Add(sqlCommand.Parameters[index].Value);
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






        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            this.openDatabase();
            sqlTransaction = sqlConnection.BeginTransaction();
            _isTransaction = true;
        }


        /// <summary>
        /// 提交事务
        /// </summary>
        public int Commit()
        {
            try
            {
                 sqlTransaction.Commit();
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
                (sqlTransaction).Rollback();
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
                sqlCommand = new SqlCommand(sqlString, sqlConnection);
                if (_isTransaction)
                    sqlCommand.Transaction = sqlTransaction;
                if (sqlCommand.CommandText.Trim() != "")
                {
                    return sqlCommand.ExecuteNonQuery();
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
