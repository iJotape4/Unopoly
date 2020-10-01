using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GreenPlayer : Player
{
     
    
    

    // Start is called before the first frame update
    void Start()
    {
        AnotherCam.enabled = false;
        PlayerTurn = 1;
        StartCoroutine(PlayerFontText());
    }

    // Update is called once per frame
    void Update()
    {             
        


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
