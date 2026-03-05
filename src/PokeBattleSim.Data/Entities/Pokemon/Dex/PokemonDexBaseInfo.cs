using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Pokemon.Dex
{
    public class PokemonDexBaseInfo(string _name, uint _dexNumber, uint _length, uint _weight, Morphologies _morphology, PokemonTypes _priType, PokemonTypes _secType)
    {
        public string Name { get; set; } = _name;

        public uint DexNumber { get; set; } = _dexNumber;

        public uint Length { get; set; } = _length;

        public uint Weight { get; set; } = _weight;

        public Morphologies Morphology { get; set; } = _morphology;

        public PokemonTypes PrimaryType { get; set; } = _priType;

        public PokemonTypes SecondaryType { get; set; } = _secType;

        public string ToDex()
        {
            string strBuilder = $"#{DexNumber} - {Name}\n";
            strBuilder += $"Length: {Length} m - ";
            strBuilder += $"Weight: {Weight} kg\n";
            strBuilder += $"Morphology: {Morphology}\n";
            strBuilder += $"Type: {PrimaryType}{(SecondaryType != PokemonTypes.None ? $"/{SecondaryType}" : "")}\n";

            return strBuilder;
        }
    }
}
