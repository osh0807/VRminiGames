using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.OpenVR;
using UnityEngine;

public class LevelOneCubeManager : MonoBehaviour
{
    public MeshCollider shelfCollider;
    private bool _isInShelf;
    public GameObject cubeObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _isInShelf = false;
    }

    // Update is called once per frame
    void Update()
    {
        _isInShelf = false; 
        if (shelfCollider.bounds.Contains(cubeObject.transform.position))
        {
            _isInShelf = true; 
        }
    }

    public bool CheckInShelfStatus()
    {
        return _isInShelf;
    }
}
