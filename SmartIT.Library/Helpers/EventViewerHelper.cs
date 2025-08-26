// <copyright file="EventViewerHelper.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/04/2015</date>
// <summary>Class to read and write event log entries in the Windows Event system.</summary>

namespace SmartIT.Library.Helpers
{
	using System.Diagnostics;
	using System.Threading.Tasks;

	/// <summary>
	/// Class that creates new entries into the Windows' Event system.
	/// </summary>
	public static class EventViewerHelper
	{
		/// <summary>
		/// Creates a new event log entry in the Windows Event system.
		/// </summary>
		/// <param name="source"> Application.</param>
		/// <param name="log"> Log entry.</param>
		/// <param name="message"> Log message.</param>
		/// <param name="type"> Entry type (1: Error, 2: Warning, 4: Info, 8: Success Audit, 16: Failure Audit).</param>
		/// <param name="eventId"> Event ID.</param>
		/// <returns>0 in case of success; -1 instead.</returns>
		public static int SetEventLog(string source, string log, string message, byte type, int eventId)
		{
			string sSource = source;
			string sLog = log;
			string sMessage = message;
			EventLogEntryType nEntryType;

			switch (type)
			{
				case 1: nEntryType = EventLogEntryType.Error; break;
				case 2: nEntryType = EventLogEntryType.Warning; break;
				case 4: nEntryType = EventLogEntryType.Information; break;
				case 8: nEntryType = EventLogEntryType.SuccessAudit; break;
				case 16: nEntryType = EventLogEntryType.FailureAudit; break;
				default: nEntryType = EventLogEntryType.Information; break;
			}

			try
			{
				if (!EventLog.SourceExists(sSource))
				{
					EventLog.CreateEventSource(sSource, sLog);
				}
				EventLog.WriteEntry(sSource, sMessage, nEntryType, eventId);
				return 0;
			}
			catch
			{
				return -1;
			}
		}

		/// <summary>
		/// Asynchrounously creates a new event log entry in the Windows Event system.
		/// </summary>
		/// <param name="source"> Application.</param>
		/// <param name="log"> Log entry.</param>
		/// <param name="message"> Log message.</param>
		/// <param name="type"> Entry type (1: Error, 2: Warning, 4: Info, 8: Success Audit, 16: Failure Audit).</param>
		/// <param name="eventId"> Event ID.</param>
		/// <returns>A Task.</returns>
		public static async Task<int> SetEventLogAsync(string source, string log, string message, byte type, int eventId)
		{
			return await Task.FromResult(SetEventLog(source, log, message, type, eventId));
		}

		/// <summary>
		/// Retrieves an existent event log entry from the Windows Event system.
		/// </summary>
		/// <param name="machineName">The machine name.</param>
		/// <param name="source">The event source (Application, Security, System, etc.)</param>
		/// <param name="message">The event message.</param>
		/// <param name="type">The event type (Error, Warning, Information, etc.). See <see cref="EventLogEntryType"/> for allowed values.</param>
		/// <param name="eventId">The event ID.</param>
		/// <returns>An <see cref="EventLogEntry"/> object.</returns>
		public static EventLogEntry GetEventLog(string machineName, string source, string message, byte type, int eventId)
		{
			EventLog[] localEventLogs = EventLog.GetEventLogs(machineName);
			foreach (EventLog eventLog in localEventLogs)
			{
				if (eventLog.Log == source)
				{
					EventLogEntryCollection eventLogEntryCollection = eventLog.Entries;
					for (int i = 0; i < eventLogEntryCollection.Count; i++)
					{
						EventLogEntry eventLogEntry = eventLogEntryCollection[i];
						if (eventLogEntry.EntryType == (EventLogEntryType)type &&
							eventLogEntry.MachineName.StartsWith(machineName) &&
							(eventLogEntry.Message == message || (eventLogEntry.ReplacementStrings.Length > 0 && eventLogEntry.ReplacementStrings[0] == message)))
						{
							return eventLogEntry;
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Asynchrounously retrieves an existent event log entry from the Windows Event system.
		/// </summary>
		/// <param name="machineName">The machine name.</param>
		/// <param name="source">The event source (Application, Security, System, etc.)</param>
		/// <param name="message">The event message.</param>
		/// <param name="type">The event type (Error, Warning, Information, etc.). See <see cref="EventLogEntryType"/> for allowed values.</param>
		/// <param name="eventId">The event ID.</param>
		/// <returns>An <see cref="EventLogEntry"/> object.</returns>
		public static async Task<EventLogEntry> GetEventLogAsync(string machineName, string source, string message, byte type, int eventId)
		{
			return await Task.Run(() => GetEventLog(machineName, source, message, type, eventId));
		}
	}
}