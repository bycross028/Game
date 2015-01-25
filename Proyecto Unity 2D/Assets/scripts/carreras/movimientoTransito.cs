using UnityEngine;
using System.Collections;

public class movimientoTransito : MonoBehaviour {

	private int velocidad = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate(0, velocidad*-Time.deltaTime, 0);
	}
}
