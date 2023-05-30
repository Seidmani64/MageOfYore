using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float force = 20f;
    [SerializeField] private float deathTimer = 3f;
    private Rigidbody rb;
    private GameObject player;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * force);
    }

    void Update()
    {
        rb.AddForce(direction.normalized * force/300);
        deathTimer -= Time.deltaTime;
        if(deathTimer <= 0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag != "Bullet")
            Destroy(gameObject);
    }
}
