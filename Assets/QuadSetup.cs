using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(-240, -135, 0),
            new Vector3(240, -135, 0),
            new Vector3(-240, 135, 0),
            new Vector3(240, 135, 0)
        };
        mesh.vertices = vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
