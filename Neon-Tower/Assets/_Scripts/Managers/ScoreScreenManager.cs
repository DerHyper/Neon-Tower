using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreLabel;
    [SerializeField]
    private List<GameObject> _scorePanels;
    private int _currentPanelNumber;
    public static ScoreScreenManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (var panel in _scorePanels)
        {
            panel.SetActive(false);
        }
    }

    public void Next()
    {
        _scorePanels[_currentPanelNumber].SetActive(false);
        _currentPanelNumber++;
        if (_currentPanelNumber >= _scorePanels.Count)
        {
            return;
        }
        _scorePanels[_currentPanelNumber].SetActive(true);
    }

    public void Skip()
    {
        _scorePanels[_currentPanelNumber].SetActive(false);
    }

    public void Reload()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void ShowScoreScreen()
    {
        _currentPanelNumber = 0;
        _scorePanels[0].SetActive(true);
        _scoreLabel.text = ((int)ScoreManager.Instance.Score).ToString();
    }
}
