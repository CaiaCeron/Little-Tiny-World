INCLUDE ../Globals.ink

-> ClotheSeler

=== ClotheSeler ===
What it is your business here?
    + [I would like to buy some clothes.]
            ->OpenBuyScreen("0")
  
    + [Forget about, I better get going.]
            ->ExitDialogue("1")
    
=== OpenBuyScreen(choice)===
~ choiceMade = choice
Take a look at my collection.
-> END
=== ExitDialogue(choice) ===
~ choiceMade = choice
OK. bye bye for now kid.
->END