using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDConstant
{
    public static class clsLogger
    {
        public const string SourceName = "DVLD";

        public const string LogName = "Application";

        /// <summary>
        /// Logs a message to the Windows Event Viewer.
        /// </summary>
        /// <param name="Message">The message to be logged.</param>
        /// <param name="EntryType">The type of entry. Default is Error.</param>
        public static void LogAtEventLog(string Message, EventLogEntryType EntryType = EventLogEntryType.Error)
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }

                EventLog.WriteEntry(SourceName, Message, EntryType);
            }
            catch
            {

            }
        }
    }
}
