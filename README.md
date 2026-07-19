# Dungeon Explorer

## Game Concept
Dungeon Explorer is a first-person 3D dungeon exploration game. You are trapped in a dark stone dungeon and must find all 5 hidden keys before escaping through the exit door — all before the 2-minute timer runs out. Watch your health: patrolling enemies and spike traps will damage you. Find health pickups to recover.

## Controls
| Key | Action |
|-----|--------|
| W / A / S / D | Move |
| Mouse | Look around |
| E | Interact with doors |
| Click in Game view | Lock mouse cursor |

## Current Gameplay Objective (Checkpoint 2)
1. Explore the dungeon — avoid enemies and spike traps
2. Collect all **5 golden keys** before the timer reaches 0
3. Find health pickups (green cubes) if you take damage
4. Use **[E]** to unlock the locked door blocking part of the dungeon
5. Once all keys are collected, the **exit door** opens — press **[E]** to escape and win!

## What Is Working in Checkpoint 2

### Core Systems
- ✅ First-person player controller (WASD + mouse look)
- ✅ 3D dungeon environment (floor, walls, ceiling with stone-like appearance)
- ✅ 5 collectible keys (rotating + bobbing animation)
- ✅ Exit door (opens when all 5 keys collected, press E to win)
- ✅ Locked door (press E to open, blocks access to part of dungeon)
- ✅ Health pickups (2 green cubes scattered in dungeon)

### Challenge Systems (3+)
- ✅ **Patrolling enemies** (2 red cube enemies patrol corridors, damage on contact)
- ✅ **Spike traps** (2 spike traps pulse up/down on floor, damage player when raised)
- ✅ **Countdown timer** (120 seconds — lose if time runs out)

### Tracking & UI
- ✅ Key count display (Keys: X / 5)
- ✅ Health display (♥ ♥ ♥ hearts)
- ✅ Countdown timer (turns red under 20s)
- ✅ Objective text (updates throughout gameplay)
- ✅ Interaction prompt (appears near doors)

### Win / Lose / Restart
- ✅ Win condition: collect all keys + press E at exit
- ✅ Lose condition: health reaches 0 OR timer runs out
- ✅ Win screen with restart button
- ✅ Lose screen with restart button

### Audio
- ✅ Ambient dungeon music (looping)
- ✅ Key pickup sound
- ✅ Health pickup sound
- ✅ Hurt sound (when taking damage)
- ✅ Win fanfare
- ✅ Lose sound

### Visual Polish
- ✅ Torch flicker lighting
- ✅ Enemy visual (red cube)
- ✅ Spike trap visual (gray rising cube)
- ✅ Health pickup visual (green cube)
- ✅ Keys are gold/yellow

## Known Issues
- Enemy collision uses physics — may occasionally clip through thin walls
- Mouse cursor must be clicked in Game view to lock on first play
- Spike trap damage interval prevents instant-kill on first contact

## Planned for Final Submission
- Better materials and textures on walls/floor
- Sound effects for enemy proximity
- Multiple dungeon rooms
- Enemy sound effects
- Particle effects on key pickup and win screen

## External Assets & Resources
- All visuals built from Unity primitive shapes (Cube, Sphere)
- Audio generated procedurally (Python wav generation)
- TextMeshPro (Unity built-in package)
- No paid or third-party assets used
