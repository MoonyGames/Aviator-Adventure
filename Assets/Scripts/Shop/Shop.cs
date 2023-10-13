using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class Product
{
    public int Id;

    public Sprite PilotSprite;
    public Sprite PlaneSprite;
    public string PilotName;
    public string PlaneName;

    public int Speed;
    public int Armor;   
    public int Damage;

    public int Price;
    public bool IsBought
    {
        get => PlayerPrefs.GetInt($"Product{Id}", Price == 0 ? 1 : 0) != 0;
        set => PlayerPrefs.SetInt($"Product{Id}", value ? 1 : 0);
    }

    public static int GetCurrentId()
    {
        return PlayerPrefs.GetInt($"Current", 0);
    }
    public static void SetCurrentId(int value)
    {
        PlayerPrefs.SetInt($"Current", value);
        OnSetCurrentId.Invoke(value);
    }

    public static UnityEvent<int> OnSetCurrentId = new UnityEvent<int>();//value
}

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Image _pilotImage = default;
    [SerializeField]
    private Image _planeImage = default;
    [SerializeField]
    private Text _pilotNameText = default;
    [SerializeField]
    private Text _planeNameText = default;

    [SerializeField]
    private Image[] _speedStars = default;
    [SerializeField]
    private Image[] _armorStars = default;
    [SerializeField]
    private Image[] _damageStars = default;

    [SerializeField]
    private Button _left = default;
    [SerializeField]
    private Button _right = default;

    [SerializeField]
    private Button _selectButton = default;
    [SerializeField]
    private Button _buyButton = default;
    [SerializeField]
    private GameObject _selected = default;
    [SerializeField]
    private Text _priceText = default;

    [SerializeField]
    private Sprite _onStarSprite = default;
    [SerializeField]
    private Sprite _offStarSprite = default;

    [SerializeField]
    private Product[] _products = default;
    private int _currentProductId = default;

    private void Awake()
    {
        _left.onClick.AddListener(() => { Set(_currentProductId - 1); });
        _right.onClick.AddListener(() => { Set(_currentProductId + 1); });

        _buyButton.onClick.AddListener(Buy);
        _selectButton.onClick.AddListener(Select);
    }

    private void OnEnable()
    {
        Set(Product.GetCurrentId());
    }


    private void Set(int id)
    {
        _currentProductId = id;

        var product = _products[id];

        _pilotImage.sprite = product.PilotSprite;
        _planeImage.sprite = product.PlaneSprite;
        _pilotNameText.text = product.PilotName;
        _planeNameText.text = product.PlaneName;
        _priceText.text = $"$<color=white>{product.Price}</color>";

        for (int i = 0; i < _speedStars.Length; i++)
        {
            _speedStars[i].sprite = product.Speed > i ? _onStarSprite : _offStarSprite;
        }
        for (int i = 0; i < _armorStars.Length; i++)
        {
            _armorStars[i].sprite = product.Armor > i ? _onStarSprite : _offStarSprite;
        }
        for (int i = 0; i < _damageStars.Length; i++)
        {
            _damageStars[i].sprite = product.Damage > i ? _onStarSprite : _offStarSprite;
        }

        int state = !product.IsBought ? 0 : (_products[_currentProductId].Id == Product.GetCurrentId() ? 2 : 1);
        _buyButton.gameObject.SetActive(state == 0);
        _selectButton.gameObject.SetActive(state == 1);
        _selected.SetActive(state == 2);

        _left.interactable = id > 0;
        _right.interactable = id < _products.Length - 1;
        _left.gameObject.SetActive(id > 0);
        _right.gameObject.SetActive(id < _products.Length - 1);
    }


    private void Buy()
    {
        var product = _products[_currentProductId];
        if (Wallet.Value >= product.Price)
        {
            Wallet.Value -= product.Price;
            product.IsBought = true;
            Select();
        }
    }

    private void Select()
    {
        Product.SetCurrentId(_currentProductId);
        Set(_currentProductId);
    }
}
