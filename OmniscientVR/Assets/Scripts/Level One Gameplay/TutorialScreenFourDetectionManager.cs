using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class TutorialScreenFourDetectionManager : MonoBehaviour
{
    private bool _detectedXROrigin = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<XROrigin>())
        {
            _detectedXROrigin = true;
            gameObject.SetActive(false);
        }
    }

    public bool GetFourthScreenFieldDetectCharacterStatus()
    {
        return _detectedXROrigin;
    }
    
}
