using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, Damage
{
    [SerializeField] private int maxhp = 5;
    [SerializeField] private HealthBar healthBar;
    private PlayerMove knockback;
    private int hp;
    private float iFrames = 1f;
    private float currentIFrames = 0f;

    void Start()
    {
        hp = maxhp;
        healthBar.SetMaxHealth(maxhp);
        knockback = GetComponent<PlayerMove>();
    }

    void Update()
    {
        currentIFrames -= Time.deltaTime;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy" && currentIFrames <= 0f)
        {
            TakeDamage(1);
            Vector3 knockbackDir = transform.position - col.gameObject.transform.position;
            knockback.AddImpact(knockbackDir, 15f);
        }
            
    }

    public void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Enemy" && currentIFrames <= 0f)
        {
            TakeDamage(1);
            Vector3 knockbackDir = transform.position - col.gameObject.transform.position;
            knockback.AddImpact(knockbackDir, 15f);
        }
            
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy" && currentIFrames <= 0f)
        {
            TakeDamage(1);
            Vector3 knockbackDir = transform.position - col.gameObject.transform.position;
            knockback.AddImpact(knockbackDir, 15f);
        }
            
    }
    
    public void TakeDamage(int amount)
    {
        currentIFrames = iFrames;
        hp -= amount;
        healthBar.SetHealth(hp);
        if(hp <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("You Died.");
            SceneManager.LoadScene("Battle");
    }
}
