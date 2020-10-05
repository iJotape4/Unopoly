using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Steps;

public class Player : MonoBehaviour
{

    public Text Resultado;
    protected int total;
    public GO Rott;
    public int rpposiicion;
    public int punto;
    public  bool movimie;

    [HideInInspector]
    public int dineroInicial=2000;
    public int dinero;
    public Text PlayerDinero;

    public Camera DadosCamera;
    public Camera OwnCamera;
    public Text PlayerText;
    public Text TextoTirar;
    public Text UseCard;
    public Text TextoPagar;

    public Casilla casiillaSiguiente;

    public int ExitCards;
   
   
    public Color PlayerColor;

    public bool RepiteTurno = false;
    public int PlayerTurn;

    public bool InBienestar=false;

    public static bool Cards;
    public static bool Chances;
    public static bool ComARrcs;

    public static bool Properties;
    public static bool HuecoVisible = false;

    public int NumVueltas=0;

    public Dado dado1;
    public Dado dado2;

    public Rigidbody rigi;
    

    
    public static Quaternion Abajo = new Quaternion(0.3f, 0.0f, 0.0f, 1.0f);
    
    public static Quaternion Izquierda = new Quaternion(0.2f, 0.7f, -0.2f, 0.7f);
    
    public static Quaternion Arriba = new Quaternion(0.0f, 1.0f, -0.3f, 0.0f);
    
    public static Quaternion Derecha = new Quaternion(0.2f, -0.7f, 0.2f, 0.7f);

    
    public static Vector3 PosAbj = new Vector3(-0.0127f, 0.03f, 0.062f);
    
    public static Vector3 PosIzq = new Vector3(-0.0194f, -0.0003f, 0.042f);
    
    public static Vector3 PosArr = new Vector3(0f, -0.02f, 0.0347f);
    
