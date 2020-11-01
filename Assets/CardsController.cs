using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{

    public Image CardImage;
    public Sprite[] ChanceCards;
    public Sprite[] ComArcsCards;
    public int CardsNum;
    public int elegida;
    public static int turno;

    public bool Chances = false;

    public static int[] cafeterias = new int[4] { 8, 16, 26, 38};
    public int[] laboratoriosArr = new int[1] { 34 };

    public string Tag ;

    Player PlayerActual;
    public Text TextoSiguiente;

    // Start is called before the first frame update
    void Start()
    {

        TextoSiguiente = GameObject.Find("TextoPasar").GetComponent<Text>();

        CardImage = GameObject.Find("Image").GetComponent<Image>();
        ChanceCards = Resources.LoadAll<Sprite>("Chances");
        CardsNum = ChanceCards.Length;

        ComArcsCards = Resources.LoadAll<Sprite>("ComArcs");
        CardsNum = ComArcsCards.Length;
        CardImage.enabled = false;

       // ComArcsCards.Append(CardImage.sprite);
    
        
       
    }

    // Update is called once per frame
    void Update()
    {
       if (Player.Cards)
        {
            ShowCard();
        }
        Tag = ("Player" + ControlPlayer.control.Turno);
        PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<Player>();
    }
    public  void ShowCard()
    {
        Sprite[] Cards = {};

        if (!CardImage.enabled) {

            if (Player.Chances)
            {
                Cards = ChanceCards;
                Player.Chances = false;
                Chances = true;
            }
            if (Player.ComARrcs)
            {
                Cards = ComArcsCards;
                Player.ComARrcs = false;
            }
            CardImage.rectTransform.sizeDelta = new Vector2(1500, 822);
            CardImage.enabled = true;
            elegida = Random.Range(2,2);
            Debug.Log("carta el" +elegida);
            CardImage.sprite = Cards[elegida];                
        }

        TextoSiguiente.enabled = true;
        TextoSiguiente.text = "X) Continuar";

        if (Input.GetKey("x"))
        {
            QuitCard();
        }                        
    }

    public void QuitCard()
    {
        TextoSiguiente.enabled = false;
        CardImage.enabled = false;
        Player.Cards = false;
        Player.ExecutingCardMethod = true;
        if (Chances)
        {
            CallCardMethodChances(elegida);
        }
        else
        {
            CallCardMethodComArcs(elegida);
        }

        Chances = false;
    }

    public  void CallCardMethodChances(int elegida)
    {
        //0. Te dio hambre, ve a la cafetería más cercana, si pasas por la salida, cobra.
        if (elegida == 0)
        {
            Cafetería();

        }
        //1. Tienes tu carro en el parqueadero, avanza hasta ahí para recogerlo.
        else if (elegida == 1)
        {
            GoParkway();

        }
        //2.Te vieron fumando en Banu, tienes que ir a bienestar.  
        else if (elegida == 2)
        {
            GoBienestar();

        }
        //3. Perdiste un libro de la biblioteca, paga 200
        else if (elegida == 3)
        {
            StartCoroutine(PlayerActual.Pagar(100));
            StartCoroutine(Waiter());

        }
        //4.Avanza hasta la salida, cobra 200
        else if (elegida == 4)
        {
            GoToGo();
        }

        //5.Puedes salir de bienestar gratis
        else if (elegida == 5)
        {
            PlayerActual.ExitCards++;
            StartCoroutine(Waiter());
        }
        //6. Ve al laboratorio de telecomunicaciones
        else if (elegida == 6)
        {

            GoToLaboratory();
        }
        //7. Tienes un taller de deporte, ve al CSU
        else if (elegida == 7)
        {
            CardMethodGoTo(13);
        }
        
    }


    public void CallCardMethodComArcs(int elegida)
    {
        //0. Te enfermaste, tienes que pagarle al medico de bienestar 100
        if (elegida == 0)
        {
            StartCoroutine(PlayerActual.Pagar(100));
            StartCoroutine(Waiter());

        }
        //1.Se venció tu plazo para pagar el semestre, paga 200 de penalización
        //3.En bienestar te matricularon en un taller contra tu voluntad, paga 200 para cancelarlo
        //8.Como perdiste tu matricula por no presentar la solicitud en 2 años, tienes que empezar de 0 otra carrera, paga 200 de matricula.
        else if (elegida == 1 || elegida == 3 || elegida == 8)
        {
            StartCoroutine(PlayerActual.Pagar(200));
            StartCoroutine(Waiter());

        }
        //2.Se venció tu prestamo universitario, paga 400 para buscar uno nuevo
        else if (elegida == 2)
        {
            StartCoroutine(PlayerActual.Pagar(400));
            StartCoroutine(Waiter());

        }
       
        //4.Rompiste un pc del bloque L, paga 100
        else if (elegida == 4)
        {
            StartCoroutine(PlayerActual.Pagar(100));
            StartCoroutine(Waiter());

        }
        //5. Perdiste un curso en 2.5, puedes habilitarlo, paga 50 
        //6.Faltaste al 20 % de clases, paga 50 para cancelar el curso
        else if (elegida == 5 || elegida == 6)
        {
            StartCoroutine(PlayerActual.Pagar(50));
            StartCoroutine(Waiter());

        }
        //7.Estás faltando mucho a clases, bienestar te cita, ve allí
        else if (elegida == 7)
        {
            StartCoroutine(PlayerActual.GoBienestar());
        }
       
    }

    public IEnumerator Waiter()
    {
        Player.ExecutingCardMethod = false;
        if (!PlayerActual.RepiteTurno)
        {
            yield return new WaitForSeconds(0.2f);
            PlayerActual.FinishTurn();
        }
        else
        {
            Player.PuedeTirar = true;
        }
       
    }
    //Tienes Tu carro en el parqueadero, avanza hasta ahí para recogerlo
    public void GoParkway()
    {
        CardMethodGoTo(20);
    }

    public void GoBienestar()
    {

        StartCoroutine(PlayerActual.GoBienestar());
    }

    //Te dio hambre, ve a la cafetería más cercana, si pasas por la salida, cobra.
    public void Cafetería()
    {
        int CafElegida = 0;
        foreach (int Cafetería in cafeterias)
        {
            if (Cafetería > PlayerActual.tableroPos)
            {
                CafElegida = Cafetería;
                break;
            }
            else
            {
                CafElegida = cafeterias[0];

            }
        }
        Debug.Log("caf" + CafElegida);
        CardMethodGoTo(CafElegida);

    }


    //Avanza hasta la salida, cobra 200
    public void GoToGo()
    {
        CardMethodGoTo(40);
        
    }

    //Avanza hasta el laboratorio más cercsano
    public void GoToLaboratory()
    {
        int LaboElegido = 0;
        foreach (int Laboratorio in laboratoriosArr)
        {
            Debug.Log("revisando" + Laboratorio);
            if (Laboratorio > PlayerActual.tableroPos)
            {
                LaboElegido = Laboratorio;
                Debug.Log("labo" + LaboElegido); 
                break;
            }
            else
            {
                LaboElegido = laboratoriosArr[0];
                
            }
        }
        CardMethodGoTo(LaboElegido);
    }


    //Método General para pagar desde ésta clase
 /*   public void Pagar(int cantidad)
    {
        PlayerActual.dinero -= cantidad;
    }
    */
    //Método General para ir a una posición desde ésta clase
    public void  CardMethodGoTo(int pos)
    {
        PlayerActual.movimie = false ;
        PlayerActual.MoveTowards(pos);
       
        PlayerActual.ComprobateProperties();

        
      
    

    }

}


