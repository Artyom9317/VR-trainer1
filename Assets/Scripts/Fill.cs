using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill : MonoBehaviour
{
    private float minFillTime = 0;
    private float fillTimer = 1;
    [SerializeField] GameObject mainObject;

    private void Update()
    {
        if (minFillTime > 0)
        {
            minFillTime -= Time.fixedDeltaTime;
            fillTimer -= Time.deltaTime;

            if (fillTimer <= 0)
            {
                mainObject.GetComponent<Pourable>().AddQuantity(1);
                fillTimer = 1;
            }
        }
    }
    public void StartFill()
    {
        minFillTime = 1;
    }
}
