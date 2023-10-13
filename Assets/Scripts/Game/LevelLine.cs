using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLine : MonoBehaviour
{
    [SerializeField]
    private float _speed = default;
    private IEnumerator _moving = default;
    private Vector3? _startPosition = null;


    private void Start()
    {
        _startPosition = transform.position;
    }

    public void StartLevel()
    {
        StopLevel();
        StartCoroutine(_moving = Moving());
    }

    public void StopLevel()
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
            _moving = null;
        }
    }

    private IEnumerator Moving()
    {
        if (_startPosition == null)
        {
            _startPosition = transform.position;
        }
        transform.position = _startPosition.Value;
        while (true)
        {
            yield return null;
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
    }
}
