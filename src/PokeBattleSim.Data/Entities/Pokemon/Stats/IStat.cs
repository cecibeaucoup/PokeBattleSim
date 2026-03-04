using System;

namespace PokeBattleSim.Data.Entities.Pokemon;

public interface IStat<T> where T : Enum
{   
    public int BaseValue { get; set; }
    
    public string Name { get; set; }

    public T Grade { get; }

    public string ToDex();
    
}
