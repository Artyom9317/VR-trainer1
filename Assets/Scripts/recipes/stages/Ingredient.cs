using System;

namespace recipes.stages
{
    [Serializable]
    public class Ingredient
    {
        public string fullName;
        public int amount;
        public IngredientType type;
    }

    [Serializable]
    public enum IngredientType
    {
        DRY_1,
        DRY_2,
        DRY_3,
        FLUID_1,
        FLUID_2,
        FLUID_3
    }
}