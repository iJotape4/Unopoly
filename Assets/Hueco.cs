using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hueco : MonoBehaviour
{

     GameObject hueco;
    Player PlayerActual;
    string Tag;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        hueco= GameObject.Find("Hueco");

        hueco.transform.position = new Vector3(-0.279f, -1.213f, -0.04f);
    }

    // Update is called once per frame
    void Update()
    {    
        if (Player.HuecoVisible)
        {

            StartCoroutine(MostrarHueco());
            
        }
        else
        {
            hueco.transform.position = new Vector3(-0.279f, -1.213f, -0.04f);
            return;
        }
    }

    public IEnumerator MostrarHueco()
    {
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();

        hueco.transform.SetParent(PlayerActual.transform);
        hueco.transform.localPosition = new Vector3(5.099997e-05f, 4e-05f, -0.00449f);


        hueco.transform.SetParent(null);
        pos = hueco.transform.TransformPoint(5.099997e-05f, 4e-05f, -0.00449f);
        hueco.transform.position = new Vector3(pos.x, -1.023f, pos.z);

        yield return new WaitForSeconds(0.3f);
      

    }

}
