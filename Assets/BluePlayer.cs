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
        PlayerColor = Color.blue;

        base.Start();
        

    }
}



