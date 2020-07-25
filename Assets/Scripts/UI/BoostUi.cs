using TMPro;
using UnityEngine;

class BoostUi : MonoBehaviour
{
    [SerializeField] private BoostType _type;

    [SerializeField] private CharacterData _characterData;
    [SerializeField] private UpBoostData _upBoostData;

    [SerializeField] private TMP_Text _coinText;

    [SerializeField] private TMP_Text _currentText;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private TMP_Text _coastText;

    
    [SerializeField] private GameObject _byingField;
    [SerializeField] private GameObject _stopByingField;
    
    [SerializeField] private TMP_Text _buyingFieldText;

    private void Start()
    {
        _byingField.SetActive(false);
        _stopByingField.SetActive(false);
        _coinText.text = $"Coins: {_characterData.CoinCount}";

        TMP_Text buyingText = _byingField.GetComponentInChildren<TMP_Text>();

        switch (_type)
        {
            case BoostType.Magnet:
                SetBoostString($"{_characterData.MagnetDuration}s", $"+{_upBoostData.UpMagnetVal}s", $"{_upBoostData.GetCoastDictionary()[BoostType.Magnet]} ©");
                buyingText.text = $"Increase the magnet duration for {_upBoostData.GetCoastDictionary()[BoostType.Magnet]} ©";
                break;
            case BoostType.Trampoline:
                SetBoostString($"x{_characterData.ForceJumpCoef}", $"+{_upBoostData.UpTrampolineVal}", $"{_upBoostData.GetCoastDictionary()[BoostType.Trampoline]} ©");
                buyingText.text = $"Increase the trampoline force for {_upBoostData.GetCoastDictionary()[BoostType.Trampoline]} ©";
                break;
            case BoostType.Jetpack:
                SetBoostString($"{_characterData.JetpackDuration}s", $"+{_upBoostData.UpJetpackVal}s", $"{_upBoostData.GetCoastDictionary()[BoostType.Jetpack]} ©");
                buyingText.text = $"Increase the jetpack duration for {_upBoostData.GetCoastDictionary()[BoostType.Jetpack]} ©";
                break;
            case BoostType.Armor:
                SetBoostString($"{_characterData.ArmorDuration}s", $"+{_upBoostData.UpArmorVal}s", $"{_upBoostData.GetCoastDictionary()[BoostType.Armor]} ©");
                buyingText.text = $"Increase the armor duration for {_upBoostData.GetCoastDictionary()[BoostType.Armor]} ©";
                break;
        }
    }
    
    private void SetBoostString(string current, string button, string coast)
    {
        _currentText.text = current;
        _buttonText.text = button;
        _coastText.text = coast;
    }

    public void UpBoost()
    {
        if (_characterData.CoinCount >= _upBoostData.GetCoastDictionary()[_type])
        {
            _byingField.SetActive(true);
        }
        else
        {
            
            _stopByingField.SetActive(true);
        }
    }

    public void Buy()
    {
        _characterData.CoinCount -= _upBoostData.GetCoastDictionary()[_type];
        switch(_type)
        {
            case BoostType.Magnet:
                _characterData.MagnetDuration += _upBoostData.UpMagnetVal;
                _currentText.text = $"{_characterData.MagnetDuration}s";
                break;
            case BoostType.Trampoline:
                _characterData.ForceJumpCoef += _upBoostData.UpTrampolineVal;
                _currentText.text = $"x{_characterData.ForceJumpCoef}";
                break;
            case BoostType.Jetpack:
                _characterData.JetpackDuration += _upBoostData.UpJetpackVal;
                _currentText.text = $"{_characterData.JetpackDuration}s";
                break;
            case BoostType.Armor:
                _characterData.ArmorDuration += _upBoostData.UpArmorVal;
                _currentText.text = $"{_characterData.ArmorDuration}s";
                break;
        }

        _coinText.text = $"Coins: {_characterData.CoinCount}";
        _byingField.SetActive(false);
    }

    public void ExitPages()
    {
        _byingField.SetActive(false);
        _stopByingField.SetActive(false);
    }
}
