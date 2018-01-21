using System;
using System.Data.Common;
using System.Data;

namespace Com.ACBC.Framework.Database
{
    public class PReader
    {
        private DbDataReader r;
        private IDatabaseOperation ido;

        public PReader(DbDataReader s, IDatabaseOperation i)
        {
            this.r = s;
            this.ido = i;
        }

        // ժҪ:
        //     ��ȡһ��ֵ����ֵָʾ��ǰ�е�Ƕ����ȡ�
        //
        // ���ؽ��:
        //     ��ǰ�е�Ƕ����ȡ�
        public int Depth 
        {
            get
            {
                return this.r.Depth;
            }
        }
        //
        // ժҪ:
        //     ��ȡ��ǰ���е�������
        //
        // ���ؽ��:
        //     ��ǰ���е�������
        public int FieldCount 
        {
            get
            {
                return this.r.FieldCount;
            }
        }
        //
        // ժҪ:
        //     ��ȡһ��ֵ����ָʾ�� System.Data.Common.DbDataReader �Ƿ����һ�������С�
        //
        // ���ؽ��:
        //     ��� System.Data.Common.DbDataReader ����һ�л���У���Ϊ true������Ϊ false��
        public bool HasRows
        {
            get
            {
                bool b = this.r.HasRows;
                if(b)
                {
                    return true;
                }
                else
                {
                    this.Close();
                    return false;
                }
            }
        }
        //
        // ժҪ:
        //     ��ȡһ��ֵ����ֵָʾ System.Data.Common.DbDataReader �Ƿ��ѹرա�
        //
        // ���ؽ��:
        //     ��� System.Data.Common.DbDataReader �ѹرգ���Ϊ true������Ϊ false��
        public bool IsClosed
        {
            get
            {
                return this.r.IsClosed;
            }
        }
        //
        // ժҪ:
        //     ��ȡһ��ֵ����ֵָʾ System.Data.Common.DbDataReader �Ƿ����һ�л���С�
        //
        // ���ؽ��:
        //     ��� System.Data.Common.DbDataReader ����һ�л���У���Ϊ true������Ϊ false��
        public int RecordsAffected
        {
            get
            {
                return this.r.RecordsAffected;
            }
        }
        //
        // ժҪ:
        //     ��ȡ System.Data.Common.DbDataReader ��δ���ص��ֶε���Ŀ��
        //
        // ���ؽ��:
        //     δ���ص��ֶε���Ŀ��
        public virtual int VisibleFieldCount
        {
            get
            {
                return this.r.VisibleFieldCount;
            }
        }

        // ժҪ:
        //     ��ȡָ���е���Ϊ System.Object ʵ����ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public object this[int ordinal]
        {
            get
            {
                return this.r[ordinal];
            }
        }
        //
        // ժҪ:
        //     ��ȡָ���е���Ϊ System.Object ʵ����ֵ��
        //
        // ����:
        //   name:
        //     �е����ơ�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public object this[string name]
        {
            get
            {
                return this.r[name];
            }
        }

