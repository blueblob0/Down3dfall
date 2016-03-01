using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    int speed = 100;
    bool huntPlayer;
    Transform player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (huntPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("fdgdfg");
            player = other.transform;
            huntPlayer = true;
        }
    }
}
