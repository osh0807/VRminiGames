using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true; 
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend; 
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1,0);
    }

    public void FadeOut()
    {
        Fade(0,1);
    }
    
    // goal is to go from one value to another - using a coroutine = a function that play through time
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            
            rend.material.SetColor("_Color", newColor);
            
            timer += Time.deltaTime;
            yield return null; // wait for one frame
        }
        
        // make sure that the final value is alphaOut so there is no issue with rendering
        Color finalColor = fadeColor;
        finalColor.a = alphaOut;
        rend.material.SetColor("_Color", finalColor);
    }
    
}
