using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Build.Content;
#endif
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int healthEnemy;
    public int pointsValue = 10;

    private Rigidbody rb;
    private GameObject player;
    public GameObject effectImpactEnemy;

    private GameManager gameManager;
    AudioManager audioManager;
    //private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
        }

        if (healthEnemy < 1)
        {
            DeleteEnemy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakenDamage(1);
        }
    }

    void TakenDamage(int damage)
    {
        healthEnemy -= damage;
        Destroy(Instantiate(effectImpactEnemy, transform.position, transform.rotation), 1.0f);
        if (healthEnemy <= 0)
        {
            DeleteEnemy();
        }
    }

    void DeleteEnemy()
    {
        Destroy(Instantiate(effectImpactEnemy, transform.position, transform.rotation), 1.0f);
        Destroy(gameObject);
        gameManager.EnemyDefeated(pointsValue);
        audioManager.PlaySFX(audioManager.Explosion);
    }




}
