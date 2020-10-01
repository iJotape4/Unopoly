using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GO : MonoBehaviour
{

    public static GO tablero;
    public static Transform Posicion;

    Transform[] lista;
    // Start is called before the first frame update
    private void Start()
    {
        GO lc = new GO();
        tablero = this.GetComponent<GO>();

        lista = GetComponentsInChildren<Transform>();
        foreach (Transform child in lista)
        {
            if (child != this.transform)
            {
               
              lc.InsertarUltimo(child);
            }
        }
        TableroPos = lc.raiz;
        Posicion = TableroPos.info;
    }

    public void NextPos()
    {
        TableroPos = TableroPos.sig;
        Posicion = TableroPos.info;
    }

    public Transform Seleccionar(int pos)
    {     
           Nodo reco = TableroPos;
          for (int f = 0; f <= pos - 1; f++)
        {
            reco = reco.sig;
        }

        return reco.info;                                           
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //Constructor de Listas Circulares
    //Ésta lista circular está compuesta por las posiciones del tablero.
    class Nodo
    {
        public Transform info;
        public Nodo ant, sig;
    }

    private Nodo raiz;
    private Nodo TableroPos;

    public GO()
    {
        raiz = null;
    }

    public void InsertarUltimo(Transform x)
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
