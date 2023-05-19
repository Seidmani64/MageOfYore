using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBullet : MonoBehaviour
{
    [SerializeField] private float force = 20f;
    [SerializeField] private float deathTimer = 3f;
    private Rigidbody rb;
    [SerializeField] private Vector3 scaleFactor = Vector3.zero;


    void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale = scaleFactor * Time.deltaTime;
        transform.localScale += newScale;
        deathTimer -= Time.deltaTime;
        if(deathTimer <= 0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Wall")
            Destroy(gameObject);
    }

    public void LaunchBullet(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction.normalized * force);
    }
}
