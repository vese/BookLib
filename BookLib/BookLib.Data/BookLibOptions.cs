using System;

namespace BookLib.Data
{
    public static class BookLibOptions
    {
        public const int GetingBookTime = 2; //Срок выдачи книги - 2 месяца

        public static DateTime GetReturnDate(DateTime takingDate)
        {
            return takingDate.AddMonths(GetingBookTime);
        }

        public enum NotificationLevel
        {
            Common = 1,
            Warning = 2,
            Danger = 3
        }

        public static NotificationLevel GetNotificationLevel(int daysLeft)
        {
            if (daysLeft > 5)
            {
                return NotificationLevel.Common;
            }
            else if (daysLeft >= 0)
            {
                return NotificationLevel.Warning;
            }
            else
            {
                return NotificationLevel.Danger;
            }
        }
    }
}
