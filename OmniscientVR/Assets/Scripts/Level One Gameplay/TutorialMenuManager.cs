using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialMenuManager : MonoBehaviour
{
    // Screen Outlets
    public GameObject introScreen;
    public GameObject controllerScreen;
    public GameObject movementScreen;
    public GameObject startScreen;

    public GameObject head;
    public GameObject statusDisplay; 
    
    private ActionBasedContinuousMoveProvider _moveAbility;
    private ActionBasedContinuousTurnProvider _turnAbility;
    
    private bool _onboardingFirstThreeScreens;
    private bool _onboardingFourthScreen;
    private bool _onboardingComplete;
    
    
    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = new Vector3(0.5f, 0.0f, 0.19f);
        head.transform.rotation = Quaternion.Euler(0, 90, 0);
        
        _moveAbility = head.GetComponent<ActionBasedContinuousMoveProvider>();
        _turnAbility = head.GetComponent<ActionBasedContinuousTurnProvider>();
        
        _onboardingFirstThreeScreens = true;
        _onboardingFourthScreen = false;
        _onboardingComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_onboardingFirstThreeScreens)
        {
            // we are taking the position of the head, and move it towards the origin direction, so it is in front of the player
            transform.position = new Vector3(0.5f, 0.0f, 0.19f);
            _moveAbility.enabled = false;
            _turnAbility.enabled = false;
            statusDisplay.SetActive(false);
        }
        else if (_onboardingFourthScreen)
        {
            _moveAbility.enabled = true;
            _turnAbility.enabled = true;
            
        } 
        else if (_onboardingComplete)
        {
            _moveAbility.enabled = true;
            _turnAbility.enabled = true;
            statusDisplay.SetActive(true);
        }

    }

    void SwitchMenu(GameObject someMenu)
    {
        introScreen.SetActive(false);
        controllerScreen.SetActive(false);
        movementScreen.SetActive(false);
        startScreen.SetActive(false);
        
        // Turn on requested menu
        someMenu.SetActive(true);
    }

    public void ShowControllerScreen()
    {
       SwitchMenu(controllerScreen);
    }

    public void ShowMovementScreen()
    {
        SwitchMenu(movementScreen);
    }

    public void EnableSearchForFourthScreen()
    {
        SwitchMenu(startScreen);
        _onboardingFirstThreeScreens = false;
        _onboardingFourthScreen = true;
        _onboardingComplete = false;
    }
    
    public void StartGame()
    {
        startScreen.SetActive(false);
        _onboardingFirstThreeScreens = false;
        _onboardingFourthScreen = false;
        _onboardingComplete = true;
    }
    
}
