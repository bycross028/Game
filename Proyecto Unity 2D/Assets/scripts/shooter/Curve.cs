using UnityEngine;
using System.Collections;

public class Curve  
{   
    static public Vector3 solve(Vector3 start, Vector3 center, Vector3 end)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        
        // Definicion de la matris
        matrix[0, 0] = Mathf.Pow(start.x    , 2.0f);    matrix[0, 1] = start.x;     matrix[0, 2] = 1;
        matrix[1, 0] = Mathf.Pow(center.x   , 2.0f);    matrix[1, 1] = center.x;    matrix[1, 2] = 1;
        matrix[2, 0] = Mathf.Pow(end.x      , 2.0f);    matrix[2, 1] = end.x;       matrix[2, 2] = 1;

        Matrix4x4 inversa = matrix.inverse;
        
        Vector3 curve = new Vector3
        (
            (inversa[0, 0] * start.y + inversa[0, 1] * center.y + inversa[0, 2] * end.y),
            (inversa[1, 0] * start.y + inversa[1, 1] * center.y + inversa[1, 2] * end.y),
            (inversa[2, 0] * start.y + inversa[2, 1] * center.y + inversa[2, 2] * end.y)
        );

        return curve;
    }

    static public float pointYMaxMinCurve(Vector3 curve)
    {
        return curve.z - (Mathf.Pow(curve.y, 2.0f) / (4.0f * curve.x));
    }

	
	
	/*void Update ()
    {

        Debug.DrawLine(start, center, Color.red);
        Debug.DrawLine(center, end, Color.red);

        for (float i = 0.0f; i < end.x; i += 0.05f)
        {
            float y = curve.x * Mathf.Pow(i, 2.0f) + curve.y * i + curve.z;
            Debug.DrawLine(new Vector3(i - 0.05f, y - 0.05f, 0), new Vector3(i + 0.05f, y + 0.05f, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 0.05f, y + 0.05f, 0), new Vector3(i + 0.05f, y - 0.05f, 0), Color.red);
        }

	}*/
}
