using Auditor.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.Core.Helpers
{
    public static class ListHelper
    {
        public static List<DataField> AddDataFields(this List<DataField> existingList, List<DataField> newList)
        {
            foreach (var dataField in newList)
            {
                var existingField = existingList.FirstOrDefault(f => f.Name == dataField.Name);
                if (existingField != null)
                {
                    existingField.OldValue = dataField.OldValue;
                    existingField.Value = dataField.Value;
                }
                else
                    existingList.Add(dataField);
            }

            return existingList;
        }
    }
}
