using System;
using System.Collections;
using System.Collections.Generic;
using assessment.mistake;
using recipes.stages;
using TMPro;
using UnityEngine;

public class AssessmentController : MonoBehaviour
{
    private const double ASSESSMENT_MAX_SCORE = 5.0;
    private const double ASSESSMENT_MIN_SCORE = 2.0;

    private List<IMistake> _mistakes = new List<IMistake>();
    private double _currentScore = ASSESSMENT_MAX_SCORE;
    
    public Recipe currentRecipe;
    public TextMeshProUGUI recipeOverview;


    public void DrawRecipe()
	{
        recipeOverview.text += "\n" + currentRecipe.title;
        recipeOverview.text += "\n" + "Смеситель:" + "\n";
        var compObj = "";
        foreach (var ingr in currentRecipe.firstMixingStage.ingredients)
        {
            recipeOverview.text += ingr.fullName + " " + ingr.amount + " у.е.";
            switch (ingr.type)
            {
                case IngredientType.DRY_1:
                    compObj = "Сухой компонент №1";
                    break;
                case IngredientType.DRY_2:
                    compObj = "Сухой компонент №2";
                    break;
                case IngredientType.DRY_3:
                    compObj = "Сухой компонент №3";
                    break;
                case IngredientType.FLUID_1:
                    compObj = "Жидкий компонент №1";
                    break;
                case IngredientType.FLUID_2:
                    compObj = "Жидкий компонент №2";
                    break;
                case IngredientType.FLUID_3:
                    compObj = "Жидкий компонент №3";
                    break;
            }
            recipeOverview.text += "(" + compObj + ")" + "\n";
        }
        if (currentRecipe.secondMixingStage.isStageOn)
		{
            foreach (var ingr in currentRecipe.secondMixingStage.ingredients)
            {
                recipeOverview.text += ingr.fullName + " " + ingr.amount + " у.е.";
                switch (ingr.type)
                {
                    case IngredientType.DRY_1:
                        compObj = "Сухой компонент №1";
                        break;
                    case IngredientType.DRY_2:
                        compObj = "Сухой компонент №2";
                        break;
                    case IngredientType.DRY_3:
                        compObj = "Сухой компонент №3";
                        break;
                    case IngredientType.FLUID_1:
                        compObj = "Жидкий компонент №1";
                        break;
                    case IngredientType.FLUID_2:
                        compObj = "Жидкий компонент №2";
                        break;
                    case IngredientType.FLUID_3:
                        compObj = "Жидкий компонент №3";
                        break;
                }
                recipeOverview.text += "(" + compObj + ")" + "\n";
            }
        }
        var overallTime = currentRecipe.firstMixingStage.timeInMinutes + currentRecipe.secondMixingStage.timeInMinutes;
        recipeOverview.text += "Время смешивания: " + overallTime + "мин. \n";
        if (currentRecipe.formStage.isStageOn)
        {
            recipeOverview.text += "Формование:" + "\n";
            recipeOverview.text += "Оборудование - " + currentRecipe.formStage.formDevice + "\n";
        }

        if (currentRecipe.dryingStage.isStageOn)
        {
            recipeOverview.text += "Сушка: " + "\n";
            recipeOverview.text += "Температура: " + currentRecipe.dryingStage.temperature + " Время: " +
                                   currentRecipe.dryingStage.timeInMinutes + "мин." + "\n";
        }
        if (currentRecipe.calcinationStage.isStageOn)
        {
            recipeOverview.text += "Прокаливание: " + "\n";
            recipeOverview.text += "Температура: " + currentRecipe.calcinationStage.temperature + " Время: " +
                                   currentRecipe.calcinationStage.timeInMinutes + "мин." + "\n";
        }
        if (currentRecipe.coolingDownStage.isStageOn)
        {
            recipeOverview.text += "Остывание: " + "\n";
            recipeOverview.text += "Температура: " + currentRecipe.coolingDownStage.temperature + " Время: " +
                                   currentRecipe.coolingDownStage.timeInMinutes + "мин." + "\n";
        }
    }

    public List<IMistake> GetMistakes()
    {
        return _mistakes;
    }

    public void AddMistake(IMistake mistake)
    {
        _mistakes.Add(mistake);
        if (_currentScore > ASSESSMENT_MIN_SCORE) RecalculateScoreByMistakes(mistake.weight);
    }

    public bool HasMistake(IMistake mistake)
    {
        return _mistakes.Exists(x => x.type == mistake.type);
    }

    private void RecalculateScoreByMistakes(double score)
    {
        _currentScore -= score;
    }

    public double GetCurrentScore()
    {
        return _currentScore;
    }
}