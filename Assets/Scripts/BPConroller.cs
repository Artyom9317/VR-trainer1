using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPConroller : MonoBehaviour
{
    [SerializeField] private GameObject Rotor;
    [SerializeField] private GameObject Fan;
    [SerializeField] private GameObject CanFill;
    [SerializeField] private GameObject Blocker;
    public AudioSource aSource;
    private bool isMixerOn = false;
    public bool isGridOpen = true;
    public bool isBPOpen = false;

    private void Update()
    {
        if (isMixerOn)
        {
            Rotor.transform.Rotate(new Vector3(1, 0, 0), 180 * Time.deltaTime, Space.Self);
            Fan.transform.Rotate(new Vector3(1, 0, 0), 1980 * Time.deltaTime, Space.Self);           
        }
    }

    public void StartMixer()
    {
        if (!isMixerOn)
        {
            CanFill.SetActive(true);
            Blocker.SetActive(false);
            isMixerOn = true;
            aSource.Play();
        }
    }

    public void StopMixer()
    {
        if (isMixerOn)
        {
            CanFill.SetActive(false);
            Blocker.SetActive(true);
            isMixerOn = false;
            aSource.Stop();
        }
    }

    public void ChangeStateGrid()
    {
        if (isGridOpen == true)
        {
            isGridOpen = false;
        }
        else
        {
            isGridOpen = true;
        }
    }
    public void ChangeStateBP()
    {
        if (isBPOpen == true)
        {
            isBPOpen = false;
        }
        else
        {
            isBPOpen = true;
        }
    }
}
