using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditor.Core.Actions.EventLog
{
    internal class EventLogSettings
    {
        public static readonly List<string> AllowedActions = new List<string>
        {
            "ENDAPP"
        };
    }
}
