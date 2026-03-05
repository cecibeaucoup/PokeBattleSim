namespace PokeBattleSim.Data.Entities.Trainer
{
    public class Trait(uint _id, string _name, string _description, IEnumerable<Tag>? _tags = null)
    {
        public uint TraitId { get; set; } = _id;

        public string Name { get; set; } = _name;

        public string Description { get; set; } = _description;

        public IEnumerable<Tag> Tags { get; set; } = _tags ?? [];

        public string ToDex() => $"{Name}: {Description}\n{(Tags.Any() ? $"Tags: {string.Join(", ", Tags.Select(t => t.Name))}\n" : "")}\n";
    }
}
