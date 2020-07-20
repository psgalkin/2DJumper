using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusArea : MonoBehaviour
{
    [SerializeField] private GameObject _textArea;
    [SerializeField] private float _deltaFields;

    struct TypeTimeField
    {
        public BoostType Type; public int Time; public GameObject Field;
        
        public TypeTimeField(BoostType type, int time, GameObject field)
        {
            Type = type; Time = time; Field = field;
        }
    }

    private static List<TypeTimeField> _data = new List<TypeTimeField>();

    public void AddField(BoostType boostType, int time)
    {
        foreach (var data in _data)
        {
            if (data.Type == boostType)
            {
                SetBoostTiming(boostType, time);
                return;
            }
        }

        GameObject field = Instantiate(_textArea, GetComponentInChildren<Canvas>().transform);

        field.transform.position = new Vector3(_textArea.transform.position.x, _textArea.transform.position.y - _deltaFields * _data.Count + 1);
        field.GetComponent<TMP_Text>().text = GetText(boostType, time);

        _data.Add(new TypeTimeField(boostType, time, field));
    }

    private string GetText(BoostType boostType, int time)
    {
        string text = "";

        switch (boostType)
        {
            case BoostType.Jetpack:
                text = $"Jetpack: {time}";
                break;
            case BoostType.Magnet:
                text = $"Magnet: {time}";
                break;
            case BoostType.Armor:
                text = $"Armor: {time}";
                break;
            case BoostType.WeaponLaser:
                text = $"Laser";
                break;
            case BoostType.WeaponRocket:
                text = $"Rocket";
                break;
            default:
                break;
        }

        return text;
    }

    public void SetBoostTiming(BoostType boostType, int time)
    {
        int elementNum = -1;
        for (int i = 0; i < _data.Count; ++i)
        {
            if (boostType == _data[i].Type) {
                elementNum = i;
            }
        }
        if (elementNum == -1) { return; }

        _data[elementNum].Field.GetComponent<TMP_Text>().text = GetText(boostType, time);
        
    }

    public void RemoveField(BoostType boostType)
    {
        bool isRemoved = false;
        int removeNum = -1;
        for ( int i = 0; i < _data.Count; ++i)
        {
            if (isRemoved) 
            {
                _data[i].Field.transform.position = new Vector3(
                     _data[i].Field.transform.position.x, _data[i].Field.transform.position.y + _deltaFields);
            }

            if (_data[i].Type == boostType)
            {
                Destroy(_data[i].Field);
                _data.RemoveAt(i);
                isRemoved = true;
                i--;
            }
        }

    }
}
