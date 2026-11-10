# Combat Stats Calculation Plan

Computes base combat values (wounds/HP, dice, autos, dodge, defense, priority, speed) per Pokemon, for two selectable rulesets.

## Design
- `Tiers` helper (Data): tier of an `AttributeGrades` value = 0 for E, else `(v + 2) / 3` (D=1 ... Ex=6). `Grades` (skills) already are tiers. Raw values above 16 are capped to the Ex grade (`Tiers.GradeOf`); they still count as points, but are always treated as Ex (tier 6, `Tiers.IsEx`).
- `ICombatStatCalculator` with `AnotherLifeCalculator` and `ZephiasCalculator`; chosen through `SystemContext` (`Ruleset` enum, default Another Life).
- Output record `CombatBaseStats`.
- `CombatantState.FromSheet(sheet, calculator)` in Battler fills the existing constructor.

## Formulas
- Power -> damage dice: 2 per point + 1 per tier.
- Toughness -> defense: 1 per point + 1 per tier.
- Speed -> hit dice: 2 per point. Another Life: dodge = points, priority = points. Zephias: dodge = number of minus/plus grades reached (cumulative), no priority.
- Stamina -> HP: Another Life: 2 wounds per point (3 for S tiers, 4 for Ex; every point of a value at 16 or higher counts 4) + Friendship points + weight bonus. Zephias: 2 per point + 1 per tier (Ex = 6 even above 16) + weight bonus.
- Weight bonus: 0 below 100 kg; +1 at 100 kg and +1 per further order of magnitude (log10).
- Aim/Brawl -> autos: 5 hit dice per level (Another Life) / 4 (Zephias); autos exceeding dice convert to dice.
- Evasion -> dodge + level x max(2, 20% of Speed dodge, rounded up).

## Deferred
Energy and wound penalties, environment, STAB, type bonuses, trainer specialties (future modifiers).
