using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessForensics.Models;

namespace BusinessForensics.Services
{
    public interface IGameService
    {
        Task<GameDto> CreateNewGame();
        Task<bool> AddAttempt(Guid gameId, int attemptNumber);
        List<GameAttemptDto> GetAttemptByGame(Guid id);
    }
}