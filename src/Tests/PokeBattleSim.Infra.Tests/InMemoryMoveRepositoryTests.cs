using Xunit;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.InMemory;

namespace PokeBattleSim.Infra.Tests
{
    public class InMemoryMoveRepositoryTests
    {
        private readonly InMemoryMoveRepository _repo = new();

        private static Move CreateMove(uint id = 1u, string name = "Thunderbolt", PokemonTypes type = PokemonTypes.Electric)
            => new(id, name, type, "A powerful move", new MoveStats(1, 2, 0, 3, 0), true);

        [Fact]
        public async Task AddAsync_And_GetByIdAsync_ReturnsEntity()
        {
            var move = CreateMove();
            await _repo.AddAsync(move);

            var result = await _repo.GetByIdAsync(1u);

            Assert.NotNull(result);
            Assert.Equal("Thunderbolt", result.Name);
            Assert.Equal(PokemonTypes.Electric, result.Type);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound_ReturnsNull()
        {
            var result = await _repo.GetByIdAsync(999u);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsMatchingEntity()
        {
            await _repo.AddAsync(CreateMove(1u, "Tackle", PokemonTypes.Normal));
            await _repo.AddAsync(CreateMove(2u, "Thunderbolt", PokemonTypes.Electric));

            var result = await _repo.GetByNameAsync("Thunderbolt");

            Assert.NotNull(result);
            Assert.Equal(2u, result.MoveId);
        }

        [Fact]
        public async Task GetByNameAsync_NotFound_ReturnsNull()
        {
            var result = await _repo.GetByNameAsync("NonExistent");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            await _repo.AddAsync(CreateMove(1u, "Tackle"));
            await _repo.AddAsync(CreateMove(2u, "Thunderbolt"));
            await _repo.AddAsync(CreateMove(3u, "Flamethrower"));

            var result = await _repo.GetAllAsync();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_Empty_ReturnsEmpty()
        {
            var result = await _repo.GetAllAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ReplacesEntity()
        {
            await _repo.AddAsync(CreateMove(1u, "Tackle"));

            var updated = new Move(1u, "Super Tackle", PokemonTypes.Normal, "Stronger tackle", new MoveStats(), false);
            await _repo.UpdateAsync(updated);

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal("Super Tackle", result.Name);
            Assert.False(result.IsClashable);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            await _repo.AddAsync(CreateMove(1u));
            await _repo.DeleteAsync(1u);

            var result = await _repo.GetByIdAsync(1u);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_NonExistentId_DoesNotThrow()
        {
            await _repo.DeleteAsync(999u);
        }

        [Fact]
        public async Task AddAsync_SameId_OverwritesExisting()
        {
            await _repo.AddAsync(CreateMove(1u, "First"));
            await _repo.AddAsync(CreateMove(1u, "Second"));

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal("Second", result.Name);
        }
    }
}
