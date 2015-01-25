using UnityEngine;
using System.Collections;

public class ShotterObject : MonoBehaviour
{  
    public float velocity;
    public Sprite sprite;
    public float scale;

    //! Private
    protected Vector3 ScreeSizeWolrlPoint = Vector3.zero;
    protected SpriteRenderer spriteRender;
	protected void Start ()
    {
       ScreeSizeWolrlPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
       spriteRender = gameObject.AddComponent<SpriteRenderer>();
       spriteRender.sprite = sprite;

       gameObject.AddComponent<CircleCollider2D>();
       gameObject.transform.localScale = Vector2.one * scale;
	}

    protected void Update() 
    {	    
	}

    protected void OnMouseUp()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);

        if (collider && collider.gameObject == gameObject)
            OnDead();
    }

    virtual protected void OnDead()
    {
        Debug.Log("Shooter Object Dead");
    }

}
