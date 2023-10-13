using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plane : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Bullet _bulletPrefab = default;
    [HideInInspector]
    public Transform BulletParent = default;
    [SerializeField]
    private Transform _muzzleTransform = default;

    public Game Game = default;

    private const float MoveUpForce = 60f;
    private Rigidbody2D _rb = default;

    [SerializeField]
    private int _maxHealth = default;
    private int _health = default;
    [SerializeField]
    private GameObject _healthObject = default;
    [SerializeField]
    private Image _healthBar = default;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = _maxHealth;
        _healthObject.SetActive(false);
        _healthBar.fillAmount = (float)_health / _maxHealth;
    }

    public void GetDamage(int value)
    {
        _healthObject.SetActive(true);
        _health -= value;
        if (_health < 0)
        {
            _health = 0;
            Dead();
        }
        _healthBar.fillAmount = (float)_health / _maxHealth;
    }

    public void Dead()
    {
        Game.Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            Dead();
        }
        else if (collision.tag == "Win")
        {
            Game.Win();
        }
    }


    public void StartShooting()
    {
        StopShooting();
        StartCoroutine(_shooting = Shooting());
    }

    public void StopShooting()
    {
        if (_shooting != null)
        {
            StopCoroutine(_shooting);
        }
    }

    private void TakeShot()
    {
        Instantiate(_bulletPrefab, _muzzleTransform.position, Quaternion.identity, BulletParent);
    }

    public void StartMovingUp()
    {
        StopMovingUp();
        StartCoroutine(_movingUp = MovingUp());
    }

    public void StopMovingUp()
    {
        if (_movingUp != null)
        {
            StopCoroutine(_movingUp);
        }
        _rb.totalForce = Vector2.zero;
    }

    private IEnumerator _movingUp = default;
    private IEnumerator MovingUp()
    {
        while (true)
        {
            _rb.totalForce = Vector2.up * MoveUpForce;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator _shooting = default;
    private IEnumerator Shooting()
    {
        var wait = new WaitForSeconds(.1f);
        while (true)
        {
            TakeShot();
            yield return wait;
        }
    }
}
