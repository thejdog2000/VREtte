using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foveation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        float cx = mouse.x - 480;
        float cy = mouse.y - 270;
        if (cx < 0)
        {
            cx = 0;
        }
        if (cx > 960)
        {
            cx = 960;
        }
        if (cy < 0)
        {
            cy = 0;
        }
        if (cy > 540)
        {
            cy = 540;
        }
        float uvx = cx / 1920f;
        float uvy = cy / 1080f;

        this.gameObject.GetComponent<RawImage>().uvRect = new Rect(uvx, uvy, .5f, .5f);
        transform.position = new Vector3(cx, cy, 0);
    }
}
