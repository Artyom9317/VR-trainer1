using System;
using UnityEngine;

namespace compaunder
{
    public class CompaunderController : MonoBehaviour, IMachineToggle
    {

        [SerializeField] private GameObject temperatureTumbler;
        [SerializeField] private GameObject temperatureAugerSpeedTumbler;
        
        public bool toggleState { get; set; }
        private float currentTemperature;
        private float currentAugerSpeed;

        private void Update()
        {
            currentTemperature = Mathf.Round(temperatureTumbler.transform.localEulerAngles.y);
            currentAugerSpeed = Mathf.Round(temperatureAugerSpeedTumbler.transform.localEulerAngles.y);
        }

        public void Toggle()
        {
            toggleState = !toggleState;
        }

        public float getCurrentTemperature()
        {
            return currentTemperature;
        }

        public float getCurrentAugerSpeed()
        {
            return currentAugerSpeed;
        }
    }
}
