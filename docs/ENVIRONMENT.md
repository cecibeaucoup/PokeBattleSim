# Plan: Environment class (Weather + Terrain with tag bindings)

**Status:** Implemented. Build and all tests pass (Data 124, Battler 30, Infra 51).

## Problem

The battle system had no notion of local conditions. `Weather` and `Terrains` enums existed (`src/PokeBattleSim.Data/Enums`) but nothing held the current state, and neither carried `TagBinding`s. `ResolutionTag` had no terrain values.

## Decisions

- Tag bindings come from a **static catalog in Data**: one lookup per enum value. `Environment` exposes the bindings of its current state.
- `Environment` is a **Data entity**; `BattleState` owns one instance.
- **One Weather and one Terrain at a time.** Setting a new one replaces the old one (source notes: "the latest one overrides").
- Dice and automatic-success effect records are **not** added yet; the checkpoints below are the documented extension point.

## What was built

1. **Resolution tags**: one `...Terrain` value per `Terrains` value was added to `ResolutionTag`. Weather already had exactly one value per `Weather` (except `NoWeather`), so only the mapping was fixed:

   | Weather | ResolutionTag |
   |---|---|
   | Sunlight | SunnyWeather |
   | Rain | RainyWeather |
   | Sandstorm | SandyWeather |
   | Hail | SnowyWeather |
   | Fog | FoggyWeather |
   | ToxicFog | SmogWeather |
   | HarshSunlight | HarshSunWeather |
   | HeavyRain | StormyWeather |
   | StrongWinds | WindyWeather |
   | ShadowSky | ShadowWeather |

2. **`EnvironmentCatalog`** (`Data/System`): `GetBindings(Weather)` / `GetBindings(Terrains)`. Every value gets an identity binding (`Rain -> SetWeather(RainyWeather)`, `Misty -> SetTerrain(MistyTerrain)`); `NoWeather` and `Ground` are empty. Undefined enum values throw. Effect bindings are added here later.
3. **`Environment`** (`Data/Entities`): `Weather`, `Terrain` (defaults `NoWeather` / `Ground`), `SetWeather`, `SetTerrain`, `Clear`, and `Tags` (weather bindings, then terrain bindings).
4. **Battler propagation**: `BattleState` owns an `Environment` (optional constructor parameter), `BattleRuleContext.Environment` reads it live, `BattleEngine.ChangeWeather/ChangeTerrain` mutate it and publish `EnvironmentChangedEvent`.
5. **Checkpoints**: the engine re-reads the environment at each point of a round via `EnvironmentCheckpointEvent`, published through the normal rule registry. The environment can change mid-round, so rules always see the current state.

   | Checkpoint | Where in `BattleEngine.ExecuteRound` | Future use |
   |---|---|---|
   | `RoundStart` | after the round-start phase and modifier reset | start-of-round effects, duration ticks |
   | `SpeedPriority` | once per legal action, before ordering | weather/terrain changing Speed or Priority |
   | `HitRoll` | before each hit roll | hit dice and automatic successes |
   | `DamageRoll` | before each damage roll | damage dice, automatics and modifiers |
   | `RoundEnd` | end of round, before `RoundEndedEvent` | end-of-turn effects, environment expiry |

   `CombatTagRule` ignores these events for now, so existing engine behavior is unchanged.

## Tests

- Data (`EnvironmentTests`): defaults, replace semantics, tag composition, `Clear`, invalid values, catalog completeness and distinct resolutions.
- Battler (`EnvironmentCheckpointTests`): state carries the environment, checkpoint order, actor/target on action checkpoints, rules see an environment change made between rounds.

## Out of scope / next steps

- Applying environment effects, durations and expiry.
- `SetWeather` / `SetTerrain` handlers and moves that set them (use `ChangeWeather/ChangeTerrain`).
- Effect records that change dice and automatic successes at the roll checkpoints.
- Persistence of `Environment`.
