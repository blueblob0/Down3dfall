using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    int speed = 100;
    bool huntPlayer;
    Transform player;
    Rigidbody enemyRig;
    float stopdis = 0.01f;
	// Use this for initialization
	void Start () {
        enemyRig = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (huntPlayer)
        {
            // transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //enemyRig.MovePosition(player.position* Time.deltaTime);

            var direction = Vector3.zero;
            if (Vector3.Distance(transform.position, player.position) > stopdis)
            {
                direction = player.position - transform.position;
                enemyRig.AddRelativeForce(direction.normalized * speed, ForceMode.Force);
            }
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
