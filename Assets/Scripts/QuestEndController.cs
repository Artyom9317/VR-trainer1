using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class QuestEndController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mistakesText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI mistakesCountText;
    
    [SerializeField] private AssessmentController _assessment;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("tray") 
            && other.gameObject.GetComponent<Tray>().getCurrentCapacity() > 1)
        {
            ClearResultTexts();
            ShowResults();
        }
    }

    private void ShowResults()
    {
        var mistakes = _assessment.GetMistakes();
        var totalScore = _assessment.GetCurrentScore();
        var mistakesCount = mistakes.Count;

        foreach (var mistake in mistakes)
        {
            mistakesText.text += "[" + mistake.title + "] - " + mistake.message + "\n";
        }

        totalScoreText.text = totalScore.ToString(CultureInfo.InvariantCulture);
        mistakesCountText.text = mistakesCount.ToString();
    }

    private void ClearResultTexts()
    {
        mistakesText.text = "";
        totalScoreText.text = "";
        mistakesCountText.text = "";
    }
}
