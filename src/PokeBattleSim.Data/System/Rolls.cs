namespace PokeBattleSim.Data.System;

public class Rolls(uint _dieSize = 6, uint _threshold = 4)
{
    // Size of the die to roll (e.g. 6 for a d6)
    public uint DieSize { get; set; } = _dieSize;

    // Threshold value for a successful roll, equal or greater than this value means success (e.g. 4 means rolling a 4, 5, or 6 is a success)
    public uint Threshold { get; set; } = _threshold;
}
