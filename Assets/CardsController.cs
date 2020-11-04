using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{

    public static Image CardImage;
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
    public static Image IconPass;

    // Start is called before the first frame update
    void Start()
    {
        CardImage = GameObject.Find("Image").GetComponent<Image>();
        ChanceCards = Resources.LoadAll<Sprite>("Chances");
        CardsNum = ChanceCards.Length;

        IconPass = GameObject.Find("ContinuarIcon").GetComponent<Image>();
        IconPass.enabled = false;

        ComArcsCards = Resources.LoadAll<Sprite>("ComArcs");
        CardsNum = ComArcsCards.Length;
        CardImage.enabled = false;          
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
            elegida = Random.Range(0, Cards.Length);
            Debug.Log("carta el" +elegida);
            CardImage.sprite = Cards[elegida];                
        }

        IconPass.enabled = true;
        

        if (Input.GetKey("x"))
        {
            QuitCard();
        }                        
    }

    public void QuitCard()
    {
        IconPass.enabled = false;
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
        //12. Fuiste visto maltratando a una ardilla del campus. tienes que ir a bienestar.
        //13.Un profesor denunció que te reunías con un grupo de amigos para jugar juegos
          //  de azar dentro de la universidad.Bienestar te cita, ve allí.
        else if (elegida == 2 || elegida ==12 || elegida ==13)
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
        //10. Aún te falta completar tus horas libres. Habrá una película en el salón de proyecciones del CSU ,ve a la parada del bus.
        else if (elegida == 7 || elegida==10)
        {
            CardMethodGoTo(13);
        }
        // Te sientes enfermo.Ve a la enfermería a que te revise el médico de bienestar.
         else if (elegida == 8)
        {
            CardMethodGoTo(24);
        }
        // Vas atrasado con los módulos de inglés, paga 50 para ver un curso vacacional
        else if (elegida == 9)
        {
            StartCoroutine(PlayerActual.Pagar(50));
            StartCoroutine(Waiter());
        }
        //11. Estás interesado en una 
        //conferencia sobre intercambios que habrá en el auditorio menor. Ve allí.
        else if (elegida == 11)
        {
            CardMethodGoTo(27);
        }
        //14. Te está yendo mal en clase de cálculo.
        //Ve a la Facultad de ingenierías para obtener información sobre tutorías.
        else if (elegida == 14)
        {
            CardMethodGoTo(14);
        }
        //Solicitaste el préstamo de una laptop de la universidad. 
        //Ve a multimedios para reclamarla.
        else if (elegida == 15)
        {
            CardMethodGoTo(15);
        }
        //16. Necesitas expedir un certificado de estudios. 
        //Ve a punto U para obtener información
        else if (elegida == 16)
        {
            CardMethodGoTo(29);
        }
        //17. Retrocede 3 casillas
        else if (elegida == 17)
        {
            PlayerActual.punto = -3;
            StartCoroutine(PlayerActual.MoveInverso());
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
        //8.Te llegó un subsidio de sostenimiento del icetex.Cobra 200
          else if (elegida == 8)
        {
            StartCoroutine(PlayerActual.Cobrar(200));
            StartCoroutine(Waiter());
        }
        //9. Faltaste a un parcial y realizas una solicitud de supletorio. Paga 10.
        else if (elegida == 9)
        {
            StartCoroutine(PlayerActual.Pagar(10));
            StartCoroutine(Waiter());
        }
        //10. Perdiste tu carnet de estudiante. 
        //Paga 50 para que te sea otorgado uno nuevo.

        else if (elegida == 10)
        {
            StartCoroutine(PlayerActual.Pagar(50));
            StartCoroutine(Waiter());
        }
        //11. Has sido seleccionado para un intercambio estudiantil 
        //y recibes un desembolso por parte del programa. Cobra 300.
        else if (elegida == 11)
        {
            StartCoroutine(PlayerActual.Cobrar(300));
            StartCoroutine(Waiter());
        }
        //12. Te has roto una pierna en un campeonato de basketball de la universidad.
        //Recibes 100 por la póliza del seguro.

        //17. Eres elegido para presentar tu proyecto de semillero en una competencia nacional. 
        //Recibes  200 para gastos de transporte. 
        else if (elegida == 12 || elegida ==17)
        {
            StartCoroutine(PlayerActual.Cobrar(200));
            StartCoroutine(Waiter());
        }
        //13. Hoy es tu cumpleaños. Cada jugador te paga 50.
        else if (elegida == 13)
        {
            StartCoroutine(PlayerActual.Cobrar((ControlPlayer.LImitedeTurno - Player.eliminados -1) * 50));

            foreach ( Player jugador in Casilla.Jugadores){
                if(jugador != PlayerActual)
                {
                    StartCoroutine(jugador.Pagar(50));                                                         
                }                
            }
            StartCoroutine(Waiter());
        }
        //14. Quedaste en primer lugar en ingeniotic y ganas bonos del garage.
        //Cobra 10.
        else if (elegida == 14)
        {
            StartCoroutine(PlayerActual.Cobrar(10));
            StartCoroutine(Waiter());
        }

        //15. Quedaste en primer lugar en un torneo universitario de billar. 
        //Reclama 100.
        else if (elegida == 15)
        {
            StartCoroutine(PlayerActual.Cobrar(100));
            StartCoroutine(Waiter());
        }
        //16. Cancelas una materia dentro de las fechas establecidas en el cronograma académico
        //y recibes una devolución del valor de matrícula. Reclama 50.

        else if (elegida == 16)
        {
            StartCoroutine(PlayerActual.Cobrar(50));
            StartCoroutine(Waiter());
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


