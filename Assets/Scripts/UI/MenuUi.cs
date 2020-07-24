using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUi : MonoBehaviour
{
    [SerializeField] private GameObject _startPage;
    [SerializeField] private GameObject _characterPage;
    [SerializeField] private GameObject _levelsPage;

    [SerializeField] private Transform _levelButtonPosition;
    [SerializeField] private GameObject _levelButton;
    [SerializeField] private LevelData[] _levelData;

    private void Start()
    {
        _startPage.SetActive(true);
        _characterPage.SetActive(false);
        _levelsPage.SetActive(false);

        for (int i = 0; i < _levelData.Count(); ++i)
        {
            int num = i;

            GameObject newButton = GameObject.Instantiate(_levelButton, _levelButtonPosition);
            newButton.transform.position = 
                new Vector3(_levelButtonPosition.position.x, _levelButtonPosition.position.y - 180.0f * i);
            newButton.GetComponentInChildren<TMP_Text>().text = $"Level {i + 1}";

            newButton.GetComponentInChildren<Button>().onClick.AddListener(() => LoadLevel(num));
        }
    }

    public void StartLevelsPage()
    {
        _startPage.SetActive(false);
        _levelsPage.SetActive(true);
    }

    private void LoadLevel(int num)
    {
        LevelLoader.Data = _levelData[num];
        SceneManager.LoadScene(1);
    }

    public void StartCharacterPage()
    {
        _startPage.SetActive(false);
        _characterPage.SetActive(true);
    }

    public void ToMainPage()
    {
        _levelsPage.SetActive(false);
        _characterPage.SetActive(false);
        _startPage.SetActive(true);
    }
}
