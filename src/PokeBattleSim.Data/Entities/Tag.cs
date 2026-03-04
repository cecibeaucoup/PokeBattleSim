namespace PokeBattleSim.Data.Entities
{
    public class Tag(string _name, string _description)
    {
        public string Name { get; set; } = _name;

        public string Description { get; set; } = _description;
    }
}
