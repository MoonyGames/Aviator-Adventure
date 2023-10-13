using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsViewer : MonoBehaviour
{
    [SerializeField]
    private Text _text = default;


    private void OnEnable()
    {
        SetText(Wallet.Value);
        Wallet.OnSet.AddListener(SetText);
    }

    private void OnDisable()
    {
        Wallet.OnSet.RemoveListener(SetText);
    }


    private void SetText(int value)
    {
        _text.text = value.ToString();
    }
}
