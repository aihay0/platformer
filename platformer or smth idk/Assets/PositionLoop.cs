using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLoop : MonoBehaviour
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
        int cur = (int)(curtime/timeinterval);
        cur %= positioncount;
        transform.position = new Vector3(xpos[cur],ypos[cur],-1);
    }
}
