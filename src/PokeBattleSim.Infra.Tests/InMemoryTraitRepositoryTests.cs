using Xunit;
using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.InMemory;

namespace PokeBattleSim.Infra.Tests
{
    public class InMemoryTraitRepositoryTests
    {
        private readonly InMemoryTraitRepository _repo = new();

        private static Trait CreateTrait(uint id = 1u, string name = "Brave", string desc = "Increases attack")
            => new(id, name, desc);

        [Fact]
        public async Task AddAsync_And_GetByIdAsync_ReturnsEntity()
        {
            var trait = CreateTrait();
            await _repo.AddAsync(trait);

            var result = await _repo.GetByIdAsync(1u);

            Assert.NotNull(result);
            Assert.Equal("Brave", result.Name);
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
            await _repo.AddAsync(CreateTrait(1u, "Brave"));
            await _repo.AddAsync(CreateTrait(2u, "Calm"));

            var result = await _repo.GetByNameAsync("Calm");

            Assert.NotNull(result);
            Assert.Equal(2u, result.TraitId);
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
            await _repo.AddAsync(CreateTrait(1u, "Brave"));
            await _repo.AddAsync(CreateTrait(2u, "Calm"));
            await _repo.AddAsync(CreateTrait(3u, "Bold"));

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
            await _repo.AddAsync(CreateTrait(1u, "Brave", "Old"));

            var updated = CreateTrait(1u, "Brave", "New description");
            await _repo.UpdateAsync(updated);

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal("New description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            await _repo.AddAsync(CreateTrait(1u));
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
            await _repo.AddAsync(CreateTrait(1u, "First"));
            await _repo.AddAsync(CreateTrait(1u, "Second"));

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal("Second", result.Name);
        }
    }
}
