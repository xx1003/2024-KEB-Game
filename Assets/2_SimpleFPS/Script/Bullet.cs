using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;
    private float bulletDamage;

    private bool isInit = false;    // init 호출 안되면 Start()

    private float timeCounter = 0f;
    private const float maxTime = 5f;

    public GameObject hitEffect;
    
    public void Init(float speed, float dmg)    // 생성자를 안씀??
    {
        bulletSpeed = speed;
        bulletDamage = dmg;
        
        isInit = true;
    }
    
    void Start()
    {
        if (isInit) return;
        
        Debug.LogError("Bullet is not initiated!");     // custom error message
        Destroy(gameObject);    // 이 컴포넌트가 달려있는 오브젝트를 삭제??
             
    }

    void Update()
    {
        transform.Translate(0f,0f, bulletSpeed * Time.deltaTime);
        
        timeCounter += Time.deltaTime;
        if (timeCounter > maxTime) Destroy(gameObject);
    }
    
    // collider vs trigger : trigger는 뚫고 들어가는거???
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.GetComponent<Enemy>().GetDamage(bulletDamage);

            Instantiate(hitEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
