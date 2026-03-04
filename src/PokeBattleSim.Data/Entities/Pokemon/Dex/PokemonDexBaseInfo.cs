using System;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon.Dex;

public class PokemonDexBaseInfo(string _name, uint _dexNumber, uint _length, uint _weight, Morphologies _morphology, PokemonTypes _priType, PokemonTypes _secType)
{
    public string Name { get; set; } = _name;

    public uint DexNumber { get; set; } = _dexNumber;

    public uint Length { get; set; } = _length;

    public uint Weight { get; set; } = _weight;

    public Morphologies Morphology { get; set; } = _morphology;

    public PokemonTypes PrimaryType { get; set; } = _priType;

    public PokemonTypes SecondaryType { get; set; } = _secType;
}
