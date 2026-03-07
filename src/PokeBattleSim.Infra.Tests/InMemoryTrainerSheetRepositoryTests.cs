using Xunit;
using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.InMemory;

namespace PokeBattleSim.Infra.Tests
{
    public class InMemoryTrainerSheetRepositoryTests
    {
        private readonly InMemoryTrainerSheetRepository _repo = new();

        private static TrainerSheet CreateTrainer(uint id = 1u, string name = "Ash", uint age = 10)
            => new(id, name, age, Regions.Kanto);

        [Fact]
        public async Task AddAsync_And_GetByIdAsync_ReturnsEntity()
        {
            var trainer = CreateTrainer();
            await _repo.AddAsync(trainer);

            var result = await _repo.GetByIdAsync(1u);

            Assert.NotNull(result);
            Assert.Equal("Ash", result.Name);
            Assert.Equal(Regions.Kanto, result.CurrentRegion);
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
            await _repo.AddAsync(CreateTrainer(1u, "Ash"));
            await _repo.AddAsync(CreateTrainer(2u, "Misty"));

            var result = await _repo.GetByNameAsync("Misty");

            Assert.NotNull(result);
            Assert.Equal(2u, result.TrainerId);
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
            await _repo.AddAsync(CreateTrainer(1u, "Ash"));
            await _repo.AddAsync(CreateTrainer(2u, "Misty"));
            await _repo.AddAsync(CreateTrainer(3u, "Brock"));

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
            await _repo.AddAsync(CreateTrainer(1u, "Ash", 10));

            var updated = new TrainerSheet(1u, "Ash", 11, Regions.Johto);
            await _repo.UpdateAsync(updated);

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal(11u, result.Age);
            Assert.Equal(Regions.Johto, result.CurrentRegion);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            await _repo.AddAsync(CreateTrainer(1u));
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
            await _repo.AddAsync(CreateTrainer(1u, "Ash"));
            await _repo.AddAsync(CreateTrainer(1u, "Gary"));

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal("Gary", result.Name);
        }
    }
}
