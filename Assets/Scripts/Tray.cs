using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class Tray : MonoBehaviour
{
    [SerializeField] private int capacityPerTime;
    [SerializeField] private int currentCapacity;
    [SerializeField] private int maxCapacity;
    private FillTimer fillTimer;
    void Start()
    {
        fillTimer = new FillTimer(1, 0);
    }
    //0.000224 //0.00007
    void Update()
    {
        fillTimer.UpdateFillingTimers(Time.deltaTime);
        currentCapacity = fillTimer.isNeedAddValue() ? currentCapacity + capacityPerTime : currentCapacity + 0;
        if (currentCapacity >= maxCapacity) currentCapacity = maxCapacity;
    }

    public int getCurrentCapacity()
    {
        return currentCapacity;
    }

    public int getMaxCapacity()
    {
        return maxCapacity;
    }

    public void Fill()
    {
        fillTimer.fillingTimer = 1;
    }
}
