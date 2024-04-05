using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LevelTwoPlayerController : MonoBehaviour
{
    // Main character

    public GameObject head;

    public CapsuleCollider bodyCollider;

    public GenerateEnemies Enemies;
    //public GeneratePistols Pistols;

    // Shelf keeping track of items
    private float playerHealth = 100f;

    // Status menu
    public GameObject statusDisplay;
    public TextMeshProUGUI statusDisplayText;

    // Status Menu buttons 
    public GameObject continueButton;
    public GameObject restartButton;

    // Timer
    public float timeLeft;
    public bool timerOn = false;


    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = new Vector3(3f, 0.1f, 0);
        head.transform.rotation = Quaternion.Euler(0, 90, 0);

        bodyCollider = head.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (statusDisplay.activeSelf)
        {
            timerOn = true;
        }
        
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;

                UpdateTimer(timeLeft);
            }

            else
            {
                timeLeft = 0;
                timerOn = false;
            }
        }
        
        if (timerOn == false)
        {
            Enemies.stop = true;
            //Pistols.stop = true;

            if (Enemies.numEnemies < 5)
            {
                statusDisplayText.text = "Congratulations! You've completed the second level! \n";

                // Set the continue button to be active 
                continueButton.SetActive(true);
            }
            else
            {
                statusDisplayText.text = "YOU LOST \n";
                statusDisplayText.text += "Please try again";

                // Set the reset button to be active 
                restartButton.SetActive(true);
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        statusDisplayText.text = string.Format("{0:00} : {1:00} \n", minutes, seconds);
    }

    public float getPlayerHealth()
    {
        return playerHealth;
    }

    public void setPlayerHealth(float a)
    {
        playerHealth = a;
    }
    
}