using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranulatorLogic : MonoBehaviour
{
    public enum GranulatorState
    {
        Idle,
        MakingGranuls
    }
    
    public AssessmentController ass;

    [SerializeField] private GranulatorController granulatorController;
    [SerializeField] private ParticleSystem granuls;
    [SerializeField] private int CapacityPerTime;

    [SerializeField] public GranulatorState granulatorState = GranulatorState.Idle;
    private float _minFillTime = 0;
    private float _fillTimer = 1;
    [SerializeField] private int _currentCapacity = 0;

    [SerializeField] private Text debugText; // DEBUG INFO

    private void Update()
    {
        if (granulatorController.isOn() 
            && granulatorState == GranulatorState.Idle 
            && _currentCapacity > 0)
        {
            granulatorState = GranulatorState.MakingGranuls;
            if (!granuls.isPlaying) granuls.Play();
            StartCoroutine(MakeGranuls());
        }
    }

    private void FixedUpdate()
    {
        if (_minFillTime > 0)
        {
            _minFillTime -= Time.fixedDeltaTime;
            _fillTimer -= Time.deltaTime;
       
            if (_fillTimer <= 0)
            {
                _currentCapacity += CapacityPerTime;
                _fillTimer = 1;
            }
        }
    }

    public void Fill(GameObject particle)
    {
        ass.currentRecipe.formStage.isStageOn = false;
        if (particle.CompareTag("CM160_Mix"))
        {
            _minFillTime = 1;
        }
    }

    private IEnumerator MakeGranuls()
    {
        while (_currentCapacity > 0)
        {
            if (!granulatorController.isOn())
            {
                granuls.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
                granulatorState = GranulatorState.Idle;
                break;
            }
            yield return new WaitForSeconds(2);
            _currentCapacity--;
        }

        if (_currentCapacity <= 0)
        {
            _currentCapacity = 0;
            granulatorState = GranulatorState.Idle;
            granuls.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    //private void ShowDebug()
    //{
    //    debugText.GetComponent<Text>().text = "";
    //    foreach (KeyValuePair<string, int> entry in Components)
    //    {
    //        debugText.GetComponent<Text>().text += entry.Key + " : " + entry.Value + " ml\n";
    //    }
    //    debugText.GetComponent<Text>().text += "Total: " + currentCapacity + " / " + maxCapacity + "\n";
    //    debugText.GetComponent<Text>().text += "Current State: " + currentState + "\n";
    //}
}
