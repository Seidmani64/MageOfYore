using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossAttacks : MonoBehaviour
{
    public void ThunderShoot()
    {
        transform.parent.GetComponent<SlimeBoss>().Shoot();
    }

    public void ThunderCharge()
    {
        transform.parent.GetComponent<SlimeBoss>().Charge();
        GetComponent<Animator>().SetBool("HasCharged", true);
    }
}
