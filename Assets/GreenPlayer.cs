using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GreenPlayer : Player
{     
    // Start is called before the first frame update
    void Start()
    {
     
        PlayerTurn = 4;
        PlayerColor = Color.green;
        base.Start();

        GirarAbajo = new Quaternion(-0.5f, 0.5f, 0.5f, -0.5f);
        GirarIzq = new Quaternion(0f, 0f, 0.7f, -0.7f);
        GirarArriba = new Quaternion(-0.5f, 0.5f, -0.7f, 0.7f);
        GirarDerecha = new Quaternion(-0.7f, 0.7f, 0f, 0f);
    }
}
