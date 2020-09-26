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
    bool movimie;
    public int resto;

    public int PlayerTurn = 2;

    // Start is called before the first frame update
    void Start()
    {
        total = 0;
        Resultado.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !movimie)
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
            }
        movimie = false;
    }

    IEnumerator Move(int resto)
    {
        {
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
            }

            movimie = false;
        }
    }
    bool MoveToNexNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));

    }
}



