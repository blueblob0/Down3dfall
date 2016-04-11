using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    int speed = 90;
    protected float health;

    bool huntPlayer;
    Transform player;
    Rigidbody enemyRig;
    Material mymat;
    public bool dead;
    float stopDis = 0.001f;
    float hitTime = -10; //Time last hit;
    float hitDiff = 0.5f;
    	// Use this for initialization
	protected virtual void Start ()
    {
        dead = false;
        enemyRig = gameObject.GetComponent<Rigidbody>();
        mymat = gameObject.GetComponent<Renderer>().material;
        mymat.color = Color.white;
    }
	
	// Update is called once per frame
	void Update () {

        if (huntPlayer)
        {
            // transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //enemyRig.MovePosition(player.position* Time.deltaTime);


            if (Vector3.Distance(transform.position, player.position) > stopDis)
            {
               // direction = player.position - transform.position;


                //enemyRig.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
               
                     transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);


            }
        }


        if(Time.time -hitTime >hitDiff)
        {
            mymat.color = Color.white;

        }

	}

    /// <summary>
    /// The amount of dmg to take
    /// </summary>
    public void takeDmg(int amount)
    {
        
        health -= amount;
      
        if (health <= 0)
        {
            dead = true;
             Destroy(gameObject);
        
        }



    } 



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("fdgdfg");
            player = other.transform;
            huntPlayer = true;
        }        
        else if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Debug.Log(other.name);
            //takeDmg(34);
            takeDmg(100);
            mymat.color =Color.red;
            hitTime = Time.time;
        }
    }
}
