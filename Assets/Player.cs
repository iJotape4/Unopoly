using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Text Resultado;
    protected int total;
    public GO Rott;
    protected int rpposiicion;
    public int punto;
    public static bool movimie;
     
    public Camera AnotherCam;

    public bool AnotherMove;

    public static bool Cards;

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
    public Vector3 PosDer = new Vector3(0.0224f, -0.0226f, 0.0063f);
    [HideInInspector]




    // Start is called before the first frame update
    public void Start()
    {
        total = 0;
        Resultado.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LanzarDado(int PlayerTurn)
    {
        int resto;
        punto = Random.Range(2, 2);
        Debug.Log("Resul" + punto);
        total = punto;
        Resultado.text = " " + total;
        if (rpposiicion + punto < Rott.Puesto.Count)
        {
            StartCoroutine(Move(PlayerTurn));
        }
        else
        {
            //Cuando llega a GO y hay que reiniciar la Lista
            resto = (rpposiicion + punto) - 40;
            punto = 40 - rpposiicion;
            StartCoroutine(Move(resto, PlayerTurn));
        }

        if(rpposiicion == 2)
        {
            Cards = true;
        }

    }

    public bool MoveToNexNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));

    }

    public void MoveCamera()
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
    public  void FinishTurn()
    {
        Camera camara = GetComponentInChildren<Camera>();
        movimie = false;
        camara.enabled = false;
        AnotherCam.enabled = true;
    }

    public IEnumerator Step()
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
                Vector3 nextPos = Rott.Puesto[rpposiicion + 0].position;
                while (MoveToNexNode(nextPos)) { yield return null; }
                yield return new WaitForSeconds(0.1f);
                punto--;
                rpposiicion++;
                MoveCamera();

            }
        FinishTurn();
    }



    //Método para completar una vuelta
    public IEnumerator Move(int resto, int PlayerTurn)
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
                Vector3 nextPos = Rott.Puesto[rpposiicion + 0].position;
                while (MoveToNexNode(nextPos)) { yield return null; }

                yield return new WaitForSeconds(0.1f);
                punto--;
                rpposiicion++;
                MoveCamera();
            }

        punto = resto;
        rpposiicion = 0;

        while (punto > 0)
        {
            Vector3 nextPos = Rott.Puesto[rpposiicion + 0].position;
            while (MoveToNexNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            punto--;
            rpposiicion++;
            MoveCamera();
        }
        FinishTurn();
    }
}

