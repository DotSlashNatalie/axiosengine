using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Axios.Engine.Data
{
#if USECUSTOMDATATABLE
    public enum CollectionChangeAction
    {
        Add,
        Remove,
        Refresh
    }

    

    public delegate void CollectionChangeEventHandler(
	    Object sender,
	    CollectionChangeEventArgs e
    );

    class DataTable
    {
        private DataColumnCollection _columnCollection = new DataColumnCollection();
        private DataRowCollection _rowCollection = new DataRowCollection();
        public DataTable()
        {

        }

        public DataColumnCollection Columns
        {
            get
            {
                return this._columnCollection;
            }
            private set
            {
                this._columnCollection = value;
            }
        }

        public int Count
        {
            get
            {
                return _columnCollection.Count();
            }
        }

        public DataRowCollection Rows
        {
            get
            {
                return this._rowCollection;
            }
            set
            {
                this._rowCollection = value;
            }
        }

        public DataRow NewRow()
        {
            DataRow r = new DataRow();
            r.Table = this;
            return r;
        }

    }

    class DataRowCollection
    {
        private List<DataRow> _rows = new List<DataRow>();
        
        public DataRowCollection()
        {

        }

        /*public AxiosDataRow Add(params Object[] values)
        {
            AxiosDataRow row = new AxiosDataRow();
            //row.Table 
            foreach (object obj in values)
            {

            }
        }*/

        public void Add(DataRow row)
        {
            _rows.Add(row);
        }
    }

    class DataRow
    {
        private DataTable _table;
        private Dictionary<string, object> _row = new Dictionary<string, object>();

        public DataRow()
        {

        }

        public Object this[string columnName]
        {
            get
            {
                if (_row.ContainsKey(columnName))
                    return _row[columnName];
                else
                    throw new ArgumentException("The column specified by " + columnName + " cannot be found.");
            }
            set
            {
                if (_row.ContainsKey(columnName))
                    _row[columnName] = value;
                else
                    throw new ArgumentException("The column specified by " + columnName + " cannot be found.");
            }
        }

        //Does this really reference a list of ints rather than a list of strings?
        public Object this[int columnIndex]
        {
            get
            {
                return _row.ElementAt(columnIndex).Value;
            }

            set
            {
                _row[_row.ElementAt(columnIndex).Key] = value;
            }
        }

        public DataTable Table
        {
            get
            {
                return _table;
            }
            set
            {
                _row.Clear();
                foreach (DataColumn col in _table.Columns)
                    _row[col.ColumnName] = "";
                this._table = value;
            }
        }
    }

    class DataColumn
    {
        private string _columnName;
        private Type _datatype;
        public DataColumn(string columnName, Type dataType)
        {
            _columnName = columnName;
            _datatype = dataType;
        }

        public string ColumnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                _columnName = value;
            }
        }
    }

    class DataColumnCollection : IEnumerable<DataColumn>
    {
        List<DataColumn> _datacolumns = new List<DataColumn>();

        public event CollectionChangeEventHandler CollectionChanged;

        public DataColumnCollection()
        {

        }

        public DataColumn Add(string columnName)
        {
            DataColumn col = new DataColumn(columnName, typeof(string));
            _datacolumns.Add(col);
            CollectionChanged(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, col));
            return col;
        }

        public DataColumn Add(string columnName, Type dataType)
        {
            DataColumn col = new DataColumn(columnName, dataType);
            _datacolumns.Add(col);
            return col;
        }

        public void Add(DataColumn column)
        {
            _datacolumns.Add(column);
        }

        public void Remove(DataColumn column)
        {
            _datacolumns.Remove(column);
        }

        public void Remove(string column)
        {
            for (int i = 0; i < _datacolumns.Count; i++)
            {
                if (_datacolumns[i].ColumnName == column)
                {
                    _datacolumns.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveAt(int index)
        {
            _datacolumns.RemoveAt(index);
        }

        public int Count()
        {
            return _datacolumns.Count();
        }



        public IEnumerator<DataColumn> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _datacolumns.GetEnumerator();
        }
    }
#endif
}
