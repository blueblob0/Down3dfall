using UnityEngine;
using System.Collections;

public class Ghost : Enemy {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        health = 100;
        mymat.color = new Color(Random.Range(0f, 1), Random.Range(0f,1), Random.Range(0f, 1));

        Debug.Log(mymat.color);
        // Update is called once per frame
    }
}
