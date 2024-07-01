using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager instance;   // 싱글턴 매니저??

    public GameObject foodPrefab;
    public GameObject dovePrefab;
    public GameObject hawkPrefab;
    
    public int initialFoodAmount;
    public int initialDoveAmount;
    public int initialHawkAmount;
    
    public float foodRate;
    private float foodTimer;
    private WaitForSeconds foodTime;
    
    // [HideInInspector] 얘는 public인데 안보여주기, [SerializeField] 얘는 privated인데 보여주기
    public float mapSize = 21f;
    
    //[Range(0.5f, 5f)]
    // public float timeMult = 1f;

    public Slider timeMultSlider;
    public TextMeshProUGUI timeScaleText;

    public TextMeshProUGUI foodAmountText;
    public TextMeshProUGUI doveAmountText;
    public TextMeshProUGUI hawkAmountText;

    public GameObject controlPanel;

    public RectTransform scoreBar;
    private const float max = 590f;
    
    //[HideInInspector] 
    public int doveCount;
    //[HideInInspector] 
    public int hawkCount;
    
    void Awake()
    {
        instance = this;
        foodTime = new WaitForSeconds(foodRate);
        
        // StartSimulation();
    }

    private void Update()
    {
        Time.timeScale = timeMultSlider.value;
        
        var tempSize = scoreBar.sizeDelta;
        tempSize.y = max * doveCount / (doveCount + hawkCount + float.Epsilon);
        scoreBar.sizeDelta = tempSize;
    }

    public void StartSimulation()
    {
        //Generate Initial Foods
        for (int i = 0; i < initialFoodAmount; i++)
        {
            SpawnPrefabRandomPos(foodPrefab);
        }
        
        for (int i = 0; i < initialDoveAmount; i++)
        {
            SpawnPrefabRandomPos(dovePrefab);
            doveCount++;
        }
        
        for (int i = 0; i < initialHawkAmount; i++)
        {
            SpawnPrefabRandomPos(hawkPrefab);
            hawkCount++;
        }
        
        controlPanel.SetActive(false);
        
        StartCoroutine(SpawningFood()); // 별도로 실행??? 애를 기다렸다가 실행 뭐 이런게 아니래. 신경노
        
    }
    
    private IEnumerator SpawningFood()
    {
        while (true)
        {
            // yield return new WaitForSeconds()
            yield return foodTime;  // 이만큼 기다리고 생성
            
            // 생성
            SpawnPrefabRandomPos(foodPrefab);
        }
    }

    private void SpawnPrefabRandomPos(GameObject prefab)
    {
        var posX = Random.Range(-mapSize, mapSize);
        var posY = Random.Range(-mapSize, mapSize);

        var hawkPos = new Vector3(posX, 0f, posY);

        Instantiate(prefab, hawkPos, Quaternion.identity);
    }

    public void SetTimeScalerText(float value)
    {
        timeScaleText.text = "x " + value.ToString("N2");
    }

    public void SetFoodAmount(bool flag)
    {
        // initialFoodAmout += flag? 1:-1;
        if (flag) initialFoodAmount++;
        else initialFoodAmount--;

        if (initialFoodAmount <= 0) initialFoodAmount = 0;
        foodAmountText.text = initialFoodAmount.ToString();
    }
    
    public void SetDoveAmount(bool flag)
    {
        if (flag) initialDoveAmount++;
        else initialDoveAmount--;
        
        if (initialDoveAmount <= 0)  initialDoveAmount = 0;
        doveAmountText.text = initialDoveAmount.ToString();
    }
    
    public void SetHawkAmount(bool flag)
    {
        if (flag) initialHawkAmount++;
        else initialHawkAmount--;
        
        if (initialHawkAmount <= 0) initialHawkAmount = 0;
        hawkAmountText.text = initialHawkAmount.ToString();
    }
}
