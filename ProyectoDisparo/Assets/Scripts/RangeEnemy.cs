using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public Animator anim;
    public EnemyGolem enemy;
    
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("run", false);
            anim.SetBool("attack", true);
            enemy.attacking = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
