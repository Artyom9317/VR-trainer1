using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionController : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "CMFillArea")
        {
            other.GetComponent<ConcreteMixerLogic>().Fill(gameObject);
        }
        else if (other.tag == "GranulatorFillArea")
        {
            //other.GetComponent<GranulatorLogic>().Fill(gameObject);
            other.transform.parent.GetComponent<GranulatorLogic>().Fill(gameObject);
        }
        else if (other.tag == "FillAreaObjects")
        {
            other.GetComponent<Fill>().StartFill();
        }
        else if (other.tag == "CompaunderFillArea")
        {
            other.GetComponent<CompaunderWork>().FillHopper(gameObject);
        }
        else if (other.tag == "tray")
        {
            other.GetComponent<Tray>().Fill();
        }
        else if (other.tag == "FillBlocker")
        {
            other.GetComponent<FillBlocker>().MakeFillMistake();
        }
    }
}