using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedPlayer : Player
{
    // Start is called before the first frame update
    void Start()
    {
        
        PlayerTurn = 3;
        PlayerColor = Color.red;
        PlayerDinero = GameObject.Find("MoneyTextBook").GetComponent<Text>();
        base.Start();

        GirarAbajo = new Quaternion(1f, 0f, 0f, -0f);
        GirarIzq = new Quaternion(0.7f, 0f, -0.7f, 0f);
        GirarArriba = new Quaternion(0f, 0f, -1f, 0f);
        GirarDerecha = new Quaternion(0.7f, 0f, 0.7f, 0f);
    }   
}
