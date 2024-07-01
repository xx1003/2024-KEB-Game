using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float rotateXSpeed;
    public float rotateYSpeed;
    
    private Rigidbody rigid;
    public float jumpForce;
    private int jumpCounter = 0;
    
    public Transform headPivot;

    private float rotationX = 0f;    // -90 ~ 90

    public bool hideCursor = true;

    //public GameObject bullet;
    public Transform muzzle;    // muzzle의 위치

    public int healthPoint = MaxHealthPoint;
    private const int MaxHealthPoint = 3;

    public Image[] heartImages;

    private bool invincible = false;

    public Weapon[] Weapons;
    private Weapon curWeapon;
    
    // public float dmg;
    // public float bulletSpeed;

    //public ParticleSystem muzzleFlash;
    
    void Start()
    {
        if (hideCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        rigid = GetComponent<Rigidbody>();
        
        // 시작 무기 
        Weapons[0].gameObject.SetActive(true);
        Weapons[0].Init(muzzle);

        curWeapon = Weapons[0];
    }

    void Update()
    {
        // move
        Vector3 moveVector = Vector3.zero; // (0f, 0f, 0f)
        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector += Vector3.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector += Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector3.right;
        }
        
        moveVector.Normalize(); // 대각선 이동스피드 다른 방향이랑 똑같게 만들어주기
        transform.Translate(moveVector * (moveSpeed * Time.deltaTime)); // transform 은 기본제공, 선언 안해

        // rotate
        // 좌우 회전
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotateXSpeed);
        // 상하 회전
        rotationX -= Input.GetAxis("Mouse Y") * rotateYSpeed;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        
        var tempRotation = headPivot.eulerAngles;
        tempRotation.x = rotationX;
        headPivot.rotation =Quaternion.Euler(tempRotation);
        
        
        // jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2)
        {
            rigid.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            jumpCounter++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectWeapon(2);
        
        // fire bullet
        // if (Input.GetMouseButtonDown(0))
        // {
        //     muzzleFlash.Play();
        //     
        //     // bullet 생성
        //     var tempBullet = Instantiate(bullet, muzzle.position, headPivot.rotation);
        //     tempBullet.GetComponent<Bullet>().Init(bulletSpeed, dmg);     // tempBullet에 있는 컴포넌트 찾음
        // }
    }

    public void GetDamage(Vector3 enemyPos)
    {
        invincible = true;
        StartCoroutine(InvicibleTimer());   // 타이머 누르고 바로 아래 코드로?? 
        
        healthPoint--;
        
        var hitVector = (transform.position - enemyPos).normalized * 5f;    // 단위벡터로 만들어주기 
        hitVector += Vector3.up * 5f;
        rigid.AddForce(hitVector, ForceMode.Impulse);   // impulse로 해야지 팍 옴
        
        if (healthPoint < 0) return;
        
        heartImages[healthPoint].enabled = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6)
        {
            jumpCounter = 0;
        }

        if (other.gameObject.layer == 7)
        {
            if (invincible) return;
            
            GetDamage(other.transform.position);
        }
    }

    private void SelectWeapon(int idx)
    {
        curWeapon.gameObject.SetActive(false);
        
        Weapons[idx].gameObject.SetActive(true);
        Weapons[idx].Init(muzzle);

        curWeapon = Weapons[idx];
    }
    private IEnumerator InvicibleTimer()
    {
        yield return new WaitForSeconds(2f);
        invincible = false;
    }
}
