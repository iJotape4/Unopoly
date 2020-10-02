using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{

    public Image CardImage;
    public Sprite[] ChanceCards;
    public Sprite[] ComArcsCards;
    public int CardsNum;
    public int elegida;
    public static int turno;

    public int[] cafeterias = new int[4] { 5, 12, 25, 38 };
    public int[] laboratorios = new int[3] { 35,  37, 39 };

    public string Tag ;

    Player PlayerActual;

    // Start is called before the first frame update
    void Start()
    {
        CardImage = GameObject.Find("Image").GetComponent<Image>();      
        ChanceCards = Resources.LoadAll<Sprite>("Chances");
        CardsNum = ChanceCards.Length;

        ComArcsCards = Resources.LoadAll<Sprite>("ComArcs");
        CardsNum = ComArcsCards.Length;
        CardImage.enabled = false;
       
    }

    // Update is called once per frame
    void Update()
    {
       if (Player.Cards)
        {
            ShowCard();
        }
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();
    }
    public  void ShowCard()
    {
        Sprite[] Cards = {};

        if (!CardImage.enabled) {

            if (Player.Chances)
            {
                Cards = ChanceCards;
                Player.Chances = false;
            }
            if (Player.ComARrcs)
            {
                Cards = ComArcsCards;
                Player.ComARrcs = false;
            }

            CardImage.enabled = true;
            elegida = Random.Range(0, Cards.Length);
            CardImage.sprite = Cards[elegida];                 
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
        Debug.Log("carta " + elegida);
    }

    public  void CallCardMethod(int elegida)
    {
        if (elegida == 0)
        {

           
           
        }
        else if (elegida == 1)
        {
         

           
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

    //Tienes Tu carro en el parqueadero, avanza hasta ahí para recogerlo
    public void GoParkway()
    {
        CardMethodGoTo(30);
    }

    //Te dio hambre, ve a la cafetería más cercana, si pasas por la salida, cobra.
    public void Cafetería()
    {
        int CafElegida = 0;
        foreach (int Cafetería in cafeterias)
        {
            if (Cafetería > PlayerActual.rpposiicion)
            {
                CafElegida = Cafetería;
                break;
            }
            else
            {
                CafElegida = cafeterias[0];

            }
        }
        CardMethodGoTo(CafElegida);

    }
    //Te vieron fumando en Banu, tienes que ir a bienestar.
    public void GoBienestar()
    {

        //Hay que corregirlo y hacer un teletransporte animado como el de monopoly 64 https://www.youtube.com/watch?v=CyDnh7eVCl8 19:40
        CardMethodGoTo(10);
    }

    //Avanza hasta la salida, cobra 200
    public void GoToGo()
    {
        CardMethodGoTo(40);
        
    }


    //Avanza hasta el laboratorio más cercano
    public void GoToLaboratory()
    {
        int LaboElegido = 0;
        foreach (int Laboratorio in laboratorios)
        {
            if (Laboratorio > PlayerActual.rpposiicion)
            {
                LaboElegido = Laboratorio;
                break;
            }
            else
            {
                LaboElegido = laboratorios[0];
                
            }
        }
        CardMethodGoTo(LaboElegido);
    }


    //Método General para pagar desde ésta clase
    public void Pagar(int cantidad)
    {
        PlayerActual.dinero -= cantidad;
    }

    //Método General para ir a una posición desde ésta clase
    public void CardMethodGoTo(int pos)
    {
        Player.movimie = false ;
        PlayerActual.MoveTowards(pos);
        PlayerActual.FinishTurn();
    }

}
