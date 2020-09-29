using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePlayer : Player
{


    // Start is called before the first frame update
     void Start()
    {
        PlayerTurn = 2;
    }

    // Update is called once per frame
    void Update()
    {
        AnotherMove = GreenPlayer.movimie;
        if (Input.GetKeyDown(KeyCode.Z) && !movimie && !AnotherMove && !Cards)
        {
            if (PlayerTurn != ControlPlayer.control.Turno)
            {
                return;
            }

            LanzarDado(PlayerTurn);

            if (PlayerTurn.Equals(ControlPlayer.control.Turno))
            {
                ControlPlayer.control.Turno = +1;
            }


        }
    }
    

}



