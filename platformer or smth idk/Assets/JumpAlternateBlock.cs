using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAlternateBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trigger;
    public float xpos;
    public float ypos;
    public bool startsdisappeared;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int)(trigger.transform.position.x);
        x %= 2;
        bool y = false;
        if(x == 1)
        	y = true;
        if(startsdisappeared == false)
        	y = !y;
        if(y)
        	transform.position = new Vector3(xpos,ypos,-1);
        else
        	transform.position = new Vector3(xpos,-1000,-1);
    }
}
