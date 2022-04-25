using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gazedetector : MonoBehaviour
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    public Color m_MouseOverColor;
    public int behavior;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    public Transform target;
    public GameObject textbox;
    public Text text;
    Transform t_trans;
    public string output;
    public string output2;
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;
    Transform transform;
    GameObject child;
    float mod_val = .02F;
    Vector3 start_pos;
    int stare_time = 0;
    int explode_time = 0;
    int slide = 0;
    bool npc_finish = false;
    bool npc_stage2 = false;
    bool npc2_end = false;
    bool npc_play = false;
    bool last_flag = false;
    bool looking = false;
    bool complete = false;
    bool on = false;
    bool active = false;

    void MoveOut(int dir)
    {
        switch (Mathf.Abs(dir))
        {
            case 1:
                if (Mathf.Abs(start_pos.x - transform.position.x) < 15 && !complete)
                {
                    transform.position = transform.position + new Vector3((dir / Mathf.Abs(dir))*mod_val, 0, 0);
                }
                else
                {
                    on = true;
                    active = false;
                }
                break;


            case 2:
                if (Mathf.Abs(start_pos.y - transform.position.y) < 15 && !complete)
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
                if (Mathf.Abs(start_pos.z - transform.position.z) < 15 && !complete)
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

    void Rotation()
    {
        transform.Rotate(0, 0.25f, 0);
    }

    void NPC()
    {
        if (!npc_finish)
        {
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
        
        else
        {
            if (slide > 0)
            {
                text_down();
            }
            else
            {
                explode();
            }

        }
        

    }

    void NPC2()
    {
        if (!npc_finish && !npc_stage2)
        {
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
            explode_time += 1;
        }
        if (explode_time >= 300)
        {
            if (!npc2_end)
            {
                if (slide < 600)
                {
                    text_up(output2);
                }
                else
                {
                    npc2_end = true;
                }
            }
            else
            {
                if (slide > 0)
                {
                    text_down();
                }
                else
                {
                    explode();
                    
                }
            }
                

        }
        

    }
    

    void text_up(string output)
    {
        

            t_trans.position = t_trans.position + new Vector3(0, 60 / 600.0F, 0);
            text.text = output;
            slide += 1;
        
        
    }

    void text_down()
    {
        t_trans.position = t_trans.position - new Vector3(0, 60 / 600.0F, 0);
        transform.LookAt(target);
        slide -= 1;
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
        Cursor.visible = true;


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
            {
                Rotation();
            }
        }
         if (active)
         {
             if (!on)
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
            active = true;
        }

            if (behavior == 4 || behavior == 6)
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
        looking = false;
        
        int count = this.gameObject.transform.childCount;
        m_Renderer.material.color = m_OriginalColor;
        for (int i = 0; i < (count); i++)
        {
            child = this.gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<MeshRenderer>().material.color = m_OriginalColor;
        }
        stare_time = 0;
        explode_time = 0;
       
    }
}