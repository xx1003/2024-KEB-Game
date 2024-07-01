using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dove : Blob
{
    private bool isKicked;
    protected override void StateInit()
    {
        idleState = new FSMstate(IdleEnter, null, null);
        wanderingState = new FSMstate(WanderingEnter, null, null);
        TracingFoodState = new FSMstate(TracingFood, null, null);
        EatingState = new FSMstate(EatingFoodEnter, EatingFoodUpdate, null); // null 바꿔라
        curState = idleState;
    }

    protected override bool TransitionCheck()   // update 문이라고 생각해라
    {
        if (curState == idleState)
        {
            idleTimer += Time.deltaTime;
            
            if (idleTimer > idleTime)
            {
                nextState = wanderingState;
                return true;
            }
           
            return CheckFoodInRange();
        }
        else if(curState == wanderingState)
        {
            if (Vector3.Distance(transform.position, WanderingPos) < 1.1f)
            {
                nextState = idleState;
                return true;
            }
            return CheckFoodInRange();
        }
        else if (curState == TracingFoodState)
        {
            if (targetFood.IsDestroyed() || targetFood.IsHawkEating())
            {
                nextState = wanderingState;
                return true;
            }
            
            if (Vector3.Distance(transform.position, targetFood.transform.position) < 1.1f)
            {
                nextState = EatingState;
                return true;
            }
        }
        else if (curState == EatingState)
        {
            if (targetFood.IsDestroyed())
            {
                targetFood = null;   
                nextState = idleState;
                return true;
            }

            if (isKicked)
            {
                nextState = wanderingState;
                isKicked = false;
                return true;
            }
        }
        
        return false;
    }

    private bool CheckFoodInRange()
    {
        var foods = Physics.OverlapSphere(transform.position, 5f, 1<<LayerMask.NameToLayer("Food"));

        if (foods.Length < 1) return false;
            
        // var min = float.MaxValue;
        // for (int i = 0; i < foods.Length; i++)
        // {
        //     var dist = Vector3.Distance(transform.position, foods[i].transform.position);
        //
        //     if (dist < min)
        //     {
        //         min = dist;
        //         targetFood = foods[i].GetComponent<Food>();
        //     }
        // }

        targetFood = foods[Random.Range(0, foods.Length)].GetComponent<Food>();

        nextState = TracingFoodState;
        return true;
    }
    
    private void IdleEnter()
    {
        idleTimer = 0f;
    }

    private void WanderingEnter()
    {
        var mapSize = SimulationManager.instance.mapSize;

        var targetPos = Random.insideUnitSphere * 5f + transform.position;

        targetPos.x = Mathf.Clamp(targetPos.x, -mapSize, mapSize);
        targetPos.z = Mathf.Clamp(targetPos.z, -mapSize, mapSize);

        WanderingPos = targetPos;
        WanderingPos.y = 0f;
        
        agent.SetDestination(targetPos);
    }

    private void TracingFood()
    {
        if(targetFood.IsDestroyed())return;
        
        agent.SetDestination(targetFood.transform.position);
    }

    private void EatingFoodEnter()
    {
        targetFood.SetOwner(this);
    }

    private void EatingFoodUpdate()
    {
        eatingTimer += Time.deltaTime;
        
        if (eatingTimer > eatingRate)
        {
            if (targetFood.IsDestroyed()) return;
            
            targetFood.TakeFood();
            hp++;
            eatingTimer -= eatingRate;
        }
    }

    public void SetKicked()
    {
        isKicked = true;
        
    }
}
