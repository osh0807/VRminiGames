using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(-4, -1, (float)-5.56);
    private Vector3 pos2 = new Vector3(-6, -1, (float)-3.41);
    public float speed = 10.0f;

    public int points1;

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "targetTracking")
        {
            points1++;
        }
    }

}
