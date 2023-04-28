using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, Damage
{
    [SerializeField] private int maxHp = 5;
    [SerializeField] private HealthBar healthBar;
    private PlayerMove knockback;
    private int hp;
    private float iFrames = 1f;
    private float currentIFrames = 0f;

    void Start()
    {
        hp = PlayerPrefs.GetInt("hp", maxHp);
        maxHp = PlayerPrefs.GetInt("maxHp", maxHp);
        healthBar.SetMaxHealth(maxHp);
        healthBar.SetHealth(hp);
        knockback = GetComponent<PlayerMove>();
    }

    void Update()
    {
        currentIFrames -= Time.deltaTime;
        if(Input.GetKeyDown("r"))
        {
            SetHealth(5);
        }
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
        PlayerPrefs.SetInt("hp", hp);
        healthBar.SetHealth(hp);
        if(hp <= 0)
            Die();
    }

    public void Die()
    {
        hp = maxHp;
        PlayerPrefs.SetInt("hp",hp);
        SceneManager.LoadScene("Battle");
    }

    public void SetHealth(int amount)
    {
        hp = amount;
        healthBar.SetMaxHealth(hp);
    }
}
