using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOnePlayerController : MonoBehaviour
{
    // Main character 
    public GameObject body;
    public GameObject head;

    // Cube objects
    public LevelOneCubeManager[] cubes;
    public GameObject interactableCubeZero;
    public GameObject interactableCubeOne;
    public GameObject interactableCubeTwo;
    public GameObject interactableCubeThree;
    public GameObject interactableCubeFour;

    // Status menu
    public GameObject statusDisplay;
    public TextMeshProUGUI statusDisplayText;

    // Status Menu buttons 
    public GameObject continueButton;
    public GameObject restartButton;

    // Sound Manager
    public GameObject levelOneSound;

    // GameObjects for the hands 
    public GameObject leftHand;
    public InputActionProperty leftPinchAction;
    public InputActionProperty leftGrabAction;

    public GameObject rightHand;
    public InputActionProperty rightPinchAction;
    public InputActionProperty rightGrabAction;

    // Level functionality
    private bool _actuallyWon;
    private int _finalNumInShelf;
    private bool _gameStarted;
    private readonly float _movementThreshold = 0.1f;

    // Shelf keeping track of items
    private int _numInShelf;

    // Boolean to see if random sound is playing
    private bool _savedPreviousMovement;

    // System for grace period freezing 
    private float _freezingGracePeriodTime;
    private float _freezingGracePeriodThreshold = 2f;

    // Vectors to keep track of previous frame's hand movement
    private Vector3 _previousBodyPosition;
    private Vector3 _previousHeadPosition;

    private float _previousLeftGrip;
    private Vector3 _previousLeftHandPosition;
    private float _previousLeftPinch;
    private float _previousRightGrip;
    private Vector3 _previousRightHandPosition;
    private float _previousRightPinch;

    private SoundManager _soundManager;

    // Timer
    private float _timeLeft = 120f;
    private bool _timerOn;
    private bool _userLostBecauseMoved;

    // Start is called before the first frame update
    private void Start()
    {
        // creating 5 cubes to put in
        cubes = new LevelOneCubeManager[5];
        cubes[0] = interactableCubeZero.GetComponent<LevelOneCubeManager>();
        cubes[1] = interactableCubeOne.GetComponent<LevelOneCubeManager>();
        cubes[2] = interactableCubeTwo.GetComponent<LevelOneCubeManager>();
        cubes[3] = interactableCubeThree.GetComponent<LevelOneCubeManager>();
        cubes[4] = interactableCubeFour.GetComponent<LevelOneCubeManager>();

        _soundManager = levelOneSound.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (statusDisplay.activeSelf && !_gameStarted)
        {
            _gameStarted = true;
            _timerOn = true;
            _savedPreviousMovement = false;

            _freezingGracePeriodTime = 0f;

            _soundManager.StartRandomSoundRoutine();
        }

        else if (_timerOn && _gameStarted)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;

                UpdateTimer(_timeLeft);
                SetNumInShelf();

                statusDisplayText.text += "Cubes in Shelf: " + _numInShelf + "/5 \n";

                // if random sound is playing, ensure that the hand movement is not moving
                if (_soundManager.GetRandomSoundStatus())
                {
                    statusDisplayText.text += "OMNISCIENCE IS HERE! STOP MOVING.";
                    // capturing the previous movement of the body movement

                    if (!_savedPreviousMovement && _freezingGracePeriodTime < _freezingGracePeriodThreshold)
                    {
                        _freezingGracePeriodTime += Time.deltaTime;
                    }
                    else if (!_savedPreviousMovement &&
                             _freezingGracePeriodTime >= _freezingGracePeriodThreshold)
                    {
                        _previousBodyPosition = body.transform.position;
                        _previousHeadPosition = head.transform.position;

                        _previousLeftHandPosition = leftHand.transform.position;
                        _previousRightHandPosition = rightHand.transform.position;

                        _previousLeftGrip = leftGrabAction.action.ReadValue<float>();
                        _previousLeftPinch = leftPinchAction.action.ReadValue<float>();
                        _previousRightGrip = rightGrabAction.action.ReadValue<float>();
                        _previousRightPinch = rightPinchAction.action.ReadValue<float>();

                        _savedPreviousMovement = true;
                    }

                    if (_savedPreviousMovement && CheckBodyNotMoving() == false)
                    {
                        _timerOn = false;
                        _actuallyWon = false;
                        _userLostBecauseMoved = true;
                    }
                }
                else
                {
                    if (_savedPreviousMovement) _savedPreviousMovement = false;
                    _freezingGracePeriodTime = 0f;
                }

                if (_numInShelf == 5)
                {
                    _timerOn = false;
                    _actuallyWon = true;
                    _finalNumInShelf = _numInShelf;
                }
            }
            else
            {
                _timeLeft = 0;
                _timerOn = false;
                _finalNumInShelf = _numInShelf;
            }
        }

        // if timer on is false, disable cubes and check the number
        else if (!_timerOn && _gameStarted)
        {
            _soundManager.StopRandomSoundRoutine();
            if (_actuallyWon)
            {
                // disabling the cubes so that they do not move   
                DisableCubes();

                statusDisplayText.text = "Congratulations! You've completed the first level! \n";

                // Set the continue button to be active 
                continueButton.SetActive(true);
            }
            else
            {
                // disabling the cubes so that they do not move 
                DisableCubes();

                if (_userLostBecauseMoved)
                {
                    statusDisplayText.text = "The Omniscience got you! :( \n";
                    statusDisplayText.text += "Please restart the level to try again.";
                }
                else
                {
                    statusDisplayText.text = "TIME'S UP! YOU LOST \n";
                    statusDisplayText.text += "You got " + _finalNumInShelf + " of 5 in shelf only. \n";
                    statusDisplayText.text += "Please try again";
                }

                // Set the reset button to be active 
                restartButton.SetActive(true);
            }
        }
    }

    // for the cubes
    private void SetNumInShelf()
    {
        // check how many cubes are in the box, prints the message after completed 
        var numInShelfRound = 0;
        for (var i = 0; i < cubes.Length; i++)
        {
            var isInShelf = cubes[i].CheckInShelfStatus();
            if (isInShelf) numInShelfRound += 1;
        }

        _numInShelf = numInShelfRound;
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        statusDisplayText.text = string.Format("{0:00} : {1:00} \n", minutes, seconds);
    }

    private void DisableCubes()
    {
        interactableCubeZero.SetActive(false);
        interactableCubeOne.SetActive(false);
        interactableCubeTwo.SetActive(false);
        interactableCubeThree.SetActive(false);
        interactableCubeFour.SetActive(false);
    }

    // for random sound hand movement
    private bool CheckBodyNotMoving()
    {
        var bodyMovementDifference = body.transform.position - _previousBodyPosition;
        var bodyMovementDifferenceAbs = new Vector3(Mathf.Abs(bodyMovementDifference.x),
            Mathf.Abs(bodyMovementDifference.y), Mathf.Abs(bodyMovementDifference.z));

        // comparing if the body movement difference is less than threshold
        if (bodyMovementDifferenceAbs.x > _movementThreshold || bodyMovementDifferenceAbs.y > _movementThreshold ||
            bodyMovementDifferenceAbs.z > _movementThreshold)
            return false;

        var headMovementDifference = head.transform.position - _previousHeadPosition;
        var headMovementDifferenceAbs = new Vector3(Mathf.Abs(headMovementDifference.x),
            Mathf.Abs(headMovementDifference.y), Mathf.Abs(headMovementDifference.z));

        // comparing if the head movement difference is less than threshold
        if (headMovementDifferenceAbs.x > _movementThreshold || headMovementDifferenceAbs.y > _movementThreshold ||
            headMovementDifferenceAbs.z > _movementThreshold) return false;

        var leftHandMovementDifference = leftHand.transform.position - _previousLeftHandPosition;
        var leftHandMovementDifferenceAbs = new Vector3(Mathf.Abs(leftHandMovementDifference.x),
            Mathf.Abs(leftHandMovementDifference.y), Mathf.Abs(leftHandMovementDifference.z));

        // comparing if the left hand movement difference is less than the threshold
        if (leftHandMovementDifferenceAbs.x > _movementThreshold ||
            leftHandMovementDifferenceAbs.y > _movementThreshold ||
            leftHandMovementDifferenceAbs.z > _movementThreshold)
            return false;

        var rightHandMovementDifference = rightHand.transform.position - _previousRightHandPosition;
        var rightHandMovementDifferenceAbs = new Vector3(Mathf.Abs(rightHandMovementDifference.x),
            Mathf.Abs(rightHandMovementDifference.y), Mathf.Abs(rightHandMovementDifference.z));

        if (rightHandMovementDifferenceAbs.x > _movementThreshold ||
            rightHandMovementDifferenceAbs.y > _movementThreshold ||
            rightHandMovementDifferenceAbs.z > _movementThreshold)
            return false;

        var leftGrabActionDifference = Mathf.Abs(leftGrabAction.action.ReadValue<float>() - _previousLeftGrip);

        // checking if any of the grips have been triggered
        if (leftGrabActionDifference > 0.2) return false;

        var leftPinchActionDifference = Mathf.Abs(leftPinchAction.action.ReadValue<float>() - _previousLeftPinch);
        if (leftPinchActionDifference > 0.2) return false;

        var rightGrabActionDifference = Mathf.Abs(rightGrabAction.action.ReadValue<float>() - _previousRightGrip);
        if (rightGrabActionDifference > 0.2) return false;

        var rightPinchActionDifference = Mathf.Abs(rightPinchAction.action.ReadValue<float>() - _previousRightPinch);
        if (rightPinchActionDifference > 0.2) return false;

        return true;
    }
}