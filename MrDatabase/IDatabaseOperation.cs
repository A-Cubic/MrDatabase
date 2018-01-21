using System.Data;
using System.Data.Common;
using System.Collections;

namespace Com.ACBC.Framework.Database
{
    /// <summary>
    /// 数据库操作接口类
    /// </summary>
    public interface IDatabaseOperation
    {
        /// <summary>
        /// 打开数据库
        /// </summary>
        void openDatabase();

        /// <summary>
        /// 关闭数据库
        /// </summary>
        void closeDatabase();

        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <returns></returns>
        string getConnectString();

        /// <summary>
        /// 设置数据库连接串
        /// </summary>
        /// <returns></returns>
        void setConnectString(string con);

        /// <summary>
        /// 返回DATASET类型数据
        /// </summary>
        /// <returns></returns>
        DataSet getDataSet(string strSql, string tableName);

        /// <summary>
        /// 返回XML类型数据
        /// </summary>
        /// <returns></returns>
        string getXML(string strSql, string tableName);

        /// <summary>
        /// 返回DATAREADER类型数据
        /// </summary>
        /// <returns></returns>
        DbDataReader getDataReader(string strSql);

        //ArrayList getDataList( );

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <returns></returns>
        int executeInsert(string strSql);

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <returns></returns>
        int executeUpdate(string strSql);

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <returns></returns>
        int executeDelete(string strSql);

        /// <summary>
        /// 按事物执行DML操作
        /// </summary>
        /// <returns></returns>
        bool executeBatch(ArrayList ary);

        ///// <summary>
        ///// 调用不返回参数存储过程
        ///// </summary>
        ///// <param name="procName"></param>
        ///// <returns></returns>
        //bool executeProc(OracleCommand oracleCommand);

        /// <summary>
        /// 返回参数的存储过程
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool executeProc(ref ProcWorker p);
        
    }
}
