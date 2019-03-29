
using System;

namespace Auditor.Core.Actions
{
    [Flags]
    public enum ActionType
    {
        Insert = 1,
        Update = 2,
        Delete = 4,
        Other = 8
    }
}