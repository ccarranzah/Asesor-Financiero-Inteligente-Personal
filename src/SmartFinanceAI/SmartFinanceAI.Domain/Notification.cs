using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinanceAI.Domain
{
    public class Notification
    {
        public Notification(AccountHolder client, string message, NotificationType type)
        {
            Timestamp = DateTime.UtcNow;
            Client = client;
            Message = message;
            Type = type;
        }

        public AccountHolder Client { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
