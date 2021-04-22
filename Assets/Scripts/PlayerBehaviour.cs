using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D ship;
    // Start is called before the first frame update

    private float zeroToSixty = 0.2f; // The interval with which thrust increases every sec;
    private float currentThrust; // Current force being applied
    public float maxThrust = 5; // Max Force Engine is capable of
    private float timeUp, timeDown, timeRight, timeLeft;

    private KeyCode up = KeyCode.W;
    private KeyCode down = KeyCode.S;
    private KeyCode right = KeyCode.D;
    private KeyCode left = KeyCode.A;



    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }
    }
