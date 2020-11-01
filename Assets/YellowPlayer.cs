using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowPlayer : Player
{
    // Start is called before the first frame update
    void Start()
    {
       
        PlayerColor = Color.yellow;
        PlayerTurn = 1;
        base.Start();
        StartCoroutine(PlayerFontText());

        GirarAbajo = new Quaternion(-0.6724985f, -0.2185081f, -0.2185081f, 0.6724985f);
        GirarIzq = new Quaternion(-0.6275942f, 0.32476171f, 0.3250952f, 0.6284654f);
        GirarArriba = new Quaternion(-0.2112316f, 0.67487771f, 0.6744944f, 0.2120819f);
        GirarDerecha = new Quaternion(0.32745f, 0.6272399f, 0.6263673f, -0.3271223f);


    }
}
