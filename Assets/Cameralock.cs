using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cameralock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Camera2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Camera>().transform.rotation = Camera2.GetComponent<Camera>().transform.rotation;
    }
}
