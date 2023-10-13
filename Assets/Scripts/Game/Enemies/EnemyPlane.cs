using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPlane : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed = default;
    private float _min = default;
    private float _max = default;
    private Plane _plane = default;
    private bool _readyToShot = true;

    [SerializeField]
    private Bullet _bulletPrefab = default;
    [SerializeField]
    private Transform _muzzleTransform = default;
    public Transform BulletParent = default;

    [SerializeField]
    private int _maxHealth = default;
    private int _health = default;
    [SerializeField]
    private GameObject _healthObject = default;
    [SerializeField]
    private Image _healthBar = default;


    public void Init(float min, float max, Transform bulletParent)
    {
        BulletParent = bulletParent;

        _plane = FindObjectOfType<Plane>();

        _min = min;
        _max = max;

        StartCoroutine(Moving());
    }

    private void Awake()
    {
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
            Destroy(gameObject);
        }
        _healthBar.fillAmount = (float)_health / _maxHealth;
    }


    private IEnumerator Moving()
    {
        var wait = new WaitForFixedUpdate();
        while (true)
        {
            var target = new Vector3(transform.localPosition.x, transform.InverseTransformPoint(0f, 0f, Random.Range(_max, _min)).z, transform.localPosition.z);
            while (transform.localPosition != target)
            {
                yield return wait;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, _speed * Time.deltaTime);

                if (Mathf.Abs(transform.position.y - _plane.transform.position.y) < 10f && _readyToShot)
                {
                    TakeShot();
                }
            }
        }
    }

    private void TakeShot()
    {
        _readyToShot = false;
        Instantiate(_bulletPrefab, _muzzleTransform.position, Quaternion.Euler(0f, 0f, 180f), BulletParent);
        StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        yield return new WaitForSeconds(.3f);
        _readyToShot = true;
    }
}
