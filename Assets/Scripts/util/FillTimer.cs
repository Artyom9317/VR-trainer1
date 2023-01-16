using UnityEngine;

namespace util
{
    public class FillTimer
    {
        public float addValueTimer;
        public float fillingTimer;

        public FillTimer(float addValueTime, float fillingTime)
        {
            addValueTimer = addValueTime;
            fillingTimer = fillingTime;
        }

        public void UpdateFillingTimers(float pastTime)
        {
            if (fillingTimer <= 0) return;

            addValueTimer -= pastTime;
            fillingTimer -= pastTime;
        }

        public bool isNeedAddValue()
        {
            if (addValueTimer > 0) return false;
            
            addValueTimer = 1;
            return true;
        }
    }
}