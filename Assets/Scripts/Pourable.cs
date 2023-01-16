using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pourable : MonoBehaviour
{
    [SerializeField] private float quantity;
    [SerializeField] private int pourTreshold;
    //[SerializeField] private Transform origin = null;
    public ParticleSystem pourPS = null;
    private bool isPouring = false;
    private bool isEmpty;

    private void Start()
    {
        if (quantity <= 0)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
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
            if (quantity <= 0)
            {
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
        return Mathf.Abs(transform.forward.y * Mathf.Rad2Deg);
    }
    
    public void AddQuantity(float amount)
    {
        quantity += amount;
        isEmpty = false;
    }

    public float getCurrentQuantity()
    {
        return quantity;
    }

    public void setQuantity(float amount)
    {
        quantity = amount;
    }
}
