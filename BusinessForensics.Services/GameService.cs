using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessForensics.Data;
using BusinessForensics.Domain;
using BusinessForensics.Models;

namespace BusinessForensics.Services
{
    public class GameService : IGameService
    {
        private readonly BusinessForensicsContext _context;
        private readonly IMapper _mapper;

        public GameService(BusinessForensicsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GameDto> CreateNewGame()
        {
            //TODO: these values can be get from database or configuration

            var game = new Game
            {
                Id = Guid.NewGuid(),
                RangeMax = 100,
                RangeMin = 0,
                SelectedNumber = 25,
                GameAttempts = new List<GameAttempt>()
            };

            _context.Game.Add(game);
            await _context.SaveChangesAsync();
            return _mapper.Map<GameDto>(game);
        }

        public async Task<bool> AddAttempt(Guid gameId, int attemptNumber)
        {
            var game = GetGame(gameId);
            
            var gameAttempt = new GameAttempt
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                AttemptedNumber = attemptNumber
            };
            game.AddGameAttempt(gameAttempt);
            
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return game.SelectedNumber == attemptNumber;
            }

            return false;
        }

        public List<GameAttemptDto> GetAttemptByGame(Guid id)
        {
            var game = GetGame(id);
            return _mapper.Map<List<GameAttemptDto>>(game?.GameAttempts);
        }

        private Game GetGame(Guid id)
        {
            var game = _context.Game.FirstOrDefault(x => x.Id == id);
            if (game != null)
            {
                return game;
            }

            return null;
        }
    }
}
