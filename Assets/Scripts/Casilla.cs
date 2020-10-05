using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Casilla : MonoBehaviour
{

    public Casilla casilla;
    public Casilla casiillaSiguiente;
    public bool ocupada;
    public int Players;

    public Player[] Jugadores = new Player[ControlPlayer.LImitedeTurno];
    public  Vector3 PosicionOriginal;

    

    // Start is called before the first frame update
    void Start()
    {
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
