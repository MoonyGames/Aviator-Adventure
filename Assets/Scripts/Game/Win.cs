using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField]
    private Text _rewardText = default;

    [SerializeField]
    private Button _replayButton = default;
    [SerializeField]
    private Button _menuButton = default;
    [SerializeField]
    private Button _nextButton = default;

    [SerializeField]
    private GameObject _game = default;
    [SerializeField]
    private GameObject _menu = default;


    private void Awake()
    {
        _replayButton.onClick.AddListener(Replay);
        _menuButton.onClick.AddListener(Menu);
        _nextButton.onClick.AddListener(Next);
    }

    private void OnEnable()
    {
        var reward = (Levels.Current + 1) * 10;
        Wallet.Value += reward;
        _rewardText.text = $"+{reward} coins";

        if (Levels.Current == Levels.Max)
        {
            Levels.Max++;
        }
    }


    private void Replay()
    {
        _game.SetActive(false);
        _game.SetActive(true);
    }

    private void Menu()
    {
        _game.SetActive(false);
        _menu.SetActive(true);
    }

    private void Next()
    {
        Levels.Current++;

        _game.SetActive(false);
        _game.SetActive(true);
    }
}
