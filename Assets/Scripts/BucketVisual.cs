using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketVisual : MonoBehaviour
{
    private const int MAX_PERCENT = 100;
    private const float MAX_PERCENTf = 100f;

    [SerializeField] private Pourable bucket;
    [SerializeField] private GameObject movingPart;
    [SerializeField] private float minYPosition;
    [SerializeField] private float maxAddY;

    private void Update()
    {
        if (bucket.getCurrentQuantity() > 0)
        {
            movingPart.SetActive(true);
            var localPosition = movingPart.transform.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y,
                getNewZPosition());
            movingPart.transform.localPosition = localPosition;
        }
        else
        {
            movingPart.SetActive(false);
        }
    }

    private float getNewZPosition()
    {
        if (bucket.getCurrentQuantity() >= 10)
        {
            bucket.setQuantity(10);
            return movingPart.transform.localPosition.z;
        }

        var fillPercent = Convert.ToInt32(bucket.getCurrentQuantity()) * MAX_PERCENT / 10;
        return minYPosition + (fillPercent * maxAddY / MAX_PERCENTf);
    }
}
