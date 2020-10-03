using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Propiedad : MonoBehaviour
{

    public int numcasilla;
    public int precio;

    public Image PropertyIMage;
    public Sprite[] PropertyCards;
    public Player propietario;

    public int casas;
    public int Posicion;
    public static int tocando;

    public string Tag;

    Player PlayerActual;



    // Start is called before the first frame update
    void Start()
    {
        PropertyIMage = GameObject.Find("Image").GetComponent<Image>();

        PropertyCards = Resources.LoadAll<Sprite>("Properties");

        PropertyIMage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Properties)
        {
            ShowPropertie();
        }
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();
        
    }

    public void ShowPropertie()
    {
        for (int i=0; i<PropertyCards.Length; i++)
        {
            if(int.Parse(PropertyCards[i].name) == PlayerActual.rpposiicion)
            {
                PropertyIMage.sprite = PropertyCards[i];
                break;
            }
        }
        PropertyIMage.enabled = true;

        if (Input.GetKey("x"))
        {
            Comprar();
        }else if (Input.GetKey("z"))
        {
            QuitCard();
        }

    }
    public void QuitCard()
    {
        PropertyIMage.enabled = false;
        Player.Properties = false;
        StartCoroutine(Waiter());
    }

    public void Comprar()
    {
        PlayerActual.dinero -= precio ;
        propietario = PlayerActual;
        QuitCard();
    }

    public IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerActual.FinishTurn();
    }

}
