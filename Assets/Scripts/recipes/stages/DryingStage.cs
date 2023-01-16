using System;

namespace recipes.stages
{
    [Serializable]
    public class DryingStage
    {
        public int temperature;
        public int timeInMinutes;
        
        public bool isStageOn;
    }
}