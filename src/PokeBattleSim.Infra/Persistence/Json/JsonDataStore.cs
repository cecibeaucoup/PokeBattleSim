using System.Text.Json;

namespace PokeBattleSim.Infra.Persistence.Json;

public sealed class JsonDataStore<T>(string _baseDirectory, string _userDirectory)
    where T : class
{
    private readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public async Task<IReadOnlyList<T>> LoadBaseAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetPath(_baseDirectory, fileName);
        return await LoadAsync(path, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> LoadUserAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetPath(_userDirectory, fileName);
        if (!File.Exists(path))
            return [];

        return await LoadAsync(path, cancellationToken);
    }

    public async Task SaveUserAsync(
        string fileName,
        IEnumerable<T> values,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(values);
        var path = GetPath(_userDirectory, fileName);
        Directory.CreateDirectory(_userDirectory);

        await using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, values.ToList(), _serializerOptions, cancellationToken);
    }

    private async Task<IReadOnlyList<T>> LoadAsync(string path, CancellationToken cancellationToken)
    {
        await using var stream = File.OpenRead(path);
        var values = await JsonSerializer.DeserializeAsync<List<T>>(stream, _serializerOptions, cancellationToken);
        return values ?? throw new InvalidDataException($"JSON file '{path}' contains no collection.");
    }

    private static string GetPath(string directory, string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName) ||
            fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            throw new ArgumentException("A valid JSON file name is required.", nameof(fileName));

        if (!fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("JSON data files must use the .json extension.", nameof(fileName));

        return Path.Combine(directory, fileName);
    }
}
