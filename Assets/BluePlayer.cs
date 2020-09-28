using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePlayer : MonoBehaviour
{
    public Text Resultado;
    private int total;
    public GO Rott;
    int rpposiicion;
    public int punto;
    public static bool movimie;
    public int resto;

    public Camera GreenCam;
    

    public int PlayerTurn = 2;
    public bool AnotherMove;

    public Quaternion Abajo = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
    public Quaternion Izquierda = new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);
    public Quaternion Arriba = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);
    public Quaternion Derecha = new Quaternion(0.0f, -0.7f, 0.0f, 0.7f);

    public Vector3 PosAbj = new Vector3(-0.0127f, 0.0234f, 0.0063f);
    public Vector3 PosIzq = new Vector3(-0.02294f, -0.0027f, 0.0063f);
    public Vector3 PosArr = new Vector3(0.0009f, -0.0205f, 0.00441f);
    public Vector3 PosDer = new Vector3(0.0224f, -0.0226f, 0.0063f);

    // Start is called before the first frame update
    void Start()
    {
        total = 0;
        Resultado.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Camera camara = GetComponentInChildren<Camera>();
        AnotherMove = GreenPlayer.movimie;
        if (Input.GetKeyDown(KeyCode.Z) && !movimie && !AnotherMove)
        {
            if (PlayerTurn != ControlPlayer.control.Turno)
            {
                return;
            }
            else if (PlayerTurn == ControlPlayer.control.Turno)
            {
            }

            punto = Random.Range(2, 12);
            Debug.Log("Resul" + punto);
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
            if (PlayerTurn.Equals(ControlPlayer.control.Turno))
            {
                if (PlayerTurn > 1)
                {
                }
                ControlPlayer.control.Turno = +1;
            }

        }
    }
    IEnumerator Move()
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
        movimie = false;
        camara.enabled = false;
        GreenCam.enabled = true;

    }

    IEnumerator Move(int resto)
    {
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
            camara.enabled = false;
            GreenCam.enabled = true;
            movimie = false;
        }
    }

    void MoveCamera()
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

    bool MoveToNexNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));

    }
}



