using UnityEngine;
using System.Collections;

public class colisionador : MonoBehaviour {

	public string nombreEscena;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name =="fondo")
		{
			Application.LoadLevel(nombreEscena);
			
		}
		
		//Debug.Log("toca el piso");
		//characterInQuicksand = true;
	}
}
