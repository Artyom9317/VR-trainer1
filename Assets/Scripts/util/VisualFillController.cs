using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFillController : MonoBehaviour
{
    private const int MAX_PERCENT = 100;
    private const float MAX_PERCENTf = 100f;

    [SerializeField] private CompaunderWork compaunderWork;
    [SerializeField] private GameObject movingPart;
    [SerializeField] private float minYPosition;
    [SerializeField] private float maxAddY;

    private void Update()
    {
        if (compaunderWork.getCurrentCapacity() > 0)
        {
            movingPart.SetActive(true);
            var localPosition = movingPart.transform.localPosition;
            localPosition = new Vector3(localPosition.x, getNewYPosition(),
                localPosition.z);
            movingPart.transform.localPosition = localPosition;
        }
        else
        {
            movingPart.SetActive(false);
        }
    }

    private float getNewYPosition()
    {
        var fillPercent = compaunderWork.getCurrentCapacity() * MAX_PERCENT / compaunderWork.getMaxCapacity();
        return minYPosition + (fillPercent * maxAddY / MAX_PERCENTf);
    }
}