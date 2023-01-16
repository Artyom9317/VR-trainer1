using UnityEngine.Experimental.XR.Interaction;

namespace compaunder
{
    public interface IMachineToggle
    {
        bool toggleState { get; set; }
        void Toggle();
    }
}