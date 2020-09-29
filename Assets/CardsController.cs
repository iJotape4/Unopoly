using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{

    public Image CardImage;
    public Sprite[] Cards;
    public int CardsNum;
    public int elegida;
    public static int turno;

    // Start is called before the first frame update
    void Start()
    {
        CardImage = GameObject.Find("Image").GetComponent<Image>();      
        Cards = Resources.LoadAll<Sprite>("Tarjetas");
        CardsNum = Cards.Length;
        CardImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (Player.Cards)
        {
            ShowCard();
        }

    }
    public  void ShowCard()
    {
        if (!CardImage.enabled) {
            CardImage.enabled = true;
            elegida = Random.Range(0, 4);
            CardImage.sprite = Cards[elegida];
            Debug.Log("card " + elegida);       
        }
        else
        {
            if (turno==1)
            {
                if (Input.GetKey("x"))
                {
                    QuitCard();
                }
            }else if (turno == 2)
            {
                if (Input.GetKey("z"))
                {
                    QuitCard();
                }
            }            
        }                           
    }

    public void QuitCard()
    {
        CardImage.enabled = false;
        Player.Cards = false;
        CallCardMethod(elegida);
    }

    public  void CallCardMethod(int elegida)
    {
        if (elegida == 0)
        {

        }
        else if (elegida == 1)
        {
            PlacheHolderCardMethod1();
        }else if (elegida == 2)
        {


        }
        else if (elegida == 3)
        {


        }
        else if (elegida == 4)
        {


        }
        else if (elegida == 5)
        {


        }
        else if (elegida == 6)
        {


        }
    }

    public void PlacheHolderCardMethod1()
    {

    }

}
