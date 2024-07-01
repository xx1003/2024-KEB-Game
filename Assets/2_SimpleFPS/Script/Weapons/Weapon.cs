using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float dmg;
    public float bulletSpeed;
    
    public ParticleSystem muzzleFlash;

    protected Transform muzzle;

    public bool isFullAuto;
     
    void Update()
    {
        if(isFullAuto && Input.GetMouseButton(0))   // Rifle
        {
            Fire();
        }
        else if (Input.GetMouseButtonDown(0))   // Pistol
        {
            Fire();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (isFullAuto && Input.GetMouseButtonUp(0))    // Rifle 마우스 뗐을 때 레이저 없애기??
        {
            OnRelease();
        }
        
    }
    
    // player로부터 머즐 위치 받기
    public void Init(Transform muzzlePos)
    {
        muzzle = muzzlePos;
    }  
    protected abstract void Fire();
    protected abstract void OnRelease();


}
