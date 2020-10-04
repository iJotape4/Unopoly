﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public bool Chances = false;

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

       // ComArcsCards.Append(CardImage.sprite);
    
        
       
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
                Chances = true;
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

        if (Input.GetKey("x"))
        {
            QuitCard();
        }

        /*else
        {
            if (turno==1)
            {
               
            }else if (turno == 2)
            {
                if (Input.GetKey("z"))
                {
                    QuitCard();
                }
            }            
        } */                           
    }

    public void QuitCard()
    {
        CardImage.enabled = false;
        Player.Cards = false;
        if (Chances)
        {
            CallCardMethodChances(elegida);
        }
        else
        {
            CallCardMethodComArcs(elegida);
        }

        Chances = false;
    }

    public  void CallCardMethodChances(int elegida)
    {
        //0. Te dio hambre, ve a la cafetería más cercana, si pasas por la salida, cobra.
        if (elegida == 0)
        {
            Cafetería();

        }
        //1. Tienes tu carro en el parqueadero, avanza hasta ahí para recogerlo.
        else if (elegida == 1)
        {
            GoParkway();

        }
        //2.Te vieron fumando en Banu, tienes que ir a bienestar.  
        else if (elegida == 2)
        {
            GoBienestar();

        }
        //3. Perdiste un libro de la biblioteca, paga 200
        else if (elegida == 3)
        {
            StartCoroutine(PlayerActual.Pagar(100));
            StartCoroutine(Waiter());

        }
        //4.Avanza hasta la salida, cobra 200
        else if (elegida == 4)
        {
            GoToGo();
        }

        //5.Puedes salir de bienestar gratis
        else if (elegida == 5)
        {
            PlayerActual.ExitCards++;
            StartCoroutine(Waiter());
        }
        //6. Ve al laboratorio más cercano
        else if (elegida == 6)
        {

            GoToLaboratory();
        }
        
    }


    public void CallCardMethodComArcs(int elegida)
    {
        //0. Te enfermaste, tienes que pagarle al medico de bienestar 100
        if (elegida == 0)
        {
            StartCoroutine(PlayerActual.Pagar(100));
            StartCoroutine(Waiter());

        }
        //1.Se venció tu plazo para pagar el semestre, paga 200 de penalización
        //3.En bienestar te matricularon en un taller contra tu voluntad, paga 200 para cancelarlo
        //8.Como perdiste tu matricula por no presentar la solicitud en 2 años, tienes que empezar de 0 otra carrera, paga 200 de matricula.
        else if (elegida == 1 || elegida == 3 || elegida == 8)
        {
            StartCoroutine(PlayerActual.Pagar(200));
            StartCoroutine(Waiter());

        }
        //2.Se venció tu prestamo universitario, paga 400 para buscar uno nuevo
        else if (elegida == 2)
        {
            StartCoroutine(PlayerActual.Pagar(400));
            StartCoroutine(Waiter());

        }
       
        //4.Rompiste un pc del bloque L, paga 100
        else if (elegida == 4)
        {
            StartCoroutine(PlayerActual.Pagar(100));
            StartCoroutine(Waiter());

        }
        //5. Perdiste un curso en 2.5, puedes habilitarlo, paga 50 
        //6.Faltaste al 20 % de clases, paga 50 para cancelar el curso
        else if (elegida == 5 || elegida == 6)
        {
            StartCoroutine(PlayerActual.Pagar(50));
            StartCoroutine(Waiter());

        }
        //7.Estás faltando mucho a clases, bienestar te cita, ve allí
        else if (elegida == 7)
        {
            StartCoroutine(PlayerActual.GoBienestar());
        }
       
    }

    public IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerActual.FinishTurn();
    }
    //Tienes Tu carro en el parqueadero, avanza hasta ahí para recogerlo
    public void GoParkway()
    {
        CardMethodGoTo(20);
    }

    public void GoBienestar()
    {

        StartCoroutine(PlayerActual.GoBienestar());
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
        Debug.Log("caf" + CafElegida);
        CardMethodGoTo(CafElegida);

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
 /*   public void Pagar(int cantidad)
    {
        PlayerActual.dinero -= cantidad;
    }
    */
    //Método General para ir a una posición desde ésta clase
    public void  CardMethodGoTo(int pos)
    {
        Player.movimie = false ;
        PlayerActual.MoveTowards(pos);
        
        PlayerActual.ComprobateProperties();
       
    }

}


