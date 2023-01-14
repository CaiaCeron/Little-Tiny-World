-> ClotheSeler

=== ClotheSeler ===
What it is your business here?
    + [I want to buy some clothes.]
            ->OpenBuyScreen
    + [I want to sell some of my clothes.]
        
            ->OpenSellScreen
    + [Forget about, I better get going.]
            ->ExitDialogue
    
=== OpenBuyScreen ===
This is what I have. (BUY-SHOP-UI CALLED!)
-> END
=== OpenSellScreen ==
Show me what you got.(SELL-SHOP-UI CALLED!)
-> END
=== ExitDialogue ===
Don't waste my time kid!!!
->END