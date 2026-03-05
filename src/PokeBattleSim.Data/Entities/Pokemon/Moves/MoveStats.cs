namespace PokeBattleSim.Data.Entities.Pokemon.Moves
{
    public class MoveStats(int _priority = 0, int _hitDice = 0, int _hitAutos = 0, int _damageDice = 0, int _damageAutos = 0)
    {
        public int Priority { get; set; } = _priority;

        public int HitDice { get; set; } = _hitDice;

        public int HitAutos { get; set; } = _hitAutos;

        public int DamageDice { get; set; } = _damageDice;

        public int DamageAutos { get; set; } = _damageAutos;

        public string ToDex() => $"Priority: {Priority}\nHit Dice: {HitDice} | Auto Hits: {HitAutos}\nDamage Dice: {DamageDice} | Auto Damage {DamageAutos}\n";
    }
}
