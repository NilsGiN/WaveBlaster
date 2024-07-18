using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] enemies;
    public int waveCount;
    public int wave;
    public int enemyType;
    public bool spawning;
    private int enemiesSpawned;
    private GameManager gameManager;

    public Image waveImage;
    public Sprite waveSprite;

    AudioManager audioManager;

    void Start()
    {
        waveCount = 2;
        wave = 1;
        spawning = false;
        enemiesSpawned = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        waveImage.gameObject.SetActive(false);
        waveImage.color = new Color(waveImage.color.r, waveImage.color.g, waveImage.color.b, 0); // Asegurarse de que la imagen sea invisible al inicio
        gameManager.UpdateWaveCounter(wave);  // Actualizar el contador de oleadas al inicio
    }

    void Update()
    {
        if (!spawning && enemiesSpawned == gameManager.defeatedEnemies)
        {
            StartCoroutine(SpawnWave(waveCount));
        }
    }

    IEnumerator SpawnWave(int waveC)
    {
        spawning = true;

        //activar texto de nueva ronda
        yield return new WaitForSeconds(1);
        waveImage.sprite = waveSprite;
        waveImage.gameObject.SetActive(true);
        LeanTween.alpha(waveImage.rectTransform, 1f, 1f); // Desvanecer la imagen a visible en 1 segundo
        audioManager.PlaySFX(audioManager.Wave);            

        yield return new WaitForSeconds(4);

        //desactivar texto de nueva ronda
        LeanTween.alpha(waveImage.rectTransform, 0f, 1f).setOnComplete(() => waveImage.gameObject.SetActive(false)); // Desvanecer la imagen a invisible en 1 segundo y luego ocultarla
        yield return new WaitForSeconds(1);

        for (int i = 0; i < waveC; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(2);
        }

        // Incrementar número de oleada y cantidad de enemigos para la próxima oleada
        wave += 1;
        waveCount += 2;
        gameManager.UpdateWaveCounter(wave);
        spawning = false;
    }

    void SpawnEnemy(int wave)
    {
        int spawnPos = Random.Range(0, spawnPoints.Length);
        if (wave == 1)
        {
            enemyType = 1;
        } 
        else if (wave < 4) 
        {
            enemyType = Random.Range(0, 2);
        }
        else
        {
            enemyType = Random.Range(0, 3);
        }
        Instantiate(enemies[enemyType], spawnPoints[spawnPos].transform.position, spawnPoints[spawnPos].transform.rotation);
        enemiesSpawned +=1;
    }
}
