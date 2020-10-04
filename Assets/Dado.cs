﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.TypeSystem;

public class Dado : MonoBehaviour
{
    public Text texto;
    public cara[] caras;
    public int NumeroActual;
    public Vector3 PosInicial;

    Player PlayerActual;
    public string Tag;
    public bool moviendo;

    // Start is called before the first frame update
    void Start()
    {
        
        NumeroDado();
        caras = GetComponentsInChildren<cara>();
        
        PosInicial = GetComponent<Dado>().transform.position;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        IsMoving();
    }

    void NumeroDado()
    {
        for (int i =0; i<caras.Length; i++)
        {
            if (caras[i].TocaSuelo)
            {
                NumeroActual = 7 - caras[i].Numero;
            }
        }
        Invoke("NumeroDado", 0.5f);

    }

    public void TirarDado()
    {
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();

        transform.position = PosInicial;
        float FuerzaInicial = Random.Range(-10, 10 );
        float FuerzaInicial2 = Random.Range(10, 10);
        float multplier = Random.Range(10, 15);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(FuerzaInicial * multplier, 0, FuerzaInicial2* multplier));
        GetComponent<Rigidbody>().rotation = Random.rotation;
     
        //StartCoroutine(TirarDado());
    }

    public bool IsMoving()
    {
        return  !GetComponent<Rigidbody>().IsSleeping();
    }
}