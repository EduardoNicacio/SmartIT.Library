// <copyright file="EventViewer.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/04/2015</date>
// <summary>Class that writes new entries into the Windows' Event system.</summary>

namespace SmartIT.Library.Utility
{
    using System.Diagnostics;

    /// <summary>
    /// Class that writes new entries into the Windows' Event system.
    /// </summary>
    public static class EventViewer
    {
        /// <summary>
        /// Creates a new entry into the Windows Event system.
        /// </summary>
        /// <param name="source"> Application.</param>
        /// <param name="log"> Log entry.</param>
        /// <param name="message"> Log message.</param>
        /// <param name="type"> Entry type (0 Information, 1 Warning, 2 Error).</param>
        /// <param name="eventID"> Event ID.</param>
        public static void SetEventLog(string source, string log, string message, byte type, int eventID)
        {
            string sSource = source;
            string sLog = log;
            string sEvent = message;
            EventLogEntryType sEntryType;

            switch (type)
            {
                case 1: sEntryType = EventLogEntryType.Error; break;
                case 2: sEntryType = EventLogEntryType.Warning; break;
                case 4: sEntryType = EventLogEntryType.Information; break;
                case 8: sEntryType = EventLogEntryType.SuccessAudit; break;
                case 16: sEntryType = EventLogEntryType.FailureAudit; break;
                default: sEntryType = EventLogEntryType.Information; break;
            }

            if (!EventLog.SourceExists(sSource))
            {
                EventLog.CreateEventSource(sSource, sLog);
            }

            try
            {
                EventLog.WriteEntry(sSource, sEvent, sEntryType, eventID);
            }
            catch (System.ArgumentException) { }
            catch (System.InvalidOperationException) { }
            catch (System.ComponentModel.Win32Exception) { }
        }
    }
}