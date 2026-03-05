using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.Entities.Trainer
{
    public class Badge(string _name, string _description, Regions _badgeRegion = Regions.None)
    {    
        public string Name { get; set; } = _name;

        public string Description { get; set; } = _description;

        public Regions BadgeRegion { get; set; } = _badgeRegion;

    }
}
