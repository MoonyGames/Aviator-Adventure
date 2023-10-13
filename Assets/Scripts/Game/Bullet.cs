using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float SPEED = 20f;
    private Rigidbody2D _rb = default;
    private float _direction = default;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Death());
    }

    private void Start()
    {
        _direction = transform.rotation.eulerAngles.z == 0 ? 1 : -1;
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector2.right * _direction * SPEED;
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<IDamageable>();
        if (target != null)
        {
            target.GetDamage(1);
        }
        Destroy(gameObject);
    }
}
