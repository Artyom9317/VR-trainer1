using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWheel : MonoBehaviour
{
    [SerializeField] private GameObject container;
    private float currentZAngle;
    private float startContainerZAngle;

    private void Start()
    {
        currentZAngle = transform.rotation.eulerAngles.z;
        startContainerZAngle = container.transform.eulerAngles.z;
    }

    private void Update()
    {
        if (currentZAngle != transform.rotation.eulerAngles.z)
        {
            WheelRotation(transform.rotation.eulerAngles.z);
        }
    }

    private void WheelRotation(float newZAngle)
    {
        Vector3 newContainerAngles = new Vector3(container.transform.eulerAngles.x, container.transform.eulerAngles.y, startContainerZAngle - newZAngle);
        container.transform.eulerAngles = newContainerAngles;
        currentZAngle = newZAngle;
    }
}
