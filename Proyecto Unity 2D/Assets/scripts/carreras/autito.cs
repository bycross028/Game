using UnityEngine;
using System.Collections;

public class autito : MonoBehaviour {

	public int velocidad;
	public GameObject particulasExplosion;
	public string nombreEscena;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate( velocidad *Time.deltaTime, 0, 0);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(velocidad *-Time.deltaTime, 0, 0);
		}
	}
	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.name =="transitoPlayhoolder")
		{
			Instantiate(particulasExplosion,this.transform.position,Quaternion.identity);
		}
		if(other.gameObject.name =="agua")
		{
			this.renderer.enabled = false;
		}
		if(other.gameObject.name =="calleGanadora")
		{
			Debug.Log("GANAMO LOCO GANAMO");
		}
	}
}
