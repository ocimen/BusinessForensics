using System;

namespace BusinessForensics.Models
{
    public class GameAttemptDto
    {
        public Guid Id { get; set; }

        public int AttemptedNumber { get; set; }

        public Guid GameId { get; set; }
    }
}