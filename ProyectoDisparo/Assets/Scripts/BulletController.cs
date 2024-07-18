using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody bulletrb;
    public GameObject effectImpact;

    public float power = 100f;
    public float lifeTime = 4f;

    public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        bulletrb = GetComponent<Rigidbody>();
        bulletrb.velocity = this.transform.forward * power;
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

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
