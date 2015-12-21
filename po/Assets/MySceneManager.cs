using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager {
    private static string _crossSceneName = "Loading";
    private static string _currentLoadingSceneName = "";
    private static string _emptySceneName = "Empty";

    public static void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(_currentLoadingSceneName)) return;
        if (string.IsNullOrEmpty(sceneName)) return;

        _currentLoadingSceneName = sceneName;
        SceneManager.LoadScene(_crossSceneName, LoadSceneMode.Single);
    }

    public static AsyncOperation OnCrossSceneLoaded ()
    {
        return SceneManager.LoadSceneAsync(_currentLoadingSceneName, LoadSceneMode.Single);
    }

    public static void OnLoadingDone()
    {
        SceneManager.UnloadScene(_crossSceneName);
    }
}
