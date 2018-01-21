using System;
using System.Text;
using System.IO;

namespace Com.ACBC.Framework.Database
{
    class OutPutError
    {
        private static string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\Error.txt";

        public static void getError(Exception e,string msg)
        {
            if(e.GetType()==typeof(PException))
            {
                PException pe = (PException)e;
                e = pe.e;
            }
            using (StreamWriter win = new StreamWriter(path, true, Encoding.Default))
            {
                win.WriteLine("------------------------------------------------------------------------------------------------------");
                win.WriteLine(DateTime.Now + " " + e.ToString());
                win.WriteLine("*******相关信息*******");
                win.WriteLine(msg);
                win.WriteLine();
            }
        }

        public static void getError(Exception e)
        {
            using (StreamWriter win = new StreamWriter(path, true, Encoding.Default))
            {
                win.WriteLine("------------------------------------------------------------------------------------------------------");
                win.WriteLine(DateTime.Now + " " + e.ToString());
                win.WriteLine();
            }
        }
    }
}
