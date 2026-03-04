using System;

namespace PokeBattleSim.Data.Entities.Pokemon
{
    public class Skills (int _aim = 0, int _efficiency = 0, int _evasion = 0, int _brawl = 0)
    {
        public Skill Aim { get; set; } = new(_aim, "Aim");

        public Skill Efficiency { get; set; } = new(_efficiency, "Efficiency");

        public Skill Evasion { get; set; } = new(_evasion, "Evasion");

        public Skill Brawl { get; set; } = new(_brawl, "Brawl");

        public static Skills Sum(params Skills[] skills)
        {
            if (skills == null || skills.Length == 0)
                return new Skills(0, 0, 0, 0);

            int aim = 0, eff = 0, eva = 0, bra = 0;

            foreach (var a in skills)
            {
                if (a == null) continue;
                aim += a.Aim.BaseValue;
                eff += a.Efficiency.BaseValue;
                eva += a.Evasion.BaseValue;
                bra += a.Brawl.BaseValue;
            }

            return new Skills(aim, eff, eva, bra);
        }

        public string ToDex() => $"{Aim.ToDex()}\n{Efficiency.ToDex()}\n{Evasion.ToDex()}\n{Brawl.ToDex()}";
    }
}
