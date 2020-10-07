using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedPlayer : Player
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        PlayerTurn = 4;
        PlayerColor = Color.red;
    }
}
