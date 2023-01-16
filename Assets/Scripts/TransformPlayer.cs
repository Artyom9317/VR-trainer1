using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlayer : MonoBehaviour
{
    public GameObject obj;
    public int distance;

    public ChooseRecipe cr;
    public AssessmentController ass;
    public ConcreteMixerLogic cm;
    public ConcreteMixerLogic cm2;
    public FurnaceController fc;

    public void OnButtonDown()
    {
        ass.currentRecipe = cr.recipes[cr.dropd.value];
        ass.DrawRecipe();
        cm.LoadRecipeCompSeq();
        cm2.LoadRecipeCompSeq();
        fc.loadRecipeSettings();
        obj.transform.position = new Vector3(-6, 0, 0);
    }

    public void OutButtonDown()
    {
        //ass.currentRecipe = cr.recipes[cr.dropd.value];
        //Debug.Log(cr.dropd.value);
        obj.transform.position = new Vector3(-16, 0, 0);
    }
}

