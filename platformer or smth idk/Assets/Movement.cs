using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    public GameObject player;
    public GameObject[] objects = new GameObject[10000];
    public int[] objecttype = new int[10000];
    public float xstartpos;
    public float ystartpos;
    public GameObject jumptrigger;
    private bool[] coltyp = new bool[100];
    private float falling;
    private float jump;
    private float djump;
    private float tslj;
    private float tsll;
    private float tslr;
    private float jumped;
    private float timesincefall;
    private float rightdash;
    private float leftdash;
    private float tsldash;
    private bool releasedleft;
    private bool releasedright;
    private bool released;
    private bool djumped;
    private bool canjump;
    private bool suffocated;
    private bool wasfalling;
    private bool stoppedjumping;
    private bool gravityup;
    // Start is called before the first frame update
    void Start()
    {
    	transform.position = new Vector3(xstartpos,ystartpos,-4);
        falling = 0.01f;
        djump = 0;
        jump = 0;
        jumped = 0;
        tslj = 100.0f;
        tsll = 100.0f;
        tslr = 100.0f;
        tsldash = 100.0f;
        timesincefall = 100.0f;
        rightdash = 0;
        leftdash = 0;
        djumped = true;
        released = true;
        canjump = true;
        stoppedjumping = false;
        suffocated = false;
        wasfalling = false;
        gravityup = false;
        releasedleft = true;
        releasedright = true;
    }
    private int collision(float curx, float cury)
    {
    	for(int i = 0; i < 10;i++)
    		coltyp[i]=false;
    	int ii=-1;
        for(int i = 0; i < 100; i++)
        {
        	if(objects[i]==null)
        		continue;
        	float x=objects[i].gameObject.transform.localPosition.x-curx;
        	float y=objects[i].gameObject.transform.localPosition.y-cury;
        	float sx=objects[i].gameObject.transform.localScale.x/2+0.5f;
        	float sy=objects[i].gameObject.transform.localScale.y/2+0.5f;
        	if(Math.Abs(x)<sx && Math.Abs(y)<sy){
        		coltyp[objecttype[i]]=true;
        		ii=i;
        	}
        	if((objecttype[i]==1||objecttype[i]==2)&&Math.Abs(x)<sx && Math.Abs(y)<sy){
        		coltyp[objecttype[i]]=true;
        		ii=i;
        	}
        	if(objecttype[i]==0 && Math.Abs(x)+0.1<sx && Math.Abs(y)+0.1<sy){
        		suffocated=true;
        	}
        }
        return ii;
    }
    private void updatepos(){
    	float curx=transform.position.x,cury=transform.position.y;
    	for(int i = 0; i < 100; i++){
    		if(objects[i]==null)
        		continue;
        	float x=objects[i].gameObject.transform.localPosition.x-curx;
        	float y=objects[i].gameObject.transform.localPosition.y-cury;
        	float sx=objects[i].gameObject.transform.localScale.x/2+0.5f;
        	float sy=objects[i].gameObject.transform.localScale.y/2+0.5f;
    		if(objecttype[i]==3 && Math.Abs(x)<sx && Math.Abs(y)<sy){
        		if(sx-Math.Abs(x)<sy-Math.Abs(y)){
        			if(x<0){
        				transform.Translate(0.1f,0,0);
        			}
        			else{
        				transform.Translate(-0.1f,0,0);
        			}
        		}
        		else{
        			if(y<0){
        				transform.Translate(0,0.1f,0);
        			}
        			else{
        				transform.Translate(0,-0.1f,0);
        			}
        		}
        	}
    	}
    	return;
    }
    // Update is called once per frame
    void Update()
    {
    	int gravity=1;
    	if(gravityup==true)
    		gravity=-1;
    	if(coltyp[1])
		    return;
    	suffocated=false;
    	collision(transform.position.x,transform.position.y);
    	if(coltyp[3])
	    	updatepos();
    	if(coltyp[2]||suffocated)
    		Start();
    	tslj+=Time.deltaTime;
    	tsll+=Time.deltaTime;
    	tslr+=Time.deltaTime;
    	timesincefall+=Time.deltaTime;
    	tsldash+=Time.deltaTime;
    	if(timesincefall>0.2&&timesincefall<100)
    		canjump=false;
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || tslj<0.5f))
        {
            if (djumped==false&&canjump==false&&(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)))
            {
                if (djumped == false && released)
                {
                    djump = 2.0f;
                    jump = 0.0f;
                    djumped = true;
                    falling = 0.01f;
                    wasfalling=true;
                }
            }
            else if(canjump)
            {
    	        jump = 0.5f;
    	        jumped = 0.5f;
    	        djump = 0.0f;
	            falling = 0.01f;
	            canjump = false;
	            djumped = true;
	            jumptrigger.gameObject.transform.Translate(new Vector3(1,0,0));
	            stoppedjumping = false;
	            tslj=100.0f;
            }
            else if(released==false&&djumped&&stoppedjumping==false&&(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))){
            	float x=Time.deltaTime*10;
            	if(jumped+x>2)
            		x=2-jumped;
            	jump+=x;
            	jumped+=x;
            }
            else if(released&&(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)))
            	tslj=0;
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
	            released = false;
	        else{
	        	released = true;
	            if(djumped==true&&stoppedjumping==false){
	            	djumped=false;
	            	stoppedjumping = true;
	            }
	        }
        }
        else
        {
            released = true;
            if(djumped==true&&stoppedjumping==false){
            	djumped=false;
            	stoppedjumping = true;
            }
        }
        if (jump > 0.1f || djump > 0.1f)
        {
            float x;
            float y=2-jumped;
            if(stoppedjumping)
            	y=0;
            if (djump>0.1f)
                x = Time.deltaTime * djump*3;
            else
                x = Time.deltaTime * (jump+y)*3;
            Vector3 v = new Vector3(0, x*5*gravity, 0);
            if(collision(transform.position.x,transform.position.y+x*5*gravity)==-1){
	            transform.Translate(v);
	        }
	        else{
	        	jump = 0.0f;
	        	djump = 0.0f;
	        	if(coltyp[1])
		    		return;
		    	if(coltyp[2])
		    		Start();
		    	if(coltyp[4])
		    		gravityup=!gravityup;
	        }
            if (jump > 0.0f)
                jump-=x*3;
            if (djump > 0.0f)
            {
                djump-=x*3;
                jump = 0.0f;
            }
        }
        else if(falling>0.1f)
        {
            falling+=Time.deltaTime;
            if (falling > 0.5f)
                falling = 0.5f;
            Vector3 v = new Vector3(0, -(falling-0.1f)*0.2f*gravity, 0);
            if(collision(transform.position.x,transform.position.y-(falling-0.1f)*0.2f*gravity)==-1){
	            transform.Translate(v);
	            if(wasfalling==false){
	            	wasfalling=true;
	            	djumped=false;
	            	timesincefall=0.0f;
	            }
	        }
	        else{
	        	wasfalling=false;
	        	falling = 0.0f;
	        	canjump=true;
	        	timesincefall=100.0f;
	        	if(coltyp[1])
		    		return;
		    	if(coltyp[2])
		    		Start();
		    	if(coltyp[4])
		    		gravityup=!gravityup;
	        }
        }
        else
        {
            falling+=Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 v = new Vector3(Time.deltaTime*5, 0, 0);
            if(collision(transform.position.x+Time.deltaTime*5,transform.position.y)==-1)
            	transform.Translate(v);
            if(coltyp[1])
	    		return;
	    	if(coltyp[2])
	    		Start();
	    	if(coltyp[4])
		    	gravityup=!gravityup;
		    if(tslr<0.1f&&releasedright&&tsldash>2)
		    	rightdash=0.25f;
		    tsll = 100.0f;
		    tslr = 0.0f;
		    releasedright = false;
        }
        else{
        	releasedright = true;
        }
        if (Input.GetKey("b") || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 v = new Vector3(-Time.deltaTime*5, 0, 0);
            if(collision(transform.position.x-Time.deltaTime*5,transform.position.y)==-1)
	            transform.Translate(v);
	        if(coltyp[1])
	    		return;
	    	if(coltyp[2])
	    		Start();
	    	if(coltyp[4])
		    	gravityup=!gravityup;
		    if(tsll<0.1f&&releasedleft&&tsldash>2)
		    	leftdash=0.25f;
		    tsll = 0.0f;
		    tslr = 100.0f;
		    releasedleft = false;
        }
        else
        	releasedleft = true;
        if(leftdash>0){
        	tsldash = 0;
        	leftdash-=Time.deltaTime;
        	Vector3 v = new Vector3(-Time.deltaTime*25, 0, 0);
        	if(collision(transform.position.x-Time.deltaTime*25,transform.position.y)==-1)
        		transform.Translate(v);
        	if(coltyp[1])
	    		return;
	    	if(coltyp[2])
	    		Start();
	    	if(coltyp[4])
		    	gravityup=!gravityup;
        }
        if(rightdash>0){
        	tsldash = 0;
        	rightdash-=Time.deltaTime;
        	Vector3 v = new Vector3(Time.deltaTime*25, 0, 0);
        	if(collision(transform.position.x+Time.deltaTime*25,transform.position.y)==-1)
        		transform.Translate(v);
        	if(coltyp[1])
	    		return;
	    	if(coltyp[2])
	    		Start();
	    	if(coltyp[4])
		    	gravityup=!gravityup;
        }
        return;
    }
}
