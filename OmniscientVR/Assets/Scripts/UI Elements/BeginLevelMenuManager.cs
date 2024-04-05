using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeginLevelMenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject head;
    public GameObject statusDisplay; 

    private ActionBasedContinuousMoveProvider _moveAbility;
    private ActionBasedContinuousTurnProvider _turnAbility;

    private bool _menuShouldBeActive = true; 
    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = new Vector3(3f, 0.1f, 0);
        head.transform.rotation = Quaternion.Euler(0, 90, 0);

        _moveAbility = head.GetComponent<ActionBasedContinuousMoveProvider>();
        _turnAbility = head.GetComponent<ActionBasedContinuousTurnProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_menuShouldBeActive)
        {
            menu.SetActive(true);
            // we are taking the position of the head, and move it towards the origin direction, so it is in front of the player
            transform.position = new Vector3(4.5f, 1, 0);
            _moveAbility.enabled = false;
            _turnAbility.enabled = false;
            statusDisplay.SetActive(false);
        }
        else
        {
            menu.SetActive(false);
            _moveAbility.enabled = true;
            _turnAbility.enabled = true;
            statusDisplay.SetActive(true);
        }

    }

    public void Play()
    {
        _menuShouldBeActive = false;
        _moveAbility.enabled = true;
        _turnAbility.enabled = true;
    }
    
}
