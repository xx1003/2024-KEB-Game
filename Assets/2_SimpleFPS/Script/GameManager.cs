using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public Transform canvasTransform;
    
    public static GameManager instance = null;

    private void Start()
    {
        instance = this;
    }
}
