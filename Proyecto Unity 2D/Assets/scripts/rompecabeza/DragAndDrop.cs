using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    public RopecabezaManagger manager;

    private bool drag = false;
    private Vector3 positionDeltaPieze = Vector3.zero;

    void Start () 
    {	
	}

	void Update()
    {
        if (drag)
        {
            Vector3 move = Camera.main.ScreenToWorldPoint(Input.mousePosition) + positionDeltaPieze;
            move.z = 0; // Zero Axis-Z
            gameObject.transform.position = move;


            manager.OnMove();

        }
    }

    void OnMouseUp()
    {
        drag = false;
        manager.OnDrop();
    }

    void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionDeltaPieze = gameObject.transform.position - mousePosition;
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);

        drag = (collider && collider.gameObject == gameObject);
    }        
}
