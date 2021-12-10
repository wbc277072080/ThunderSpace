using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{   
    
    [SerializeField] float fadeTime = 1f;

    Color color;

    const string GAMEPLAY = "gameplay";
    const string MAINMENU = "MainMenu";

    IEnumerator LoadingCoroutine(string sceneName)
    {
        // Load new scene in background and
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        // Set this scene inactive
        loadingOperation.allowSceneActivation = false;
        while(color.a < 1f){
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            yield return null;
        }
        // Fade out
        // transitionImage.gameObject.SetActive(true);

        // while (color.a < 1f)
        // {
        //     color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
        //     transitionImage.color = color;

        //     yield return null;
        // }

        // yield return new WaitUntil(() => loadingOperation.progress >= 0.9f);

        // Activate the new scene
        loadingOperation.allowSceneActivation = true;
        while(color.a > 0f){
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            yield return null;
        }

        // Fade in
        // while (color.a > 0f)
        // {
        //     color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
        //     transitionImage.color = color;

        //     yield return null;
        // }

        // transitionImage.gameObject.SetActive(false);
    }


    void Load(string sceneName){
        //0: mainmenu
        //1: gameplay
       
        SceneManager.LoadScene(sceneName);
    }

    public void LoadGameplayScene(){
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(GAMEPLAY));
    }

    public void LoadMainMenuScene(){
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(MAINMENU));
    }
}
