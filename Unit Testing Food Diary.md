Create GetFood(), test name and amount

Create EatFood(), test amount is valid, amount is modified

Next step, want to start cooking food. How to start? Create CookService? Create a basic cooking procedure to run like boil., roast, etc.? No. CookFood(). Add FoodModel.IsRaw. Test inital and value and after cooking.

Made ICookable. got rid of it

got rid of CookFood

creating ILiquid. LiquidState.

readded ICookable

Create Heat methods with math to calculate heating:
* Temp increases by Time * HeatDifferential * FoodHeatTransferRate / Density
* Then, if above minimum cooking temp, calculate "cooked" amount by Time * (FoodTemp - MinCookTemp) * CookRate / (1 + FoodSolidity) 

Create FoodModel, move IFood to Core. Create Data. Create UI to test out heating different types of food

realize this physics based approach is going to be hard to configure to feel right, is too abstract, and will be slow to work on.

Scrap it all

Start working on new system with explicit enum values with names instead of calculating mathematically. Treat cooking like a state machine.