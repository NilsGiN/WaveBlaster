using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyFirePoint;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootEnemy", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootEnemy()
    {
        Instantiate(bullet, enemyFirePoint.transform.position, enemyFirePoint.transform.rotation);
    }
}
