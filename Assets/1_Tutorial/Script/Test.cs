using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class Test : MonoBehaviour
 {
     public Vector3 move = new Vector3(0f, 0f, 0f);
     private Transform trans;

     public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Test log");
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            move = new Vector3(0f, 0f, speed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A)) //left
        {
            move = new Vector3(-speed*Time.deltaTime, 0f, 0f);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            move = new Vector3(0f, 0f, -speed*Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = new Vector3(speed*Time.deltaTime, 0f, 0f);
        }
        else
        {
            move = new Vector3(0f, 0f, 0f);

        }
        trans.Translate(move);
    }
}
