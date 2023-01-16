using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranulatorController : MonoBehaviour
{
    [SerializeField] private GameObject[] RotateParts;
	[SerializeField] private AudioSource Audio;
    [SerializeField] private GameObject FillArea;
	private bool isGranulatorOn;
	
	private void Update()
    {
        if (isGranulatorOn)
        {
            foreach(var part in RotateParts)
			{
				part.transform.Rotate(new Vector3(0,1,0), 500 * Time.deltaTime, Space.Self);
			}
        }
    }
	
	public void StartGranulator()
	{
		if (!isGranulatorOn)
		{
			isGranulatorOn = true;
			Audio.Play();
            FillArea.SetActive(true);
		}
	}
	
	public void StopGranulator()
	{
		if (isGranulatorOn)
		{
			isGranulatorOn = false;
			Audio.Stop();
            FillArea.SetActive(false);
        }
	}

	public bool isOn()
	{
		return isGranulatorOn;
	}
}
