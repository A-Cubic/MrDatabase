
namespace Com.ACBC.Framework.Database
{
    public interface IType
    {
        DBType getDBType();

        string getConnString();

        void setConnString(string s);
    }

    public class SingleDB : IType
    {
        private DBType dbt;
        private string str = "";

        public SingleDB(DBType d, string s)
        {
            this.dbt = d;
            this.str = s;
        }

        public DBType getDBType()
        {
            return dbt;
        }

        public string getConnString()
        {
            return str;
        }

        public void setConnString(string s)
        {
            this.str = s;
        }
    }
}
