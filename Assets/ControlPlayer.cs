using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class ControlPlayer : MonoBehaviour
{   
    //Variables
    public int Turno = 0;
    public static int LImitedeTurno = 3;
    public static ControlPlayer control;


    public string Tag;

    Player PlayerActual;

    void Start()
    {
        //Construye la lista circular con el número de turnos que se definió
        //en la variable LimitedeTurno
        ControlPlayer lc = new ControlPlayer();
        control = this.GetComponent<ControlPlayer>();
        for (int i=1; i<=LImitedeTurno; i++ )
        {
            lc.InsertarUltimo(i);
        }
        
        //En el Start define como primer turno al Nodo Raíz (Turno 1)
        NodoTurno = lc.raiz;
        Turno = NodoTurno.info;
    }
    void Update()
    {
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();
    }

    //Éste método maneja el cambio de turnos
    public void NextTurno()
    {
        NodoTurno = NodoTurno.sig;
        Turno = NodoTurno.info;
    }


    //Constructor de Listas Circulares
    //Ésta lista circular está compuesta por los turnos de los players.
    class Nodo
    {
        public int info;
        public Nodo ant, sig;
    }

    private Nodo raiz;
    private Nodo NodoTurno;

    public ControlPlayer()
    {
        raiz = null;
    }

    public void InsertarUltimo(int x)
    {
        Nodo nuevo = new Nodo();
        nuevo.info = x;
        if (raiz == null)
        {
            nuevo.sig = nuevo;
            nuevo.ant = nuevo;
            raiz = nuevo;
        }
        else
        {
            Nodo ultimo = raiz.ant;
            nuevo.sig = raiz;
            nuevo.ant = ultimo;
            raiz.ant = nuevo;
            ultimo.sig = nuevo;
        }
    }

}