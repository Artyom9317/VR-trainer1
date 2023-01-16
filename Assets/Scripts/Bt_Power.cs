using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bt_Power : MonoBehaviour
{
    public AudioSource aSource;
    public bool state = false;
    public GameObject button;
    public Quaternion minAngle;
    public Quaternion maxAngle;

    void Start()
    {
        
    }

    public void Furnace_On()
    {
        if (state == false)
        {
            aSource.Play();
            state = true;
        }

    }
    public void Furnace_Off()
    {
        if (state == true)
        {
            aSource.Play();
            state = false;
        }
        

    }

    void Update()
    {
       
    }
}
