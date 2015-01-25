using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;


public class RopecabezaManagger : MonoBehaviour 
{
    public Vector2 Piezas = new Vector2(3, 3);
    public Sprite[] Sprites;
    public GameObject PiezaFaltante;
    public float Radio = 0.5f;
    public float SpeedRotation = 10.0f;
    public float angleMaxMin = 5.0f;
    public float RadioRandoPiezaFaltante = 25.0f;
    public float TimeRestarMap = 0.6f;
    public int TotalRopecabesas = 10;
    public Cronometro cronometro;
    public string texto = "<b>{0}</b> rompecabesas resueltos";
    public GUIText UiText;
    public string NameSceneEndGame = "Menu";

    //! Private
    private Vector3 ScreeSizeWolrlPoint = Vector3.zero;
    private Vector2 PiezaSize = Vector2.zero;
    private int indexPiezaFaltante = 0;
    private GameObject GameObjectPiezaFaltante;
    private bool Rotation = false;
    private List<GameObject> objects = new List<GameObject>();
    private int intentos = 0;

	void Start ()
    {
        ScreeSizeWolrlPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        // Configurate Cronometro
        cronometro.eventComplete += new Cronometro.EventHandler(timeOut); 
        initialize();
	}

    private void timeOut() 
    {
        cronometro.StartTimer();
        Application.LoadLevel(NameSceneEndGame);
    }

    private void initialize()
    {
        UiText.text = string.Format(texto, intentos.ToString());

        if (Sprites.Length > 0)
            PiezaSize = Sprites[0].bounds.size;

        // Selecciono la pieza faltante
        indexPiezaFaltante = Random.Range(0, (int)(Piezas.x * Piezas.y - 1));

        // Agrega las piezas en el tablero
        int index = 0;
        for (int row = 0; row < Piezas.x; row++)
        {
            for (int col = 0; col < Piezas.y; col++)
            {
                GameObject pieza = new GameObject("Pieza " + index.ToString());
                pieza.transform.position = gameObject.transform.position +
                    new Vector3(col * PiezaSize.x, row * PiezaSize.y * -1);

                pieza.transform.parent = gameObject.transform;

                if (index != indexPiezaFaltante)
                {
                    SpriteRenderer spriteRender = pieza.AddComponent<SpriteRenderer>();
                    spriteRender.sprite = Sprites[index];
                    spriteRender.sortingOrder = 0;
                }
                else GameObjectPiezaFaltante = pieza;

                objects.Add(pieza);
                index++;
            }
        }

        // Random Position Pieza faltante
        float angle = Random.Range(0, 2 * Mathf.PI);
        PiezaFaltante.transform.position = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * RadioRandoPiezaFaltante;

        // Limites del drag and Drop
        if (ScreeSizeWolrlPoint.y < PiezaFaltante.transform.position.y + PiezaSize.y * 0.5f)
            PiezaFaltante.transform.position = new Vector3(PiezaFaltante.transform.position.x,
                                                        ScreeSizeWolrlPoint.y - PiezaSize.y * 0.5f,
                                                        PiezaFaltante.transform.position.z);
        if (ScreeSizeWolrlPoint.x < PiezaFaltante.transform.position.x + PiezaSize.x * 0.5f)
            PiezaFaltante.transform.position = new Vector3(ScreeSizeWolrlPoint.x - PiezaSize.x * 0.5f,
                                                        PiezaFaltante.transform.position.y,
                                                        PiezaFaltante.transform.position.z);
        if (-ScreeSizeWolrlPoint.y > PiezaFaltante.transform.position.y - PiezaSize.y * 0.5f)
            PiezaFaltante.transform.position = new Vector3(PiezaFaltante.transform.position.x,
                                                        -ScreeSizeWolrlPoint.y + PiezaSize.y * 0.5f,
                                                        PiezaFaltante.transform.position.z);
        if (-ScreeSizeWolrlPoint.x > PiezaFaltante.transform.position.x - PiezaSize.x * 0.5f)
            PiezaFaltante.transform.position = new Vector3(-ScreeSizeWolrlPoint.x + PiezaSize.x * 0.5f,
                                                        PiezaFaltante.transform.position.y,
                                                        PiezaFaltante.transform.position.z);

        SpriteRenderer spritePiezaFaltante = PiezaFaltante.GetComponent<SpriteRenderer>();
        spritePiezaFaltante.sprite = Sprites[indexPiezaFaltante];
        spritePiezaFaltante.sortingOrder = 1;

        if(!PiezaFaltante.GetComponent<BoxCollider2D>())
            PiezaFaltante.AddComponent<BoxCollider2D>();  
    }

    private void clean()
    {
        for (int i = 0; i < objects.Count; i++)
            DestroyImmediate(objects[i]);

        PiezaFaltante.GetComponent<DragAndDrop>().enabled = true;
    }

    public void restart()
    {
        clean();
        initialize();
    }

    public void OnMove()
    {
        if ((GameObjectPiezaFaltante.transform.position - PiezaFaltante.transform.position).sqrMagnitude < Radio)
        {
            if (PiezaFaltante.transform.eulerAngles.z > angleMaxMin &&
                 PiezaFaltante.transform.eulerAngles.z < 50 && !Rotation)
                Rotation = true;

            if (PiezaFaltante.transform.eulerAngles.z < (359 - angleMaxMin) &&
                 PiezaFaltante.transform.eulerAngles.z > 50 && Rotation)
                Rotation = false;


            PiezaFaltante.transform.Rotate(new Vector3(0f, 0f, (Rotation) ? -1f : 1f) * Time.deltaTime * SpeedRotation);
        }
        else
        {
            PiezaFaltante.transform.rotation = GameObjectPiezaFaltante.transform.rotation;    
        }
    }

    public void OnDrop()
    {
        if ((GameObjectPiezaFaltante.transform.position - PiezaFaltante.transform.position).sqrMagnitude < Radio)
        {
            PiezaFaltante.GetComponent<DragAndDrop>().enabled = false;
            PiezaFaltante.transform.position = GameObjectPiezaFaltante.transform.position;
            PiezaFaltante.transform.rotation = GameObjectPiezaFaltante.transform.rotation;
            
            intentos++;
            Invoke("restart", TimeRestarMap);         
        }
    }
}
