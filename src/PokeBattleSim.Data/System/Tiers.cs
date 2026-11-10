using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Data.System
{
    /// <summary>Converts grades into tiers (full letter grades: D-, D and D+ share a tier).</summary>
    public static class Tiers
    {
        public const int ExValue = (int)AttributeGrades.Ex;

        /// <summary>Caps a raw attribute value at the Ex grade; values above 16 are still Ex.</summary>
        public static AttributeGrades GradeOf(int attributeValue) =>
            (AttributeGrades)Math.Clamp(attributeValue, 0, ExValue);

        public static bool IsEx(int attributeValue) => attributeValue >= ExValue;

        public static int Of(AttributeGrades grade) => Of((int)grade);

        public static int Of(int attributeValue)
        {
            var value = (int)GradeOf(attributeValue);
            return value == 0 ? 0 : (value + 2) / 3;
        }

        public static int Of(Grades grade) => (int)grade;
    }
}
