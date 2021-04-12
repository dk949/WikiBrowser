# WikiReader mod for tModLoader

## Completed Features

* Drop an item in a dialogue window to get its wiki page
* String wrapping
* Pagination
* Background/frame
* Loaded articles should have titles
* If an Item does not have an individual page the closest page is loaded
    * E.g. "Chlorophyte Mask" should redirect to "Chlorophyte armor"
* Added vesion log

## Known bugs/missing features
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

* Allow the user to type the name of an item/npc if they can't place them in the window.
* (maybe) allow hovering over items/NPCs and pressing a key to get the article about the thing under the cursor

## Version Log

== 0.1-alpha ==  
Articles can be loaded into a custom UI by putting an Item in item frame

== 0.2-alpha ==  
Text is formatted (wrapped and split into pages)

== 0.2.1-alpha ==  
Added titles

== 0.3-alpha ==  
Added an extra request to search for the item if an individual page does not exist