    public static Vector3 PosDer = new Vector3(0.0251f, 0f, 0.057f);

    
    // Start is called before the first frame update
    public void Start()
    {
        
        total = 0;
        Resultado.text = "";
        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();
        PlayerText.enabled = false;

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator LanzarDado()
    {

        MoveCamera();
        TextoTirar.enabled = false;
        dado1 = GameObject.Find("Dado1").GetComponent<Dado>();
        dado2 = GameObject.Find("Dado2").GetComponent<Dado>();
        RestoreText();

        /*if(!dado1.IsMoving() && !dado2.IsMoving())
        {
            dado1.TirarDado();
            dado2.TirarDado();
        }     
        
        while (dado1.IsMoving() || dado2.IsMoving())
        {           
            DadosCamera.enabled = true;
            OwnCamera.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }      

        OwnCamera.enabled = true;
        DadosCamera.enabled = false;
        punto = dado1.NumeroActual + dado2.NumeroActual;
      

        if(dado1.NumeroActual == dado2.NumeroActual)
        {
            RepiteTurno = true;
            StartCoroutine(BienestarText("!!!Doubles!!!"));
        }
        else
        {
            RepiteTurno = false; ;
        } */
        yield return new WaitForSeconds(0.1f);
        punto = Random.Range(3,3);
        //Debug.Log("Resul" + punto);
        total = punto;
        Resultado.text = punto.ToString();

        StartCoroutine(Move());
    }

    public bool MoveToNexNode(Vector3 goal)
    {   
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }

    public void ComprobarOcupacion()
    {
        int posicion = (rpposiicion + 1) - (40 * NumVueltas);
        casiillaSiguiente = GameObject.Find("POST (" + posicion + ")").GetComponent<Casilla>();

        if (casiillaSiguiente.ocupada)
        {
            if (posicion < 10 || posicion == 40)
            {
                casiillaSiguiente.transform.position = new Vector3(casiillaSiguiente.transform.position.x, casiillaSiguiente.transform.position.y, 0.093f);
            }
            else if (posicion >= 30)
            {
                casiillaSiguiente.transform.position = new Vector3( 0.03f, casiillaSiguiente.transform.position.y, casiillaSiguiente.transform.position.z);
            }
                 
            else if (posicion >= 20 )
            {
                casiillaSiguiente.transform.position = new Vector3(casiillaSiguiente.transform.position.x, casiillaSiguiente.transform.position.y, 10.594f); 
            }
            else if (posicion >= 10)
            {
                casiillaSiguiente.transform.position = new Vector3(-10.33f, casiillaSiguiente.transform.position.y, casiillaSiguiente.transform.position.z);
            }   
         }
    }

    public void MoveCamera()
    {
       
        Camera camara = GetComponentInChildren<Camera>();

        int Posicion = rpposiicion - (NumVueltas * 40);
        

        if (Posicion < 10 )
        {
            camara.transform.rotation = Abajo;
            camara.transform.localPosition = PosAbj;
        }
        else if (Posicion >= 30)
        {
            camara.transform.rotation = Derecha;
            camara.transform.localPosition = PosDer;

        }
        else if (Posicion >= 20 || InBienestar)
        {
            camara.transform.rotation = Arriba;
            camara.transform.localPosition = PosArr;
        }
        else if (Posicion >= 10)
        {
            camara.transform.localPosition = PosIzq;
            camara.transform.rotation = Izquierda;

        }
        
    }


    public void FinishTurn()
    {
        movimie = false;
        OwnCamera.enabled = false;

        ControlPlayer.control.NextTurno();
        StartCoroutine(PlayerFontText());
        TextoTirar.enabled = true;
    }

    public IEnumerator PlayerFontText()
    {
        PlayerText.enabled = true;
        PlayerText.fontSize = 20;
        PlayerText.text = ("Player " + ControlPlayer.control.Turno + " Turn!");
        
        while (PlayerText.fontSize > 1) {
            
           PlayerText.fontSize--;
            yield return new WaitForSeconds(0.1f);
            
        }
        
        RestoreText();
    }

    public IEnumerator BienestarText(string Text)
    {
        PlayerText.enabled = true;
        PlayerText.fontSize = 20;
        PlayerText.text = (Text);
        while (PlayerText.fontSize > 1)
        { 
            PlayerText.fontSize--;
            yield return new WaitForSeconds(0.1f);
        }
        RestoreText();
        
    }

    public void RestoreText()
    {
        PlayerText.enabled = false;
        PlayerText.fontSize = 20;
        
    }


    ///AQUI ESTÁ MOVE
    public IEnumerator Move()
    {
       
        Camera camara = GetComponentInChildren<Camera>();
        if (movimie)
        {
            yield break;
        }
        
        movimie = true;
        if (PlayerTurn.Equals(ControlPlayer.control.Turno))
            while (punto > 0)
            {
                ComprobarOcupacion();
                Vector3 nextPos = GO.tablero.Seleccionar(rpposiicion+0).position;
                while (MoveToNexNode(nextPos)) { yield return null; }
                yield return new WaitForSeconds(0.1f);
                punto--;
                rpposiicion++;
              
                MoveCamera();
                           
                
                if (rpposiicion%40 == 0)
                {
                    NumVueltas++;
                    StartCoroutine(Cobrar(200));
                }
            }

        ComprobateCards();
        ComprobateProperties();


        if ((rpposiicion-(40*NumVueltas))%30 ==0  && rpposiicion % 40 != 0)
        {        
            StartCoroutine(GoBienestar());
        }else if (RepiteTurno)
        {
            while (Cards || Properties)
            {
                yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(BienestarText("Lanza de nuevo!!"));
            TextoTirar.enabled = true;
            movimie = false;
        }
        else if ((rpposiicion - (40 * NumVueltas)) % 10 == 0 && rpposiicion % 40 != 0 )
        {
            StartCoroutine(BienestarText("!Sólo visitando!!"));
            yield return new WaitForSeconds(1f);
            FinishTurn();
        }
        else if (!Cards && !Properties && !RepiteTurno)
        {
            FinishTurn();
        }        
    }


    public void MoveTowards(int pos)
    {
        Debug.Log("se mueve hasta"+pos);
        if (pos > rpposiicion)
        {
            punto = pos-rpposiicion;
        }
        else
        {
            punto = 40 - rpposiicion + pos;
        }
        StartCoroutine(Move());
    }

    public void ComprobateCards()
    {
        int TableroPos = rpposiicion - (NumVueltas * 40);
        if ((TableroPos == 2 || TableroPos == 17 || TableroPos == 33)  && punto == 0)
        {
            ComARrcs = true;
            Cards = true;           
            CardsController.turno = PlayerTurn;
        }

        if ((TableroPos == 7 || TableroPos == 22 || TableroPos == 36) && punto == 0)
        {
            Chances = true;
            Cards = true;         
            CardsController.turno = PlayerTurn;
        }
    }

    public void ComprobateProperties()
    {
        int[] properties = new int[30] { 1, 3, 4, 5, 6, 8, 9, 11, 12, 13, 14, 15, 16, 18, 19, 21, 23, 24, 25, 26, 27, 28, 29, 31, 32, 34, 35, 37, 38, 39 };

        foreach (int element in properties)
        {
           // Debug.Log("revisando + " + element);
            if (element == (rpposiicion - (40 * NumVueltas)))
            {
                
                Properties = true;
                //Debug.Log("Properties = true" + rpposiicion);
                break;
            }
        }                   
    }

    public IEnumerator Cobrar(int cantidad)
    {
        int Goal = dinero + cantidad;
        while (dinero != Goal)
        {
            if (dinero - Goal > 10)
            {
                dinero += 10;
            }
            else
            {
                dinero++;
            }

            yield return new WaitForSeconds(0.015f);
        }
    }

    public IEnumerator Pagar(int cantidad)
    {
        int Goal = dinero - cantidad;
        while (dinero != Goal)
        {
            if (dinero - Goal > 10)
            {
                dinero -= 10;
            }
            else
            {
                dinero--;
            } 
           
            yield return new WaitForSeconds(0.015f);
        }            
    }

    public IEnumerator GoBienestar()
    {
        //Hay que corregirlo y hacer un teletransporte animado como el de monopoly 64 https://www.youtube.com/watch?v=CyDnh7eVCl8 19:40      
        Vector3 bienestar = GameObject.Find("Bienestar").transform.position;
        HuecoVisible = true;
        rigi.isKinematic = false;

        Player parent = OwnCamera.transform.GetComponentInParent<Player>();

        while (transform.position != bienestar)
        {
            OwnCamera.transform.SetParent(null);
            yield return new WaitForSeconds(1f);
            transform.position = (bienestar);
            OwnCamera.transform.SetParent(parent.transform);
        }        
        //Esto es para que caiga
        rigi.isKinematic = true;
        rpposiicion = 10;     
       
        InBienestar = true;
        MoveCamera();

        StartCoroutine(BienestarText("!!En bienestar!!"));
        yield return new WaitForSeconds(1f);
        HuecoVisible = false;
       ResetBienestarGuide();

        FinishTurn();
        
    }

    public IEnumerator SalirBienestar()
    {
        TextoTirar.enabled = true;
        TextoPagar.enabled = true;

        if (ExitCards > 0)
        {
            UseCard.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            dado1 = GameObject.Find("Dado1").GetComponent<Dado>();
            dado2 = GameObject.Find("Dado2").GetComponent<Dado>();

            if (!dado1.IsMoving() && !dado2.IsMoving())
            {
                dado1.TirarDado();
                dado2.TirarDado();
            }

            while (dado1.IsMoving() || dado2.IsMoving())
            {
                DadosCamera.enabled = true;
                OwnCamera.enabled = false;
                yield return new WaitForSeconds(0.1f);
            }

            OwnCamera.enabled = true;
            DadosCamera.enabled = false;

            if(dado1.NumeroActual == dado2.NumeroActual)
            {
                
                punto = dado1.NumeroActual + dado2.NumeroActual;
                total = punto;
                Resultado.text = punto.ToString();
                StartCoroutine(Move());
                InBienestar = false;
            }
            else
            {
                FinishTurn();
            }
            ResetBienestarGuide();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Pagar(200));
           
            ResetBienestarGuide();
            StartCoroutine(LanzarDado());
            InBienestar = false;
        }

        if (Input.GetKeyDown(KeyCode.C)  && ExitCards > 0)
        {
            
            ExitCards--;
            ResetBienestarGuide();
            StartCoroutine(LanzarDado());
            InBienestar = false;
        }      
        yield return new WaitForSeconds(1f);
    }

    public void ResetBienestarGuide()
    {
        TextoTirar.enabled = false;
        TextoPagar.enabled = false;
        UseCard.enabled = false;
    }
}

