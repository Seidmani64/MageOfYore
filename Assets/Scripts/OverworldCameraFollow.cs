using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCameraFollow : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float cameraDistance = 10f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, cameraDistance, 0);
    }
}
