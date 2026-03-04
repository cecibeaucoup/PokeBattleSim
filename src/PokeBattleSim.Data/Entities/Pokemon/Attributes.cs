using System;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Attributes(int _pow = 0, int _spd = 0, int _tgh = 0, int _sta = 0)
    {
        public Attribute Power { get; set ;} = new (_pow, "Power");

        public Attribute Speed { get; set; } = new (_spd, "Speed");

        public Attribute Toughness { get; set; } = new (_tgh, "Toughness");

        public Attribute Stamina { get; set; }  = new (_sta, "Stamina");

        public static Attributes Sum(params Attributes[] attributes)
        {
            if (attributes == null || attributes.Length == 0)
                return new Attributes(0, 0, 0, 0);

            int pow = 0, spd = 0, tgh = 0, sta = 0;

            foreach (var a in attributes)
            {
                if (a == null) continue;
                pow += a.Power.BaseValue;
                spd += a.Speed.BaseValue;
                tgh += a.Toughness.BaseValue;
                sta += a.Stamina.BaseValue;
            }

            return new Attributes(pow, spd, tgh, sta);
        }

        public string ToDex() => $"{Power.ToDex()}\n{Speed.ToDex()}\n{Toughness.ToDex()}\n{Stamina.ToDex()}";
    }
}
