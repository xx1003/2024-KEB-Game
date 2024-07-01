using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Blob : MonoBehaviour
{
    public GameObject blobPrefab;
    
    protected FSMstate idleState;
    protected FSMstate wanderingState;
    protected FSMstate TracingFoodState;
    protected FSMstate EatingState;
    
    protected FSMstate curState;
    protected FSMstate nextState;
    
    private bool isTransition;

    protected NavMeshAgent agent;

    protected Food targetFood;

    protected int hp = 20;
    protected const int maxHp = 40; // 번식 가능 hp

    public float idleTime = 1f;
    protected float idleTimer;

    protected float eatingRate = 0.2f;
    protected float eatingTimer;
    
    protected Vector3 WanderingPos;
    
    private WaitForSeconds energyUseRate = new WaitForSeconds(2f);
    
    private void Awake()
    {
        StateInit();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(UsingEnergy());
    }

    private void Update()
    {
        if (isTransition)
        {
            curState = nextState;
            curState.OnEnter?.Invoke();
            isTransition = false;
        }
        
        curState.OnUpdate?.Invoke();
        isTransition = TransitionCheck();

        if (isTransition) curState.OnExit?.Invoke();    // 상태전이 됐으니까 기존상태 벗어나고 다음 상태로..? 끊이지 않고??
        // 상태 확인
        // 전이
    }

    protected abstract void StateInit(); 
    protected abstract bool TransitionCheck();

    private IEnumerator UsingEnergy()   //couroutine??
    {
        while (hp>0)
        {
            yield return energyUseRate;  // 2sec
            hp--;

            if (hp <= 0)
            {
                if(targetFood != null && targetFood.IsDestroyed())
                    targetFood.RemoveOwner(this);
                if (gameObject.CompareTag("Dove")) SimulationManager.instance.doveCount--;
                if (gameObject.CompareTag("Hawk")) SimulationManager.instance.hawkCount--;
                Destroy(gameObject);
            }

            if (hp >= maxHp)    // 번식
            {
                Instantiate(blobPrefab, transform.position, Quaternion.identity);
                if (blobPrefab.CompareTag("Dove")) SimulationManager.instance.doveCount++;
                if (blobPrefab.CompareTag("Hawk")) SimulationManager.instance.hawkCount++;
                hp -= 20; 
            }
        }
    }
    
}
