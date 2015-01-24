using UnityEngine;
using System.Collections;

public class Carta : MonoBehaviour 
{
    public int id;
    public float timeWait = 2.0f;
    public float timeRotation = 1.0f;
    public float timeScale = 2.0f;
    public float rotationVelocity;
    
    public GameObject front;
    public GameObject back;
    public Sprite SpriteBack;
    public MemoryGameMannager managger;

    //! Private
    private SpriteRenderer sptriteRenderFront;

    public SpriteRenderer SptriteRenderFront
    {
        get { return sptriteRenderFront; }
    }

    private SpriteRenderer sptriteRenderBack;

    public SpriteRenderer SptriteRenderBack
    {
        get { return sptriteRenderBack; }
    }

    private bool orientation = false;
    private bool rotationHidde = false;
    private bool rotationShow = false;
    private bool scalingHidde = false;

    //! Private Timer
    private float time = 0.0f;

	void Start () 
    {
        sptriteRenderFront = front.GetComponent<SpriteRenderer>();
        sptriteRenderBack = back.GetComponent<SpriteRenderer>();

        sptriteRenderBack.sprite = SpriteBack;

        // Hiden Cart
        Invoke("rotateHidde", timeWait);
	}

	void Update () 
    {
        
        if (gameObject.transform.rotation.eulerAngles.y < 90f && orientation)
            toogleOrder();

        if (gameObject.transform.rotation.eulerAngles.y > 90f && !orientation)
            toogleOrder();

        updateRotate();
	}

    void toogleOrder()
    {
        int temp = sptriteRenderFront.sortingOrder;
        sptriteRenderFront.sortingOrder = sptriteRenderBack.sortingOrder;
        sptriteRenderBack.sortingOrder = temp;

        orientation = !orientation;
    }

    public void rotateShow()
    {
        time = gameObject.transform.rotation.eulerAngles.y * timeRotation / 180.0f;
        rotationShow = true;
    }

    public void rotateHidde()
    {
        time = timeRotation - gameObject.transform.rotation.eulerAngles.y * timeRotation / 180.0f;
        rotationHidde = true;
    }

    public void scaleHidde()
    {
        time = timeScale - gameObject.transform.localScale.x * timeScale / 1.0f;
        scalingHidde = true;
    }
    
    void updateRotate()
    {
        if (!rotationHidde && !rotationShow && !scalingHidde)
            return;
        
        time += Time.deltaTime;
        if (time > ((scalingHidde) ? timeScale : timeRotation))
            time = (scalingHidde)?timeScale:timeRotation;

        float value = time * ((scalingHidde)?1.0f:180.0f) / ((scalingHidde) ? timeScale : timeRotation); 

        if (rotationHidde)
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f - value, 0.0f));

        if (rotationShow)
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, value, 0.0f));

        if (scalingHidde)
            gameObject.transform.localScale = new Vector3(1.0f - value, 1.0f - value, 0.0f);
        
        if ( (value >= 180.0f && !scalingHidde) || (value >= 1.0f && scalingHidde))        
            rotationShow = rotationHidde = scalingHidde = false;  

    }

    void OnMouseUp()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);     
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);

        if (collider && collider.gameObject == gameObject && !orientation)
            managger.OnClickCarta(this);        
    }
}
