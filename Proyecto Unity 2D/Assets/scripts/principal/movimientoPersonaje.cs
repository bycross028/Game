using UnityEngine;
using System.Collections;

public class movimientoPersonaje : MonoBehaviour {

	public string nombreJuego1;
	public string nombreJuego2;
	public string nombreJuego3;
	public string nombreJuego4;
	public string nombreJuego5;

	private bool ir1= false;
	private bool ir2= false;
	private bool ir3= false;
	private bool ir4= false;
	private bool ir5= false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Debug.Log("ir1 es:" + ir1);
		Debug.Log("ir2 es:" + ir2);
		Debug.Log("ir3 es:" + ir3);
		Debug.Log("ir4 es:" + ir4);
		Debug.Log("ir5 es:" + ir5);

		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Time.deltaTime, 0, 0);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(0, -Time.deltaTime, 0);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-Time.deltaTime, 0, 0);
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(0, Time.deltaTime, 0);
		}

		if (Input.GetAxis("Submit") >= 1.0f)
		{
			if(ir1)
			{
				Application.LoadLevel(nombreJuego1);
			}
			if(ir2)
			{
				Application.LoadLevel(nombreJuego2);
			}
			if(ir3)
			{
				Application.LoadLevel(nombreJuego3);
			}
			if(ir4)
			{
				Application.LoadLevel(nombreJuego4);
			}
			if(ir5)
			{
				Application.LoadLevel(nombreJuego5);
			}
		}
		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log("enter");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("entra en un trigger");
		if(other.gameObject.name =="juego1")
		{
			ir1 = true;

			//Application.LoadLevel(nombreJuego1);
		}
		if(other.gameObject.name =="juego2")
		{
			ir2 = true;
			//Application.LoadLevel(nombreJuego2);
		}
		if(other.gameObject.name =="juego3")
		{
			ir3 = true;
			//Application.LoadLevel(nombreJuego3);
		}
		if(other.gameObject.name =="juego4")
		{
			ir4 = true;
			//Application.LoadLevel(nombreJuego4);
		}
		if(other.gameObject.name =="juego5")
		{
			ir5 = true;
			//Application.LoadLevel(nombreJuego5);
		}
		if(other.gameObject.name =="triggerSinJuegos")
		{
			ir1 = false;
			ir2 = false;
			ir3 = false;
			ir4 = false;
			ir5 = false;
			//Application.LoadLevel(nombreJuego5);
		}
	}
}
