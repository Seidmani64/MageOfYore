using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 20f;
    [SerializeField] private float deathTimer = 3f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * force);
    }

    void Update()
    {
        deathTimer -= Time.deltaTime;
        if(deathTimer <= 0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
            GameObject.FindWithTag("Player").GetComponent<PlayerFire>().GainMana();
        Destroy(gameObject);
    }
}
