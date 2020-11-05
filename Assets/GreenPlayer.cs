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
        PlayerDinero = GameObject.Find("MoneyTextLeaf").GetComponent<Text>();
        ChibiIcon = GameObject.Find("ImageLeaf").GetComponent<Image>();
        WinSprite = Resources.Load<Sprite>("LeafWins");
        base.Start();

        GirarAbajo = new Quaternion(-0.6914649f, -0.132073f, 0.132073f, 0.6973236f);
        GirarIzq = new Quaternion(-0.4699396f, 0.3331037f, 0.5423005f, 0.6116446f);
        GirarArriba = new Quaternion(0.132073f, 0.6973236f , 0.6973236f, 0.132073f);
        GirarDerecha = new Quaternion(0.5423005f, 0.6116446f, 0.3331037f, -0.4699396f);
    }
}
