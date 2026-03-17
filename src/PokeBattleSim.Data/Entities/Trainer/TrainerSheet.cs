using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Trainer
{
    public class TrainerSheet(uint _trainerId, string _name, Genders _gender, uint _age, Regions _currentRegion = Regions.None)
    {
        public uint TrainerId { get; set; } = _trainerId;
        
        public string Name { get; set; } = _name;

        public Genders Gender { get; set; } = _gender;

        public uint Age { get; set; } = _age;

        public Regions CurrentRegion { get; set; } = _currentRegion;

        public IEnumerable<Badge> Badges { get; set; } = [];

        public uint TrainerLevel { get; set; } = 1;

        public TrainerRanks Rank { get; set; } = TrainerRanks.NotATrainer;

        public IEnumerable<Trait> Traits { get; set; } = [];

        public uint CarryLimit { get; set; } = 6;

        public uint PokemonLimit { get; set; } = 10;

        public IEnumerable<PokemonSheet> PokemonParty { get; set; } = [];

        public IEnumerable<PokemonSheet> PokemonBox { get; set; } = [];
    }
}
