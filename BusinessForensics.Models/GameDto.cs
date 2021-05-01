using System;
using System.Collections.Generic;

namespace BusinessForensics.Models
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public int SelectedNumber { get; set; }
        public int RangeMin { get; set; }
        public int RangeMax { get; set; }
        public List<GameAttemptDto> GameAttempts { get; set; }
    }
}
