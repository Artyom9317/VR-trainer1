using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteMixerController : MonoBehaviour
{
    [SerializeField] private GameObject ConcreteMixer;
    [SerializeField] private GameObject MiniGear;
    [SerializeField] private GameObject Subst;
    [SerializeField] private GameObject CanFill;
    [SerializeField] private GameObject blocker;
    public AudioSource aSource;
    private bool isMixerOn = false;

    private void Update()
    {
        if (isMixerOn)
        {
            ConcreteMixer.transform.Rotate(new Vector3(0,0,1), 180 * Time.deltaTime, Space.Self);
            MiniGear.transform.Rotate(new Vector3(1,0,0), 1980 * Time.deltaTime, Space.Self);
            Subst.transform.Rotate(new Vector3(0, 1, 0), 100 * Time.deltaTime, Space.Self);
        }
    }

    public void StartMixer()
    {
        if (!isMixerOn)
        {
            CanFill.SetActive(true);
            blocker.SetActive(false);
            //CanFill.GetComponent<BoxCollider>().enabled = true;
            //blocker.GetComponent<BoxCollider>().enabled = false;
            isMixerOn = true;
            aSource.Play();
        }
    }

    public void StopMixer()
    {
        if (isMixerOn)
        {
            CanFill.SetActive(false);
            blocker.SetActive(true);
            //CanFill.GetComponent<BoxCollider>().enabled = false;
            //blocker.GetComponent<BoxCollider>().enabled = true;
            isMixerOn = false;
            aSource.Stop();
        }
    }
}
