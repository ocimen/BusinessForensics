using System;

namespace BusinessForensics.Domain
{
    public class GameAttempt
    {
        public Guid Id { get; set; }

        public int AttemptedNumber { get; set; }

        public Guid GameId { get; set; }
    }
}
