using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Plane[] _planePrefabs = default;
    [SerializeField]
    private Transform _planeParent = default;
    private Plane _plane = default;

    [SerializeField]
    private Enemies _enemies = default;
    [SerializeField]
    private Transform _bulletParent = default;

    [SerializeField]
    private Control[] _controls = default;

    [SerializeField]
    private Button _pauseOnButton = default;
    [SerializeField]
    private Button _pauseOffButton = default;

    [SerializeField]
    private GameObject _lose = default;
    [SerializeField]
    private GameObject _win = default;

    [SerializeField]
    private BG _bg = default;
    [SerializeField]
    private LevelLine _levelLine = default;

    [SerializeField]
    private GameObject _tutorial = default;

    private IEnumerator _enemiesSpawning = default;


    private void Awake()
    {
        _pauseOnButton.onClick.AddListener(PauseOn);
        _pauseOffButton.onClick.AddListener(PauseOff);
    }

    private void OnEnable()
    {
        StartGame();
    }

    private void OnDisable()
    {
        FinishGame();
    }

    private void StartGame()
    {
        _bg.SetBGs();
        _bg.StartMoving();

        _levelLine.StartLevel();
        PauseOff();
        SetPlane();

        StartSpawning();

        if (PlayerPrefs.GetInt("FirstGame", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstGame", 0);
            PauseOn();
            _tutorial.SetActive(true);
        }
    }

    private void FinishGame()
    {
        StopSpawning();

        _bg.StopMoving();

        _levelLine.StopLevel();
        PauseOff();
        for (int i = 0; i < _bulletParent.childCount; i++)
        {
            Destroy(_bulletParent.GetChild(i).gameObject);
        }
        _enemies.Clean();
    }

    private void SetPlane()
    {
        if (_plane != null)
        {
            Destroy(_plane.gameObject);
        }

        _plane = Instantiate(_planePrefabs[Product.GetCurrentId()], _planeParent.position, Quaternion.identity, _planeParent);
        _plane.BulletParent = _bulletParent;
        _plane.Game = this;
        foreach (var control in _controls)
        {
            control.Plane = _plane;
        }
    }


    private void StartSpawning()
    {
        StopSpawning();
        StartCoroutine(_enemiesSpawning = EnemiesSpawning());
    }
    private void StopSpawning()
    {
        if (_enemiesSpawning != null)
        {
            StopCoroutine(_enemiesSpawning);
            _enemiesSpawning = null;
        }
    }
    private IEnumerator EnemiesSpawning()
    {
        var wait = new WaitForSeconds(2f);
        while (true)
        {
            yield return wait;
            _enemies.Spawn();
        }
    }


    public void Win()
    {
        PauseOn();
        _win.SetActive(true);
    }

    public void Lose()
    {
        PauseOn();
        _lose.SetActive(true);
    }

    public void PauseOn()
    {
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
    }
}
