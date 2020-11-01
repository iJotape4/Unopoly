﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    protected int total;
    public GO Rott;
    public int rpposiicion;
    public int punto;
    public int tableroPos;
    public  bool movimie;

    [HideInInspector]
    public int dineroInicial=2000;
    public int dinero;
    public Text PlayerDinero;

    public Camera DadosCamera;
    public Camera OwnCamera;
    public Camera BienestarCamera;
    public Text PlayerText;
    public Text TextoTirar;
    public Text UseCard;
    public Text TextoPagar;

    public Casilla casiillaSiguiente;
    public Casilla bienestar;

    public int ExitCards;
   
   
    public Color PlayerColor;

    public bool RepiteTurno = false;
    public int PlayerTurn;

    public bool InBienestar=false;
    public bool inChameraRotateChange = false;

    public static bool Cards;
    public static bool Chances;
    public static bool ComARrcs;
    public static bool ExecutingCardMethod=false;

    public static bool Properties;
    public static bool HuecoVisible = false;


    public static bool PuedeTirar=true;

    public int NumVueltas=0;
    public string MeshSeted;

    public Dado dado1;
    public Dado dado2;

    public Rigidbody rigi;

    public GameObject self;
    public MeshFilter MeshFilter;
    
    public  static Quaternion Abajo = new Quaternion(0.4f, 0.0f, 0.0f, 1.0f);
    
    public static  Quaternion Izquierda = new Quaternion(0.2f, 0.6f, -0.2f, 0.6f);
    
    public static  Quaternion Arriba = new Quaternion(0.0f, 1.0f, -0.5f, 0.0f);
    
    public static  Quaternion Derecha = new Quaternion(0.2f, -0.6f, 0.2f, 0.6f);

    public  Quaternion rotationActual;

    [HideInInspector]
    public  Quaternion GirarAbajo;
    [HideInInspector]
    public  Quaternion GirarIzq;
    [HideInInspector]
    public  Quaternion GirarArriba;
    [HideInInspector]
    public  Quaternion GirarDerecha;




   public static Vector3 PosAbj = new Vector3(-0.02135776f, 0.0370839f, 0.124f);
    
    public static Vector3 PosIzq = new Vector3(-0.042f, -0.036f, 0.151f);
    
    public static Vector3 PosArr = new Vector3(0.01085193f, -0.03380395f, 0.2340318f);
    
    public static Vector3 PosDer = new Vector3(0.0391f, 0.04f, 0.154f);


    public void Awake()
    {
        this.self = gameObject;
    }

    // Start is called before the first frame update
    public void Start()
    {

        total = 0;
        //  Resultado.text = "";

        OwnCamera = GameObject.Find("GreenCamera").GetComponent<Camera>();

        

        dinero = dineroInicial;

        PlayerDinero = GameObject.Find("MoneyText").GetComponent<Text>();
        PlayerDinero.text = "$" + dinero.ToString();
        PlayerDinero.enabled = true;

        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();
        PlayerText.enabled = false;

        Rott = GameObject.Find("GO").GetComponent<GO>();


        OwnCamera.enabled = true;

        DadosCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        DadosCamera.enabled = false;

        TextoTirar = GameObject.Find("TextoTirar").GetComponent<Text>();
        TextoTirar.enabled = true;

        UseCard = GameObject.Find("TextoTSalida").GetComponent<Text>();
        UseCard.enabled = false;

        TextoPagar = GameObject.Find("TextoPagar").GetComponent<Text>();
        TextoPagar.enabled = false;

        bienestar = GameObject.Find("Bienestar").GetComponent<Casilla>();

        BienestarCamera = GameObject.Find("BienestarCamera").GetComponent<Camera>();
        BienestarCamera.enabled = false;

        rigi = GetComponent<Rigidbody>();

        MeshFilter = GetComponent<MeshFilter>();

      //  rotationActual = OwnCamera.transform.rotation;

        if (ControlPlayer.LImitedeTurno < PlayerTurn)
        {
            self.SetActive(false);

        }
        //SetCameraPosByMesh();

      

    }

    void SetCameraPosByMesh()
    {
        if (MeshFilter.mesh.name == "Mesh.005 Instance"){
        PosAbj = new Vector3(-0.02135776f, 0.0370839f, 0.124f);
        PosIzq = new Vector3(-0.042f, -0.036f, 0.151f);
        PosArr = new Vector3(0.01085193f, -0.03380395f, 0.2340318f);
        PosDer = new Vector3(0.0391f, 0.04f, 0.154f);
}
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn == ControlPlayer.control.Turno)
        {
            PlayerDinero.text = "$" + dinero.ToString();
            PlayerDinero.color = PlayerColor;
            
            if (InBienestar)
            {

                if (PlayerTurn != ControlPlayer.control.Turno)
                {
                    return;
                }
                
                StartCoroutine(SalirBienestar());
            }
        }



        if (Input.GetKeyDown(KeyCode.X) && !movimie && !Cards && !Properties && !InBienestar  && PuedeTirar)
        {
            if (PlayerTurn != ControlPlayer.control.Turno)
            {
                return;
            }
            PuedeTirar = false;
            StartCoroutine(LanzarDado());
        }
    }


    public IEnumerator LanzarDado()
    {
        BienestarCamera.enabled = false;
        TextoTirar.enabled = false;
        dado1 = GameObject.Find("Dado1").GetComponent<Dado>();
        dado2 = GameObject.Find("Dado2").GetComponent<Dado>();
        RestoreText();
        
        if(!dado1.IsMoving() && !dado2.IsMoving() )
        {
            dado1.TirarDado();
            dado2.TirarDado();
        }     
        
        while (dado1.IsMoving() || dado2.IsMoving())
        {           
            DadosCamera.enabled = true;
            OwnCamera.enabled = false;
            yield return new WaitForSeconds(0.001f);
        }

        //yield return new WaitForSeconds(0.2f);

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
            RepiteTurno = false;
    } 
        yield return new WaitForSeconds(0.1f);
       punto = Random.Range(7,7);
        total = punto;

        StartCoroutine(Move());

        
    }

    public bool MoveToNexNode(Vector3 goal)
    {   
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }

    public void ComprobarOcupacion()
    {
        int posicion = tableroPos + 1;


        //Debug.Log(tableroPos);
        casiillaSiguiente = GameObject.Find("POST (" + posicion + ")").GetComponent<Casilla>();

        if (casiillaSiguiente.ocupada)
        {
            if (casiillaSiguiente.ocupadaby2)
            {
                if (casiillaSiguiente.ocupadaby3)
                {
                    MoverCasilla(3);
                    casiillaSiguiente.ocupadaby2 = false;
                    casiillaSiguiente.ocupadaby3 = false;
                }
                else
                {
                    MoverCasilla(2);
                    casiillaSiguiente.ocupadaby3 = true;
                }              
            }
            else
            {
                MoverCasilla(1);
                casiillaSiguiente.ocupadaby2 = true;
            }
           
        }
    }

    public  void MoverCasilla(int PlayersNum)
    {
        int posicion = tableroPos + 1;
        float diff = 0.39f;
        float diffLats = 0.48f;

        diff *= PlayersNum;
        diffLats *= PlayersNum;

        if (posicion < 10 || posicion == 40)
        {
            casiillaSiguiente.transform.position = new Vector3(casiillaSiguiente.transform.position.x, casiillaSiguiente.transform.position.y, (casiillaSiguiente.transform.position.z + diff));

        }
        else if (posicion >= 30)
        {
            casiillaSiguiente.transform.position = new Vector3((casiillaSiguiente.transform.position.x - diffLats), casiillaSiguiente.transform.position.y, casiillaSiguiente.transform.position.z);
        }

        else if (posicion >= 20)
        {
            casiillaSiguiente.transform.position = new Vector3(casiillaSiguiente.transform.position.x, casiillaSiguiente.transform.position.y, (casiillaSiguiente.transform.position.z - diff));
        }
        else if (posicion >= 10)
        {
            casiillaSiguiente.transform.position = new Vector3((casiillaSiguiente.transform.position.x + diffLats), casiillaSiguiente.transform.position.y, casiillaSiguiente.transform.position.z);

        }
        
    }

    public void RotatePosicion()
    {


        int Posicion = tableroPos;

        

        if (InBienestar)
        {
            //camara.transform.rotation = new Quaternion(0.0f, 1.0f, -0.3f, 0.0f);
           // camara.transform.localPosition = new Vector3(0f, -0.0773f, 0.0501f);
        }
        else if (Posicion < 9)
        {
            //camara.transform.rotation = Abajo;
            //camara.transform.localPosition = PosAbj;
            //camara.transform.rotation = new Quaternion(1f, 0.0f, -0.0f, 0.0f);

            self.transform.rotation = GirarAbajo;
        }
        else if (Posicion >= 29)
        {
            //camara.transform.rotation = Derecha;
           // camara.transform.localPosition = PosDer;

            self.transform.rotation = GirarDerecha;

        }
        else if (Posicion >= 19 )
        {
         //   camara.transform.rotation = Arriba;
         //   camara.transform.localPosition = PosArr;

            self.transform.rotation = GirarArriba;
        }
        else if (Posicion >= 9)
        {
            //camara.transform.localPosition = PosIzq;
            //camara.transform.rotation = Izquierda;
           self.transform.rotation = GirarIzq;

        }
       
        
    }



    public void FinishTurn()
    {
        BienestarCamera.enabled = false;
        
        if (ExecutingCardMethod)
        {
            ExecutingCardMethod = false;
        }
        movimie = false;

        MoveCamera();

       

        
        ChangeCameraParent();
   

        StartCoroutine(PlayerFontText());
        TextoTirar.enabled = true;
        PuedeTirar = true;
    }


    public void ChangeCameraParent()
    {
        ControlPlayer.control.NextTurno();
      
        //SetCameraPosByMesh();
  
            OwnCamera.transform.SetParent(GameObject.FindGameObjectWithTag("Player" + ControlPlayer.control.Turno).transform);
        
     
 
     
         if(OwnCamera.transform.parent.tag == "Player4")
        {
            OwnCamera.transform.localPosition = new Vector3(-0.5f, -0.3f, 0f);
        }
         else if(OwnCamera.transform.parent.tag == "Player2")
        {
            OwnCamera.transform.localPosition = new Vector3(-95.8f, 7.4f, 247.6f);
        }
         else if(OwnCamera.transform.parent.tag == "Player1")
        {
            OwnCamera.transform.localPosition = new Vector3(-0.0014f, 0.0428f, 0.1346f);
        }
         else if(OwnCamera.transform.parent.tag == "Player3")
        {
            OwnCamera.transform.localPosition = new Vector3(0.48f, -2.62f, 5.13f);
        }

        
    }


    public void MoveCamera()
    {
      //  inChameraRotateChange = true;
        int turnSiguiente=0;
        if (PlayerTurn == 4){
            turnSiguiente = 1;
            }
        else
        {
            turnSiguiente = PlayerTurn + 1;
        }

        Player NextPlayer = GameObject.FindGameObjectWithTag("Player" + (turnSiguiente)).GetComponent<Player>();
        int Posicion = NextPlayer.tableroPos;
        Debug.Log(Posicion);    

        if (Posicion < 10)
        {
            OwnCamera.transform.rotation = Abajo;
            //camara.transform.localPosition = PosAbj;



        }
        else if (Posicion >= 30)
        {
            OwnCamera.transform.rotation = Derecha;
            // camara.transform.localPosition = PosDer;


        }
        else if (Posicion >= 20)
        {
            OwnCamera.transform.rotation = Arriba;
            //   camara.transform.localPosition = PosArr;

        }
        else if (Posicion >= 10)
        {
            //camara.transform.localPosition = PosIzq;
            OwnCamera.transform.rotation = Izquierda;


        }
       // rotationActual = OwnCamera.transform.rotation;

      //  inChameraRotateChange = false;


    }
    public IEnumerator PlayerFontText()
    {
      
        yield return new WaitForSeconds(0.0001f);
        PlayerText.enabled = true;
        PlayerText.fontSize = 40;
        PlayerText.text = ("Player " + ControlPlayer.control.Turno + " Turn!");
        
        while (PlayerText.fontSize > 1) {
            
           PlayerText.fontSize--;
            yield return new WaitForSeconds(0.02f);
            
        }
        
        RestoreText();
    }

    public IEnumerator BienestarText(string Text)
    {

        while (Cards || Properties)
        {
            yield return new WaitForSeconds(0.02f);
        }

        PlayerText.fontSize = 40;
        PlayerText.enabled = true;
       
        PlayerText.text = (Text);
        while (PlayerText.fontSize > 1)
        { 
            PlayerText.fontSize--;
            yield return new WaitForSeconds(0.02f);
        }
        RestoreText();
        
    }

    public void RestoreText()
    {
        PlayerText.enabled = false;
        PlayerText.fontSize = 40;
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
                //yield return new WaitForSeconds(0.06f);
                punto--;
                rpposiicion++;

                /*  Debug.Log("Player" + self.name + "x" + self.transform.rotation.x);
                  Debug.Log("Player" + self.name + "y" + self.transform.rotation.y);
                  Debug.Log("Player" + self.name + "z" + self.transform.rotation.z);
                  Debug.Log("Player" + self.name + "w" + self.transform.rotation.w);


                  Debug.Log("Cam" + self.name + "x" + OwnCamera.transform.rotation.x);
                  Debug.Log("Cam" + self.name + "y" + OwnCamera.transform.rotation.y);
                  Debug.Log("Cam" + self.name + "z" + OwnCamera.transform.rotation.z);
                  Debug.Log("Cam" + self.name + "w" + OwnCamera.transform.rotation.w);*/
                RotatePosicion();
                MoveCamera();


                if (rpposiicion % 40 == 0)
                {
                    NumVueltas++;
                    StartCoroutine(Cobrar(200));
                }
                tableroPos = rpposiicion - (40 * NumVueltas);
            }
        

        ComprobateCards();
        ComprobateProperties();


        if (tableroPos ==30 )
        {        
            StartCoroutine(GoBienestar());
            
        }else if (RepiteTurno)
        {
            while (Cards || Properties )
            {
                yield return new WaitForSeconds(0.1f);
            }
            /// Esto hace que no pueda volver a lanzar si sacó un par para entrar a bienestar
            if (InBienestar)
            {
                RepiteTurno = false;
            }
            else
            {
                while (ExecutingCardMethod && punto>0 )
                {
                    yield return new WaitForSeconds(0.1f);
                }
                ExecutingCardMethod = false;
                PuedeTirar = true;

                while (Cards || Properties)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                StartCoroutine(BienestarText("Lanza de nuevo!!"));
                TextoTirar.enabled = true;
                //Esto para que pueda alcanzarse a ver el texto
                yield return new WaitForSeconds(0.7f);
                movimie = false;
            }         
        }
        else if (tableroPos == 10 )
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
        if (pos > tableroPos)
        {
            punto = pos- tableroPos;
        }
        else
        {
            punto = 40 - tableroPos + pos;
        }
        Debug.Log("se mueve hasta" + pos);
        StartCoroutine(Move());
    }

    public void ComprobateCards()
    {
        
        if ((tableroPos == 2 || tableroPos == 17 || tableroPos == 33)  && punto == 0)
        {
            ComARrcs = true;
            Cards = true;           
            CardsController.turno = PlayerTurn;
        }

        if ((tableroPos == 7 || tableroPos == 22 || tableroPos == 36) && punto == 0)
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
            if (element == tableroPos)
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
        Vector3 bienestarPos;

        if (bienestar.ocupada)
        {
            bienestar.transform.localPosition = new Vector3(-10.9f, bienestar.PosicionOriginal.y, bienestar.PosicionOriginal.z);
        }

        bienestarPos = bienestar.transform.position;

        HuecoVisible = true;
        rigi.isKinematic = false;

        Player parent = OwnCamera.transform.GetComponentInParent<Player>();


        while (transform.position != bienestarPos)
        {
            OwnCamera.transform.SetParent(null);
            yield return new WaitForSeconds(1f);


            transform.position = (bienestarPos);
          
        }
        OwnCamera.transform.SetParent(parent.transform);
        //Esto es para que caiga
        rigi.isKinematic = true;
        rpposiicion = 10;
        NumVueltas = 0;

        InBienestar = true;

        self.transform.rotation = GirarIzq;
        //OwnCamera.transform.rotation = Arriba;

        //poner la cámara según cada uno

        //CamaraBienestar();
        BienestarCamera.enabled =true;

        StartCoroutine(BienestarText("!!En bienestar!!"));
        yield return new WaitForSeconds(1f);
        HuecoVisible = false;
        ResetBienestarGuide();

        while (PlayerText.enabled || HuecoVisible)
        {
            yield return new WaitForSeconds(0.1f);
        }

        FinishTurn();
        
    }

    public void CamaraBienestar()
    {
        if (self.tag == "Player1")
        {
            OwnCamera.transform.localPosition = new Vector3(-0.0733f, -0.0689f, 0.1839f);
        }
        else if (self.tag == "Player2")
        {
            OwnCamera.transform.localPosition = new Vector3(-8.800361f, -273.7f, 231.4f);
        }
        else if (self.tag == "Player3")
        {
            OwnCamera.transform.localPosition = new Vector3(-8.15f, -2.54f, -1.997924f);
        }
        else if (self.tag == "Player4")
        {
            OwnCamera.transform.localPosition = new Vector3(-0.872f, 0.152f, 0.343f);
        }

    }

    public IEnumerator SalirBienestar()
    {
        if (!BienestarCamera.enabled && !dado1.IsMoving() && !dado2.IsMoving())
        {
            BienestarCamera.enabled = true;
        }
        TextoTirar.enabled = true;
        TextoPagar.enabled = true;

        if (ExitCards > 0)
        {
            UseCard.enabled = true;
        }
        if (PuedeTirar) { 
        if (Input.GetKeyDown(KeyCode.X))
        {
            //PuedeTirar = false;
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
                BienestarCamera.enabled = false;
                yield return new WaitForSeconds(0.1f);
            }
             BienestarCamera.enabled = true;
            //OwnCamera.enabled = true;
            DadosCamera.enabled = false;

            if(dado1.NumeroActual == dado2.NumeroActual)
            {
                
                punto = dado1.NumeroActual + dado2.NumeroActual;
                total = punto;
                    
                StartCoroutine(Move());
                InBienestar = false;
                BienestarCamera.enabled = false;
                OwnCamera.enabled = true;
                    ChangeCameraParent();
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
                BienestarCamera.enabled = false;
                OwnCamera.enabled = true;
                ChangeCameraParent();
            }

        if (Input.GetKeyDown(KeyCode.C)  && ExitCards > 0)
        {
            
            ExitCards--;
            ResetBienestarGuide();
            StartCoroutine(LanzarDado());
                
                InBienestar = false;
                BienestarCamera.enabled = false;
                OwnCamera.enabled = true;
                ChangeCameraParent();
            }      
        yield return new WaitForSeconds(1f);
        }
    }

    public void ResetBienestarGuide()
    {
        TextoTirar.enabled = false;
        TextoPagar.enabled = false;
        UseCard.enabled = false;
    }
}

