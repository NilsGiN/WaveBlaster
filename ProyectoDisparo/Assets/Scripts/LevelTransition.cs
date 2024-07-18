using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTransition : MonoBehaviour
{
    public GameObject particleSystema; // Asigna el objeto del sistema de partículas en el Inspector
    public float transitionDuration = 2.0f; // Duración de la transición

    private void Start()
    {
        // Asegúrate de que el sistema de partículas no se destruya al cargar la nueva escena
        DontDestroyOnLoad(particleSystema);
        particleSystema.SetActive(false);
    }

    public void StartTransition()
    {
        StartCoroutine(TransitionCoroutine());
    }

    IEnumerator TransitionCoroutine()
    {
        if (particleSystema != null)
        {
            // Activar el sistema de partículas
            particleSystema.SetActive(true);

            // Esperar un tiempo para que las partículas se esparzan
            yield return new WaitForSeconds(transitionDuration);

            // Iniciar la carga asincrónica de la nueva escena
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            asyncLoad.allowSceneActivation = false;

            // Esperar hasta que la escena esté completamente cargada
            while (!asyncLoad.isDone)
            {
                // Cuando la carga está casi completa, activar la escena
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }

            // Desactivar el sistema de partículas una vez que la nueva escena está cargada
            particleSystema.SetActive(false);
        }
    }
}