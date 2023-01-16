using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketVisualSubst : MonoBehaviour
{
    public ParticleSystem myPs;
    public GameObject visualFill;
    
    public void updatePSMaterial(GameObject ps)
    {
        myPs.GetComponent<ParticleSystemRenderer>().material.color = ps.GetComponent<ParticleSystemRenderer>().material.color;
        visualFill.GetComponent<MeshRenderer>().material.color = ps.GetComponent<ParticleSystemRenderer>().material.color;
    }
}
