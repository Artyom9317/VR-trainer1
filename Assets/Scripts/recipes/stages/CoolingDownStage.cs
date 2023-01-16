using System;

namespace recipes.stages
{
    [Serializable]
    public class CoolingDownStage
    {
        public int temperature;
        public int timeInMinutes;
        
        public bool isStageOn;
    }
}