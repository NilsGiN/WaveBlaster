using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject botonPause;
    [SerializeField] private GameObject botonOptions;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject menuOptions;

    [SerializeField] private WeaponController weaponController; // Referencia al WeaponController

    private bool gamePause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePause)
            {
                Reanudar();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Debug.Log("Pausando el juego");
        gamePause = true;
        Time.timeScale = 0f;
        
        // Deshabilitar el disparo
        weaponController.EnableShooting(false);
        
        // Ocultar ambos botones
        botonPause.SetActive(false);
        botonOptions.SetActive(false);

        // Activar el menú de pausa con transparencia 0
        menuPause.SetActive(true);
        CanvasGroup canvasGroup = menuPause.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = menuPause.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;

        // Desvanecer el menú de pausa
        StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0.5f));
    }
    public void Options()
    {
        Debug.Log("Pausando el juego");
        gamePause = true;
        Time.timeScale = 0f;

        // Deshabilitar el disparo
        weaponController.EnableShooting(false);
        
        // Ocultar ambos botones
        botonPause.SetActive(false);
        botonOptions.SetActive(false);

        // Activar el menú de pausa con transparencia 0
        menuOptions.SetActive(true);
        CanvasGroup canvasGroup = menuOptions.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = menuOptions.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;

        // Desvanecer el menú de pausa
        StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0.5f));
    }

    public void Reanudar()
    {
        Debug.Log("Reanudando el juego");
        gamePause = false;
        Time.timeScale = 1f;

        // Habilitar el disparo
        weaponController.EnableShooting(true);

        // Mostrar ambos botones
        botonPause.SetActive(true);
        botonOptions.SetActive(true);

        // Desvanecer el menú de pausa
        CanvasGroup canvasGroupPause = menuPause.GetComponent<CanvasGroup>();
        if (menuPause.activeSelf)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroupPause, 0f, 0.5f, () => menuPause.SetActive(false)));
        }

        // Desvanecer el menú de opciones si está activo
        CanvasGroup canvasGroupOptions = menuOptions.GetComponent<CanvasGroup>();
        if (menuOptions.activeSelf)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroupOptions, 0f, 0.5f, () => menuOptions.SetActive(false)));
        }
    }

    public void Restart()
    {
        Debug.Log("Reiniciando el juego");
        Time.timeScale = 1f;
        SceneController.RestartLevel();
    }

    public void Home()
    {
        Debug.Log("Volviendo al menú principal");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration, System.Action onComplete = null)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        onComplete?.Invoke();
    }
}