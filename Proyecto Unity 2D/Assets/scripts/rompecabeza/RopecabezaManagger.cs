using UnityEngine;
using System.Collections;

public class RopecabezaManagger : MonoBehaviour 
{
    public Vector2 Piezas = new Vector2(3, 3);
    public Sprite[] Sprites;
    public GameObject PiezaFaltante;
    public float Radio = 0.5f;
    public float SpeedRotation = 10.0f;
    public float angleMaxMin = 5.0f;

    //! Private
    private Vector2 PiezaSize = Vector2.zero;
    private int indexPiezaFaltante = 0;
    private GameObject GameObjectPiezaFaltante;
    private bool Rotation = false;

	void Start () 
    {
        if (Sprites.Length > 0)        
            PiezaSize = Sprites[0].bounds.size;

        // Selecciono la pieza faltante
        indexPiezaFaltante = Random.Range(0, (int)(Piezas.x * Piezas.y - 1));
 
        // Instance Piezas
        int index = 0;
        for (int row = 0; row < Piezas.x; row++)
        {
            for (int col = 0; col < Piezas.y; col++)
            {
               
                GameObject pieza = new GameObject("Pieza " + index.ToString());
                pieza.transform.position = gameObject.transform.position +
                    new Vector3(col * PiezaSize.x, row * PiezaSize.y * -1);

                pieza.transform.parent = gameObject.transform;
               
                if(index != indexPiezaFaltante)
                {                
                    SpriteRenderer spriteRender = pieza.AddComponent<SpriteRenderer>();
                    spriteRender.sprite = Sprites[index];
                    spriteRender.sortingOrder = 0;
                }
                else  GameObjectPiezaFaltante = pieza;               
                
                index++;
            }
        }

        SpriteRenderer spritePiezaFaltante = PiezaFaltante.GetComponent<SpriteRenderer>();
        spritePiezaFaltante.sprite = Sprites[indexPiezaFaltante];
        spritePiezaFaltante.sortingOrder = 1;
        PiezaFaltante.AddComponent<BoxCollider2D>();        
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
        }
    }
}
