using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public Text Opcion1;
    public Text Opcion2;
    public Text Opcion3;
    public Text Opcion4;

    public Text Arrow;

    public GameObject lista;
    int indice = 0;

    public GameObject self;

    public Image IconEscoger;
    public Image IconUnPause;
    public Image IconUpDown;
    public Image TutorialImage;

    public Canvas canvasPause;

    public Player PlayerActual;
    public string Tag;

    public Dado dado1;
    public Dado dado2;

    public void Awake() 
    {
        this.self = gameObject;

    }


    void Start()
    {

        dado1 = GameObject.Find("Dado1").GetComponent<Dado>();
        dado2 = GameObject.Find("Dado2").GetComponent<Dado>();

        Opcion1 = GameObject.Find("Opcion1").GetComponent<Text>();
        Opcion2 = GameObject.Find("Opcion2").GetComponent<Text>();
        Opcion3 = GameObject.Find("Opcion3").GetComponent<Text>();
        Opcion4 = GameObject.Find("Opcion4").GetComponent<Text>();

        IconEscoger = GameObject.Find("EscogerIcon").GetComponent<Image>();
        IconUnPause = GameObject.Find("UnpauseIcon").GetComponent<Image>();
        IconUpDown = GameObject.Find("UpDownIcon").GetComponent<Image>();

        TutorialImage = GameObject.Find("TutorialImage").GetComponent<Image>();


        IconEscoger.enabled = false;
        IconUnPause.enabled = false;
        IconUpDown.enabled = false;

        TutorialImage.enabled = false;

        lista = GameObject.Find("Lista");

        canvasPause = gameObject.GetComponentInParent<Canvas>();

        Arrow = GameObject.Find("Flecha").GetComponent<Text>();
        Arrow.enabled = false;
        canvasPause.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {     

        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();
        if (Input.GetKeyDown("space") && !dado1.IsMoving() && !dado2.IsMoving())
        {
            Pausar();
        }

        if(canvasPause.enabled && Arrow.enabled)
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

        void Pausar()
        {
            canvasPause.enabled = !canvasPause.isActiveAndEnabled;
            Arrow.enabled = true; ;
            IconEscoger.enabled = !IconEscoger.isActiveAndEnabled;
            IconUnPause.enabled = !IconUnPause.isActiveAndEnabled;
            IconUpDown.enabled = !IconUpDown.isActiveAndEnabled;
            TutorialImage.enabled = false;

            indice = 0;
            Transform opcion = lista.transform.GetChild(0);
            Arrow.transform.position = opcion.position;
            Time.timeScale = (canvasPause.isActiveAndEnabled) ? 0 : 1;
        }


        void Dibujar()
        {
           
            Transform opcion = lista.transform.GetChild(indice);
            Arrow.transform.position = opcion.position;
        }

        void Accion()
        {
            Transform opcion = lista.transform.GetChild(indice);
            if (opcion.gameObject.name == "Opcion1")
            {

                Pausar();
            }
            else if (opcion.gameObject.name == "Opcion4")
            {
                
                SceneManager.LoadScene("Menu");

            }else if (opcion.gameObject.name == "Opcion2")
            {

                TutorialImage.enabled = true;
                Arrow.enabled = false;

            }
            else if (opcion.gameObject.name == "Opcion3")
            {
                Pausar();
                StartCoroutine(PlayerActual.Perder(" se retira"));
            }


        }

        if (Input.GetKeyDown("c") && TutorialImage.isActiveAndEnabled)
        {
            TutorialImage.enabled = false;
            Arrow.enabled = true;
        }


    }
}


