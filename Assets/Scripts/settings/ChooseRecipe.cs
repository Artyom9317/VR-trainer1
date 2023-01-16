using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRecipe : MonoBehaviour
{
    public List<Recipe> recipes;
    public TMP_Dropdown dropd;

    private void Start()
    {
        dropd.ClearOptions();
        foreach (var recipe in recipes)
        {
            dropd.options.Add(new TMP_Dropdown.OptionData() {text = recipe.title});
        }
        
        dropd.value = 1;
        dropd.value = 0;
    }
}