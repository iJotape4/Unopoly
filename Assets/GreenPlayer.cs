using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class GreenPlayer : MonoBehaviour
{
    public Text Resultado;
    private int total;
    public GO Rott;
    int rpposiicion;
    public int punto;
    bool movimie;
    // Start is called before the first frame update
    void Start()
    {
        total =0;
        Resultado.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !movimie)
        {
            punto = Random.Range(2, 12);
            Debug.Log("Resul"+punto);
            total = punto;
            Resultado.text = " " + total;
            if (rpposiicion + punto < Rott.Puesto.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                Debug.Log("Resultado");
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
    bool MoveToNexNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f *Time.deltaTime));

    }

     
    }
