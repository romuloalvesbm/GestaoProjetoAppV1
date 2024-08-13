using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.CrossCutting.Exception.Messages
{
    public static class MessagesExtensions
    {
        /// <summary>
        /// Get code message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Code to message</returns>
        public static string GetCodeMessage(string message)
        {
            if (!message.Contains("|")) return message;
            return message.Split('|')[0] ?? "";
        }

        /// <summary>
        /// Get text message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Text to message</returns>
        public static string GetTextMessage(string message)
        {
            if (!message.Contains("|")) return message;
            return message.Split('|')[1] ?? "";
        }
    }
}
