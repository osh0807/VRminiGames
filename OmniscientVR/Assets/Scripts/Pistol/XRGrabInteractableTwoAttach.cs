using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    // use the same behavior as XRGrabInteractable, but add additional functionality to it
    // Two attachment form - one grabbing the object 
    // Start is called before the first frame update
    public Transform leftAttachTransform;
    public Transform rightAttachTransform; 
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }
        
        
        // after changing the transform, it will continue with the XRGrabInteractable
        base.OnSelectEntered(args);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
