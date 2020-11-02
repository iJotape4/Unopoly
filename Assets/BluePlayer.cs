﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePlayer : Player
{

    // Start is called before the first frame update
     void Start()
    {
        
        PlayerTurn = 2;
        PlayerColor = Color.blue;
        PlayerDinero = GameObject.Find("MoneyTextSquirrel").GetComponent<Text>();
        ChibiIcon = GameObject.Find("ImageSquirrel").GetComponent<Image>();
        base.Start();

        GirarAbajo = new Quaternion(-0.5f, -0.5f, -0.5f, 0.5f);
        GirarIzq = new Quaternion(-0.7071068f, -0f, -0f, 0.7071068f);
        GirarArriba = new Quaternion(-0.5f, 0.5f, 0.5f, 0.5f);
        GirarDerecha = new Quaternion(-0f, 0.7071068f, 0.7071068f, -0f);

    }
}



