# WikiReader mod for tModLoader

## Completed Features

* Drop an item in a dialogue window to get its wiki page

## Future features

* Allow the user to type the name of an item/npc if they can't place them in the window.
* (maybe) allow hovering over items/NPCs and pressing a key to get the article about the thing under the cursor

## Needed bugfixes:

* String wrapping
    * There is currently no wrapping, each paragraph is a long string going off screen.
* If an Item does not have an individual page nothing is loaded.
    * E.g. "Chlorophyte Mask" should redirect to "Chlorophyte armor", but currently does not.
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