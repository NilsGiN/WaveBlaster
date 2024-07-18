using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTransition : MonoBehaviour
{
    public GameObject particleSystema; // Asigna el objeto del sistema de part�culas en el Inspector
    public float transitionDuration = 2.0f; // Duraci�n de la transici�n

    private void Start()
    {
        // Aseg�rate de que el sistema de part�culas no se destruya al cargar la nueva escena
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
            // Activar el sistema de part�culas
            particleSystema.SetActive(true);

            // Esperar un tiempo para que las part�culas se esparzan
            yield return new WaitForSeconds(transitionDuration);

            // Iniciar la carga asincr�nica de la nueva escena
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            asyncLoad.allowSceneActivation = false;

            // Esperar hasta que la escena est� completamente cargada
            while (!asyncLoad.isDone)
            {
                // Cuando la carga est� casi completa, activar la escena
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }

            // Desactivar el sistema de part�culas una vez que la nueva escena est� cargada
            particleSystema.SetActive(false);
        }
    }
}