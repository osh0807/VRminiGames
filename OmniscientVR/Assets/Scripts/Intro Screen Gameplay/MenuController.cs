using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuController instance;
    
    // Outlets
    public GameObject mainMenu;
    public GameObject levelMenu;

    void Awake()
    {
        instance = this;
    }

    void SwitchMenu(GameObject someMenu)
    {
        // Clean-up Menus
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        
        // Turn on requested menu
        someMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {   
        SwitchMenu(mainMenu);
    }

    public void ShowLevelMenu()
    {
        SwitchMenu(levelMenu);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
