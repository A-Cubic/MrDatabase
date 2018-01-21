using System;
using Com.ACBC.Framework.Database;
using System.Data;

namespace TestLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseOperation.TYPE = new DBManager();
            DataTable dt = DatabaseOperation.ExecuteSelectDS("select now()", "t").Tables[0];
            Console.WriteLine(dt.Rows[0][0].ToString());
            Console.ReadKey();
        }
    }
}
