using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessForensics.Data;
using BusinessForensics.Services.Mappings;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BusinessForensics.Services.UnitTests
{
    public class GameServiceTests
    {
        private readonly IGameService _gameService;
        private readonly IMapper mapper;
        private readonly BusinessForensicsContext _context;
        

        public GameServiceTests()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            mapper = mapConfig.CreateMapper();

            var options = new DbContextOptionsBuilder<BusinessForensicsContext>()
                .UseInMemoryDatabase(databaseName: "BusinessForensicsDb")
                .Options;

            _context = new BusinessForensicsContext(options);
            _gameService = new GameService(_context, mapper);
        }

        [Fact]
        public async Task Should_Add_Game()
        {
            var game = await _gameService.CreateNewGame();
            Assert.NotNull(game);

            Assert.NotNull(game);
            Assert.Equal(25, game.SelectedNumber);
            Assert.Equal(0, game.RangeMin);
            Assert.Equal(100, game.RangeMax);
        }

        [Fact]
        public async Task Should_Add_Attempt()
        {
            var game = await _gameService.CreateNewGame();
            await _gameService.AddAttempt(game.Id, 1);

            var updatedGame = _context.Game.FirstOrDefault(f => f.Id == game.Id);
            Assert.Equal(1, updatedGame.GameAttempts.Count);
        }

        [Fact]
        public async Task Should_Know_The_Correct_Number()
        {
            var game = await _gameService.CreateNewGame();
            var result = await _gameService.AddAttempt(game.Id, 25);

            var updatedGame = _context.Game.FirstOrDefault(f => f.Id == game.Id);
            Assert.Equal(1, updatedGame.GameAttempts.Count);
            Assert.True(result);
        }

        [Fact]
        public async Task Should_Get_Game_Attempts()
        {
            var attemptNumber = 50;
            var game = await _gameService.CreateNewGame();
            await _gameService.AddAttempt(game.Id, attemptNumber);

            var result = _gameService.GetAttemptByGame(game.Id);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(result.First().AttemptedNumber,attemptNumber );
        }
    }
}
