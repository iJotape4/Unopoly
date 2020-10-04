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
        dinero = 3000;
        StartCoroutine(PlayerFontText());
        OwnCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn == ControlPlayer.control.Turno)
        {
            PlayerDinero.color = PlayerColor;
            PlayerDinero.text = "$" + dinero.ToString();
            OwnCamera.enabled = true;
        }

        AnotherMove = GreenPlayer.movimie;
        if (Input.GetKeyDown(KeyCode.X) && !movimie && !AnotherMove && !Cards && !Properties)
        {
            if (PlayerTurn != ControlPlayer.control.Turno)
            {
                return;
            }

            StartCoroutine(LanzarDado());


        }
    }
    

}



