using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{    
    //public Material mat;
    public GameObject startVertex;
    public GameObject endVector;
    void OnPostRender()
    {
        /*
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        */
        GL.PushMatrix();
        //mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex.transform.position);
        GL.Vertex(new Vector3(endVector.transform.position.x / Screen.width, endVector.transform.position.y / Screen.height, 0));
        GL.End();
        GL.PopMatrix();
    }
}
