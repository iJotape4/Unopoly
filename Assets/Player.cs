using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Text Resultado;
    protected int total;
    public GO Rott;
    public int rpposiicion;
    public int punto;
    public static bool movimie;

    [HideInInspector]
    public int dineroInicial=2000;
    public int dinero;
    public Text PlayerDinero;
     
    public Camera OwnCamera;
    public Text PlayerText;

    public bool AnotherMove;
    public int PlayerTurn;

    public bool InBienestar=false;

    public static bool EventTurn;

    public static bool Cards;
    public static bool Chances;
    public static bool ComARrcs;

    [HideInInspector]
    public Quaternion Abajo = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
    [HideInInspector]
    public Quaternion Izquierda = new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);
    [HideInInspector]
    public Quaternion Arriba = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);
    [HideInInspector]
    public Quaternion Derecha = new Quaternion(0.0f, -0.7f, 0.0f, 0.7f);

    [HideInInspector]
    public Vector3 PosAbj = new Vector3(-0.0127f, 0.0234f, 0.0063f);
    [HideInInspector]
    public Vector3 PosIzq = new Vector3(-0.02294f, -0.0027f, 0.0063f);
    [HideInInspector]
    public Vector3 PosArr = new Vector3(0.0009f, -0.0205f, 0.00441f);
    [HideInInspector]
    public Vector3 PosDer = new Vector3(0.0224f, 0f, 0.0063f);
    
    // Start is called before the first frame update
    public void Start()
    {
        
        total = 0;
        Resultado.text = "";
        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();
        PlayerText.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void LanzarDado(int PlayerTurn)
    {
        RestoreText();
        punto = Random.Range(2 ,2);
        Debug.Log("Resul" + punto);
        total = punto;
        Resultado.text = " " + total;      
        StartCoroutine(Move(PlayerTurn));      

    }

    public bool MoveToNexNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));

    }

    public void MoveCamera()
    {
        Camera camara = GetComponentInChildren<Camera>();
        if (rpposiicion < 10 || rpposiicion >40)
        {
            camara.transform.rotation = Abajo;
            camara.transform.localPosition = PosAbj;
        }
        else if (rpposiicion >= 30)
        {
            camara.transform.rotation = Derecha;
            camara.transform.localPosition = PosDer;

        }
        else if (rpposiicion >= 20)
        {
            camara.transform.rotation = Arriba;
            camara.transform.localPosition = PosArr;
        }
        else if (rpposiicion >= 10)
        {
            camara.transform.localPosition = PosIzq;
            camara.transform.rotation = Izquierda;

        }
    }
    public void FinishTurn()
    {
        movimie = false;
        OwnCamera.enabled = false;

        ControlPlayer.control.NextTurno();
        StartCoroutine(PlayerFontText());
    }

    public IEnumerator PlayerFontText()
    {
        PlayerText.enabled = true;
        PlayerText.fontSize = 20;
        PlayerText.text = ("Player " + ControlPlayer.control.Turno + " Turn!");

        while (PlayerText.fontSize > 1) {
            
           PlayerText.fontSize--;
            yield return new WaitForSeconds(0.1f);
            
        }
        RestoreText();
    }

    public void RestoreText()
    {
        PlayerText.enabled = false;
        PlayerText.fontSize = 20;
        
    }


    ///AQUI ESTÁ MOVE
    public IEnumerator Move(int PlayerTurn)
    {
        Camera camara = GetComponentInChildren<Camera>();
        if (movimie)
        {
            yield break;
        }
        movimie = true;
        if (PlayerTurn.Equals(ControlPlayer.control.Turno))
            while (punto > 0)
            {
                Vector3 nextPos = GO.tablero.Seleccionar(rpposiicion+0).position;
                while (MoveToNexNode(nextPos)) { yield return null; }
                yield return new WaitForSeconds(0.1f);
                punto--;
                rpposiicion++;
                MoveCamera();

                ComprobateCards();

                if (rpposiicion == 40)
                {
                    Cobrar(200);
                }
            }

        if (rpposiicion == 30)
        {
            GoBienestar();
        }
        if (!Cards)
        {
            FinishTurn();
        }
        
    }


    public void MoveTowards(int pos)
    {
        Debug.Log("se mueve hasta"+pos);
        if (pos > rpposiicion)
        {
            punto = pos;
        }
        else
        {
            punto = 40 - rpposiicion + pos;
        }
        StartCoroutine(Move(PlayerTurn));
    }

    public void ComprobateCards()
    {
        if ((rpposiicion == 2 || rpposiicion == 17 || rpposiicion == 33)  &&punto == 0)
        {
            ComARrcs = true;
            Cards = true;           
            CardsController.turno = PlayerTurn;
        }

        if ((rpposiicion == 7 || rpposiicion == 22 || rpposiicion == 36) && punto == 0)
        {
            Chances = true;
            Cards = true;         
            CardsController.turno = PlayerTurn;
        }
    }

    public void Cobrar(int cantidad)
    {
        dinero += cantidad;
    }

    public void GoBienestar()
    {
        //Hay que corregirlo y hacer un teletransporte animado como el de monopoly 64 https://www.youtube.com/watch?v=CyDnh7eVCl8 19:40
        Vector3 bienestar = new Vector3(-2.6f,-0.6f,-5.3f);
        rpposiicion = 10;
        transform.position = (bienestar);
        InBienestar = true;
        
    }
}

