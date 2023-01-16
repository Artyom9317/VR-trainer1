using System.Collections.Generic;
using System.Globalization;
using compaunder;
using TMPro;
using UnityEngine;

public class CompaunderUIRenderer : MonoBehaviour
{
    private const string CELSIUS = "C";
    
    [SerializeField] private CompaunderController compaunderController;
    [SerializeField] private TextMeshProUGUI temperatureUIText;
    [SerializeField] private TextMeshProUGUI augerSpeedUIText;

    private void Update()
    {
        temperatureUIText.text = compaunderController.getCurrentTemperature() + CELSIUS;
        augerSpeedUIText.text = compaunderController.getCurrentAugerSpeed().ToString(CultureInfo.InvariantCulture);
    }
}