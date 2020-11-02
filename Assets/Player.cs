using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Text Resultado;
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
    public Text PlayerText;
    
    public Image IconTirar;
    public Image IconUseCard;
    public Image IconPagar;

    public Casilla casiillaSiguiente;
    public Casilla bienestar;

    public int ExitCards;
   
   
    public Color PlayerColor;

    public bool RepiteTurno = false;
    [HideInInspector]
    public int PlayerTurn;

    public bool InBienestar=false;
    

    public static bool Cards;
    public static bool Chances;
    public static bool ComARrcs;
    public static bool ExecutingCardMethod=false;

    public static bool Properties;
    public static bool HuecoVisible = false;

    public static bool PuedeTirar=true;

    public int NumVueltas=0;

    public Dado dado1;
    public Dado dado2;

    public Rigidbody rigi;

    public GameObject self;
    public GameObject TridiModel;
    
    //Quaterniones de la orientación de la cámara al girar
    public  static Quaternion Abajo = new Quaternion(0.4f, 0.0f, 0.0f, 1.0f);
    
    public static  Quaternion Izquierda = new Quaternion(0.2f, 0.6f, -0.2f, 0.6f);
    
    public static  Quaternion Arriba = new Quaternion(0.0f, 1.0f, -0.5f, 0.0f);
    
    public static  Quaternion Derecha = new Quaternion(0.2f, -0.6f, 0.2f, 0.6f);

    //Vectores de Giro de la posición de la camara
    public static Vector3 PosAbj = new Vector3(-0.0127f, 0.0228f, 0.062f);
    
    public static Vector3 PosIzq = new Vector3(- 0.0291f, 0f, 0.0785f);
    
    public static Vector3 PosArr = new Vector3(0f, -0.0159f, 0.093f);
    
    public static Vector3 PosDer = new Vector3(0.0291f, 0f, 0.0785f);

    //Quaterniones de Giro de los modelos 3D
    [HideInInspector]
    public Quaternion GirarAbajo;
    [HideInInspector]
    public Quaternion GirarIzq;
    [HideInInspector]
    public Quaternion GirarArriba;
    [HideInInspector]
    public Quaternion GirarDerecha;


    public void Awake()
    {
        this.self = gameObject;
        
    }

    // Start is called before the first frame update
    public void Start()
    {
        
        total = 0;

        OwnCamera = GetComponentInChildren<Camera>();

        dinero = dineroInicial;

        
        PlayerDinero.text = "$" + dinero.ToString();
        PlayerDinero.enabled = true;

        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();
        PlayerText.enabled = false;       

        Rott = GameObject.Find("GO").GetComponent<GO>();


        OwnCamera.enabled = false;

        DadosCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        DadosCamera.enabled = false;

        IconTirar = GameObject.Find("LanzarIcon").GetComponent<Image>();


        IconUseCard = GameObject.Find("TsalidaIcon").GetComponent<Image>();
        IconUseCard.enabled = false;


        IconPagar = GameObject.Find("PagarIcon").GetComponent<Image>();
        IconPagar.enabled = false;

        bienestar = GameObject.Find("Bienestar").GetComponent<Casilla>();

        rigi = GetComponent<Rigidbody>();

        if (ControlPlayer.LImitedeTurno < PlayerTurn)
        {
            self.SetActive(false);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn == ControlPlayer.control.Turno)
        {
            PlayerDinero.text = "$" + dinero.ToString();
            //PlayerDinero.color = PlayerColor;
            

            if (!DadosCamera.enabled)
            {
                OwnCamera.enabled = true;
            }
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

        MoveCamera();
        IconTirar.enabled = false;
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
     //punto = Random.Range(3,3);
        //Debug.Log("Resul" + punto);
        total = punto;
        //Resultado.text = punto.ToString();

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

    public void MoveCamera()
    {
       
        Camera camara = GetComponentInChildren<Camera>();

        int Posicion = tableroPos;

        if (InBienestar)
        {
            camara.transform.rotation = new Quaternion(0.0f, 1.0f, -0.3f, 0.0f);
            camara.transform.localPosition = new Vector3(0f, -0.0773f, 0.0501f);
        }
     
        else if (Posicion < 10)
        {
            camara.transform.rotation = Abajo;
            camara.transform.localPosition = PosAbj;           
        }
        else if (Posicion >= 30
       )
        {
            camara.transform.rotation = Derecha;
            camara.transform.localPosition = PosDer;
        }
        else if (Posicion >= 20 )
        {
            camara.transform.rotation = Arriba;
            camara.transform.localPosition = PosArr;           
        }
        else if (Posicion >= 10)
        {
            camara.transform.localPosition = PosIzq;
            camara.transform.rotation = Izquierda;            
        }


        if (Posicion < 9 || Posicion == 39)
        {
            TridiModel.transform.rotation = GirarAbajo;
        }
        else if (Posicion >=29)
        {
            TridiModel.transform.rotation = GirarDerecha;
        }
        else if (Posicion >= 19)
        {
            TridiModel.transform.rotation = GirarArriba;

        }
        else if (Posicion >= 9)
        {
            TridiModel.transform.rotation = GirarIzq;
        }
    }


    public void FinishTurn()
    {
        if (ExecutingCardMethod)
        {
            ExecutingCardMethod = false;
        }
        movimie = false;
        OwnCamera.enabled = false;

        ControlPlayer.control.NextTurno();
        StartCoroutine(PlayerFontText());
        IconTirar.enabled = true;
        PuedeTirar = true;
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
                yield return new WaitForSeconds(0.06f);
                punto--;
                rpposiicion++;

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
                IconTirar.enabled = true;
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
                PlayerDinero.text = "$" + dinero.ToString();
                yield return new WaitForSeconds(0.015f);

        }
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
            OwnCamera.transform.SetParent(parent.transform);
        }
        TridiModel.transform.rotation = GirarIzq;
        //Esto es para que caiga
        rigi.isKinematic = true;
        rpposiicion = 10;
        NumVueltas = 0;
       
        InBienestar = true;

        OwnCamera.transform.rotation = new Quaternion(0.0f, 1.0f, -0.3f, 0.0f);
        OwnCamera.transform.localPosition = new Vector3(0f, -0.0773f, 0.0501f);
        
        StartCoroutine(BienestarText("!!En bienestar!!"));
        yield return new WaitForSeconds(1f);
        HuecoVisible = false;
       ResetBienestarGuide();

        while (PlayerText.enabled || HuecoVisible)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Se termina en gonbienes");
        FinishTurn();
        
    }

    public IEnumerator SalirBienestar()
    {

        if (ExitCards > 0)
        {
            IconUseCard.enabled = true;
        }
        if (PuedeTirar) { 
            
        IconTirar.enabled = true;
        IconPagar.enabled = true;
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
                yield return new WaitForSeconds(0.1f);
            }

            OwnCamera.enabled = true;
            DadosCamera.enabled = false;

            if(dado1.NumeroActual == dado2.NumeroActual)
            {
                
                punto = dado1.NumeroActual + dado2.NumeroActual;
                total = punto;

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
    }

    public void ResetBienestarGuide()
    {
        IconTirar.enabled = false;
        IconPagar.enabled = false;
        IconUseCard.enabled = false;
    }
}

