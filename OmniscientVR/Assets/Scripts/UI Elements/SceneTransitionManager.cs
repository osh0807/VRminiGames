using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    // if we want to wait until a scene has completed, we can use a coroutine to do it
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        // will actually wait necessary until screen to fade
        yield return new WaitForSeconds(fadeScreen.fadeDuration); 
        
        // Launch a new scene
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        
        // Launch the new scene
        // can see if the operation is done, or check the progress of the operation 
        
        // wait until the fading as finished or the async operation as finished first
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        // while the operation is not done, increment the time
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null; 
        }

        // now allow the switch to the new scene completely
        operation.allowSceneActivation = true;
    }

}
