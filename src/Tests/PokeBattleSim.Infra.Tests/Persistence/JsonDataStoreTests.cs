using PokeBattleSim.Infra.Persistence.Json;
using Xunit;

namespace PokeBattleSim.Infra.Tests.Persistence;

public class JsonDataStoreTests
{
    [Fact]
    public async Task LoadBaseAndSaveUser_KeepDirectoriesSeparate()
    {
        var root = Path.Combine(Path.GetTempPath(), "PokeBattleSimTests", Guid.NewGuid().ToString("N"));
        var baseDirectory = Path.Combine(root, "base");
        var userDirectory = Path.Combine(root, "user");
        Directory.CreateDirectory(baseDirectory);
        await File.WriteAllTextAsync(
            Path.Combine(baseDirectory, "moves.json"),
            """[{"id":1,"name":"Tackle"}]""");

        try
        {
            var store = new JsonDataStore<TestRecord>(baseDirectory, userDirectory);
            var baseRecords = await store.LoadBaseAsync("moves.json");
            await store.SaveUserAsync("moves.json", [new TestRecord(2, "Hydro Pump")]);
            var userRecords = await store.LoadUserAsync("moves.json");

            Assert.Equal(new TestRecord(1, "Tackle"), Assert.Single(baseRecords));
            Assert.Equal(new TestRecord(2, "Hydro Pump"), Assert.Single(userRecords));
        }
        finally
        {
            if (Directory.Exists(root))
                Directory.Delete(root, recursive: true);
        }
    }

    private sealed record TestRecord(uint Id, string Name);
}
