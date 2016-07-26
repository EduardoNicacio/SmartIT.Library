// <copyright file="EventViewer.cs" company="SmartIT Technologies LLC.">
// Copyright SmartIT Technologies LLC. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>15/04/2015</date>
// <summary>Classe pra consulta/gravação de mensagens no EventViewer do Windows.</summary>

namespace SmartIT.Library.Utility
{
    using System.Diagnostics;

    /// <summary>
    /// Classe pra consulta/gravação de mensagens no EventViewer do Windows. 
    /// </summary>
    public static class EventViewer
    {
        /// <summary>
        /// Grava uma entrada no Log de Eventos do Windows.
        /// </summary>
        /// <param name="source"> Aplicação de Origem.</param>
        /// <param name="log"> Entrada do Log.</param>
        /// <param name="message"> Mensagem de Log.</param>
        /// <param name="type"> Tipo de entrada (0 Information, 1 Warning, 2 Error).</param>
        /// <param name="eventID"> ID do evento.</param>
        public static void SetEventLog(string source, string log, string message, byte type, int eventID)
        {
            string sSource;
            string sLog;
            string sEvent;
            EventLogEntryType sEntryType;

            sSource = source;
            sLog = log;
            sEvent = message;
            sEntryType = EventLogEntryType.Information;

            switch (type)
            {
                case 0:
                default: sEntryType = EventLogEntryType.Information; break;
                case 1: sEntryType = EventLogEntryType.Warning; break;
                case 2: sEntryType = EventLogEntryType.Error; break;
            }

            if (!EventLog.SourceExists(sSource))
            {
                EventLog.CreateEventSource(sSource, sLog);
            }

            EventLog.WriteEntry(sSource, sEvent, sEntryType, eventID);
        }
    }
}