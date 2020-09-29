using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{

    public Image CardImage;
    public Sprite[] Cards;
    public int CardsNum;
    public int elegida;

    // Start is called before the first frame update
    void Start()
    {
        CardImage = GameObject.Find("Image").GetComponent<Image>();      
        Cards = Resources.LoadAll<Sprite>("Tarjetas");
        CardsNum = Cards.Length;
        //CardImage.enabled = false;
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
  
            CardImage.enabled = true;
            elegida = Random.Range(0, 4);
            CardImage.sprite = Cards[elegida];
            Debug.Log("card " + elegida);


            if (Input.GetKey("o"))
        {
            CardImage.enabled = false;
        }
            

        
    }

}
