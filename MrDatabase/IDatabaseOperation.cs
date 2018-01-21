using System.Data;
using System.Data.Common;
using System.Collections;

namespace Com.ACBC.Framework.Database
{
    /// <summary>
    /// ���ݿ�����ӿ���
    /// </summary>
    public interface IDatabaseOperation
    {
        /// <summary>
        /// �����ݿ�
        /// </summary>
        void openDatabase();

        /// <summary>
        /// �ر����ݿ�
        /// </summary>
        void closeDatabase();

        /// <summary>
        /// ��ȡ���ݿ����Ӵ�
        /// </summary>
        /// <returns></returns>
        string getConnectString();

        /// <summary>
        /// �������ݿ����Ӵ�
        /// </summary>
        /// <returns></returns>
        void setConnectString(string con);

        /// <summary>
        /// ����DATASET��������
        /// </summary>
        /// <returns></returns>
        DataSet getDataSet(string strSql, string tableName);

        /// <summary>
        /// ����XML��������
        /// </summary>
        /// <returns></returns>
        string getXML(string strSql, string tableName);

        /// <summary>
        /// ����DATAREADER��������
        /// </summary>
        /// <returns></returns>
        DbDataReader getDataReader(string strSql);

        //ArrayList getDataList( );

        /// <summary>
        /// ִ�в������
        /// </summary>
        /// <returns></returns>
        int executeInsert(string strSql);

        /// <summary>
        /// ִ�и��²���
        /// </summary>
        /// <returns></returns>
        int executeUpdate(string strSql);

        /// <summary>
        /// ִ��ɾ������
        /// </summary>
        /// <returns></returns>
        int executeDelete(string strSql);

        /// <summary>
        /// ������ִ��DML����
        /// </summary>
        /// <returns></returns>
        bool executeBatch(ArrayList ary);

        ///// <summary>
        ///// ���ò����ز����洢����
        ///// </summary>
        ///// <param name="procName"></param>
        ///// <returns></returns>
        //bool executeProc(OracleCommand oracleCommand);

        /// <summary>
        /// ���ز����Ĵ洢����
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool executeProc(ref ProcWorker p);
        
    }
}
