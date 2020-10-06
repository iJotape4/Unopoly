using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePlayer : Player
{


    // Start is called before the first frame update
     void Start()
    {
        OwnCamera = GetComponentInChildren<Camera>();
        PlayerTurn = 2;
        dinero = dineroInicial;
        StartCoroutine(PlayerFontText());
        OwnCamera.enabled = false;

        DadosCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        DadosCamera.enabled = false;

        TextoTirar = GameObject.Find("TextoTirar").GetComponent<Text>();
        TextoTirar.enabled = true;

        UseCard = GameObject.Find("TextoTSalida").GetComponent<Text>();
        UseCard.enabled = false;

        TextoPagar = GameObject.Find("TextoPagar").GetComponent<Text>();
        TextoPagar.enabled = false;

        PlayerColor = Color.blue;

    }


}



