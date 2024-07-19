using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int defeatedEnemies;
    public float vidaMaxima = 100;
    public float vidaActual;
    public Image barraDeVida;

    private int points;
    public TMP_Text pointsText;
    public TMP_Text waveCounterText;  // Texto para el contador de oleadas

    [SerializeField] private GameObject MenuGameOver;

    void Start()
    {
        defeatedEnemies = 0;
        vidaActual = vidaMaxima;
        ActualizarBarraDeVida();
        points = 0;
        ActualizarPuntosUI();
        Time.timeScale = 1;
        MenuGameOver.SetActive(false);
    }

    void Update()
    {

    }

    public void EnemyDefeated(int pointsValue)
    {
        defeatedEnemies++;
        AddPoints(pointsValue);
    }

    // Método para hacer que el jugador reciba daño
    public void PlayerTakeDamage(float damageAmount)
    {
        vidaActual -= damageAmount;
        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {
            // Lógica para el jugador muerto
            Debug.Log("¡El jugador ha muerto!");
            // Desactivar el jugador
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.SetActive(false);
                MenuGameOver.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    // Método para actualizar la barra de vida
    void ActualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            barraDeVida.fillAmount = vidaActual / vidaMaxima;
        }
    }

    public void AddPoints(int value)
    {
        points += value;
        ActualizarPuntosUI();
        Debug.Log("Puntos actuales: " + points);
    }

    // Método para actualizar el texto de puntos en la UI
    void ActualizarPuntosUI()
    {
        if (pointsText != null)
        {
            pointsText.text = "Puntos: " + points;
        }
    }

    public void UpdateWaveCounter(int wave)
    {
        StartCoroutine(UpdateWaveCounterWithDelay(wave));
    }

    IEnumerator UpdateWaveCounterWithDelay(int wave)
    {
        yield return new WaitForSeconds(1);  // Retraso de 1 segundo
        if (waveCounterText != null)
        {
            waveCounterText.text = "Oleada: " + wave;
        }
    }
}