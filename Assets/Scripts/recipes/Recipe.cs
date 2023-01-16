using System;
using System.Collections;
using System.Collections.Generic;
using recipes.stages;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string title;
    public MixingStage firstMixingStage;
    public MixingStage secondMixingStage;
    public FormStage formStage;
    public DryingStage dryingStage;
    public Calcination calcinationStage;
    public CoolingDownStage coolingDownStage;
}
