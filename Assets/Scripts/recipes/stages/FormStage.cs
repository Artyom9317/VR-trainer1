using System;

namespace recipes.stages
{
    [Serializable]
    public class FormStage
    {
        public int nozzle;
        public FormDevice formDevice;
        
        public bool isStageOn;
    }

    [Serializable]
    public enum FormDevice
    {
        GRANULATOR,
        EXTRUDER
    }
}