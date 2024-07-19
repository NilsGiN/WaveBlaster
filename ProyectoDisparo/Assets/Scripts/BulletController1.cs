using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController1 : MonoBehaviour
{
    Rigidbody bulletrb;
    public GameObject effectImpact;
    public GameObject effectImpactcoll;
    AudioManager audioManager;

    private GameManager gameManager;
    public float damageAmount;

    public float power = 100f;
    public float lifeTime = 3f;

    public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        bulletrb = GetComponent<Rigidbody>();
        bulletrb.velocity = this.transform.forward * power;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        damageAmount = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            Destroy(Instantiate(effectImpact, transform.position, transform.rotation), 2.0f);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.PlayerTakeDamage(damageAmount);
            Destroy(Instantiate(effectImpactcoll, transform.position, transform.rotation), 2.0f);
            Destroy(this.gameObject);
            audioManager.PlaySFX(audioManager.Explosion);
        }
    }
}
