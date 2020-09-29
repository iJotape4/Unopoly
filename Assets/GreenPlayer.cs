using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GreenPlayer : Player
{
    public int PlayerTurn = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        AnotherCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {                   
        AnotherMove = BluePlayer.movimie;
        if (Input.GetKeyDown(KeyCode.X) && !movimie && !AnotherMove )
        {
            if (PlayerTurn != ControlPlayer.control.Turno)
            {
                return;
            }
            LanzarDado(PlayerTurn);

            if (PlayerTurn.Equals(ControlPlayer.control.Turno))
            {
                ControlPlayer.control.Turno = +2;
            }
        }
    } 
    }
