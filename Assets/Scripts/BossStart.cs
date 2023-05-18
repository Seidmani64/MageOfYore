using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStart : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Animator animator;
    private DialogueManager dialogueManager;
    private bool active = false;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnStart(2f));
        StartCoroutine(SpawnChase(4f));
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 1)
        {
            SceneManager.LoadScene("SlimeBoss");
        }
        if(active)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
    }

    private IEnumerator SpawnStart(float duration)
    {
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("Activate");
    }

    private IEnumerator SpawnChase(float duration)
    {
        yield return new WaitForSeconds(duration);
        active = true;
    }
}
