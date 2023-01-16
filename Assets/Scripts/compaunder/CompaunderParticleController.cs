using System.Collections;
using System.Collections.Generic;
using compaunder;
using UnityEngine;

public class CompaunderParticleController : MonoBehaviour
{
    [SerializeField] private CompaunderController compaunderController;
    [SerializeField] private CompaunderWork compaunderWork;
    [SerializeField] private ParticleSystem granuls;
    [SerializeField] private ParticleSystem noodles;
    void Update()
    {
        if (compaunderWork.getCurrentCapacity() > 0 && !granuls.isPlaying)
        {
            StartCoroutine(makeGranuls());
        }
    }
    
    private IEnumerator makeGranuls()
    {
        granuls.Play();
        noodles.Play();
        while (compaunderWork.getCurrentCapacity() > 0 && compaunderController.toggleState)
        {
            yield return new WaitForSeconds(2);
            compaunderWork.addCapacity(-compaunderWork.getCapacityPerTime());
        }
        granuls.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
        noodles.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
    }
}
