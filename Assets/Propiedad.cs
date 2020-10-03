using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Propiedad : MonoBehaviour
{
    public int precio;

    [HideInInspector]
    public Image PropertyIMage;
    [HideInInspector]
    public Sprite[] PropertyCards;

    [HideInInspector]
    public Text TextoComprar;
    [HideInInspector]
    public Text TextoPasar;
    
    public Player propietario;

    public int casas;
    public int Renta;

    [HideInInspector]
    public string Tag;

    Player PlayerActual;

    [HideInInspector]
    public Propiedad Tarjeta;

    // Start is called before the first frame update
    void Start()
    {
        PropertyIMage = GameObject.Find("Image").GetComponent<Image>();

        PropertyCards = Resources.LoadAll<Sprite>("Properties");

        TextoComprar = GameObject.Find("TextoComprar").GetComponent<Text>();
        TextoPasar = GameObject.Find("TextoPasar").GetComponent<Text>();

        TextoComprar.enabled = false;
        TextoPasar.enabled = false;

        PropertyIMage.enabled = false;
        Tarjeta = this.GetComponent<Propiedad>();
    }

    // Update is called once per frame
    void Update()
    {
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();
        if (Player.Properties)
        {
            
            if (Tarjeta.propietario == null)
            {
                ShowPropertie();
            }
            else
            {
                StartCoroutine(PlayerActual.Pagar(Renta));
              StartCoroutine(Tarjeta.propietario.Cobrar(Renta));
              PlayerActual.StartCoroutine(Waiter());
            }
            
        }            
    }

    public void ShowPropertie()
    {
        for (int i=0; i<PropertyCards.Length; i++)
        {
            if(int.Parse(PropertyCards[i].name) == PlayerActual.rpposiicion)
            {
                PropertyIMage.sprite = PropertyCards[i];               
                Tarjeta = GameObject.Find("POST ("+PropertyCards[i].name+")").GetComponent<Propiedad>();
                break;
            }
        }
        PropertyIMage.enabled = true;

        TextoComprar.text = ("X) Comprar $" + Tarjeta.precio);
        TextoComprar.enabled = true;
        TextoPasar.enabled = true;

        if (Input.GetKey("x"))
        {
            StartCoroutine(Comprar());
        }else if (Input.GetKey("z"))
        {
            QuitCard();
        }

    }
    public void QuitCard()
    {
        TextoComprar.enabled = false;
        TextoPasar.enabled = false;

        PropertyIMage.enabled = false;
        
        StartCoroutine(Waiter());
    }

    public IEnumerator Comprar()
    {

        StartCoroutine(PlayerActual.Pagar(Tarjeta. precio)) ;
        Tarjeta.propietario = PlayerActual;
        QuitCard();
        yield return null;
    }

    public IEnumerator Waiter()
    {
        Player.Properties = false;
        yield return new WaitForSeconds(0.5f);
        PlayerActual.FinishTurn();
    }

    

}
