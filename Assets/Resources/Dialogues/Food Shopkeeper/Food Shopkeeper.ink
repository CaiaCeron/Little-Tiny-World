INCLUDE ../Globals.ink

-> FoodSeler

=== FoodSeler ===
Hello sweet, what can I do for?
    + [I would like to buy deserts.]
            ->OpenBuyScreen("0")
  
    + [Forget about, I better get going.]
            ->ExitDialogue("1")
    
=== OpenBuyScreen(choice)===
~ choiceMade = choice
Take a look at my gorgeous deserts.
-> END
=== ExitDialogue(choice) ===
~ choiceMade = choice
OK. Have a nice day!
->END