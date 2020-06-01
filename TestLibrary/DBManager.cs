using System;
using System.Collections.Generic;
using System.Text;
using Com.ACBC.Framework.Database;

namespace TestLibrary
{
    class DBManager : IType
    {
        /// <summary>
        /// 数据库类型对象
        /// </summary>
        private DBType dbt = DBType.Oracle;
        /// <summary>
        /// 连接串
        /// </summary>
        private string str = "DATA SOURCE=(DESCRIPTION ="
                                    + "    (ADDRESS_LIST ="
                                    + "      (ADDRESS = (PROTOCOL = TCP)(HOST = )(PORT = 1521))"
                                    + "    )"
                                    + "    (CONNECT_DATA ="
                                    + "      (SERVICE_NAME = orcl)"
                                    + "    )"
                                    + "  )"
                                    + "; USER ID =;PASSWORD=";
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBManager()
        {

        }
        /// <summary>
        /// 数据库类型对象
        /// </summary>
        /// <returns></returns>
        public DBType getDBType()
        {
            return dbt;
        }
        /// <summary>
        /// 获取连接串
        /// </summary>
        /// <returns></returns>
        public string getConnString()
        {
            return str;
        }
        /// <summary>
        /// 设置连接串
        /// </summary>
        /// <param name="s"></param>
        public void setConnString(string s)
        {
            this.str = s;
        }
    }
}
