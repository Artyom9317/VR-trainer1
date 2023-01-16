using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Xml.Schema;
using assessment.mistake;

public class FurnaceController : MonoBehaviour
{
    [SerializeField] private AssessmentController assessment;
 
    public enum FurnaceStages
	{
        DRYING,
        CALCINATION,
        COLLINGDOWN
	}

    public List<FurnaceStages> currentStages = new List<FurnaceStages>();
    
    public float timeStart = 20;
    public float speed = 5f;
    public Text timerText;
    public float time_h = 0;
    public float time_m = 0;
    public AudioSource Dinnn;
    public bool timeGO = false;

    public Text timeMenutxt;
    public int timeMenu = 0;

    public GameObject temp_control;
    public GameObject display;
    public Text temp_txt;
    public Text real_temp_txt;
    public Text comment;
    public AudioSource Tikkk;
    private int temp;
    public float real_temp = 20;
    public bool isOpen = true;
    public Text speedtext;

	public void loadRecipeSettings()
	{
        if (assessment.currentRecipe.dryingStage.isStageOn)
		{
            currentStages.Add(FurnaceStages.DRYING);
		}
        if (assessment.currentRecipe.calcinationStage.isStageOn)
		{
            currentStages.Add(FurnaceStages.CALCINATION);
		}
        if (assessment.currentRecipe.coolingDownStage.isStageOn)
        {
            currentStages.Add(FurnaceStages.COLLINGDOWN);
        }
    }

