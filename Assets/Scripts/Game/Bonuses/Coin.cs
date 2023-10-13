using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IDamageable
{
    public void GetDamage(int value)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Plane>() != null)
        {
            Wallet.Value++;
            Destroy(gameObject);

        }
    }
}
