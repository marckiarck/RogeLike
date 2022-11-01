# RogePaloma
***Roge Paloma*** was a game that I made with another class mate of the **Game Programing** master curse for the subject **Unity**. In this subject we learned the foundations of the game engine Unity. The final practie was to made a small **Game Jam** of 12 hours where when had to create a simple game in that time.

We do ***RogePaloma***, a **rogue like** genre game where the player take control of a *brave* **pigeon** that tries to scape the creepy doungeon where is confined. The player can shoot bullets that will allow him to defend from the enemies of the dungeon.

There is no scape of the dungeon, but this **Adorable** **Bloody** ***PIDGEON*** is going to take to the grave as much monsters as she can before fall defeated.

# My contribution to the project

I made the gameplay part of the game while my team mate made the procedural infinite daungeon.

## Attribute system

To make the game scalable I made a small attribute system that instead of directly create variables in code that correspondes to the game features (like damage or health), y made a data structure that associated a key to a numeric value. This way, you could create a feature like health and associate it to the attribute set of and object and give him the health behaviour that will be used for the rest of the elements of the game related to the health mechanic.

These system is so open that the bullet variants are just normal bullets with a diferent sprite each. Is the attribute system who give each bullet its unic behaviour.

![CC](https://img.shields.io/badge/Clean_Code-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![HM](https://img.shields.io/badge/Hash_Map-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![SC](https://img.shields.io/badge/Scalable_Code-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)

## Collision System

To handle the diferent types of collisions of the game I made a collison event system. This system works adding to the GameObjects an entity collision system where you can specify which event is going to execute based on a tag added to the objects that collides with the object. This made an easy and clean way to make the same bullet to react in a different ways if collided against an enemy or a wall. Other example of what this collision system can do are the enemies, that using the collision system, the knew if what they where colliding was a wall o other thing and change its behaviour based on it.

![CC](https://img.shields.io/badge/Clean_Code-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![SC](https://img.shields.io/badge/Scalable_Code-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![INHT](https://img.shields.io/badge/Inheritance-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)

![RB](https://img.shields.io/badge/RigidBody-62B5FF?style=for-the-badge&logo=unity&logoColor=black&labelColor=D8D8D8)
![TGS](https://img.shields.io/badge/Tags-62B5FF?style=for-the-badge&logo=unity&logoColor=black&labelColor=D8D8D8)

## Enemy behaviour
For the enemy behaviour I made an interface with the purpouse of inherit form it and statrting from simple behaviours like a seek behaviour get complex behaviours like an enemy that seeks the player taking into account the walls and the possibility of ambushes.

This simple behaviours become so useful that bullets in the game moves using these enemy behaviour system.

![CC](https://img.shields.io/badge/Clean_Code-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![INHT](https://img.shields.io/badge/Inheritance-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)

## Event system

I made a simple interface that allows it sons to reproduce events just overriding one method. It was a way I used to connect the different systems of the game without create dependencies beetwen them. I didn't make a complex event architecture for the event system beacuse for the Game Jam it was going to be used for simple tasks that did not require a large investment of time.

![CC](https://img.shields.io/badge/Clean_Code-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![INHT](https://img.shields.io/badge/Inheritance-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![DPNDC](https://img.shields.io/badge/Dependencies-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
![YAGNI](https://img.shields.io/badge/YAGNI-57FF70?style=for-the-badge&logo=c&logoColor=black&labelColor=D8D8D8)
