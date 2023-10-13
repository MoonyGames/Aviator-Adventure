using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG : MonoBehaviour
{
    [SerializeField]
    private float _speed = default;
    [SerializeField]
    private Image[] _images = default;
    private int _firstImage = 0;
    private IEnumerator _moving = null;

    [SerializeField]
    private Sprite[] _locations = default;

    private void Start()
    {
        SetBGs();
    }


    public void SetBGs()
    {
        SetBGs(Levels.Current % _locations.Length);
    }
    private void SetBGs(int id)
    {
        foreach (var image in _images)
        {
            image.sprite = _locations[id];
        }
    }


    public void StartMoving()
    {
        StopMoving();
        StartCoroutine(_moving = Moving());
    }

    public void StopMoving()
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
            _moving = null;
        }
    }


    private IEnumerator Moving()
    {
        while (true)
        {
            yield return null;
            _images[_firstImage].transform.position += Vector3.left * Time.deltaTime * _speed;
            if (_images[_firstImage].transform.localPosition.x < -30000)
            {
                Swap();
            }
        }
    }

    private void Swap()
    {
        int next = _firstImage >= _images.Length - 1 ? 0 : _firstImage + 1;
        int last = _firstImage == 0 ? _images.Length - 1 : _firstImage - 1;
        _images[next].transform.SetParent(transform);
        _images[_firstImage].rectTransform.anchorMin = new Vector2(1, _images[_firstImage].rectTransform.anchorMin.y);
        _images[_firstImage].rectTransform.anchorMax = new Vector2(1, _images[_firstImage].rectTransform.anchorMax.y);
        _images[_firstImage].transform.SetParent(_images[last].transform);
        _images[_firstImage].transform.localPosition = Vector3.right * _images[_firstImage].rectTransform.rect.width;
        _firstImage = next;
    }
}
