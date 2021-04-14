# WikiReader mod for tModLoader

## How this works

* Open the console (Enter)
* Type /wiki to get a blank window with an item slot
    * Place an item into the slot to get its page
    * IMPORTANT: Remove the item from the slot before closing the game. Closing the window will automatically return the
      item to the player, but closing the game will remove it.
* Alternatively type /wiki [Search term]. If a page exists or a similar one can be found, it will show up.
    * Note: spaces are allowed, as well as any capitalisation.

## Completed Features

* Drop an item in a dialogue window to get its wiki page
* String wrapping
* Pagination
* Background/frame
* Loaded articles should have titles
* If an Item does not have an individual page the closest page is loaded
    * E.g. "Chlorophyte Mask" should redirect to "Chlorophyte armor"
* Version log
* Allow the user to type the name of an item/npc if they can't place them in the window.

## Known bugs/missing features

* If an item is still in the slot when the game is closed (via crash or by the user), the item will be lost
* Some sections are blank.
    * "== Crafting ==" is always blank
* Whenever there are are differences between versions, on the wiki they are displayed as
  [Value 1] {icon} {icon}/[value 2] {icon} {icon} {icon}, this currently gets translated as
  [value1]   /[value 2]
    * There is no indication as to which version values belong to
    * E.g.
        * "Crafting the full set with every helmet type requires 78 bars
          ( {icon} 390 / {icon} {icon} {icon} {icon}468 ore). " ->
        * "Crafting the full set with every helmet type requires 78 bars
          ( 390 / 468 ore).

## Nice-to-have features

* allow hovering over items/NPCs and pressing a key to get the article about the thing under the cursor
* add crafting hints (these can't be loaded from the wiki the way I'm doing it now, but maybe I can make a simpler
  version of recipe browser?)
* add a mode where info is not loaded from the wiki and it just displays stats that are available in the game

## Version Log
== 1.1 == Added an icon

== 1.0 ==  
Out of alpha, no UX changes from 0.4, just internal tweaks

== 0.4-alpha ==  
Changed command to /wiki [Item name]
Added handling for pages which could not be found using search

== 0.3-alpha ==  
Added an extra request to search for the item if an individual page does not exist Added version log

== 0.2.1-alpha ==  
Added titles

== 0.2-alpha ==  
Text is formatted (wrapped and split into pages)

== 0.1-alpha ==  
Articles can be loaded into a custom UI by putting an Item in item frame
