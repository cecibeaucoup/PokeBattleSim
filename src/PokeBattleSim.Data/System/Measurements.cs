namespace PokeBattleSim.Data.System
{
    public static class Measurements
    {
        /// <summary>
        /// Validates a length or weight: C# has no unsigned floating-point type, so positivity is enforced here.
        /// </summary>
        public static double EnsurePositive(double value, string paramName) =>
            double.IsFinite(value) && value > 0
                ? value
                : throw new ArgumentOutOfRangeException(paramName, value, "Value must be a positive, finite number.");
    }
}
