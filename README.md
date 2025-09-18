# CookingMaster Game
A salad making couch co-op game, where 2 players have to pickup vegetables, chop them into a salad and deliver it to a customer in time to get rewards.

## Controls
**Player 1 Red:** Move: W, A, S, D & Action: F

**Player 2 Blue:** Move: Arrow Keys & Action: G

## Features Implemented
**1. Player Movement:** 2 controllable players using same keyboard (WASD and Arrow Keys).

**2. Vegetable PickUp:** Players can pick up from upto 6 vegetables using zones and triggers. Vegetables are sprites in the game for logic and display both.

**3. Player Inventory:** A single inventory system for both players separated by ID to carry upto 2 raw vegetables.

**4. DustBin:** A dustbin where the player can discard the carried vegetable if they pick up the wrong one.

**5. Chopping:** 2 separate vegetable chopping stations, where the player can go and chop the first carried vegetable for 2s, which is then displayed in chpped zone and then can do so for more vegetables as well.

**6. Serving:** A simple zone displayed by a plate where the players can pick up their chopped vegetable (as a sprite list).

**7. Customer:** 2 separate customers which generate a random vegetable order for their respective players simultaneously. Player can deliver in the customer zone, which validates the order from the customer.

**8. Customer Mood:** Added a customer mood system with a timer over the customers head. Each customer comes for 25s, they give full reward if order delivered with 10s remaining, half reeward after that, and if order not delivered in 25s at all, customer leaves with penalty points. Also penalty points f wrong order given.

**9. Scoring:** +5 points awarded if order correct and customer satisfied (with 10s remaining). +3 Points if after that. -2 points if not delivered and -1 point if wrong order delivered.

**10. Player Timer:** Each player has its own timer set at 120s. When both player timer ends, the game stops.

**11. Power Ups:** There are 3 types of powerups: Speed (gives player 1.5x speed for 7s), Score (gives player +3 extra score) and Time (adds 7s to the player timer). a random power up spawns in a pre definined location if a player delivers with full reward. The player can then take the power up and changes are applied.

**12. Game Over Panel:** Once game is over (timer runs out) a panel appears with both players score on it and a restart and a quit option.


## Script Information
**1. PlayerController.cs:** Basic control script i am using to handle both players controls. Can set whatever key for whichever player the script is attached to. Also handles speed boost.

**2. PlayerInventory.cs:** Manages the inventory of the player based on ID. Handles vegetables as sprites throughout the game. Shows inventory display and discarding vegetables.

**3. PickUpZone.cs:** Attached to each vegetable pickup zone. Manages the picking up of the vegetables form the table and storing it in the inventory.

**4. DustbinZone.cs:** Fetches the inventory and discards the first vegetable in the dustbin.

**5. ChoppingBoardZone.cs:** Handles the 2s vegetable chopping and displaying the chopped vegetable. ID based. Keeps the chooped vegetables also in a sprite list.

**6. ServingZone.cs:** Handles picking up the chopped vegetables. Gets a copy from the chopping script and simply gives the copied list to the player id in inventory.

**7. Customer.cs:** Attached to the customer. Displays the customers order and handles customer reward and timing system. Generates an order based on a list of sprites and displays it. Compares the served list and the customer list by index to check if the sprites match, hence the order matches and gives points.

**8. CustomerZone.cs:** Works with Customer.cs by letting the player deliver. Matches order using the Customer.cs script. Adds score and requests power up. This script handles the interaction between player and customer.

**9. PowerUp.cs:** Handles what all 3 power ups do.

**10. PowerUpSpawner.cs:** Spawns the random powerup prefabs ar random pre definedd points when conditions met.

**11. GameManager.cs:** Controls displaying the player time and final game over panel.

**12. ScoreManager.cs:** Manages the player scores and displaying them.

## Dev Notes
1. Entire game works on list and sprite logic. 

2. Vegetables are managed as sprites in all systems by copying, and validating lists.

3. FIFO concept used for putting down vegetables and validationg them.

4. Loops used to run through them.

5. High score system could be made through json file storing names and scores locally.

6. Zone system being used for interaction through out.

7. Player ID's used to diffrenciate players.

8. Modular Scripts for all zones.

9. Had to shift to player id as a lot of confusion wasa being created.

10. Could possible to put more load on managers and reduce load from other important script.


## Future Upgrades

1. High Score system.

2. Special Customers

3. Mini challenges betweeen players, where time is paused, and whoever wins gets an advantage.

4. Difficult combination of orders























