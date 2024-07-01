using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public class FSMstate   // 쌩 클래스..?
{
   public Action OnEnter;
   public Action OnUpdate;
   public Action OnExit;
   
   public FSMstate(Action onEnter,Action onUpdate, Action onExit)
   {
      OnEnter = onEnter;
      OnUpdate = onUpdate;
      OnExit = onExit;

   }
   // // delegate 는 함수 포인터??  
   // private delegate void HappyDaram(int a);  // 선언만
   // private HappyDaram a;   // 실제로 쓰기 위해서 이렇게 변수를 만들어줘야 함?
   //
   // private Action<int, float> a; // 위에 두줄이랑 똑같다고??
   // private Func<int, float> c;
   // private Predicate<int> d; 
   //
   // private void Test1(int b)
   // {
   //          
   // }
   //
   // private void Test2(int c)
   // {
   //    
   // }
   //
   // private void main()
   // {
   //    a = Test1;
   //    a += Test2;
   //    
   //    a.Invoke(2);   // 들어간 함수들에 값 '2' 넣어서 실행시켜줌. 함수 넣은 순서대로 실행
   // }
}

public class IdleState : FSMstate
{
   public IdleState(Action onEnter, Action onUpdate, Action onExit) : base(onEnter, onUpdate, onExit)
   {
      
   }
}
