using Xunit;
using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.InMemory;

namespace PokeBattleSim.Infra.Tests
{
    public class InMemoryPokemonDexRepositoryTests
    {
        private readonly InMemoryPokemonDexRepository _repo = new();

        private static PokemonDex CreatePokemonDex(uint dexNumber = 25u, string name = "Pikachu")
        {
            var baseInfo = new PokemonDexBaseInfo(name, dexNumber, 4, 60, Morphologies.Animal,
                PokemonTypes.Electric, PokemonTypes.None, EggGroups.Field, EggGroups.Fairy);
            var gameInfo = new PokemonDexGameInfo(
                PokeAttribute.GetDefaultStats(),
                PokeSkill.GetDefaultStats(),
                [MobilityTypes.Ground]);
            return new PokemonDex(baseInfo, gameInfo);
        }

        [Fact]
        public async Task AddAsync_And_GetByIdAsync_ReturnsEntity()
        {
            var pokemon = CreatePokemonDex();
            await _repo.AddAsync(pokemon);

            var result = await _repo.GetByIdAsync(25u);

            Assert.NotNull(result);
            Assert.Equal("Pikachu", result.BaseInfo.Name);
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
            await _repo.AddAsync(CreatePokemonDex(1u, "Bulbasaur"));
            await _repo.AddAsync(CreatePokemonDex(25u, "Pikachu"));

            var result = await _repo.GetByNameAsync("Pikachu");

            Assert.NotNull(result);
            Assert.Equal(25u, result.BaseInfo.DexNumber);
        }

        [Fact]
        public async Task GetByNameAsync_NotFound_ReturnsNull()
        {
            var result = await _repo.GetByNameAsync("MissingNo");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            await _repo.AddAsync(CreatePokemonDex(1u, "Bulbasaur"));
            await _repo.AddAsync(CreatePokemonDex(4u, "Charmander"));
            await _repo.AddAsync(CreatePokemonDex(7u, "Squirtle"));

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
            await _repo.AddAsync(CreatePokemonDex(25u, "Pikachu"));

            var updated = CreatePokemonDex(25u, "Raichu");
            await _repo.UpdateAsync(updated);

            var result = await _repo.GetByIdAsync(25u);
            Assert.NotNull(result);
            Assert.Equal("Raichu", result.BaseInfo.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            await _repo.AddAsync(CreatePokemonDex(25u));
            await _repo.DeleteAsync(25u);

            var result = await _repo.GetByIdAsync(25u);
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
            await _repo.AddAsync(CreatePokemonDex(25u, "Pikachu"));
            await _repo.AddAsync(CreatePokemonDex(25u, "Raichu"));

            var result = await _repo.GetByIdAsync(25u);
            Assert.NotNull(result);
            Assert.Equal("Raichu", result.BaseInfo.Name);
        }
    }
}
