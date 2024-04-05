using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LevelThreePlayerController : MonoBehaviour
{
    // Main character 
    public GameObject head;

    // Shelf keeping track of items
    public int _Points = 0;

    public MovingTarget target1;
    public VerticalTarget target2;
    public StaticTarget target3;

    // Status menu
    public GameObject statusDisplay;
    public TextMeshProUGUI statusDisplayText;

    // Status Menu buttons 
    public GameObject continueButton;
    public GameObject restartButton;

    // Timer
    public float timeLeft = 30f;
    public bool timerOn = false;

    // Level functionality
    private bool _actuallyWon = false;

    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = new Vector3(3f, 0.1f, 0);
        head.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _Points = target1.points1 + target2.points2 + target3.points1;

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

                statusDisplayText.text += "Current Points: " + _Points + "\n";


                if (_Points == 5)
                {
                    timerOn = false;
                    _actuallyWon = true;
                }
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
            }
        }

        // if timer on is false, disable cubes and check the number
        if (timerOn == false)
        {
            if (_actuallyWon)
            {
                // disabling the cubes so that they do not move   

                statusDisplayText.text = "Congratulations! You've completed the third level! \n";

                // Set the continue button to be active 
                continueButton.SetActive(true);
            }
            else
            {
                // disabling the cubes so that they do not move 


                statusDisplayText.text = "TIME'S UP! YOU LOST \n";
                statusDisplayText.text += "You got " + _Points + " ";
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
}