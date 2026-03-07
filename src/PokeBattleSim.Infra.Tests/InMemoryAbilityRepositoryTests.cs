using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.InMemory;

namespace PokeBattleSim.Infra.Tests
{
    public class InMemoryAbilityRepositoryTests
    {
        private readonly InMemoryAbilityRepository _repo = new();

        private static Ability CreateAbility(uint id = 1u, string name = "Levitate", string desc = "Prevents ground damage")
            => new(id, name, desc);

        [Fact]
        public async Task AddAsync_And_GetByIdAsync_ReturnsEntity()
        {
            var ability = CreateAbility();
            await _repo.AddAsync(ability);

            var result = await _repo.GetByIdAsync(1u);

            Assert.NotNull(result);
            Assert.Equal("Levitate", result.Name);
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
            await _repo.AddAsync(CreateAbility(1u, "Static", "May paralyze"));
            await _repo.AddAsync(CreateAbility(2u, "Levitate", "Float"));

            var result = await _repo.GetByNameAsync("Levitate");

            Assert.NotNull(result);
            Assert.Equal(2u, result.AbilityId);
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
            await _repo.AddAsync(CreateAbility(1u, "Static"));
            await _repo.AddAsync(CreateAbility(2u, "Levitate"));
            await _repo.AddAsync(CreateAbility(3u, "Intimidate"));

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
            var ability = CreateAbility(1u, "Static", "Old description");
            await _repo.AddAsync(ability);

            var updated = CreateAbility(1u, "Static", "New description");
            await _repo.UpdateAsync(updated);

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal("New description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            await _repo.AddAsync(CreateAbility(1u));
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
            await _repo.AddAsync(CreateAbility(1u, "First"));
            await _repo.AddAsync(CreateAbility(1u, "Second"));

            var result = await _repo.GetByIdAsync(1u);
            Assert.NotNull(result);
            Assert.Equal("Second", result.Name);
        }
    }
}
