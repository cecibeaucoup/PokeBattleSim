using System.Text.Json.Serialization;

namespace PokeBattleSim.Data.Enums
{
    /// <summary>
    /// A mechanic tag followed by the resolution tags that parameterize it.
    /// </summary>
    public sealed record TagBinding
    {
        [JsonConstructor]
        public TagBinding(MechanicTag mechanic, IReadOnlyList<ResolutionTag>? resolutions)
        {
            Mechanic = mechanic;
            Resolutions = [.. resolutions ?? []];
        }

        public TagBinding(MechanicTag mechanic, params ResolutionTag[] resolutions)
            : this(mechanic, (IReadOnlyList<ResolutionTag>)resolutions)
        {
        }

        public MechanicTag Mechanic { get; }

        public IReadOnlyList<ResolutionTag> Resolutions { get; }

        public void Deconstruct(out MechanicTag mechanic, out IReadOnlyList<ResolutionTag> resolutions)
        {
            mechanic = Mechanic;
            resolutions = Resolutions;
        }

        public bool Equals(TagBinding? other) =>
            other is not null && Mechanic == other.Mechanic && Resolutions.SequenceEqual(other.Resolutions);

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Mechanic);
            foreach (var resolution in Resolutions)
                hash.Add(resolution);
            return hash.ToHashCode();
        }

        public override string ToString() =>
            Resolutions.Count == 0 ? Mechanic.ToString() : $"{Mechanic}({string.Join(", ", Resolutions)})";
    }
}
