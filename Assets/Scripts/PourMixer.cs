using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourMixer : MonoBehaviour
{
    [SerializeField] private float quantity;
    [SerializeField] private int pourTreshold;
    //[SerializeField] private Transform origin = null;
    [SerializeField] private ParticleSystem pourPS = null;
    [SerializeField] private GameObject fillingSystem;
    private bool isPouring = false;
    private bool isEmpty = true;
    private float secTime = 1;
    private int capPerTime;

    private void Start()
    {
        var main = pourPS.main;
        main.duration = (int)quantity;
    }

    private void Update()
    {
        bool pourCheck = PourAngle() < pourTreshold;

        if (isPouring != pourCheck && !isEmpty)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
        if (isPouring && !isEmpty)
        {
            quantity -= Time.deltaTime;
            secTime -= Time.deltaTime;
            if (secTime <=0)
            {
                capPerTime = fillingSystem.GetComponent<ConcreteMixerLogic>().GetCapacityPerTime();
                fillingSystem.GetComponent<ConcreteMixerLogic>().ChangeCurrentCapacity(-capPerTime);
                secTime = 1;
            }
            if (quantity <= 0)
            {
                fillingSystem.GetComponent<ConcreteMixerLogic>().ClearMixer();
                isEmpty = true;
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        pourPS.Play();
    }

    private void EndPour()
    {
        pourPS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private float PourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

    public void SetQuantity(float amount)
    {
        quantity = amount;
        isEmpty = false;
    }
}
