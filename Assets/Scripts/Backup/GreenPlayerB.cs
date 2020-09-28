﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GreenPlayerB : MonoBehaviour
{
    public Text Resultado;
    private int total;
    public GO Rott;
    int rpposiicion;
    public int punto;
    public static bool movimie;
    public int resto;   
    public bool AnotherMove;
    public int Player1Turn = 1;

    public Camera BlueCam;

    public Quaternion Abajo = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
    public Quaternion Izquierda = new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);
    public Quaternion Arriba = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);
    public Quaternion Derecha = new Quaternion(0.0f, -0.7f, 0.0f, 0.7f);

    public Vector3 PosAbj = new Vector3(-0.0127f, 0.0234f, 0.0063f);
    public Vector3 PosIzq = new Vector3(-0.02294f, -0.0027f, 0.0063f);
    public Vector3 PosArr = new Vector3(0.0009f, -0.0205f, 0.00441f);
    public Vector3 PosDer = new Vector3(0.0224f, -0.0226f, 0.0063f);

    //public Camera camara;

    // Start is called before the first frame update
    void Start()
    {
        total =0;
        Resultado.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        
        Camera camara = GetComponentInChildren<Camera>();
       
      
        AnotherMove = BluePlayer.movimie;
        if (Input.GetKeyDown(KeyCode.X) && !movimie && !AnotherMove )
        {
            if (Player1Turn != ControlPlayer.control.Turno)
            {
                return;
            }
            else if (Player1Turn == ControlPlayer.control.Turno)
            {
            }

            punto = Random.Range(2,13);
            Debug.Log("Resul"+punto);
            total = punto;
            Resultado.text = " " + total;
           
            if (rpposiicion + punto < Rott.Puesto.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                resto = (rpposiicion + punto) - 40;
                punto = 40 - rpposiicion;
                StartCoroutine(Move(resto));
            }
            if (Player1Turn.Equals(ControlPlayer.control.Turno))
            {
                if (Player1Turn > 2)
                {
                }
                ControlPlayer.control.Turno = +2;
            }
           

        }
    }
    IEnumerator Move()
        {
       
        if (movimie)
        {
            yield break;

        }
        movimie = true;

        if (Player1Turn.Equals(ControlPlayer.control.Turno))

        Step();
        FinishTurn();       
    }

    IEnumerator Step()
    {
        while (punto > 0)
        {
            Vector3 nextPos = Rott.Puesto[rpposiicion + 0].position;
            while (MoveToNexNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            punto--;
            rpposiicion++;
            MoveCamera();
        }
    }

    void FinishTurn()
    {
        Camera camara = GetComponentInChildren<Camera>();
        movimie = false;
        camara.enabled = false;
        BlueCam.enabled = true;        
    }

    //Método para completar una vuelta
    IEnumerator Move(int resto)
    {
        Camera camara = GetComponentInChildren<Camera>();
        if (movimie)
            {
                yield break;

            }
            movimie = true;

            if (Player1Turn.Equals(ControlPlayer.control.Turno))
         
        Step();
                punto = resto;
                rpposiicion = 0;
        Step();
        FinishTurn();   
    }

    bool MoveToNexNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f *Time.deltaTime));

    }

    void  MoveCamera()
    {
        Camera camara = GetComponentInChildren<Camera>();
        if (rpposiicion < 10)
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
     
    }