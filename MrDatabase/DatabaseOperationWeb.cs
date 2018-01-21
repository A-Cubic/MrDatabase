﻿using System;
using System.Data;
using System.Collections;

namespace Com.ACBC.Framework.Database
{
    public class DatabaseOperationWeb
    {
        private static IType it = null;
        

        //在设置TYPE时，实例化相关数据库对象，同时对连接串赋值
        public static IType TYPE
        {
            get
            {
                return it;
            }
            set
            {
                it = value;
                
            }
        }

        //无特殊说明
        public static DataSet ExecuteSelectDS(string sql, string tablename)
        {
            DataSet ds = null;
            IDatabaseOperation ido = null;
            if (TYPE == null)
            {
                OutPutError.getError(null, "数据库类型及连接串未配置");
                return null;
            }
            else
            {
                switch (TYPE.getDBType())
                {
                    //case DBType.Access:
                    //    ido = new BaseDataAccess();

                    //    break;

                    case DBType.Mysql:
                        ido = new MysqlAccess();

                        break;

                    //case DBType.Oracle:
                    //    ido = new OracleAccess();

                    //    break;

                    //case DBType.SqlServers:
                    //    ido = new SqlAccess();

                    //    break;
                }
                ido.setConnectString(TYPE.getConnString());
            }
            

            try
            {
                ds = ido.getDataSet(sql, tablename);
            }
            catch (PException ex)
            {
                OutPutError.getError(ex, sql);
                throw ex;
            }
            //catch (Exception e)
            //{
            //    null;
            //}

            return ds;
        }

        //考虑第三参数，表示XML类型，类型为枚举
        public static string ExecuteSelectXML(string sql, string tablename)
        {
            IDatabaseOperation ido = null;
            if (TYPE == null)
            {
                OutPutError.getError(null, "数据库类型及连接串未配置");
                return null;
            }
            else
            {
                switch (TYPE.getDBType())
                {
                    //case DBType.Access:
                    //    ido = new BaseDataAccess();

                    //    break;

                    case DBType.Mysql:
                        ido = new MysqlAccess();

                        break;

                    //case DBType.Oracle:
                    //    ido = new OracleAccess();

                    //    break;

                    //case DBType.SqlServers:
                    //    ido = new SqlAccess();

                    //    break;
                }
                ido.setConnectString(TYPE.getConnString());
            }
            string xml = "";

            try
            {
                xml = ido.getXML(sql, tablename);
            }
            catch (PException ex)
            {
                OutPutError.getError(ex, sql);
                throw ex;
            }
            //catch (Exception e)
            //{
            //    null;
            //}

            return xml;
        }

        //内部处理需要统一，需要讨论
        public static bool ExecuteDML(string sql)
        {
            if (TYPE == null)
            {
                OutPutError.getError(null, "数据库类型及连接串未配置");
                return false;
            }

            bool t = false;
            ArrayList al = new ArrayList();
            al.Add(sql);
            t = ExecuteDML(al);

            return t;
        }

        //同上，重点在于ArrayList的生成
        public static bool ExecuteDML(ArrayList al)
        {
            IDatabaseOperation ido = null;

            if (TYPE == null)
            {
                OutPutError.getError(null, "数据库类型及连接串未配置");
                return false;
            }
            else
            {
                
                switch (TYPE.getDBType())
                {
                    //case DBType.Access:
                    //    ido = new BaseDataAccess();

                    //    break;

                    case DBType.Mysql:
                        ido = new MysqlAccess();

                        break;

                    //case DBType.Oracle:
                    //    ido = new OracleAccess();

                    //    break;

                    //case DBType.SqlServers:
                    //    ido = new SqlAccess();

                    //    break;
                }
                ido.setConnectString(TYPE.getConnString());
            }
            bool t = false;
            try
            {
                t = ido.executeBatch(al);
            }
            catch (PException ex)
            {
                OutPutError.getError(ex, ex.Remark);
                throw ex;
            }
            //catch (Exception e)
            //{
            //    null;
            //}

            return t;
        }

        //PReader在Read和HasRows的返回上处理了数据连接的关闭
        public static PReader ExecuteSelectDR(string sql, string tablename)
        {
            IDatabaseOperation ido = null;

            if (TYPE == null)
            {
                OutPutError.getError(null, "数据库类型及连接串未配置");
                return null;
            }
            else
            {
                
                switch (TYPE.getDBType())
                {
                    //case DBType.Access:
                    //    ido = new BaseDataAccess();

                    //    break;

                    case DBType.Mysql:
                        ido = new MysqlAccess();

                        break;

                    //case DBType.Oracle:
                    //    ido = new OracleAccess();

                    //    break;

                    //case DBType.SqlServers:
                    //    ido = new SqlAccess();

                    //    break;
                }
                ido.setConnectString(TYPE.getConnString());
            }
            PReader pr = null;

            try
            {
                pr = new PReader(ido.getDataReader(sql), ido);
            }
            catch (PException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                OutPutError.getError(e, sql);
            }

            return pr;
        }

        //存储过程的调用，利用ProcWorker创建存储过程对象，整合参数类型和输出模式。
        public static bool executeProc(ref ProcWorker p)
        {
            IDatabaseOperation ido = null;

            if (TYPE == null)
            {
                OutPutError.getError(null, "数据库类型及连接串未配置");
                return false;
            }
            else
            {

                switch (TYPE.getDBType())
                {
                    //case DBType.Access:
                    //    ido = new BaseDataAccess();

                    //    break;

                    case DBType.Mysql:
                        ido = new MysqlAccess();

                        break;

                    //case DBType.Oracle:
                    //    ido = new OracleAccess();

                    //    break;

                    //case DBType.SqlServers:
                    //    ido = new SqlAccess();

                    //    break;
                }
                ido.setConnectString(TYPE.getConnString());
            }

            try
            {
                return ido.executeProc(ref p);
            }
            catch (PException ex)
            {
                OutPutError.getError(ex, ex.Remark);
                throw ex;
            }
        }

        //下一步工作重点在异常的统一，对外发布的异常的规则
        //XML的格式制定
        //存储过程参数和返回值的类型枚举
        //ProcWorker的编写
        //时间初步定在下周一检查
    }
}
