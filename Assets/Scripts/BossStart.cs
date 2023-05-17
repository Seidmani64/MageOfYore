using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStart : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 1)
        {
        SceneManager.LoadScene("SlimeBoss");
        }
    }
}
