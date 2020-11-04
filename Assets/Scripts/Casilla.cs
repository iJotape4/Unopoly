using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{

    public Casilla casilla;
    public Casilla casiillaSiguiente;
    public bool ocupada;
    public bool ocupadaby2;
    public bool ocupadaby3;
    public int Players;

    public static Player[] Jugadores;
    public  Vector3 PosicionOriginal;

    

    // Start is called before the first frame update
    void Start()
    {
        Jugadores = new Player[ControlPlayer.LImitedeTurno];
    
        for (int i = 0; i < Jugadores.Length; i++)
        {
            Jugadores[i] = GameObject.FindGameObjectWithTag("Player" + (i + 1)).GetComponent<Player>();
        }
        
        casilla = GetComponent<Casilla>();
        PosicionOriginal = casilla.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Jugadores.Length; i++)
        {
            if (Jugadores[i].transform.position == casilla.transform.position)
            {
                ocupada = true;
                break;
            }
            else
            {
                ocupada = false;
                GoToOriginal();
            }               
        }
    }



    public void MoveCasilla()
    {

    }

    public void GoToOriginal()
    {
        transform.localPosition = PosicionOriginal;
    }


}
