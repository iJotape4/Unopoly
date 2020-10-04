using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(caras);
        NumeroDado();
        caras = GetComponentsInChildren<cara>();
        StartCoroutine(TirarDado());
        PosInicial = GetComponent<Dado>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = "# " + NumeroActual;
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

    public IEnumerator TirarDado()
    {
        float FuerzaInicial = Random.Range(-10, 10 );
        float FuerzaInicial2 = Random.Range(10, 10);
        float multplier = Random.Range(10, 15);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(FuerzaInicial * multplier, 0, FuerzaInicial2* multplier));
        GetComponent<Rigidbody>().rotation = Random.rotation;
        yield return new WaitForSeconds(2f);
        transform.position = PosInicial;
        StartCoroutine(TirarDado());
    }
}
