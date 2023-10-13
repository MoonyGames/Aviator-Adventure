using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private Sprite _close = default;
    [SerializeField]
    private Sprite _open = default;
    [SerializeField]
    private Text _rewardText = default;

    [SerializeField]
    private GameObject page = default;

    private static bool isOpen = false;

    private void Awake()
    {
        GetComponent<Image>().sprite = _close;
        GetComponent<Button>().onClick.AddListener(Open);
    }

    private void Open()
    {
        if (isOpen) { return; }
        isOpen = true;
        DailyBonus.GetBonus();

        GetComponent<Image>().sprite = _open;

        int reward = Random.Range(20, 100);
        Wallet.Value += reward;
        _rewardText.enabled = true;
        _rewardText.text = $"+{reward}";

        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        yield return new WaitForSecondsRealtime(1f);
        page.SetActive(false);
    }
}
