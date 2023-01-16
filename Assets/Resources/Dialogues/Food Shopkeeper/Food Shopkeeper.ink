INCLUDE ../Globals.ink

-> FoodSeler

=== FoodSeler ===
What it is your business here?
    + [I would like to buy some clothes.]
            ->OpenBuyScreen("0")
  
    + [Forget about, I better get going.]
            ->ExitDialogue("1")
    
=== OpenBuyScreen(choice)===
~ choiceMade = choice
This is what I have. (BUY-SHOP-UI CALLED!)
-> END
=== ExitDialogue(choice) ===
~ choiceMade = choice
OK. Have a nice day!
->END