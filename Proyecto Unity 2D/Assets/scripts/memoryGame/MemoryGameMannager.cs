using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryGameMannager : MonoBehaviour 
{
    public Vector2 Piezas = new Vector2(4, 4);
    public Sprite SpriteBack;
    public Sprite[] Sprites;
    public GameObject CartaPrefab;
    public Cronometro cronometro;
    public float TiempoEsperaInicio = 3.0f;
    public float RetardoEfecto = 0.05f;
    public float TimpoEsperaResolver = 0.5f;
    public float TimpoRotacionCarta = 0.5f;
    public float TimpoScalingCarta= 0.5f;
    public string NameSceneEndGame = "meun";

    //! Private
    private int maxNumber = 0;
    private Vector2 CartaSize = Vector2.zero;
    private List<int> ids = new List<int>();

    private List<Carta> cartSelected = new List<Carta>();
    private int total = 0;

	void Start () 
    {
        maxNumber = (int)( (Piezas.x * Piezas.y) * 0.5f);
            
        if ((Piezas.x * Piezas.y)%2.0f > 0.0f)
        {
            Debug.Log("Matiz impar, ejmplos validos 2x2,2x3,4x4,4x2,8x8");
            return;
        }

        if (Sprites.Length > 0)
            CartaSize = Sprites[0].bounds.size;

        // Crea la lista de ids
        for (int i = 0; i < Sprites.Length; i++) 
            ids.Add(i);

        // Shuffle Lista
        Shuffle(ids);
        ids.RemoveRange(maxNumber, ids.Count - maxNumber);
        ids.AddRange(ids.GetRange(0, ids.Count));
        Shuffle(ids);

        Invoke("start", TiempoEsperaInicio + (Piezas.x * Piezas.y) * RetardoEfecto);


        int index = 0;
        for (int row = 0; row < Piezas.x; row++)
        {
            for (int col = 0; col < Piezas.y; col++)
            {
                GameObject carta =  Instantiate(CartaPrefab) as GameObject;
                carta.name = "Carta " + index.ToString();
                
                carta.transform.position = gameObject.transform.position +
                    new Vector3(col * CartaSize.x, row * CartaSize.y * -1);                

                carta.transform.parent = gameObject.transform;

                Carta scriptCarta = carta.GetComponent<Carta>();
                scriptCarta.managger = this;
                scriptCarta.id = ids[index];
                scriptCarta.timeWait = TiempoEsperaInicio + index * RetardoEfecto;
                scriptCarta.timeRotation = TimpoRotacionCarta;
                scriptCarta.timeScale = TimpoScalingCarta;
                scriptCarta.SpriteBack = SpriteBack;

                SpriteRenderer spriteRenderCartaFront = scriptCarta.front.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderCartaFront.sprite = Sprites[ids[index]];  
                
                SpriteRenderer spriteRenderCartaBack = scriptCarta.back.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderCartaBack.sprite = SpriteBack;

                index++;
            }
        }
	}

    void Update()
    {
        if (total == maxNumber)
        {
            Invoke("loadLevel", TimpoScalingCarta);
            total = 0;
        }
    }		
        
    public void loadLevel()  
    {
        Application.LoadLevel(NameSceneEndGame);
	}

    public void OnClickCarta(Carta carta)
    {
        if (cartSelected.Count == 2)
            return;

        carta.rotateShow();
        cartSelected.Add(carta);

        if (cartSelected.Count < 2)
            return;

        Invoke("resolver", TimpoEsperaResolver);              
    }

    public void resolver()
    {
        if (cartSelected[0].id == cartSelected[1].id)
        {
            cartSelected[0].scaleHidde();
            cartSelected[1].scaleHidde();
            total++;
        }
        else
        {
            cartSelected[0].rotateHidde();
            cartSelected[1].rotateHidde();
        }

        cartSelected.Clear();  
    }

    public void start()
    {
        cronometro.StartTimer();
    }

    void Shuffle<T>(List<T> array)
    {
        int n = array.Count;
        for (int i = 0; i < n; i++)
        {
            int r = Random.Range(0, n - 1);  //i + (int)(Random.value * (n - i));
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

}
