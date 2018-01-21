using System;

namespace Com.ACBC.Framework.Database
{
    public class PException : Exception
    {
        public Exception e;
        private Type exType;
        private object remark;

        public PException(Exception e, Type exType, object remark)
        {
            this.e = e;
            this.exType = exType;
            this.remark = remark;
        }

        public string Remark
        {
            get
            {
                return this.remark.ToString();
            }
        }
    }
}
