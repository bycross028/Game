using UnityEngine;
using System.Collections;

public class managerAnimacion : MonoBehaviour {

	public GameObject idle;
	public GameObject walk;
	public GameObject die;
	private Vector2 girarDer = new Vector2(0,180f);
	private Vector2 girarIzq = new Vector2(0,0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.D))
		{
			walk.transform.localEulerAngles = girarDer;
			idle.transform.localEulerAngles = girarDer;
			idle.SetActive(false);
			walk.SetActive(true);
		}
		if (Input.GetKey(KeyCode.S))
		{
			walk.transform.localEulerAngles = girarIzq;
			idle.transform.localEulerAngles = girarIzq;
			idle.SetActive(false);
			walk.SetActive(true);
		}
		if (Input.GetKey(KeyCode.A))
		{
			walk.transform.localEulerAngles = girarIzq;
			idle.transform.localEulerAngles = girarIzq;
			idle.SetActive(false);
			walk.SetActive(true);
		}
		if (Input.GetKey(KeyCode.W))
		{

			idle.SetActive(false);
			walk.SetActive(true);
		}
	

	
		if(!Input.anyKey)
		{
			idle.SetActive(true);
			walk.SetActive(false);
			Debug.Log("no toca ninguna");
		}

	}
}
