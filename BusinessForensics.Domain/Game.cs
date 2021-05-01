using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessForensics.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public int SelectedNumber { get; set; }
        public int RangeMin { get; set; }
        public int RangeMax { get; set; }
        public ICollection<GameAttempt> GameAttempts { get; set; }
        internal int CurrentAttemptCount => GameAttempts.Count;

        public Game()
        { }

        public Game(Guid id, int selectedNumber, int rangeMin, int rangeMax)
        {
            Id = id;
            SelectedNumber = selectedNumber;
            RangeMin = rangeMin;
            RangeMax = rangeMax;
        }

        public void AddGameAttempt(GameAttempt gameAttempt)
        {
            if (CurrentAttemptCount < 3)
            {
                GameAttempts.Add(gameAttempt);
            }
        }
    }
}
