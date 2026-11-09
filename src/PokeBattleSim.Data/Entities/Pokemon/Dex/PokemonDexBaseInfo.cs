using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.System;

namespace PokeBattleSim.Data.Entities.Pokemon.Dex
{
    public class PokemonDexBaseInfo(string _name, uint _dexNumber, double _length, double _weight, Morphologies _morphology, PokemonTypes _priType, 
                                        PokemonTypes _secType, EggGroups _eggGroup1, EggGroups _eggGroup2 = EggGroups.NoGroup)
    {
        public string Name { get; set; } = _name;

        public uint DexNumber { get; set; } = _dexNumber;

        public double Length
        {
            get => _lengthValue;
            set => _lengthValue = Measurements.EnsurePositive(value, nameof(Length));
        }

        public double Weight
        {
            get => _weightValue;
            set => _weightValue = Measurements.EnsurePositive(value, nameof(Weight));
        }

        private double _lengthValue = Measurements.EnsurePositive(_length, nameof(_length));

        private double _weightValue = Measurements.EnsurePositive(_weight, nameof(_weight));

        public Morphologies Morphology { get; set; } = _morphology;

        public PokemonTypes PrimaryType { get; set; } = _priType;

        public PokemonTypes SecondaryType { get; set; } = _secType;

        public EggGroups EggGroup1 { get; set; } = _eggGroup1;

        public EggGroups EggGroup2 { get; set; } = _eggGroup2;

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
