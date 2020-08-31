using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GO : MonoBehaviour
{
    Transform[] lista;
    public List<Transform> Puesto = new List<Transform>();
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Puesto.Clear();
        lista = GetComponentsInChildren<Transform>();

        foreach (Transform child in lista)
        {
            if (child != this.transform)
            {
                Puesto.Add(child);
            }
        }
    }
}
