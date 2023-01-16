using System.Collections;
using compaunder;
using UnityEngine;
using util;

public class CompaunderWork : MonoBehaviour
{
    public AssessmentController ass;
    [SerializeField] private int capacityPerTime;
    [SerializeField] private int maxCapacity;
    private int currentCapacity;
    private FillTimer fillTimer;

    private void Start()
    {
        fillTimer = new FillTimer(1, 0);
    }

    private void Update()
    {
        fillTimer.UpdateFillingTimers(Time.deltaTime);
        currentCapacity = fillTimer.isNeedAddValue() ? currentCapacity + capacityPerTime : currentCapacity + 0;
    }

    public void FillHopper(GameObject particle)
    {
        ass.currentRecipe.formStage.isStageOn = false;
        fillTimer.fillingTimer = 1;
    }

    public int getCurrentCapacity()
    {
        return currentCapacity;
    }

    public void addCapacity(int value)
    {
        currentCapacity += value;
    }

    public int getMaxCapacity()
    {
        return maxCapacity;
    }

    public int getCapacityPerTime()
    {
        return capacityPerTime;
    }
}