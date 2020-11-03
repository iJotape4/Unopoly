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
    public Text Creditos;
    public Image TutorialImage;

    public Vector3 IniPosOp1;
    public Vector3 IniPosOp2;
    public Vector3 IniPosOp3;
    public Vector3 IniPosOp4;

    int indice = 0;



    void Start()
    {

        lista = GameObject.Find("Lista");

        Opcion1 = GameObject.Find("Opcion1").GetComponent<Text>();
        Opcion2 = GameObject.Find("Opcion2").GetComponent<Text>();
        Opcion3 = GameObject.Find("Opcion3").GetComponent<Text>();
        Opcion4 = GameObject.Find("Opcion4").GetComponent<Text>();

      

        IniPosOp1 = new Vector3(-329.4f, 212.86f);
        IniPosOp2 = new Vector3(-331, 131);
        IniPosOp3 = new Vector3(-331.4f, 40.5f);
        IniPosOp4 = new Vector3(-334f, -21f);
        flecha = GameObject.Find("Flecha").GetComponent<Text>();

        flecha.rectTransform.localPosition = new Vector2(7.600001f, 151.86f);
        

      
        Opcion1.text = ("NUEVO JUEGO");
        Opcion1.transform.SetParent(lista.transform);
        Opcion1.transform.localPosition = IniPosOp1;
        Opcion1.enabled = true;
        

        
        Opcion2.text = ("CREDITOS");
        Opcion2.transform.SetParent(lista.transform);
        Opcion2.transform.localPosition = IniPosOp2;
        Opcion2.enabled = true;


        Opcion3.text = ("COMO JUGAR?");
        Opcion3.transform.SetParent(lista.transform);
        Opcion3.transform.localPosition = IniPosOp3;
        Opcion3.enabled = true;

        Opcion4.text = ("SALIR");
        Opcion4.transform.SetParent(lista.transform);
        Opcion4.transform.localPosition = IniPosOp4;
        Opcion4.enabled = true;


        TutorialImage = GameObject.Find("TutoImage").GetComponent<Image>();
        TutorialImage.enabled = false;

        // Opcion4.enabled = false;
        //  Opcion4.transform.SetParent(null);

        title = GameObject.Find("Title").GetComponent<Text>();
        title.enabled = false;

        Creditos = GameObject.Find("Creditos").GetComponent<Text>();
        Creditos.enabled = false;

        Dibujar();
    }

    // Update is called once per frame
    void Update()
    {
        if (flecha.enabled)
        {
            bool up = Input.GetKeyDown("up");
            bool down = Input.GetKeyDown("down");

            if (up) indice--;
            if (down) indice++;

            if (indice > lista.transform.childCount - 1) indice = 0;
            if (indice < 0) indice = lista.transform.childCount - 1;

            if (up || down) Dibujar();

            if (Input.GetKeyDown("return") || Input.GetKeyDown("space") || Input.GetKeyDown("x")) Accion();
        }
        
        if (Input.GetKeyDown("c") && TutorialImage.isActiveAndEnabled)
        {
            TutorialImage.enabled = false;
            flecha.enabled = true;
        }

    }

    void Dibujar()
    {
        Transform opcion = lista.transform.GetChild(indice);
        flecha.transform.position = opcion.position;
    }

    void Accion()
    {
        Transform opcion = lista.transform.GetChild(indice);
        if (opcion.gameObject.name == "Opcion4" && opcion.gameObject.GetComponent<Text>().text == "SALIR")
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
            Opcion4.text = ("ATRAS");

        }
        else if (opcion.gameObject.GetComponent<Text>().text == "ATRAS") 
        {
            //Para volver a Main Menu
            Start();

        }
          else if (opcion.gameObject.name == "Opcion1" && opcion.gameObject.GetComponent<Text>().text == "2 Jugadores")
        {
            ControlPlayer.LImitedeTurno = 2;

            Time.timeScale = 1;
            SceneManager.LoadScene("NuevoJuego");
        }
         else if (opcion.gameObject.name == "Opcion2" && opcion.gameObject.GetComponent<Text>().text == "3 Jugadores")
        {
            ControlPlayer.LImitedeTurno = 3;

            Time.timeScale = 1;
            SceneManager.LoadScene("NuevoJuego");
        }
         else if (opcion.gameObject.name == "Opcion3" && opcion.gameObject.GetComponent<Text>().text == "4 Jugadores")
        {
            ControlPlayer.LImitedeTurno = 4;

            Time.timeScale = 1;
            SceneManager.LoadScene("NuevoJuego");
        }    
        else if (opcion.gameObject.name == "Opcion2" && opcion.gameObject.GetComponent<Text>().text == "CREDITOS")
        {

            Creditos.enabled = true;
            Opcion3.enabled = false;
            Opcion3.transform.SetParent(null);
           Opcion4.enabled = false;
           Opcion4.transform.SetParent(null);
           Opcion2.enabled = false;
            Opcion2.transform.SetParent(null);

            Opcion1.text = ("ATRAS");
           
            Opcion1.rectTransform.localPosition = new Vector2(-479, -243f);
           flecha.rectTransform.localPosition = new Vector2(-119f, -322f);
            
            Dibujar();


            SceneManager.LoadScene("NuevoJuego");
        }         else if (opcion.gameObject.name == "Opcion3" && opcion.gameObject.GetComponent<Text>().text == "COMO JUGAR?")
        {
            TutorialImage.enabled = true;
            flecha.enabled = false;
        }        
    }
}
