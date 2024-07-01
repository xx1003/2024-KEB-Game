using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private float grenadeDamage;

    private bool isInit = false;    

    //private Rigidbody rigid;
    
    private float timeCounter = 0f;
    private const float maxTime = 5f;
    
    public GameObject hitEffect;
    public void Init(float dmg) {  
    
        grenadeDamage = dmg;
        
        isInit = true;
    }
    
    void Start()
    {
        if (isInit) return;
        
        Debug.LogError("Grenade is not initiated!");     // custom error message
        Destroy(gameObject);    // 이 컴포넌트가 달려있는 오브젝트를 삭제??
             
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > maxTime) Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            other.transform.GetComponent<Enemy>().GetDamage(grenadeDamage);

            Instantiate(hitEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
