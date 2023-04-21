using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, Damage
{
    [SerializeField] private int maxhp = 5;
    [SerializeField] private HealthBar healthBar;
    private int hp;

    void Start()
    {
        hp = maxhp;
        healthBar.SetMaxHealth(maxhp);
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            Debug.Log("Ouch!");
        }
            
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
            TakeDamage(1);
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
            SceneManager.LoadScene("SampleScene");
    }
}
