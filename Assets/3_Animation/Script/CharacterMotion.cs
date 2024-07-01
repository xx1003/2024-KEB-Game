using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMotion : MonoBehaviour
{
    private Animator ani;
    public Transform camPivot;

    private Vector3 characterDir;
    private Vector2 moveVector;
    
    public float moveSpeed;

    private bool isRunning = false;
    
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsRun = Animator.StringToHash("isRun");
    private static readonly int JumpTrigger = Animator.StringToHash("jump");

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        camPivot.position = transform.position;
        
        var threeDDir = new Vector3(moveVector.x, 0f, moveVector.y);
        characterDir = Vector3.Slerp(characterDir, threeDDir, 0.5f); 
        
        transform.Translate(characterDir * moveSpeed * Time.deltaTime, Space.World);
        
        transform.LookAt(transform.position + characterDir, Vector3.up);
    }

    public void WalkForward(InputAction.CallbackContext context)    // context가 버튼입력에 대한 거를 파악함??
    {
        if (context.started) // 입력이 딱 처음 들어오기 시작한 순간
        {
            ani.SetBool(IsMoving, true);
            moveVector = context.ReadValue<Vector2>();
        }
        
        if (context.canceled)
        {
            ani.SetBool(IsMoving, false);
            moveVector = Vector2.zero;
        }
    }

    public void RunPhase(InputAction.CallbackContext context)
    {
        if (context.started) // 입력이 딱 처음 들어오기 시작한 순간
        {
            ani.SetBool(IsRun, true);
            isRunning = true;
        }
        

        if (context.canceled)
        {
            ani.SetBool(IsRun, false);
            isRunning = false;
        }
    }
    
    public void JumpPhase(InputAction.CallbackContext context)
    {
        if (context.started) // 입력이 딱 처음 들어오기 시작한 순간
        {
            ani.SetTrigger(JumpTrigger);
           
        }
    } 
}
