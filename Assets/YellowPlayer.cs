using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowPlayer : Player
{
    // Start is called before the first frame update
    void Start()
    {
        OwnCamera = GetComponentInChildren<Camera>();

        dinero = dineroInicial;
        PlayerTurn = 3;
        PlayerDinero = GameObject.Find("MoneyText").GetComponent<Text>();
        PlayerDinero.text = "$" + dinero.ToString();
        PlayerDinero.enabled = true;

        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();
        PlayerText.enabled = true;

        Rott = GameObject.Find("GO").GetComponent<GO>();       

        
        OwnCamera.enabled = false;

        DadosCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        DadosCamera.enabled = false;

        TextoTirar = GameObject.Find("TextoTirar").GetComponent<Text>();
        TextoTirar.enabled = true;

        UseCard = GameObject.Find("TextoTSalida").GetComponent<Text>();
        UseCard.enabled = false;

        TextoPagar = GameObject.Find("TextoPagar").GetComponent<Text>();
        TextoPagar.enabled = false;

        bienestar = GameObject.Find("Bienestar").GetComponent<Casilla>();

        PlayerColor = Color.yellow;

    }

    // Update is called once per frame

    
}