        // ժҪ:
        //     �ر� System.Data.Common.DbDataReader ����
        private void Close()
        {
            //if(!r.IsClosed)
            //    this.r.Close();
            this.ido.closeDatabase();
        }
        //
        // ժҪ:
        //     �ͷŴ� System.Data.Common.DbDataReader ʹ�õ���Դ��
        private void Dispose()
        {
            //this.r.Dispose();
            this.ido.closeDatabase();
        }
        //
        // ժҪ:
        //     ��ȡָ���еĲ���ֵ��ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public bool GetBoolean(int ordinal)
        {
            return this.r.GetBoolean(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е��ֽ���ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public byte GetByte(int ordinal)
        {
            return this.r.GetByte(ordinal);
        }
        //
        // ժҪ:
        //     ��ָ���ж�ȡһ���ֽ������� dataIndex ָʾ��λ�ÿ�ʼ���������������У��� bufferIndex ָʾ��λ�ÿ�ʼ����
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        //   buffer:
        //     ��Ϊ���ݸ���Ŀ��Ļ�������
        //
        //   dataOffset:
        //     ���е����������俪ʼ��ȡ������
        //
        //   bufferOffset:
        //     ������Ϊ���ݸ���Ŀ��Ļ�������������
        //
        //   length:
        //     ����ȡ���ַ�����
        //
        // ���ؽ��:
        //     ��ȡ��ʵ���ֽ�����
        public long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return this.r.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }
        //
        // ժҪ:
        //     ��ȡָ���еĵ����ַ�����ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public char GetChar(int ordinal)
        {
            return this.r.GetChar(ordinal);
        }
        //
        // ժҪ:
        //     ��ָ���ж�ȡһ���ַ������� dataIndex ָʾ��λ�ÿ�ʼ�������������У��� bufferIndex ָʾ��λ�ÿ�ʼ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        //   buffer:
        //     ��Ϊ���ݸ���Ŀ��Ļ�������
        //
        //   dataOffset:
        //     ���е����������俪ʼ��ȡ������
        //
        //   bufferOffset:
        //     ������Ϊ���ݸ���Ŀ��Ļ�������������
        //
        //   length:
        //     ����ȡ���ַ�����
        //
        // ���ؽ��:
        //     ��ȡ��ʵ���ַ�����
        public long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return this.r.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }
        //
        // ժҪ:
        //     ��ȡָ���е��������͵����ơ�
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ��ʾ�������͵����Ƶ��ַ�����
        public string GetDataTypeName(int ordinal)
        {
            return this.r.GetDataTypeName(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е� System.DateTime ������ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public DateTime GetDateTime(int ordinal)
        {
            return this.r.GetDateTime(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е� System.Decimal ������ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public decimal GetDecimal(int ordinal)
        {
            return this.r.GetDecimal(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е�˫���ȸ�������ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public double GetDouble(int ordinal)
        {
            return this.r.GetDouble(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е��������͡�
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е��������͡�
        public Type GetFieldType(int ordinal)
        {
            return this.r.GetFieldType(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���еĵ����ȸ�������ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public float GetFloat(int ordinal)
        {
            return this.r.GetFloat(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е�ȫ��Ψһ��ʶ�� (GUID) ��ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public Guid GetGuid(int ordinal)
        {
            return this.r.GetGuid(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е� 16 λ�з���������ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public short GetInt16(int ordinal)
        {
            return this.r.GetInt16(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е� 32 λ�з���������ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public int GetInt32(int ordinal)
        {
            return this.r.GetInt32(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е� 64 λ�з���������ʽ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public long GetInt64(int ordinal)
        {
            return this.r.GetInt64(ordinal);
        }
        //
        // ժҪ:
        //     �����˴��㿪ʼ�������ʱ����ȡ�е����ơ�
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е����ơ�
        public string GetName(int ordinal)
        {
            return this.r.GetName(ordinal);
        }
        //
        // ժҪ:
        //     ����������ʱ����ȡ����š�
        //
        // ����:
        //   name:
        //     �е����ơ�
        //
        // ���ؽ��:
        //     ���㿪ʼ������š�
        public int GetOrdinal(string name)
        {
            return this.r.GetOrdinal(name);
        }
        //
        // ժҪ:
        //     ����һ�� System.Data.DataTable�������� System.Data.Common.DbDataReader ����Ԫ���ݡ�
        //
        // ���ؽ��:
        //     һ��������Ԫ���ݵ� System.Data.DataTable��
        public DataTable GetSchemaTable()
        {
            return this.r.GetSchemaTable();
        }
        //
        // ժҪ:
        //     ��ȡָ���е���Ϊ System.String ��ʵ����ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public string GetString(int ordinal)
        {
            return this.r.GetString(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡָ���е���Ϊ System.Object ʵ����ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ָ���е�ֵ��
        public object GetValue(int ordinal)
        {
            return this.r.GetValue(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡ��ǰ�еļ����е����������С�
        //
        // ����:
        //   values:
        //     Ҫ�������и��Ƶ��� System.Object ���顣
        //
        // ���ؽ��:
        //     ������ System.Object ��ʵ������Ŀ��
        public int GetValues(object[] values)
        {
            return this.r.GetValues(values);
        }
        //
        // ժҪ:
        //     ��ȡһ��ֵ����ֵָʾ�����Ƿ���������ڵĻ��Ѷ�ʧ��ֵ��
        //
        // ����:
        //   ordinal:
        //     ���㿪ʼ������š�
        //
        // ���ؽ��:
        //     ���ָ�������� System.DBNull ��Ч����Ϊ true������Ϊ false��
        public bool IsDBNull(int ordinal)
        {
            return this.r.IsDBNull(ordinal);
        }
        //
        // ժҪ:
        //     ��ȡ���������Ľ��ʱ��ʹ��ȡ��ǰ������һ�������
        //
        // ���ؽ��:
        //     ������ڶ�����������Ϊ true������Ϊ false��
        public bool NextResult()
        {
            return this.r.NextResult();
        }
        //
        // ժҪ:
        //     ����ȡ��ǰ����������е���һ����¼��
        //
        // ���ؽ��:
        //     ������ڶ���У���Ϊ true������Ϊ false��
        public bool Read()
        {
            bool b = this.r.Read();
            if(b)
            {
                return true;
            }
            else
            {
                this.Close();
                return false;
            }
            
        }
    }
}
