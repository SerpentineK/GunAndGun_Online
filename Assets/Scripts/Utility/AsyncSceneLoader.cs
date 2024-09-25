using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AsyncSceneLoader
{
    public static bool finishedLoading;
    public static bool finishedUnloading;

    public static IEnumerator LoadNextSceneAsync(string nextScene)
    {
        finishedLoading = false;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        finishedLoading = true;
    }

    public static IEnumerator UnloadPreviousSceneAsync(string previousScene)
    {
        finishedUnloading = false;

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(previousScene);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        Resources.UnloadUnusedAssets();

        finishedUnloading = true;
    }
}
