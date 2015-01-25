using UnityEngine;
using System.Collections;

public class ShooterManagger : MonoBehaviour 
{
    //! Configuracion
    public Sprite[] spritesLineal;
    public Sprite[] spritesCurve;

    public Vector2 catidadMaxMin;
    public float dificultadInicial;
    public float dificultadSpeed;

    public Vector2 maxMinSpeedLienal;
    public Vector2 maxMinSpeedCurve;

    public Vector2 maxMinScalingLienal;
    public Vector2 maxMinScalingCurve;

    public Vector2 maxMinLienalZigzacAplitud;
    public Vector2 maxMinLienalZigzacSpeed;

    //! Private
    private Vector3 ScreeSizeWolrlPoint = Vector3.zero;
    private float currentDificultad = 1.0f;

    void Start () 
    {
        ScreeSizeWolrlPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

	}

    void InstanceShootherLineal()
    {
        GameObject shooter = new GameObject("Shooter Lineal");
        
        // Lineal Shoother
        ShooterLineal scriptLineal = shooter.AddComponent<ShooterLineal>();
        scriptLineal.sprite = spritesLineal[Random.Range(0, spritesLineal.Length - 1)];
        scriptLineal.velocity = Random.Range(maxMinSpeedLienal.x, maxMinSpeedLienal.y) + dificultadInicial * (dificultadSpeed * Time.deltaTime);
        scriptLineal.scale = Random.Range(maxMinScalingLienal.x, maxMinScalingLienal.y);
        scriptLineal.ZigzacAplitud = Random.Range(maxMinLienalZigzacAplitud.x, maxMinLienalZigzacAplitud.y);
        scriptLineal.ZigzacVelocitdad = Random.Range(maxMinLienalZigzacSpeed.x, maxMinLienalZigzacSpeed.y);

        bool LeftPosition = (Random.Range(0, 2) == 1);
        shooter.transform.position = new Vector3(
            (LeftPosition ? -ScreeSizeWolrlPoint.x - scriptLineal.sprite.bounds.size.x * 0.5f : 
                ScreeSizeWolrlPoint.x + scriptLineal.sprite.bounds.size.x * 0.5f),
            Random.Range(-ScreeSizeWolrlPoint.y + scriptLineal.sprite.bounds.size.x * 0.5f, 
                          ScreeSizeWolrlPoint.y - scriptLineal.sprite.bounds.size.x * 0.5f),
            0.0f);

        scriptLineal.isLeft = LeftPosition;

    }
	
	void Update()
    {
	    
	}
}
