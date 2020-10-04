using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cara : MonoBehaviour
{

    public int Numero;
    public bool TocaSuelo;

        // Start is called before the first frame update
    void Start()
    {
        Numero = int.Parse(GetComponent<cara>().name) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tablero") ;
        TocaSuelo = true;
    }
    private void OnTriggerExit(Collider other)
    {
        TocaSuelo = false;
    }

}
