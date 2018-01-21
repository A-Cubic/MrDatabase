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

        // 摘要:
        //     获取一个值，该值指示当前行的嵌套深度。
        //
        // 返回结果:
        //     当前行的嵌套深度。
        public int Depth 
        {
            get
            {
                return this.r.Depth;
            }
        }
        //
        // 摘要:
        //     获取当前行中的列数。
        //
        // 返回结果:
        //     当前行中的列数。
        public int FieldCount 
        {
            get
            {
                return this.r.FieldCount;
            }
        }
        //
        // 摘要:
        //     获取一个值，它指示此 System.Data.Common.DbDataReader 是否包含一个或多个行。
        //
        // 返回结果:
        //     如果 System.Data.Common.DbDataReader 包含一行或多行，则为 true；否则为 false。
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
        // 摘要:
        //     获取一个值，该值指示 System.Data.Common.DbDataReader 是否已关闭。
        //
        // 返回结果:
        //     如果 System.Data.Common.DbDataReader 已关闭，则为 true；否则为 false。
        public bool IsClosed
        {
            get
            {
                return this.r.IsClosed;
            }
        }
        //
        // 摘要:
        //     获取一个值，该值指示 System.Data.Common.DbDataReader 是否包含一行或多行。
        //
        // 返回结果:
        //     如果 System.Data.Common.DbDataReader 包含一行或多行，则为 true；否则为 false。
        public int RecordsAffected
        {
            get
            {
                return this.r.RecordsAffected;
            }
        }
        //
        // 摘要:
        //     获取 System.Data.Common.DbDataReader 中未隐藏的字段的数目。
        //
        // 返回结果:
        //     未隐藏的字段的数目。
        public virtual int VisibleFieldCount
        {
            get
            {
                return this.r.VisibleFieldCount;
            }
        }

        // 摘要:
        //     获取指定列的作为 System.Object 实例的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public object this[int ordinal]
        {
            get
            {
                return this.r[ordinal];
            }
        }
        //
        // 摘要:
        //     获取指定列的作为 System.Object 实例的值。
        //
        // 参数:
        //   name:
        //     列的名称。
        //
        // 返回结果:
        //     指定列的值。
        public object this[string name]
        {
            get
            {
                return this.r[name];
            }
        }

        // 摘要:
        //     关闭 System.Data.Common.DbDataReader 对象。
        private void Close()
        {
            //if(!r.IsClosed)
            //    this.r.Close();
            this.ido.closeDatabase();
        }
        //
        // 摘要:
        //     释放此 System.Data.Common.DbDataReader 使用的资源。
        private void Dispose()
        {
            //this.r.Dispose();
            this.ido.closeDatabase();
        }
        //
        // 摘要:
        //     获取指定列的布尔值形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public bool GetBoolean(int ordinal)
        {
            return this.r.GetBoolean(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的字节形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public byte GetByte(int ordinal)
        {
            return this.r.GetByte(ordinal);
        }
        //
        // 摘要:
        //     从指定列读取一个字节流（从 dataIndex 指示的位置开始），读到缓冲区中（从 bufferIndex 指示的位置开始）。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        //   buffer:
        //     作为数据复制目标的缓冲区。
        //
        //   dataOffset:
        //     行中的索引，从其开始读取操作。
        //
        //   bufferOffset:
        //     具有作为数据复制目标的缓冲区的索引。
        //
        //   length:
        //     最多读取的字符数。
        //
        // 返回结果:
        //     读取的实际字节数。
        public long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return this.r.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }
        //
        // 摘要:
        //     获取指定列的单个字符串形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public char GetChar(int ordinal)
        {
            return this.r.GetChar(ordinal);
        }
        //
        // 摘要:
        //     从指定列读取一个字符流，从 dataIndex 指示的位置开始，读到缓冲区中，从 bufferIndex 指示的位置开始。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        //   buffer:
        //     作为数据复制目标的缓冲区。
        //
        //   dataOffset:
        //     行中的索引，从其开始读取操作。
        //
        //   bufferOffset:
        //     具有作为数据复制目标的缓冲区的索引。
        //
        //   length:
        //     最多读取的字符数。
        //
        // 返回结果:
        //     读取的实际字符数。
        public long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return this.r.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }
        //
        // 摘要:
        //     获取指定列的数据类型的名称。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     表示数据类型的名称的字符串。
        public string GetDataTypeName(int ordinal)
        {
            return this.r.GetDataTypeName(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的 System.DateTime 对象形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public DateTime GetDateTime(int ordinal)
        {
            return this.r.GetDateTime(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的 System.Decimal 对象形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public decimal GetDecimal(int ordinal)
        {
            return this.r.GetDecimal(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的双精度浮点数形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public double GetDouble(int ordinal)
        {
            return this.r.GetDouble(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的数据类型。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的数据类型。
        public Type GetFieldType(int ordinal)
        {
            return this.r.GetFieldType(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的单精度浮点数形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public float GetFloat(int ordinal)
        {
            return this.r.GetFloat(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的全局唯一标识符 (GUID) 形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public Guid GetGuid(int ordinal)
        {
            return this.r.GetGuid(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的 16 位有符号整数形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public short GetInt16(int ordinal)
        {
            return this.r.GetInt16(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的 32 位有符号整数形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public int GetInt32(int ordinal)
        {
            return this.r.GetInt32(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的 64 位有符号整数形式的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public long GetInt64(int ordinal)
        {
            return this.r.GetInt64(ordinal);
        }
        //
        // 摘要:
        //     给定了从零开始的列序号时，获取列的名称。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的名称。
        public string GetName(int ordinal)
        {
            return this.r.GetName(ordinal);
        }
        //
        // 摘要:
        //     给定列名称时，获取列序号。
        //
        // 参数:
        //   name:
        //     列的名称。
        //
        // 返回结果:
        //     从零开始的列序号。
        public int GetOrdinal(string name)
        {
            return this.r.GetOrdinal(name);
        }
        //
        // 摘要:
        //     返回一个 System.Data.DataTable，它描述 System.Data.Common.DbDataReader 的列元数据。
        //
        // 返回结果:
        //     一个描述列元数据的 System.Data.DataTable。
        public DataTable GetSchemaTable()
        {
            return this.r.GetSchemaTable();
        }
        //
        // 摘要:
        //     获取指定列的作为 System.String 的实例的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public string GetString(int ordinal)
        {
            return this.r.GetString(ordinal);
        }
        //
        // 摘要:
        //     获取指定列的作为 System.Object 实例的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     指定列的值。
        public object GetValue(int ordinal)
        {
            return this.r.GetValue(ordinal);
        }
        //
        // 摘要:
        //     获取当前行的集合中的所有属性列。
        //
        // 参数:
        //   values:
        //     要将属性列复制到的 System.Object 数组。
        //
        // 返回结果:
        //     数组中 System.Object 的实例的数目。
        public int GetValues(object[] values)
        {
            return this.r.GetValues(values);
        }
        //
        // 摘要:
        //     获取一个值，该值指示列中是否包含不存在的或已丢失的值。
        //
        // 参数:
        //   ordinal:
        //     从零开始的列序号。
        //
        // 返回结果:
        //     如果指定的列与 System.DBNull 等效，则为 true；否则为 false。
        public bool IsDBNull(int ordinal)
        {
            return this.r.IsDBNull(ordinal);
        }
        //
        // 摘要:
        //     读取批处理语句的结果时，使读取器前进到下一个结果。
        //
        // 返回结果:
        //     如果存在多个结果集，则为 true；否则为 false。
        public bool NextResult()
        {
            return this.r.NextResult();
        }
        //
        // 摘要:
        //     将读取器前进到结果集中的下一个记录。
        //
        // 返回结果:
        //     如果存在多个行，则为 true；否则为 false。
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
