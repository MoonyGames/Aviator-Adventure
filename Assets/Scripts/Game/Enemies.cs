using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private Transform _xPosition = default;

    [SerializeField]
    private Transform _bonusMaxPosition = default;
    [SerializeField]
    private Transform _bonusMinPosition = default;
    [SerializeField]
    private GameObject[] _bonusPrefabs = default;

    [SerializeField]
    private Transform _topMaxPosition = default;
    [SerializeField]
    private Transform _topMinPosition = default;
    [SerializeField]
    private Transform _bottomMaxPosition = default;
    [SerializeField]
    private Transform _bottomMinPosition = default;
    [SerializeField]
    private GameObject[] _staticPrefabs = default;
    
    [SerializeField]
    private EnemyPlane[] _planePrefabs = default;
    [SerializeField]
    private Transform _bulletParent = default;

    [SerializeField]
    private Rocket[] _rocketPrefabs = default;

    [SerializeField]
    private Transform _finish = default;


    public void Clean()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Spawn()
    {
        if (_finish.position.x < _xPosition.position.x) { return; }
        switch (Random.Range(0, 5))
        //switch(4)
        {
            case 0:
                SpawnBonus();
                break;
            case 1:
                SpawnStatic();
                break;
            case 2:
                SpawnPlane();
                break;
            case 3:
                SpawnBonus();
                break;
            case 4:
                SpawnRocket();
                break;
        }
    }

    public void SpawnBonus()
    {
        var prefab = _bonusPrefabs[Random.Range(0, 5) == 0 ? 1 : 0];
        Instantiate(
            prefab,
            new Vector3(_xPosition.position.x, Random.Range(_bonusMaxPosition.position.y, _bonusMinPosition.position.y), _xPosition.position.z),
            Quaternion.identity,
            transform);
    }

    public void SpawnStatic()
    {
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(
                _staticPrefabs[0],
                new Vector3(_xPosition.position.x, Random.Range(_topMinPosition.position.y, _topMaxPosition.position.y), _xPosition.position.z),
                Quaternion.identity,
                transform);
        }
        else
        {
            Instantiate(
                _staticPrefabs[Random.Range(1, 3)],
                new Vector3(_xPosition.position.x, Random.Range(_bottomMinPosition.position.y, _bottomMaxPosition.position.y), _xPosition.position.z),
                Quaternion.identity,
                transform);
        }
    }

    public void SpawnPlane()
    {
        var plane = Instantiate(
            _planePrefabs[Random.Range(0, _planePrefabs.Length)],
            new Vector3(_xPosition.position.x, Random.Range(_bonusMaxPosition.position.y, _bonusMinPosition.position.y), _xPosition.position.z),
            Quaternion.identity,
            transform);

        plane.Init(_bonusMaxPosition.position.y, _bonusMinPosition.position.y, _bulletParent);
    }

    public void SpawnRocket()
    {
        Instantiate(
            _rocketPrefabs[Random.Range(0, _rocketPrefabs.Length)],
            new Vector3(_xPosition.position.x, FindObjectOfType<Plane>().transform.position.y, _xPosition.position.z),
            Quaternion.identity,
            transform);
    }
}
