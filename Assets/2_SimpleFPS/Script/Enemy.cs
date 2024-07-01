using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private float healthPoint;
    private float maxHealthPoint;
    
    private bool isArrived = true;
    private Vector3 targePos;
    private float MoveRange = 5f;

    public GameObject hpBarPrefab;

    private HpBar hpBar;
    
    public void Init(float hp)
    {
        healthPoint = hp;
        maxHealthPoint = healthPoint;
    }

    public void GetDamage(float dmg)
    {
        healthPoint -= dmg;
        hpBar.SetHp(healthPoint / maxHealthPoint);
        
        if (healthPoint < 0)
        {
            Destroy(gameObject);
            Destroy(hpBar.gameObject);
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();                               
        //agent.SetDestination(new Vector3(0f,0f,0f));     // transform 어쩌고 안해도 그냥 움직인다. 높이 무시
        hpBar = Instantiate(hpBarPrefab, GameManager.instance.canvasTransform).GetComponent<HpBar>();
        
    }

    private void Update()
    {
        if (isArrived)
        {
            var randPos = Random.insideUnitCircle;
            targePos = new Vector3(randPos.x, 0f, randPos.y) * MoveRange + transform.position;
            agent.SetDestination(targePos);
            
            isArrived = false;
        }

        if(Vector3.Distance(transform.position, targePos) < 1.1f)
        {
            isArrived = true;
        }
                            // 내 포지션
        hpBar.SetPosition(transform.position + Vector3.up * 2f);
    }
}
