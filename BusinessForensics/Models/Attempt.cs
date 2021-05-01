using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessForensics.Models
{
    public class Attempt
    {
        public int AttemptNumber { get; set; }

        public Guid GameId { get; set; }

        public List<GameAttemptDto> GameAttempt { get; set; }

        public string Message { get; set; }

        public int AttemptCountLeft { get; set; }
    }
}
