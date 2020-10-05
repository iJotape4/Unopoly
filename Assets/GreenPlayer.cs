using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GreenPlayer : Player
{
     
    
    

    // Start is called before the first frame update
    void Start()
    {
        OwnCamera = GetComponentInChildren<Camera>();

        dinero = dineroInicial;
        PlayerTurn = 1;

        PlayerDinero.text = "$"+dinero.ToString();
        PlayerDinero.enabled = true;
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


        PlayerColor = Color.green;

        //StartCoroutine(GoBienestar());

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn == ControlPlayer.control.Turno)
        {
            PlayerDinero.text = "$" + dinero.ToString();
            PlayerDinero.color = PlayerColor;


            if (!DadosCamera.enabled)
            {
                OwnCamera.enabled = true;
            }
            if (InBienestar)
            {
                StartCoroutine(SalirBienestar());
            }
        }


       
        if (Input.GetKeyDown(KeyCode.X) && !movimie  && !Cards && !Properties && !InBienestar)
        {
            if (PlayerTurn != ControlPlayer.control.Turno)
            {   
                return;
            }
            
            StartCoroutine(LanzarDado());
        }
    } 
    }
