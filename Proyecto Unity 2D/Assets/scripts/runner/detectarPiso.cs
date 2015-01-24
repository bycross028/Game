using UnityEngine;
using System.Collections;

public class detectarPiso : MonoBehaviour {

	private bool tocaPiso = true;
	private Vector2 speed = new Vector2(3, 0);
	public GameObject personaje;
	public int velocidad;
	//private bool aux = false;
	// Use this for initialization
	void Start () {
		personaje.rigidbody2D.velocity = new Vector2(4*velocidad,0);
		//personaje.rigidbody2D.AddForce(new Vector2(1 * velocidad,0));
	}
	
	// Update is called once per frame
	void Update () {
	
		//personaje.rigidbody2D.MovePosition(personaje.rigidbody2D.position + speed * Time.deltaTime);
		personaje.rigidbody2D.AddForce(new Vector2(1 * velocidad,0));
		Debug.Log(tocaPiso +" ;es  asi");
		if(tocaPiso)
		if (Input.GetMouseButtonDown(0))
		{

			personaje.rigidbody2D.AddForce(new Vector2(0,500));
			tocaPiso = false;

		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name =="piso")
		{
			tocaPiso = true;
			Debug.Log(tocaPiso);

		}
		if(other.gameObject.name =="piso2")
		{
			tocaPiso = false;
			Debug.Log(tocaPiso);
			
		}
	
		//Debug.Log("toca el piso");
		//characterInQuicksand = true;
	}
}
