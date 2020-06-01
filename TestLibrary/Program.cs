using System;
using Com.ACBC.Framework.Database;
using System.Data;

namespace TestLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseOperationWeb.TYPE = new DBManager();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS("select * from dual", "t").Tables[0];
            Console.WriteLine(dt.Rows[0][0].ToString());
            Console.ReadKey();
        }
    }
}
