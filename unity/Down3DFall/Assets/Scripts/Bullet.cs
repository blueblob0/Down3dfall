using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    float startTime;
    int timeout = 10;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
	if(Time.time - startTime > timeout)
        {
            Destroy(gameObject);


        }
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
           
            Destroy(gameObject);
          

        }
    }
}
