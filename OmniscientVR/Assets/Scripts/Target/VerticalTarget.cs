using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTarget : MonoBehaviour
{
    private Vector3 pos1 = new Vector3((float)5.55, 1, (float)-2.81);
    private Vector3 pos2 = new Vector3((float)5.55, 3, (float)-2.81);
    public float speed = 5.0f;

    public int points2;

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "targetTracking")
        {
            points2++;
        }
    }
}
