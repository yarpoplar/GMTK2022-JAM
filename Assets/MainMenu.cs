using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void SwitchScene()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SwitchScene();
    }

    IEnumerator LoadYourAsyncScene()
    {
        //loadingScreen.gameObject.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
