using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform shootSpawn;
    public bool shooting;
    public GameObject bulletPrefab;
    public bool canShoot = true; // Variable para habilitar o deshabilitar el disparo

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.Mouse0))
        {
            InstantiateBullet();
            audioManager.PlaySFX(audioManager.Shoot);
        }
    }

    public void InstantiateBullet()
    {
        Instantiate(bulletPrefab, shootSpawn.position, shootSpawn.rotation);
    }

    public void EnableShooting(bool enable)
    {
        canShoot = enable;
    }
}
