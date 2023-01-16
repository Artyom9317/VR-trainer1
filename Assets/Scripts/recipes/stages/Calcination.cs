using System;

namespace recipes.stages
{
    [Serializable]
    public class Calcination
    {
        public int temperature;
        public int timeInMinutes;

        public bool isStageOn;
    }
}