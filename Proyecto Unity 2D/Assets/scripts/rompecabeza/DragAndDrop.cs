using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    public RopecabezaManagger manager;

    private bool drag = false;
    private Vector3 positionDeltaPieze = Vector3.zero;
    private Vector3 ScreeSizeWolrlPoint = Vector3.zero;
    private SpriteRenderer sprite;

    void Start () 
    {
        ScreeSizeWolrlPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        sprite = GetComponent<SpriteRenderer>();
	}

	void Update()
    {
        if (drag)
        {
            Vector3 move = Camera.main.ScreenToWorldPoint(Input.mousePosition) + positionDeltaPieze;
            move.z = 0; // Zero Axis-Z
            gameObject.transform.position = move;

            // Limites del drag and Drop
            if (ScreeSizeWolrlPoint.y < gameObject.transform.position.y + sprite.bounds.size.y * 0.5f) 
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                            ScreeSizeWolrlPoint.y - sprite.bounds.size.y * 0.5f,
                                                            gameObject.transform.position.z);
            if (ScreeSizeWolrlPoint.x < gameObject.transform.position.x + sprite.bounds.size.x * 0.5f)
                gameObject.transform.position = new Vector3(ScreeSizeWolrlPoint.x - sprite.bounds.size.x * 0.5f,
                                                            gameObject.transform.position.y,
                                                            gameObject.transform.position.z);
            if (-ScreeSizeWolrlPoint.y > gameObject.transform.position.y - sprite.bounds.size.y * 0.5f)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                            -ScreeSizeWolrlPoint.y +sprite.bounds.size.y * 0.5f,
                                                            gameObject.transform.position.z);
            if (-ScreeSizeWolrlPoint.x > gameObject.transform.position.x - sprite.bounds.size.x * 0.5f)
                gameObject.transform.position = new Vector3(-ScreeSizeWolrlPoint.x + sprite.bounds.size.x * 0.5f,
                                                            gameObject.transform.position.y,
                                                            gameObject.transform.position.z);

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
