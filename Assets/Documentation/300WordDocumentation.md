# The Shopkeeper

The Shopkeeper is a 2D simulation prototype, reminiscent of 'The Sims' and 'Stardew Valley', focusing on a clothes shop where players interact with a shopkeeper, buy/sell items, and dress their character.

## Development Process

The project began with setting up Unity 2021.3.2f1 and structuring tasks. Key features were incrementally built, ensuring functional milestones before advancing.

**Character Control and Environment:** Simple animations and 2D controls were established early. The environment design followed, introducing natural boundaries using 2D sprites to complement the shop's aesthetics.

**Shopkeeper Interaction:** The interaction mechanic was initiated with OnTriggerEnter2D, evolving to an OverlapCircle method for flexibility with multiple shops.

**UI Design:** The UI demanded significant time, challenged by Unity's UI Toolkit, pushing boundaries of comfort and skill.

**Inventory System:** Reusing the shop UI for the player’s inventory was a strategic pivot, highlighting adaptive problem-solving.

**Refactoring:** Transitioned the player into IBuyer and ISeller interfaces. A Command pattern centralized the selling mechanics, streamlining the transaction process.

**Equipment Mechanic:** The character’s appearance changes were handled by individual sprite renderers for body parts, controlled via an EquipItem function.

**Asset Integration:** Utilized [Brackeys 2D Mega Pack](https://assetstore.unity.com/packages/2d/free-2d-mega-pack-177430) and original assets designed in Aseprite.

**Final Touches:** Integrated custom clothing items, taxing artistically more than programmatically.

The project is a balance between ambition and scope, conscious of the 48-hour deadline. Though robust, it recognizes its limitations, chiefly born from time constraints.

Please enjoy the journey within The Shopkeeper, where every attire holds a story.
