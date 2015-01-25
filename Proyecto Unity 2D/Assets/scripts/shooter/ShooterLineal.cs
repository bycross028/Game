using UnityEngine;
using System.Collections;

public class ShooterLineal : ShotterObject
{
    public bool isLeft = false;
    public Vector2 direccion = Vector2.zero;
    public float ZigzacAplitud = 0.03f;
    public float ZigzacVelocitdad = 0.03f;
    
    //! private
    public bool Dead = false;

    public Vector3 start    = Vector3.zero;
    public Vector3 center   = Vector3.zero;
    public Vector3 end      = Vector3.zero;
    public Vector3 cuveDead = Vector3.zero;
    public Vector2 rango ;

    void Start () 
    {
        base.Start();
        direccion = new Vector2((isLeft) ? 1.0f : -1.0f, 0.0f);
	}
	
	void Update ()
    {
        if (!Dead)
        {
            // Update Movimiento
            gameObject.transform.Translate(direccion * velocity * Time.deltaTime);
            gameObject.transform.position += new Vector3(0, Mathf.Sin(Time.time * ZigzacVelocitdad) * ZigzacAplitud, 0);
        }
        else
        {
            rango.x += Time.deltaTime * 3.5f * ((isLeft)?1.0f:-1.0f);

            float y = cuveDead.x * Mathf.Pow(rango.x, 2.0f) + cuveDead.y * rango.x + cuveDead.z;
            gameObject.transform.position = new Vector3(rango.x, y, 0.0f);
            
            /*
            Debug.DrawLine(start, center, Color.red);
            Debug.DrawLine(center, end, Color.red);

            Debug.DrawLine(new Vector3(-0.5f, ymax, 0f),
                           new Vector3(0.5f, ymax, 0f), Color.blue);
            Debug.DrawLine(new Vector3(-0.5f, ymax, 0f),
                           new Vector3(0.5f, ymax, 0f), Color.blue);
            

            for (float i = start.x; i > end.x; i -= 0.1f)
            {
               // Debug.Log(i);

                y = cuveDead.x * Mathf.Pow(i, 2.0f) + cuveDead.y * i + cuveDead.z;
                Debug.DrawLine(new Vector3(i - 0.05f, y - 0.05f, 0), new Vector3(i + 0.05f, y + 0.05f, 0), Color.red);
                Debug.DrawLine(new Vector3(i - 0.05f, y + 0.05f, 0), new Vector3(i + 0.05f, y - 0.05f, 0), Color.red);
            }
            */
        }

        // Auto-Destroy
        if ( ( isLeft && gameObject.transform.position.x - (sprite.bounds.size.x * 0.5f) > ScreeSizeWolrlPoint.x) ||
             (!isLeft && gameObject.transform.position.x + (sprite.bounds.size.x * 0.5f) < -ScreeSizeWolrlPoint.x) )
            Destroy(gameObject);
	}

    override protected void OnDead()
    {
        start   = gameObject.transform.position;
        center  = (!isLeft) ? gameObject.transform.position + new Vector3(-1.0f,  1.0f, 0.0f) : gameObject.transform.position + new Vector3(1.0f,  1.0f, 0.0f);
        end     = (!isLeft) ? gameObject.transform.position + new Vector3(-2.0f, -5.0f, 0.0f) : gameObject.transform.position + new Vector3(2.0f, -5.0f, 0.0f);
        
        cuveDead = Curve.solve(start,center,end);
        rango = new Vector2(start.x, end.x);
        Dead = true;
    } 
}
