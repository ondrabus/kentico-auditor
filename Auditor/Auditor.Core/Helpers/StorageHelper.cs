using Auditor.Core.Models;
using System.Collections.Generic;

namespace Auditor.Core.Helpers
{
    public sealed class StorageHelper : Singleton<StorageHelper>
    {
        private List<AuditData> _data = new List<AuditData>();

        private List<AuditData> Data
        {
            get
            {
                return _data;
            }
        }
        internal void Add(AuditData data)
        {
            lock (_lock)
            {
                Data.Add(data);
            }
        }

        public int Process()
        {
            var table = DataTableHelper.GetDataTable<AuditData>();

            lock (_lock)
            {
                if (Data.Count > 0)
                {
                    for (var i = 0; i < Data.Count; i++)
                    {
                        var row = table.NewRow();
                        DataTableHelper.FillRow(row, Data[i]);
                        table.Rows.Add(row);
                    }

                    Data.Clear();
                }
            }

            if (table.Rows.Count > 0)
            {
                DatabaseHelper.BulkInsert<AuditData>(table);
            }

            return table.Rows.Count;
        }
    }
}