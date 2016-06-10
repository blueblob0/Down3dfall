using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour
{
    private void Update()
    {
        Vector3 trans = transform.position;

        trans.y +=25 * Time.deltaTime;
        transform.position= trans;
    }




}
