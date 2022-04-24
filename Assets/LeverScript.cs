using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public GameObject Door;
    int shouldShutDoor = 0;
    
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        shouldShutDoor = 1;
    }

    void OnMouseExit()
    {
        shouldShutDoor = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && shouldShutDoor==1)
        {
            Destroy(Door);
            Destroy(gameObject);
        }
    }
}