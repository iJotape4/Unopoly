using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject  lista;
    public Text flecha;

    public Text Opcion1;
    public Text Opcion2;
    public Text Opcion3;
    public Text Opcion4;

    public Text title;



    int indice = 0;
    void Start()
    {
        flecha = GameObject.Find("Flecha").GetComponent<Text>();

        flecha.rectTransform.localPosition = new Vector2(7.600001f, 151.86f);
        

        Opcion1 = GameObject.Find("Opcion1").GetComponent<Text>();
        Opcion1.text = ("NUEVO JUEGO");

        Opcion2 = GameObject.Find("Opcion2").GetComponent<Text>();
        Opcion2.text = ("CREDITOS");

        Opcion3 = GameObject.Find("Opcion3").GetComponent<Text>();
        Opcion3.text = ("SALIR");

        Opcion4 = GameObject.Find("Opcion4").GetComponent<Text>();     

        Opcion4.enabled = false;
        Opcion4.transform.SetParent(null);

        title = GameObject.Find("Title").GetComponent<Text>();
        title.enabled = false;

        Dibujar();
    }

    // Update is called once per frame
    void Update()
    {
        bool up = Input.GetKeyDown("up");
        bool down = Input.GetKeyDown("down");

        if (up) indice--;
        if (down) indice++;

        if (indice > lista.transform.childCount - 1) indice = 0;
        if (indice < 0) indice = lista.transform.childCount - 1;

        if (up || down) Dibujar();

        if (Input.GetKeyDown("return")) Accion();

    }

    void Dibujar()
    {
        Transform opcion = lista.transform.GetChild(indice);
        flecha.transform.position = opcion.position;
    }

    void Accion()
    {
        Transform opcion = lista.transform.GetChild(indice); 
        if (opcion.gameObject.name == "Opcion3" && opcion.gameObject.GetComponent<Text>().text =="SALIR")
        {
            print("cerrando Juego");
            Application.Quit();
        }
        else if (opcion.gameObject.name == "Opcion1" && opcion.gameObject.GetComponent<Text>().text == "NUEVO JUEGO")
        {
            Opcion4.transform.SetParent(lista.transform);
            Opcion4.enabled = true;

            Opcion4.rectTransform.localPosition = new Vector2(-335, -38.3f);
            Dibujar();

            title.enabled = true;
            Opcion1.text = ("2 Jugadores");
            Opcion2.text = ("3 Jugadores");
            Opcion3.text = ("4 Jugadores");

        }
          else if (opcion.gameObject.name == "Opcion4")
        {
            //Para volver a Main Menu
            Start();

        }
          else if (opcion.gameObject.name == "Opcion1" && opcion.gameObject.GetComponent<Text>().text == "2 Jugadores")
        {
            ControlPlayer.LImitedeTurno = 2;


            SceneManager.LoadScene("NuevoJuego");
        }
         else if (opcion.gameObject.name == "Opcion2" && opcion.gameObject.GetComponent<Text>().text == "3 Jugadores")
        {
            ControlPlayer.LImitedeTurno = 3;


            SceneManager.LoadScene("NuevoJuego");
        }
         else if (opcion.gameObject.name == "Opcion3" && opcion.gameObject.GetComponent<Text>().text == "4 Jugadores")
        {
            ControlPlayer.LImitedeTurno = 4;


            SceneManager.LoadScene("NuevoJuego");
        }        
    }
}
