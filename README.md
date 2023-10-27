## Overview

A 2D Top-Down Shop Interaction Game Made in Unity. "The Shopkeeper" is a prototype that draws inspiration from simulation games like 'The Sims' and 'Stardew Valley'. This project focuses on the creation of a functional clothes shop within a game, incorporating player-shopkeeper interactions, an inventory system, and character customization through equippable items.

## How To Play
- Movement: WASD Keys
- Toggle Inventory: I Key
- Interact: E Key (Near the Shopkeeper)
- Selling: Press the Inventory Key (I) while interacting with the Shopkeeper

## Development Process

### Initial Setup
Development began by setting up the appropriate Unity Editor version and organizing the project's structure. This setup included initializing a new 2D Core project and setting up a GitHub repository for version control.

### Asset Integration
Assets were integrated from various sources, including the [Brackeys 2D Mega Bundle](https://assetstore.unity.com/packages/2d/characters/2d-mega-bundle-177124) for the development of the character and environment. Additional custom assets, like clothing items, were created using Aseprite to provide a unique flair to the game prototype.

### Environment Design
A basic gameplay environment was set up, including defining the level's boundaries and adding a fitting grassy background. The player's movements were confined within these limits using strategically placed rigidbodies and aesthetically complementary 2D sprites.

### Shopkeeper Interaction
The interaction system evolved from an initial `OnTriggerEnter2D` approach to a more generic `OverlapCircle` method, accommodating a more flexible interaction system. This decision was primarily driven by the project's scale and the desire to maintain simplicity due to time constraints.

### UI/UX Design
A significant portion of the development time was dedicated to UI/UX, particularly in implementing the inventory system. While the resulting UI might not reflect the highest standards due to time limitations, the use of Unity's UI Toolkit presented a welcome challenge and a step out of the comfort zone, enhancing the overall skill set and appreciation for the tool.

### Gameplay Mechanics
The project saw the integration of a wallet system and a player HUD for financial tracking, along with refactoring to streamline interactions within the shop. Given time constraints, certain design decisions were made to favor simplicity and functionality over complexity.

### Inventory System
Adaptation and reuse were key strategies, particularly evident in the decision to adapt the shopkeeper UI for the player inventory. This choice not only saved development time but also helped maintain consistency in the game's look and feel.

### Command Pattern Implementation
A Command pattern was used to centralize the selling mechanics, making the process consistent regardless of whether the player or the shopkeeper initiated the sale. This approach was chosen for its scalability and ease of maintenance, despite known limitations.

### Equipment Mechanics
Character customization was achieved by creating a sprite renderer for each body part. This functionality allows players to visually equip items, although it introduced some minor rendering artifacts identified for future refinement.

### Custom Asset Creation
Dedication to the project's aesthetic led to the creation of custom clothing items using Aseprite. While this process was time-consuming, it ensured a unique and tailored appearance for each character.

### Limitations and Future Improvements
The development process, while thorough, was not without its limitations, primarily due to time constraints. These limitations affected various aspects of the project, from the interaction system's simplicity to the visual artifacts in the equipment mechanics. Future iterations would benefit from revisiting these areas for refinement.

## Conclusion
"The Shopkeeper" is a testament to adaptive learning and efficient decision-making in game development. Despite the time constraints and ensuing limitations, the project was brought to life with a keen focus on functionality and player experience.

## How to Run
1. Clone the repository to your local machine.
2. Open the Unity Editor.
3. Navigate to and open the project.
4. Press the "Play" button in Unity.

Alternatively, check the repo's releases.

## Contact
For any queries or feedback related to this project, please feel free to reach out!
