using System;

namespace recipes.stages
{
    [Serializable]
    public class MixingStage
    {
        public Ingredient[] ingredients;
        public int timeInMinutes;
        
        public bool isStageOn;
    }
}