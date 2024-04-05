using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MainScreenPlayerController : MonoBehaviour
{
    public GameObject head; 
    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = Vector3.zero;
        head.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
