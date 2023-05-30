using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBonesAttacks : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePos;
    public void Fireball()
    {
        Instantiate(fireballPrefab, firePos.position, Quaternion.identity);
    }
}
