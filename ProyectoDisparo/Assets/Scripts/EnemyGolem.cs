using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolem : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    public GameObject effectImpactcoll;  // Efecto de impacto
    public Transform impactPoint;  // Punto de impacto en el puño

    public bool attacking;

    public RangeEnemy range;

    public NavMeshAgent agent;
    public float distance_attack;

    private GameManager gameManager;
    public float damageAmount;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        damageAmount = 20f;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        BehaviorEnemy();
    }

    public void BehaviorEnemy()
    {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        agent.enabled = true;
        agent.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) > distance_attack && !attacking)
        {
            anim.SetBool("run", true);
        }
        else
        {
            if (!attacking)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                anim.SetBool("run", false);
            }
        }

        if (attacking)
        {
            agent.enabled = false;
        }
    }

    // Método que muestra el efecto de impacto en el puño
    public void ShowImpactEffect()
    {
        Destroy(Instantiate(effectImpactcoll, impactPoint.position, impactPoint.rotation), 2.0f);
        gameManager.PlayerTakeDamage(damageAmount);
        audioManager.PlaySFX(audioManager.Explosion);

    }

    public void Final_Ani()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > distance_attack + 0.2f)
        {
            anim.SetBool("attack", false);
        }
        attacking = false;
        range.GetComponent<CapsuleCollider>().enabled = true;
    }
}