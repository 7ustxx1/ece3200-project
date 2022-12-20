using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossedHit : MonoBehaviour
{
    public void CrossedHitEnd()
    {
        Destroy(gameObject);
    }
}
