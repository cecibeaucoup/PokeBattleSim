# Project structure

PokeBattleSim follows an inward dependency direction:

```text
PokeBattleSim.Data
    ^                 ^
    |                 |
PokeBattleSim.Infra  PokeBattleSim.Battler
    ^                 ^
    |                 |
PokeBattleSim.ConsoleUI
```

## Data

`PokeBattleSim.Data` contains storage-independent domain vocabulary:

- Trainers, Pokemon, Moves, Abilities, Merits, rosters, and value objects.
- Enums, tags, grades, types, movement, weather, terrain, and static typing data.
- Pure calculations and repository/configuration contracts.

Data must not reference infrastructure, UI, file paths, JSON clients, databases,
or battle orchestration.

## Battler

`PokeBattleSim.Battler` contains the battle system:

- Teams, active slots, encounter setup, rounds, phases, outcomes, and switching.
- Move/ability tag rules, typed events/effects, reactions, clashes, and resolution.
- Seeded randomness, replay support, and battle-log projections.

Battler depends on Data contracts and entities, never on concrete Infra
implementations. The runtime belongs here even when it uses Data entities.

## Infra

`PokeBattleSim.Infra` contains concrete external concerns:

- Read-only predefined/base JSON loading.
- Separate writable user-data JSON for created or edited entities.
- DTOs, versioning, schema/semantic validation, mapping, and repositories.
- Future database, API, caching, and external-data adapters.

Base data is never overwritten by authoring workflows. Invalid input must produce
an explicit diagnostic containing the source and entity identity.

## ConsoleUI

`PokeBattleSim.ConsoleUI` is the composition root and presentation layer. It
will provide catalog and authoring workflows for Trainers, Pokemon, Moves,
Abilities, and rosters, plus a shared team-based battle setup for:

- Trainer versus trainer.
- Trainer/team versus wild Pokemon.

Both encounter types use the same Battler engine. The UI calls application
services and displays structured battle events; it does not mutate repositories
or battle state directly.

## Tests

Tests should follow the owning boundary:

```text
src/Tests/PokeBattleSim.Data.Tests/
src/Tests/PokeBattleSim.Battler.Tests/
src/Tests/PokeBattleSim.Infra.Tests/
src/Tests/PokeBattleSim.ConsoleUI.Tests/
src/Tests/PokeBattleSim.Integration.Tests/
```

Unit tests remain independent of concrete persistence. Integration tests compose
JSON data, user data, Battler, and UI workflows.

## Data boundaries

Markdown under `baseData/` is reference/source material. Runtime data should be
loaded through explicit Infra adapters into Data contracts; the battle engine
must not parse narrative Markdown logs.
