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
        ChibiIcon = GameObject.Find("ImageBook").GetComponent<Image>();
        WinSprite = Resources.Load<Sprite>("BookWins");
        base.Start();
        StartCoroutine(PlayerFontText());


          GirarAbajo = new Quaternion(-0.5f, 0.5f, -0.5f, 0.5f);
         GirarIzq = new Quaternion( 0.7071068f, -0.7071068f, -0f,0f );
        GirarArriba = new Quaternion(0.5f, -0.5f, -0.5f, 0.5f);
        GirarDerecha = new Quaternion(0f, 0f, -0.7071068f, 0.7071068f);
    }   
}
