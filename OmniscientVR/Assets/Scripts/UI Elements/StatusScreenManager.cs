using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class StatusScreenManager : MonoBehaviour
{
    public Transform head;
    public GameObject menu;
    private float _spawnDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
        menu.transform.position =
            head.position + new Vector3(head.forward.x, 0.2f, head.forward.z - 0.2f).normalized * _spawnDistance;

    }

    // Update is called once per frame
    void Update()
    {
     
    }
    
}
