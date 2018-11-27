using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    [SerializeField]
    private float loadDelay = 1f;

	public void loadScene(int scene)
    {
        loadSceneAfterDelay(loadDelay, scene);
    }

    public void loadNextScene()
    {
        Debug.Log("Loading next scene");
        SceneManager.LoadScene(currentSceneIndex() + 1);
        //loadSceneAfterDelay(loadDelay, currentSceneIndex() + 1);
    }

    public static int currentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void loadSceneAfterDelay(float time, int index)
    {
        StartCoroutine(delayLoad(time, index));
    }

    private IEnumerator delayLoad(float time, int index)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
    }

    public void quitGame()
    {
        Application.Quit();
    }



}
