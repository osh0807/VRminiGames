using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BodyController : MonoBehaviour
{

    // Status menu
    public TextMeshProUGUI statusDisplayText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        statusDisplayText.text += "An enemy touched the player";
    }


}
