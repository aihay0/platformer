using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingObject : MonoBehaviour
{
    // Start is called before the first frame update
    public int positioncount;
    public float looptime;
    public float[] xpos = new float[100];
    public float[] ypos = new float[100];
    private float curtime = 0;
    void Start()
    {
        transform.position = new Vector3(xpos[0],ypos[0],-1);
    }

    // Update is called once per frame
    void Update()
    {
        float timeinterval = looptime/positioncount;
    	curtime += Time.deltaTime;
        int cur = (int)(curtime/timeinterval)+1;
        cur %= positioncount;
        int prev = (cur-1+positioncount)%positioncount;
        float newx=xpos[prev]+(xpos[cur]-xpos[prev])*((curtime/timeinterval)-(int)(curtime/timeinterval));
        float newy=ypos[prev]+(ypos[cur]-ypos[prev])*((curtime/timeinterval)-(int)(curtime/timeinterval));
        transform.position = new Vector3(newx,newy,-1);
    }
}
