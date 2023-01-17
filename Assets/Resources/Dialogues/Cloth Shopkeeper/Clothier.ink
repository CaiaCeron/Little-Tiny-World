INCLUDE ../Globals.ink

-> ClotheSeler

=== ClotheSeler ===
Hello young one, I have best outfits in the town. Do you wanna take look?
    + [Of course, show me your collectionn.]
            ->OpenBuyScreen("0")
  
    + [Forget about, I better get going.]
            ->ExitDialogue("1")
    
=== OpenBuyScreen(choice)===
~ choiceMade = choice
Suit your self.
-> END
=== ExitDialogue(choice) ===
~ choiceMade = choice
OK. bye bye for now kid.
->END