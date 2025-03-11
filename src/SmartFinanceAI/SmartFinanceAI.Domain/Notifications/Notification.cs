using SmartFinanceAI.Domain.Entities;
using SmartFinanceAI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinanceAI.Domain.Notifications
{
    public class Notification
    {
        public Notification(string clientId, string message, NotificationType type)
        {
            Timestamp = DateTime.UtcNow;
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
        }

        public required string ClientId { get; set; } // Identifier of the user receiving the notification
        public required string Message { get; set; } // Content of the notification
        public NotificationType Type { get; set; } // Type of notification
        public DateTime Timestamp { get; set; } // Timestamp of when the notification was created (UTC)

    }
}
