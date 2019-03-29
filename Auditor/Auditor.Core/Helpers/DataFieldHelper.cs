using Auditor.Core.Models;

namespace Auditor.Core.Helpers
{
    public static class DataFieldHelper
    {
        public static bool ValuesAreIdentical(DataField field)
        {
            if (field.Value == field.OldValue)
                return true;

            return false;
        }
    }
}
