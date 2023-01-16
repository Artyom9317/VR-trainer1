using System.Collections;
using System.Collections.Generic;
using assessment.mistake;
using UnityEngine;

public class FillBlocker : MonoBehaviour
{
    [SerializeField] private AssessmentController assessment;

    public void MakeFillMistake()
    {
        var fillMistake =
            new MistakeCM160(
                MistakeType.CONCRETE_MIXER_DISABLE_FILL_MISTAKE,
                StringConstants.CONCRETE_MIXER_DISABLE_FILL_MISTAKE,
                MistakeWeight.LIGHT);
        if (assessment.HasMistake(fillMistake)) return;
        assessment.AddMistake(fillMistake);
    }
}