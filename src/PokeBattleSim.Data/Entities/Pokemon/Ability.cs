using System;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Ability(uint _id, string _name, string _description)
    {
        public uint AbilityId { get; set; } = _id;

        public string Name { get; set; } = _name;

        public string Description { get; set; } = _description;

        public IEnumerable<Tag> Tags { get; set; } = [];

        public string ToDex() => $"{Name}: {Description}\n{(Tags.Any() ? $"Tags: {string.Join(", ", Tags)}\n" : "")}\n";
    }
}
