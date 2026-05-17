# Dungeon RPG — Combat System Prototype

A turn-based dungeon RPG built in Unity, currently in active development. This repository showcases the core combat and data architecture systems.

## 🎮 Demo


<!-- Add your video/gif here -->

## 🛠️ Systems Built

### Data-Driven Architecture
All game entities are defined as ScriptableObjects — classes, enemies, and skills are created as assets in the editor without touching code. This makes balancing and adding new content straightforward.

### Entity Factory
Characters and enemies are spawned at runtime with randomized stats within class-defined ranges. Party members keep their recruited stats permanently while enemies scale per encounter type.

### Combat System
- Speed-based turn order — all combatants sorted by speed at combat start
- Physical and magical damage types with separate armor and resistance stats
- Armor penetration and magic penetration as flat stat values
- Critical hits at 1.7x multiplier based on crit stat
- Permadeath — defeated party members are permanently removed
- AoE and single target skill support

### Skill System
Skills are ScriptableObjects with configurable damage type, target type and damage multiplier. Each class tier unlocks additional skills — base class has 1, advanced has 2, master has 3.

### Class Evolution System (in progress)
Three base classes — Warrior, Archer, Mage — each with branching evolution paths at level thresholds.

## 📁 Project Structure
Assets/
├── Scripts/
│   ├── CharacterStats.cs      — unified stat system for all entities
│   ├── CombatManager.cs       — turn order, death handling, XP distribution
│   ├── CombatUI.cs            — skill buttons, turn indicator, target selection
│   ├── EntityFactory.cs       — runtime character and enemy spawning
│   ├── ClassData.cs           — ScriptableObject defining class stat ranges and growth
│   ├── EnemyData.cs           — ScriptableObject defining enemy stat ranges
│   ├── SkillData.cs           — ScriptableObject defining skill effects and targeting
│   ├── FloatingTextSpawner.cs — world space damage and heal number display
│   └── HealthBar.cs           — dynamic health bar tied to CharacterStats
├── Classes/                   — ClassData assets (Warrior, Knight, etc.)
├── Enemies/                   — EnemyData assets (Goblin, etc.)
├── Skills/                    — SkillData assets (Strike, etc.)
└── Prefabs/                   — Character and UI prefabs

## 🗺️ Planned Features
- 20-floor dungeon with corruption mechanic — cleared floors reclaim over time based on player movement
- Tavern recruitment with randomized recruits scaling to camp floor depth
- Full class evolution trees for Warrior, Archer and Mage
- Permadeath party management — replace fallen members at tavern
- Floor-themed enemy encounters with boss fights
- Building system on cleared floors (smithy, camp, shrine)
- Race system with innate abilities (Elf, Dwarf, Halfling, Human)

## 🔧 Built With
- Unity 2D
- C#
- ScriptableObject architecture<img width="917" height="513" alt="gamepick" src="https://github.com/user-attachments/assets/dca04088-92f0-40aa-8edc-7a56ebd50a3f" />
