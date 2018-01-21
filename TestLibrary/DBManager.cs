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
        private DBType dbt = DBType.Mysql;
        /// <summary>
        /// 连接串
        /// </summary>
        private string str = "Server=xx.xx.xx.xx;Port=xx;Database=xx;Uid=xx;Pwd=xx;CharSet=utf8;";
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
