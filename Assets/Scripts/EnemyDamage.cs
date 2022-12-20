using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float swordDamage;
    public void TakeDamageNear()
    {
        SendMessage("TakeDamage", swordDamage, SendMessageOptions.DontRequireReceiver);
    }
}
