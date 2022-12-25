##### Name
A unique identifer for a food item of this type.

##### Item Name
A more descriptive name describing unique properties of this item, its temperature, some of the ingredients, if it is cooked or raw, etc.

##### Weight
The weight of this item in grams.

##### Volume
The volume of this item in millilitres.

##### Temperature
The volume of this item in celsius.
Temperature is applied over time externally.
	* Currently no different between directional, convective, or ambient heat transfer
	* Heat increases by 1 Degree / (1 + SolidPercent) / Second. Fully solid items heat up at half the speed of liquid items. (should consider a nonlinear curve, anyliquid is much more influential than none at all)

##### Heat Transfer Rate
How fast heat transfers into the item. This value is multiplied with the solid percent to calculate how long it takes to raise the temperature of this item by 1 degree.

##### Solid Percent
How much of this food item is solid, in percent. The rest is liquid.

##### Cook Temperature
The temperature this needs to reach before becoming cooked. Once cooked, its temperature can drop again and it will stay cooked.

##### Cooked Percent
How much of this item is cooked, in percent.

##### Cook Rate
How quickly this items cooks. Hotter items cook faster. Cook percent increases as a rate of time, divided by the volume. Cooked Percent += Cook Rate * Time * (Temperature - Cook Temperature)  / Volume

## Cooking
Cooking is done by applying heat to a food item over time.
Density is a factor used for calculatig heating and cook rates, as Volume / Weight.
* 