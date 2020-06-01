using System;
using System.Data;
using System.Collections;

namespace Com.ACBC.Framework.Database
{
    public class DatabaseOperation
    {
        private static IType it = null;
        private static IDatabaseOperation ido = null;

        //������TYPEʱ��ʵ����������ݿ����ͬʱ�����Ӵ���ֵ
        public static IType TYPE
        {
            get
            {
                return it;
            }
            set
            {
                it = value;
                switch (TYPE.getDBType())
                {
                    //case DBType.Access:
                    //    ido = new BaseDataAccess();
                        
                    //    break;

                    case DBType.Mysql:
                        ido = new MysqlAccess();
                        
                        break;

                    case DBType.Oracle:
                        ido = new OracleAccess();

                        break;

                    case DBType.SqlServers:
                        ido = new SqlAccess();

                        break;
                }
                ido.setConnectString(TYPE.getConnString());
            }
        }

        //������˵��
        public static DataSet ExecuteSelectDS(string sql, string tablename)
        {
            if (TYPE == null)
            {
                OutPutError.getError(null, "���ݿ����ͼ����Ӵ�δ����");
                return null;
            }
            DataSet ds = null;
            
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

        //���ǵ�����������ʾXML���ͣ�����Ϊö��
        public static string ExecuteSelectXML(string sql, string tablename)
        {
            if (TYPE == null)
            {
                OutPutError.getError(null, "���ݿ����ͼ����Ӵ�δ����");
                return null;
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

        //�ڲ�������Ҫͳһ����Ҫ����
        public static bool ExecuteDML(string sql)
        {
            if (TYPE == null)
            {
                OutPutError.getError(null, "���ݿ����ͼ����Ӵ�δ����");
                return false;
            }
            bool t =false;
            ArrayList al = new ArrayList();
            al.Add(sql);
            t = ExecuteDML(al);

            return t;
        }

        //ͬ�ϣ��ص�����ArrayList������
        public static bool ExecuteDML(ArrayList al)
        {
            if (TYPE == null)
            {
                OutPutError.getError(null, "���ݿ����ͼ����Ӵ�δ����");
                return false;
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

        //PReader��Read��HasRows�ķ����ϴ������������ӵĹر�
        public static PReader ExecuteSelectDR(string sql, string tablename)
        {
            if (TYPE == null)
            {
                OutPutError.getError(null, "���ݿ����ͼ����Ӵ�δ����");
                return null;
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

        //�洢���̵ĵ��ã�����ProcWorker�����洢���̶������ϲ������ͺ����ģʽ��
        public static bool executeProc(ref ProcWorker p)
        {
            if (TYPE == null)
            {
                OutPutError.getError(null, "���ݿ����ͼ����Ӵ�δ����");
                return false;
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
    }
}
