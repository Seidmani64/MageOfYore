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

    void Start()
    {
        hp = maxhp;
        healthBar.SetMaxHealth(maxhp);
        knockback = GetComponent<PlayerMove>();
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            Vector3 knockbackDir = transform.position - col.gameObject.transform.position;
            knockback.AddImpact(knockbackDir, 15f);
        }
            
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            Vector3 knockbackDir = transform.position - col.gameObject.transform.position;
            knockback.AddImpact(knockbackDir, 15f);
        }
            
    }
    
    public void TakeDamage(int amount)
    {
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
