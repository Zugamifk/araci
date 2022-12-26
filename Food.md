# Cooking Methods
Cooking methods are command objects witha  single method, Cook(). Cooking affect the CookState of an item and possibly other properties as well.

## Roasting
A slow cooking method using ambient heat. cooks outside and inside fairly evenly. Can dry out food.
Roast any cookable.
Cooking and moisture interact when roasting.

Raw + Dry => Cooked + Dry
Raw + Moist => Cooked + Moist
Raw + Saturated => Cooked + Saturated

Cooked + Dry => Burnt + Dry
Cooked + Moist => Cooked + Dry
Cooked + Saturated => Cooked + Moist

Burnt + Dry => Burnt + Dry
Burnt + Moist => Burnt + Dry
Burnt + Saturated => Burnt + Moist

## Boiling
Boiling involves cooking ingredients in a pot of boiling water. This at least a container of water and a heat source. Ingredients can be added to the water before or after boiling.
Any cookable can be boiled.
Boiling increases moisture.
Boiling can not burn food.