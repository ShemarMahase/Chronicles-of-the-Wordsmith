# Chronicles-of-the-Wordsmith
Unity capstone project - Turn Based Language Learning RPG designed to teach you Spanish

## Technical Systems Built
- Turn-based combat system with event-driven player/enemy coordination - Battle can continue until a combatants hp reaches 0
- Two card Types - Translation and Multiple Choice
- Google TTS integration with automated caching and duplicate prevention - Parry system, and general audio throughout the game
- Modifier system - special effects players and enemies can use and be afflicted by
- Stance System - player can switch stances modifying stats and access to different effects
- Modularity - Stances,Stats, Modifiers, and Cards use Scriptable Objects and are designed to be easily expanded upon
  
## Next Steps
- More Card Types - Story and Matching
- Modular Custom AI for Enemies
- UI Polish and Art changes
- Content Creation - Systems are in place, building interesting content for players to interact with is next
- Bug fixes 

## Technologies
Unity 6, C#, Google TTS

## Setup
1. Clone repo
2. Open in Unity 6
3. Open either CombatTest Scene or TestScene Scene
4. You can press play or create new cards by Create > Scriptable Objects > Cards > [Card of your choice] and fill out the data, and then drag the new card into CardManager
5. [Optional] for more audio, Create a new GTTSKEY scriptable object and enter in your Google TTS key.
   - you can then open the "Audio Donwloader" Scene and press play to download audio for any new cards added

## Controls
In TestScene - use arrow keys or WASD to move around, J to interact  
In CombatTest - use mouse to interact with buttons, and keyboard for typing minigames

## Code Highlights
Walker's Alias Method in the stance system, or the Custom Fuzzy String class for the Parry system.
