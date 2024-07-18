using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;

    public void NextLevel()
    {
        StartCoroutine(LoadScene());
    }

    public static void RestartLevel() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpecificScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }
}
