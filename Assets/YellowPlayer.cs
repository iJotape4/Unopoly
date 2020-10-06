using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowPlayer : Player
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        PlayerColor = Color.yellow;
        PlayerTurn = 3;
    }

    // Update is called once per frame

    
}
