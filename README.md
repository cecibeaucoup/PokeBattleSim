# PokeBattleSim

Pokemon Battle Simulator based on Red Flag's system for the quest
`To Live is to Dream`, alongside Zephias' quest `To Forge Our Story`.

This project was made using agentic AI after an initial implementation,
because I need to learn how to better use it due to work requirements
and it seemed a fitting enough project to do so.

## Project structure

- **Data** contains storage-independent entities such as Trainers, Pokemon,
  Moves, Abilities, enums, static typing data, pure calculations, and contracts.
- **Battler** contains the battle system: teams, phases, tag-driven rules,
  events/effects, resolution, replay, and battle-log projections.
- **Infra** contains persistence and external integrations, including read-only
  base JSON and separate writable user-data JSON for authoring changes.
- **ConsoleUI** composes the services and provides catalog, authoring, team
  setup, trainer-vs-trainer, and trainer/team-vs-wild workflows.

- **baseData** contains simple text files from the two quests mentioned above,
used for reference.
- **docs** contains the plans made with AI for the implementation.

`Battler` and `Infra` depend on `Data`; `ConsoleUI` is the composition root.
Predefined data is not overwritten by user workflows. See
[docs/PROJECT-STRUCTURE.md](docs/PROJECT-STRUCTURE.md) for the detailed
dependency and folder rules.
