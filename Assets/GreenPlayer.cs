using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GreenPlayer : Player
{
     
    
    

    // Start is called before the first frame update
    void Start()
    {
        dinero = dineroInicial;
        AnotherCam.enabled = false;
        PlayerTurn = 1;

        PlayerDinero.text = "$"+dinero.ToString();
        PlayerDinero.enabled = true;
        StartCoroutine(PlayerFontText());
      
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn == ControlPlayer.control.Turno)
        {
            PlayerDinero.text = "$" + dinero.ToString();
            PlayerDinero.color = Color.green;
        }


        AnotherMove = BluePlayer.movimie;
        if (Input.GetKeyDown(KeyCode.X) && !movimie && !AnotherMove && !Cards )
        {
            if (PlayerTurn != ControlPlayer.control.Turno)
            {
                return;
            }
           
            LanzarDado(PlayerTurn);

          /*  if (PlayerTurn.Equals(ControlPlayer.control.Turno))
            {
                
                ControlPlayer.control.Turno = +2;
            */
        }
    } 
    }
