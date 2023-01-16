using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayVisual : MonoBehaviour
{
    private const int MAX_PERCENT = 100;
    private const float MAX_PERCENTf = 100f;

    [SerializeField] private Tray tray;
    [SerializeField] private GameObject movingPart;
    [SerializeField] private float minYPosition;
    [SerializeField] private float maxAddY;

    private void Update()
    {
        if (tray.getCurrentCapacity() > 0)
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
        var fillPercent = tray.getCurrentCapacity() * MAX_PERCENT / tray.getMaxCapacity();
        return minYPosition + (fillPercent * maxAddY / MAX_PERCENTf);
    }
}
