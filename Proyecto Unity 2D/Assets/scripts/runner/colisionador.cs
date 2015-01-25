using UnityEngine;
using System.Collections;

public class colisionador : MonoBehaviour {

	public GameObject particulaExplosiva;
	public string nombreEscena;
	private bool activaSalto = false;
	private float tiempo = 0.6f;
	private float tiempoAux = 0.6f;

	public GameObject salto;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0))
		{
			this.GetComponent<SpriteRenderer>().enabled = false;
			salto.SetActive(true);
				activaSalto = true;
			Debug.Log("salta");
		}
		if(activaSalto)
		{
			tiempo = tiempo - Time.deltaTime;
			if(tiempo < 0)
			{
				
				activaSalto = false;
				this.GetComponent<SpriteRenderer>().enabled = true;
				salto.SetActive(false);
				tiempo = tiempoAux;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name =="fondo")
		{
			Application.LoadLevel(nombreEscena);
			
		}
		if(other.gameObject.name =="dinamita")
		{

			Instantiate(particulaExplosiva,this.transform.position,Quaternion.identity);
			//Application.LoadLevel(nombreEscena);
			
		}
		if(other.gameObject.name =="obstaculo techo")
		{
			Application.LoadLevel(nombreEscena);
			
		}
		
		//Debug.Log("toca el piso");
		//characterInQuicksand = true;
	}
}
