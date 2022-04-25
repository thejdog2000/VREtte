using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Foveation : MonoBehaviour
{
    bool update = false;
    float prev_uvx = 0;
    float prev_uvy = 0;
    public float screen_width = 1920;
    public float screen_height = 1080;
    public float portion = .5F;
       
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (update)//Limits updates to every other frame to keep processing power low
        {
            Vector3 mouse = Input.mousePosition;
            float cx = mouse.x - screen_width*portion/ 2.0F; //Because position starts in the bottom left corner, subtract half the distance of the image to determine its real position from centered mouse
            float cy = mouse.y - screen_height * portion / 2.0F;

            //Limits values to keep foveation entirely on screen
            if (cx < 0)
            {
                cx = 0;
            }
            if (cx > (1 - portion) * screen_width) //1-portion is used to find what portion of the screen it could cover, as to limit values to the edge as cleanly as possible
            {
                cx = (1 - portion) * screen_width;
            }
            if (cy < 0)
            {
                cy = 0;
            }
            if (cy > (1 - portion) * screen_height)
            {
                cy = (1 - portion) * screen_height;
            }

            float uvx = cx / screen_width; //Finds starting portion of the camera to render
            float uvy = cy / screen_height;

            if (Mathf.Abs(uvx - prev_uvx) > .015 || Mathf.Abs(uvy - prev_uvy) > .015)//Checks if the distance is enough to warrant updating values
            {
                this.gameObject.GetComponent<RawImage>().uvRect = new Rect(uvx, uvy, portion, portion);//Creates the image using the portion of the render you want, and the starting position of render
                transform.position = new Vector3(cx, cy, 0);//Sets position of rect
                prev_uvx = uvx;//Sets new values for check
                prev_uvy = uvy;
            }


        }
        update = !update;//Alternates whether the frame can be updated or not as most webcam software runs at 30fps anyways

    }
}
