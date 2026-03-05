using System;

namespace PokeBattleSim.Data.Entities.Pokemon;

public interface IStat<T1, T2> where T1 : Enum where T2 : Enum
{   
    public int BaseValue { get; set; }
    
    public T1 Name { get; set; }

    public T2 Grade { get; }

    public string ToDex();
    
}
