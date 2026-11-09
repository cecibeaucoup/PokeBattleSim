# Plan: Split Tags into Mechanic and Resolution tags

**Status:** Implemented.

## Problem

`Tags` was one flat enum, referenced as `IEnumerable<Tags>` on `Move`, `Ability`, `Trait`, `CombatantState.Tags`, `UnsupportedTagEvent` and `CombatTagRule`. It could not express "set weather -> sunny" or "grant immunity -> ground".

## Approach

1. **Enums**
   - `MechanicTag`: what the system does (`SetWeather`, `SetTerrain`, `ApplyCondition`, `GrantImmunity`, `RestrictResistance`, `STAB`, `Mitigation`, roll/dice/auto modification, checks, combat stat modifiers).
   - `ResolutionTag`: the exact parameter (weather, types, veils, multipliers, `Up`/`Down`, and so on).
   - Old values map to a mechanic plus resolutions, e.g. `NormalSTAB` -> `STAB(NormalType)`, `PriorityUp` -> `ModifyPriority(Up)`.
2. **`TagBinding`**: `sealed record TagBinding(MechanicTag Mechanic, IReadOnlyList<ResolutionTag> Resolutions)` with a `params ResolutionTag[]` constructor, `Deconstruct`, sequence-based equality and `ToString` (`SetWeather(SunnyWeather)`). It replaces tuples because C# has no variable-size mixed tuples (`ValueTuple` caps at 8 and is not uniformly iterable, and System.Text.Json does not round-trip it).
3. **Entities**: `Move`, `Ability` and `Trait` hold `IEnumerable<TagBinding>`; `ToDex` prints bindings.
4. **Battler**: `CombatantState.Tags` is an `ISet<TagBinding>`; `UnsupportedTagEvent` carries a `TagBinding`; `CombatTagRule` switches on `Mechanic` and reads `Resolutions`.
5. **Persistence**: `TagBinding` round-trips through `JsonDataStore` options (covered by a test).
6. **Tests**: Data and Battler tests migrated; `TagBindingTests` added (equality, deconstruct, `ToString`, JSON).

## Outcome

Build and all tests pass (Data 112, Infra 51, Battler 26).

## Open items

- Review the mechanic groupings and shared multiplier names (`Double`, `Half`, ...), which were chosen during the refactor.
- Add a `TagBindingValidator` (allowed resolutions and arity per mechanic) so invalid combinations such as `SetWeather(FireType)` are rejected. The alternative, typed per-mechanic records, is safer but heavier.
- Introduce per-mechanic handlers (registry keyed by `MechanicTag`) in place of the single `CombatTagRule`.
