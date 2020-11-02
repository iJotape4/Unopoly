using System.Collections;
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
    public Image IconComprar;

    [HideInInspector]
    public Image IconPass;

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


        IconComprar = GameObject.Find("ComprarIcon").GetComponent<Image>();
        IconPass = GameObject.Find("PasarIcon").GetComponent<Image>();

        IconComprar.enabled = false;
        IconPass.enabled = false;

        PropertyIMage.enabled = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();
       

         //Tarjeta = this.GetComponent<Propiedad>();
        if (Player.Properties)
        {
            for (int i = 0; i < PropertyCards.Length; i++)
            {
                if (int.Parse(PropertyCards[i].name) == PlayerActual.tableroPos)
                {
                    Tarjeta = GameObject.Find("POST (" + PropertyCards[i].name + ")").GetComponent<Propiedad>();
                    PropertyIMage.sprite = PropertyCards[i];
                    break;
                }
            }

            if (Tarjeta.propietario == null)
            {
                ShowPropertie();
            }
            else
            {
                StartCoroutine(PlayerActual.Pagar(Tarjeta.Renta));           
              StartCoroutine(Tarjeta.propietario.Cobrar(Tarjeta.Renta));
                Debug.Log("player" + PlayerActual.PlayerTurn + "paga $" + Tarjeta.Renta + " a Player" + Tarjeta.propietario.PlayerTurn + "Por la propiedad #" + Tarjeta.name) ;
              PlayerActual.StartCoroutine(Waiter());
            }
            
        }            
    }

    public void ShowPropertie()
    {
        PropertyIMage.rectTransform.sizeDelta = new Vector2(700, 822);
        PropertyIMage.enabled = true;

       
        IconComprar.enabled = true;
        IconPass.enabled = true;
    

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
        IconComprar.enabled = false;
        IconPass.enabled = false;

        PropertyIMage.enabled = false;
        
        StartCoroutine(Waiter());
    }

    public IEnumerator Comprar()
    {
        StartCoroutine(PlayerActual.Pagar(Tarjeta.precio)) ;
        Tarjeta.propietario = PlayerActual;
        QuitCard();
        yield return null;
    }

    public IEnumerator Waiter()
    {
        Player.Properties = false;
        if (!PlayerActual.RepiteTurno)
        {          
            yield return new WaitForSeconds(0.5f);
            PlayerActual.FinishTurn();
        }
        else
        {
            Player.PuedeTirar = true;
        }             
    }

    

}
