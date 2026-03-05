namespace PokeBattleSim.Data.Entities.Pokemon;

public interface IStat
{   
    int BaseValue { get; set; }
    string ToDex();
}

public interface IStat<TStat, TGrade> : IStat
        where TStat : Enum
        where TGrade : Enum
    {
        TStat Name { get; set; }
        TGrade Grade { get; }
    }
