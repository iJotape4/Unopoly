using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GreenPlayer : Player
{     
    // Start is called before the first frame update
    void Start()
    {
     
        PlayerTurn = 1;
        PlayerColor = Color.green;
        base.Start();
        StartCoroutine(PlayerFontText());
    }
}
