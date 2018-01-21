using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace Com.ACBC.Framework.Database
{

    public class ProcWorker
    {
        private string p;
        private List<Para> lp = new List<Para>();
        private bool isExecute = false;
        private bool isSuccess = false;
        private ArrayList pOut = new ArrayList();
        private DataSet ds;
        private PException mp = null;

        public ProcWorker(string p_name)
        {
            this.p = p_name;
        }

        public void setReturn(ArrayList pOut, DataSet ds)
        {
            this.pOut = pOut;
            this.ds = ds;
            this.isExecute = true;
            this.isSuccess = true;
        }

        public PException Mp
        {
            get
            {
                return this.mp;
            }
            set
            {
                this.mp = value;
            }
        }

        public string ProcName
        {
            get
            {
                return this.p;
            }
        }

        public bool IsExecute
        {
            get
            {
                return this.isExecute;
            }
        }

        public bool IsSuccess
        {
            get
            {
                return this.isSuccess;
            }
            set
            {
                this.isSuccess = value;
            }
        }

        public List<Para> LP
        {
            get
            {
                return this.lp;
            }
        }

        public void addP(string pname, DBDataType dtp, object value, int size)
        {
            this.lp.Add(new Para(pname, dtp, value, ProcType.IN, size));
        }

        public void addP(string pname, DBDataType dtp, object value, int size, ProcType pt)
        {
            this.lp.Add(new Para(pname, dtp, value, pt, size));
        }

        public ArrayList getOut()
        {
            if (this.IsExecute)
            {
                return this.pOut;
            }
            else
            {
                return null;
            }
        }

        public DataSet getReturn()
        {
            if (this.IsExecute)
            {
                return this.ds;
            }
            else
            {
                return null;
            }
        }
    }

    public class Para
    {
        private DBDataType dtp;
        private object value;
        private ProcType pt;
        private string pname;
        private int size;

        public Para(string pname, DBDataType dtp, object value, ProcType pt, int size)
        {
            this.dtp = dtp;
            this.value = value;
            this.pt = pt;
            this.pname = pname;
            this.size = size;
        }

        public string Pname
        {
            get
            {
                return this.pname;
            }
        }

        public DBDataType Dtp
        {
            get
            {
                return this.dtp;
            }
        }

        public object Value
        {
            get
            {
                return this.value;
            }
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        public ProcType Pt
        {
            get
            {
                return this.pt;
            }
        }
    }


}
