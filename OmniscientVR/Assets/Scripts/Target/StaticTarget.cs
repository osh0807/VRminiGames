using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTarget : MonoBehaviour
{

    public int points1;


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "targetTracking")
        {
            points1++;
        }
    }
}
