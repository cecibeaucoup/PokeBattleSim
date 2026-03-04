using System;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon.Moves
{

    public class Move(uint _id, string _name, PokemonTypes _type, string _description, MoveStats _moveStats, bool _isClashable, PokemonTypes _secType = PokemonTypes.None, IEnumerable<Tag>? _tags = null)
    {
        #region MoveDex
        public uint MoveId { get; set; } = _id;

        public string Name { get; set; } = _name;

        public PokemonTypes Type { get; set; } = _type;

        public PokemonTypes SecondaryType { get; set; } = _secType;

        public string Description { get; set; } = _description;

        #endregion

        #region MoveFlags
        public bool IsClashable { get; set; } = _isClashable;

        public IEnumerable<Tag> Tags { get; set; } = _tags ?? [];
        #endregion

        public MoveStats MoveStats { get; set; } = _moveStats;

        public string ToDex() => $"{Name} - ({Type}{(SecondaryType != PokemonTypes.None ? $"/{SecondaryType}" : "")}): {Description}\n{MoveStats.ToDex()}\n";
    }
}
