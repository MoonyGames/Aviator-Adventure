using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed = default;

    public void GetDamage(int value)
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.left * _speed;
    }
}
