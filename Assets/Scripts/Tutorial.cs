using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public AssessmentController ass;
    public GameObject player;
    public GameObject Exit;
    public TextMeshProUGUI hint;
    public ConcreteMixerLogic MixerLogic;
    public FurnaceController fc;
    public GranulatorLogic GranLogic;
    public AudioSource aSource;
    private int step = 0;
    float myX, myY, myZ;

    private bool isSkipGran = false;
    void Start()
    {
        hint.text = "Подойдите к смесителю CM-160";
    }

    void Update()
    {
        FirstStep();
        ThirdStep();
        FourthStep();
        FifthStep();
        SixsStep();
        EightStep();
        NineStep();
    }
    
    public void FirstStep() 
    {
        GetPosition(player);

        if (step == 0 && myZ > 6.5 && myX < -3.5) //Подошли к гранулятору
        {
            hint.text = "Включите смеситель";
            aSource.Play();
            step++;
        }
    }

    public void SecondStep()
    {
        if (step == 1) //Включили смеситель(проверка на кнопке)
        {
            aSource.Play();
            hint.text = "Наполните нужными смесями и дождитесь завершения смешивания \n Затем выгрузите смесь";
            step++;
        }
    }

    public void ThirdStep()
    {
        if (step == 2 && MixerLogic.currentState == ConcreteMixerLogic.MixerState.MixDone) //Смесь готова
        {
            aSource.Play();
            hint.text = "Пересыпьте смесь, затем не забудьте выключить смеситель";
            step++;
        }
        
    }

    public void FourthStep()
    {
        if (step == 3) //Засыпали компоненты и дождались завершения смешивания
        {
            aSource.Play();
            hint.text = "Выключите смеситель";
            if (ass.currentRecipe.formStage.isStageOn)
			{
                step++;
                isSkipGran = false;
            } else
			{
                step = 8;
                isSkipGran = true;
			}
                
        }
    }
   
    public void FifthStep()
    {
        if (step == 4 ) // Высыпали смесь
        {
            aSource.Play();
            hint.text = "Отправляйтесь к гранулятору/экструдеру \n (прибор смотреть в рецепте)";
            step++;
        }
    }
    public void SixsStep()
    {
        GetPosition(player);

        if (step == 5 && myZ > 7 && myX > 4) //Подошли к гранулятору
        {
            hint.text = "Включите гранулятор/экструдер";
            aSource.Play();
            step++;
        }
    }

    public void SevenStep()
    {
        if (step == 6) //Включили гранулятор(проверка на кнопке)
        {
            aSource.Play();
            hint.text = "Засыпайте смесь";
            step++;
        }
    }
    public void EightStep()
    {
        if (step == 7 && GranLogic.granulatorState == GranulatorLogic.GranulatorState.MakingGranuls)
        {
            step++;
        }

        if (step == 8)
        {
            aSource.Play();
            if (isSkipGran)
			{
                hint.text = "Пересыпьте смесь в противень и отнесите их в печь";
            } else
			{
                hint.text = "Возьмите противень с гранулами и отнесите его в печь";
            }
            step++;
        }

    }

    public void NineStep()
    {
        GetPosition(player);
        if (step == 9 && myZ < 1 && myX > 4 && myZ > 0.5)
        {
            aSource.Play();
            hint.text = "Закройте печь, включите её (кнопка i - справка)";
            step++;
        }

    }

    public void TenStep()
    {
        if (step == 10)
        {
            aSource.Play();
            hint.text = "Установите необходимые параметры и проведите этапы работы с печью исходя из рецепта";
            step++;
        }
    }

    public void ElevenStep()
    {
        if (step > 10 && fc.currentStages.Count == 0)
        {
            aSource.Play();
            hint.text = "Всё готово. Выключите печь и положите противень на стол справа от печи";
            step++;
            Exit.SetActive(true);
        }
    }
    private void GetPosition(GameObject go)
    {       
        myX = go.transform.position.x;
        myY = go.transform.position.y;
        myZ = go.transform.position.z;      
    }
}

