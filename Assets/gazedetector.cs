using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gazedetector : MonoBehaviour
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    public Color m_MouseOverColor; //Color the object turns when looking at it
    public int behavior; //integer to determine the behavior of the blocks script is attached to

    //This stores the GameObject’s original color
    Color m_OriginalColor; //Its original color

    //Position of block
    public Transform target;

    //Textbox and text for npcs
    public GameObject textbox;
    public Text text;

    //Transform for textbox
    Transform t_trans;

    //Possible text for npcs
    public string output;
    public string output2;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;
    Transform transform;

    //Holder for any child object the attached object may have
    GameObject child;
    float mod_val = .02F; //Movement speed value
    Vector3 start_pos; //Original position of object

    //Keeps track of how long you look at npc
    int stare_time = 0;
    int explode_time = 0; //Boss defeat timer
    int slide = 0; //Value used to track movement of textbox

    //Bools handling npc flags
    bool npc_finish = false;
    bool npc_stage2 = false;
    bool npc2_end = false;
    bool npc_play = false;


    bool last_flag = false;
    bool looking = false;//Check if player is looking
    bool on = false;//Check if object is activating in its primary direction or back the way it came
    bool active = false;//Check if object can be activated


    //Inputs behavior type and then runs movement value in x, y, or z
    void MoveOut(int dir)
    {
        switch (Mathf.Abs(dir))//Convert to abs for concentrated case statement
        {
            case 1:
                if (Mathf.Abs(start_pos.x - transform.position.x) < 15)
                {
                    //Can use dir, you can normalize it and modify it to be a direction speed
                    transform.position = transform.position + new Vector3((dir / Mathf.Abs(dir))*mod_val, 0, 0);
                }
                else
                {
                    on = true;
                    active = false;
                }
                break;


            case 2:
                if (Mathf.Abs(start_pos.y - transform.position.y) < 15)
                {
                    transform.position = transform.position + new Vector3(0, (dir / Mathf.Abs(dir)) * mod_val, 0);
                }
                else
                {
                    on = true;
                    active = false;
                }
                break;

            case 3:
                if (Mathf.Abs(start_pos.z - transform.position.z) < 15)
                {
                    transform.position = transform.position + new Vector3(0, 0, (dir / Mathf.Abs(dir)) * mod_val);
                }
                else
                {
                    on = true;
                    active = false;
                }
                break;
        }

        
        
    }
    //Inputs behavior type and then runs movement value in x, y, or z back towards its original position
    void MoveBack(int dir)
    {

        switch (Mathf.Abs(dir))
        {
            case 1:
                if (Mathf.Abs(start_pos.x - transform.position.x) > 1 )
                {
                    transform.position = transform.position - new Vector3((dir / Mathf.Abs(dir)) * mod_val, 0, 0);
                }
                else
                {
                    on = false;
                    active = false;
                }
                
                break;

            case 2:
                if (Mathf.Abs(start_pos.y - transform.position.y) > 1)
                {
                    transform.position = transform.position - new Vector3(0, (dir / Mathf.Abs(dir)) * mod_val, 0);
                }
                else
                {
                    on = false;
                    active = false;
                }
                break;
            case 3:
                if (Mathf.Abs(start_pos.z - transform.position.z) > 1)
                {
                    transform.position = transform.position - new Vector3(0, 0, (dir / Mathf.Abs(dir)) * mod_val);
                }
                else
                {
                    on = false;
                    active = false;
                }
                break;
        }
    }

    //Rotate object that runs this function
    void Rotation()
    {
        transform.Rotate(0, 0.25f, 0);
    }


    //Handles timing, text, and explosion of NPC
    void NPC()
    {
        if (!npc_finish)
        {
            if (slide < 600)//how far and how long text is up
            {
                text_up(output);//Moves text up
                transform.LookAt(target);//Makes npc face player, levitation is on purpose because I thought it would be funny
            }
            else
            {
                npc_finish = true;
            }
        }
        
        else
        {
            if (slide > 0)//Moves text down
            {
                text_down();
            }
            else
            {
                explode();//Release restraints on object to have physics explosion
            }

        }
        

    }

    //Handles timing, text, and explosion of NPC
    void NPC2()
    {
        if (!npc_finish && !npc_stage2)
        {
            //Text 1 up
            if (slide < 600)
            {
                text_up(output);
                transform.LookAt(target);
            }
            else
            {
                npc_finish = true;
            }
        }
        //Move it back down
        else if (npc_finish && !npc_stage2)
        {
            if (slide > 0)
            {
                text_down();
            }
            else
            {
                npc_stage2 = true;
            }

        }
        //Run next stage of logic
        else if (npc_finish && npc_stage2)
        {
            NPC2_2();
        }


    }

    void NPC2_2()
    {
        transform.LookAt(target);
        if (looking)
        {
            explode_time += 1;//Increase counter while staring
        }
        if (explode_time >= 300)
        {
            if (!npc2_end)
            {
                if (slide < 600)//Move text up
                {
                    text_up(output2);
                }
                else
                {
                    npc2_end = true;
                }
            }
            else
            {   //Move it down
                if (slide > 0)
                {
                    text_down();
                }
                else
                {
                    explode();//esplode
                    
                }
            }
                

        }
        

    }
    

    void text_up(string output)
    {
        
            //Move box and set text
            t_trans.position = t_trans.position + new Vector3(0, 60 / 600.0F, 0);
            text.text = output;
            slide += 1;
        
        
    }

    void text_down()
    {
        t_trans.position = t_trans.position - new Vector3(0, 60 / 600.0F, 0);
        transform.LookAt(target);
        slide -= 1;
        //move box back
    }


    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<MeshRenderer>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.material.color;
        transform = GetComponent<Transform>();
        start_pos = transform.position;
        t_trans = textbox.transform;
        Cursor.visible = true;//Releases cursor


    }

    void explode()
    {
        int count = this.gameObject.transform.childCount;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        for (int i = 0; i < (count); i++)
        {
            child = this.gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            text.text = "";
        }

    }

    void Update()
    {
        if (npc_play)
        {
            if (behavior == 4)
            {
                NPC();
            }
            else
            {
                NPC2();
            }
            
        }

        if (!looking)
        {

            if (behavior == 5)
            {//If its the specific behavior type that handles rotation, make it spin when you aren't looking at it
                Rotation();
            }
        }
         if (active)
         {
             if (!on)//Checks whether it is in the on state or not
             {
                 MoveOut(behavior);
             }
             else
             {
                 MoveBack(behavior);
             }    
                
         }
        



            
        
        
    }

    void OnMouseOver()
    {
        stare_time += 1;
        looking = true;
        // Change the color of the GameObject to red when the mouse is over GameObject
        int count = this.gameObject.transform.childCount;
        m_Renderer.material.color = m_MouseOverColor;
        for (int i = 0; i < (count); i++)
        {
            child = this.gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<MeshRenderer>().material.color = m_MouseOverColor;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            active = true;//Activates scripting when button pushed while looking at the thing
   
        }

            if (behavior == 4 || behavior == 6)//Handles npc stare time
        {
            if (stare_time >= 60)
            {
                npc_play = true;
            }
            
        }
        


    }

    void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        looking = false;//States whether you are looking at object
        
        int count = this.gameObject.transform.childCount;
        m_Renderer.material.color = m_OriginalColor;
        for (int i = 0; i < (count); i++)
        {
            child = this.gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<MeshRenderer>().material.color = m_OriginalColor;
        }
        stare_time = 0;
        explode_time = 0;
        //Decolorizes and resets stare timers
       
    }
}