	void Update()
    {
        speedtext.text = "x" + speed;
        if (timeGO == true)
        {
            if (time_h >= 0)
            {
                if (time_m > 0)
                {
                    time_m -= Time.deltaTime * speed;
                }

                if (Mathf.Round(time_m) <= 0 && time_h > 0)
                {
                    time_h--;
                    time_m = 60;
                }
                timerText.text = time_h.ToString() + "ч. " + Mathf.Round(time_m).ToString() + "мин.";
                if (time_m <= 0 && time_h <= 0) Dinnn.Play();
            }
            if (Mathf.Round(time_m) == 0)
            {
                Dinnn.Play();
                timeGO = false;
            }
        }
        Chenge_Temp();
        Get_Time();
        Heating();
    }
    public void Chenge_Temp()
    {
       temp = (int)(-1*(temp_control.transform.forward.y * Mathf.Rad2Deg - 57))*25;       
       temp_txt.text = temp.ToString();
    }  
    public void Furnace_On()
    {
        if (isOpen == false)
        {
            Tikkk.Play();
            display.SetActive(true);
        }
        else
        {
            MakeOpenDoorMistake();
        }
    }
    public void Furnace_Off()
    {
        if (isOpen == false)
        {
            Tikkk.Play();
            display.SetActive(false);
        }
    }
    public void ChangeState()
    {
        if (isOpen)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }
    }
    public void Heating()
    {
        if (timeGO == true && temp > real_temp && timeMenu <= 0)
        {          
            real_temp += Time.deltaTime * speed * 140 / 60;
            real_temp_txt.text = Mathf.Round(real_temp).ToString() + " C";
        }    
        if(timeGO == true && temp <real_temp)
        {
            real_temp -= Time.deltaTime * speed * 140 / 60;
            real_temp_txt.text = Mathf.Round(real_temp).ToString() + " C";
        }
    }
    public void Get_Time()
    {
        if (timeMenu <= 0)
        {
            timeStart = Mathf.Abs(Mathf.Round(((Mathf.Round(temp) - Mathf.Round(real_temp)) / 140) * 60));
        }

    }

    public void Time_Plus()
    {
        timeMenu += 5;
        timeMenutxt.text = timeMenu.ToString() + "мин.";
    }

    public void Time_Minus()
    {
        timeMenu -= 5;
        timeMenutxt.text = timeMenu.ToString() + "мин.";
    }

    public void SpeedUp()
    {
        if (speed <= 60)
            speed += 5;
    }
    public void SpeedDown()
    {
        if (speed > 5)
            speed -= 5;
    }

    public void StartTime()
    {
        if (assessment.currentRecipe.formStage.isStageOn)
		{
            var mistake = new MistakeGranulator(MistakeType.EXTRUDER_SKIP_MISTAKE, "Пропущена стадия формования", 0);
            if (!assessment.HasMistake(mistake)) assessment.AddMistake(mistake);
		}
        if (currentStages.Count > 0)
		{
            var stage = currentStages[0];
            currentStages.RemoveAt(0);
            switch (stage)
			{
                case FurnaceStages.DRYING:
                    if (timeMenu != assessment.currentRecipe.dryingStage.timeInMinutes)
					{
                        var furMistake = new MistakeFurnace(MistakeType.FURNACE_DRYING_WRONG_TIME_MISTAKE, "Неверное время для этапа 'Сушка' ", 0);
                        if (!assessment.HasMistake(furMistake)) assessment.AddMistake(furMistake);
					}
                    if (temp != assessment.currentRecipe.dryingStage.temperature)
					{
                        var furMistake = new MistakeFurnace(MistakeType.FURNACE_DRYING_WRONG_TEMP_MISTAKE, "Неверное температура для этапа 'Сушка' ", 0);
                        if (!assessment.HasMistake(furMistake)) assessment.AddMistake(furMistake);
                    }
                    break;
                case FurnaceStages.CALCINATION:
                    if (timeMenu != assessment.currentRecipe.calcinationStage.timeInMinutes)
                    {
                        var furMistake = new MistakeFurnace(MistakeType.FURNACE_CALCINATION_WRONG_TIME_MISTAKE, "Неверное время для этапа 'Прокаливание' ", 0);
                        if (!assessment.HasMistake(furMistake)) assessment.AddMistake(furMistake);
                    }
                    if (temp != assessment.currentRecipe.calcinationStage.temperature)
                    {
                        var furMistake = new MistakeFurnace(MistakeType.FURNACE_CALCINATION_WRONG_TEMP_MISTAKE, "Неверное температура для этапа 'Прокаливание' ", 0);
                        if (!assessment.HasMistake(furMistake)) assessment.AddMistake(furMistake);
                    }
                    break;
                case FurnaceStages.COLLINGDOWN:
                    if (timeMenu != assessment.currentRecipe.coolingDownStage.timeInMinutes)
                    {
                        var furMistake = new MistakeFurnace(MistakeType.FURNACE_COOLINGDOWN_WRONG_TIME_MISTAKE, "Неверное время для этапа 'Остывание' ", 0);
                        if (!assessment.HasMistake(furMistake)) assessment.AddMistake(furMistake);
                    }
                    if (temp != assessment.currentRecipe.coolingDownStage.temperature)
                    {
                        var furMistake = new MistakeFurnace(MistakeType.FURNACE_COOLINGDOWN_WRONG_TEMP_MISTAKE, "Неверное температура для этапа 'Остывание' ", 0);
                        if (!assessment.HasMistake(furMistake)) assessment.AddMistake(furMistake);
                    }
                    break;
            }
		}

        Debug.Log("Time menu = " + timeMenu);
        Debug.Log("Time start = " + temp);

        if (timeGO == false && timeMenu == 0)
        {
            comment.text = "Нагревание (140 С/ч)";
            time_m = timeStart;
            while (time_m >= 60)
            {
                time_h++;
                time_m -= 60;
            }
            timerText.text = time_h.ToString() + "ч. " + Mathf.Round(time_m).ToString() + "мин.";
            timeGO = true;
        }
        if (timeGO == false && timeMenu >= 0)
        {
            comment.text = "Удержание температуры";
            time_m = timeMenu;            
            while (time_m >= 60)
            {
                time_h++;
                time_m -= 60;
            }
            timerText.text = time_h.ToString() + "ч. " + Mathf.Round(time_m).ToString() + "мин.";
            timeGO = true;
            for (int i = 0; i < 1; i++) timeMenu = 0;
        }
    }

    private void MakeOpenDoorMistake()
    {
        var mistake = new MistakeFurnace(
            MistakeType.FURNACE_OPEN_DOOR_MISTAKE,
            StringConstants.FURNACE_OPEN_DOOR_MISTAKE,
            MistakeWeight.LIGHT);
        if (assessment.HasMistake(mistake)) return;
        assessment.AddMistake(mistake);
    }
}

