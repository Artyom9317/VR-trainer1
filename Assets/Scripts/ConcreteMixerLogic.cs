using System.Collections;
using System.Collections.Generic;
using System.Linq;
using assessment.mistake;
using recipes.stages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConcreteMixerLogic : MonoBehaviour
{
    public enum MixerState
    {
        AddingComponents,
        MixingComponents,
        MixDone,
        MixFail
    }

    [SerializeField] private AssessmentController assessment;

    private Dictionary<string, int> Components = new Dictionary<string, int>();
    public List<IngredientType> _currentComponents;
    [SerializeField] private List<Material> MixtureMaterials;
    [SerializeField] private List<IngredientType> correctComponentsSeq;
    [SerializeField] private List<int> correctComponentsCap;
    [SerializeField] private int maxCapacity;
    [SerializeField] private int capacityPerTime;
    [SerializeField] private GameObject cmContainer;
    [SerializeField] private GameObject VisualFill;
    [SerializeField] private ParticleSystem doneSubst;
    [SerializeField] private float visualZMin;
    [SerializeField] private float visualZMax;
    [SerializeField] private bool visualCheck;

    private float startMixingDelay = 5f;

    private int currentCapacity = 0;
    private float minFillTime = 0;
    private float fillTimer = 1;
    private string currentComponent;
    public MixerState currentState;
    private int amountComponents;
    public float time = 0f;
    public float speed = 5f; //Скорость времени на случай, если разброс времени в рецептах будет большой
    public int TimeRequired = 10; //Требуемое время смешивание получаем из рецепта

    [SerializeField] private TextMeshProUGUI debugText; // DEBUG INFO

    private int currentMixingStageNumber = 1;
    
    private void Start()
    {
        currentState = MixerState.AddingComponents;
        
        ShowDebug();
    }

    public void LoadRecipeCompSeq()
	{
        correctComponentsCap.Clear();
        correctComponentsSeq.Clear();
        foreach (var ingr in assessment.currentRecipe.firstMixingStage.ingredients)
        {
            correctComponentsSeq.Add(ingr.type);
            correctComponentsCap.Add(ingr.amount);
        }
        if (assessment.currentRecipe.secondMixingStage.isStageOn)
		{
            foreach (var ingr in assessment.currentRecipe.secondMixingStage.ingredients)
            {
                correctComponentsSeq.Add(ingr.type);
                correctComponentsCap.Add(ingr.amount);
            }
            TimeRequired = assessment.currentRecipe.firstMixingStage.timeInMinutes + assessment.currentRecipe.secondMixingStage.timeInMinutes;
        } else
		{
            TimeRequired = assessment.currentRecipe.firstMixingStage.timeInMinutes;
        }
        
        amountComponents = correctComponentsSeq.Count;
    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        ///////Отслеживаем время смешивания
        if (currentState != MixerState.AddingComponents)
        {
            time += Time.deltaTime * speed;
            if (currentState == MixerState.MixDone &&
                (int) time >=
                TimeRequired +
                10) //Если время смешимания превысило приблизительное требуемое время смешивания + небольшой... 
            {
                //...допустимый запас времени, то смесь становится испорченой
               // SetMixerState(MixerState.MixFail);
            }

            ShowDebug();
        }

        //////
        if (_currentComponents.Count >= amountComponents && currentState == MixerState.AddingComponents && minFillTime <= 0 && startMixingDelay <= 0)
        {
            if (IsComponentSeqIncorrect())
            {
                MakeWrongOrderComponentsMistake();
            }
            else
            {
                if (IsComponentCapIncorrect() || _currentComponents.Count > amountComponents)
                {
                    MakeWrongComponentsCapacityMistake();
                }
            }

            currentState = MixerState.MixingComponents;
            StartCoroutine(Mixing());
        }
        if (_currentComponents.Count >= amountComponents && currentState == MixerState.AddingComponents && startMixingDelay > 0)
		{
            startMixingDelay -= Time.deltaTime;
		}

        if (minFillTime > 0)
        {
            if (currentCapacity > maxCapacity)
            {
                Debug.Log("CONTAINER IS CROWED!");
            }

            minFillTime -= Time.fixedDeltaTime;
            fillTimer -= Time.deltaTime;

            if (fillTimer <= 0)
            {
                Components[currentComponent] += capacityPerTime;
                currentCapacity += capacityPerTime;
                ShowDebug();
                fillTimer = 1;
            }
        }

        if (currentCapacity > 0)
        {
            if (VisualFill.activeInHierarchy)
            {
                if (visualCheck)
                {
                    VisualFill.transform.localPosition = new Vector3(VisualFill.transform.localPosition.x,
                        visualZMin + GetVisualFillPosition(), VisualFill.transform.localPosition.z);
                }
                else
                {
                    VisualFill.transform.localPosition = new Vector3(VisualFill.transform.localPosition.x,
                        VisualFill.transform.localPosition.y, visualZMin + GetVisualFillPosition());
                }
            }
            else
            {
                if (currentCapacity > 0)
                {
                    VisualFill.SetActive(true);
                }
            }
        }
        else
        {
            VisualFill.SetActive(false);
            if (currentCapacity < 0)
                currentCapacity = 0;
        }
    }

    public void Fill(GameObject part)
    {
        if (currentState == MixerState.AddingComponents)
        {
            startMixingDelay = 5f;
            if (!Components.ContainsKey(part.name))
            {
                Components.Add(part.name, 0);
                _currentComponents.Add(part.GetComponent<IngrGameObject>().type);
                ShowDebug();
                /*if (Components.Count > 1)
                {
                    VisualFill.GetComponent<MeshRenderer>().material.Lerp(
                        VisualFill.GetComponent<MeshRenderer>().material,
                        part.GetComponent<ParticleSystemRenderer>().material, 0.5f);
                }*/
            }

            currentComponent = part.name;
            minFillTime = 1;
        }

        /*if (Components.Count == 1)
        {
            VisualFill.GetComponent<MeshRenderer>().material = part.GetComponent<ParticleSystemRenderer>().material;
        } */
    }

    private IEnumerator Mixing()
    {
        ShowDebug();
        yield return new WaitForSeconds(TimeRequired);
        SetMixerState(MixerState.MixDone);
        Components.Clear();
        _currentComponents.Clear();
        if (assessment.currentRecipe.secondMixingStage.isStageOn)
        {
            loadSecondMixingStage();
        }
        //doneSubst.GetComponent<ParticleSystemRenderer>().material = VisualFill.GetComponent<MeshRenderer>().material;
        // Components.Clear();
        cmContainer.GetComponent<PourMixer>().SetQuantity(currentCapacity / capacityPerTime);
        ShowDebug();
    }

    private void loadSecondMixingStage()
    {
        correctComponentsCap.Clear();
        correctComponentsSeq.Clear();
        foreach (var ingr in assessment.currentRecipe.secondMixingStage.ingredients)
        {
            correctComponentsSeq.Add(ingr.type);
            correctComponentsCap.Add(ingr.amount);
        }

        TimeRequired = assessment.currentRecipe.secondMixingStage.timeInMinutes;
    }

    public void ClearMixer()
    {
        Components.Clear();
        currentCapacity = 0;
        currentComponent = null;
        SetMixerState(MixerState.AddingComponents);
        time = 0;
        ShowDebug();
    }

    public void SetMixerState(MixerState state)
    {
        currentState = state;
    }


    private float GetVisualFillPosition()
    {
        int fillPrecent = currentCapacity * 100 / maxCapacity;
        return fillPrecent * visualZMax / 100f;
    }

    public void ChangeCurrentCapacity(int capacity)
    {
        currentCapacity += capacity;
        ShowDebug();
    }

    public int GetCapacityPerTime()
    {
        return capacityPerTime;
    }

    private void ShowDebug()
    {
        debugText.text = "";
        foreach (KeyValuePair<string, int> entry in Components)
        {
            debugText.text += entry.Key + " : " + entry.Value + " ml\n";
        }

        debugText.text += "Кол-во: " + currentCapacity + " / " + maxCapacity + "\n";
        debugText.text += "Стадия: " + getStateName(currentState) + "\n";
    }

    private string getStateName(MixerState state)
    {
        if (state == MixerState.AddingComponents)
        {
            return "Добавление компонентов";
        }
        else
        {
            return "Смешивание: " + ((int) time).ToString() + " мин.";
        }
        /*
        if (state == MixerState.MixingComponents || state == MixerState.MixDone)
        {
            return "Смешивание: " + ((int)time).ToString() + " мин.";
        }
		if (state == MixerState.MixDone) Убрал, чтобы пользователь не мог понять, что смесь готова

		{
			return "Смесь готова";
		}
		/////// 
		if (state == MixerState.MixFail)
		{
            return "Смесь испорчена";
		}
        ///////
        
        return "";
        */
    }

    private bool IsComponentSeqIncorrect()
    {
        for (int i = 0; i < correctComponentsSeq.Count; i++)
        {
            if (correctComponentsSeq[i] != _currentComponents[i])
            {
                return true;
            }
        }
        
        return false;
    }

    private bool IsComponentCapIncorrect()
    {
        for (int i = 0; i < correctComponentsCap.Count; i++)
        {
            if (correctComponentsCap[i] != Components.ElementAt(i).Value)
            {
                return true;
            }
        }
        return false;
    }

    private void MakeWrongOrderComponentsMistake()
    {
        var mistake = new MistakeCM160(
            MistakeType.CONCRETE_MIXER_WRONG_COMPONENTS_ORDER_MISTAKE,
            StringConstants.CM160_WRONG_COMPONENTS_ORDER_MISTAKE,
            MistakeWeight.MEDIUM);
        if (assessment.HasMistake(mistake)) return;
        assessment.AddMistake(mistake);
    }

    private void MakeWrongComponentsCapacityMistake()
    {
        var mistake = new MistakeCM160(
            MistakeType.CONCRETE_MIXER_WRONG_COMPONENTS_CAPACITY_MISTAKE,
            StringConstants.CM160_WRONG_COMPONENTS_CAPACITY_MISTAKE,
            MistakeWeight.MEDIUM);
        if (assessment.HasMistake(mistake)) return;
        assessment.AddMistake(mistake);
    }
}