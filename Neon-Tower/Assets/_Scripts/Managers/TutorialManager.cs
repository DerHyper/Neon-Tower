using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private bool _deactivateTutorial = false;
    [SerializeField]
    private List<GameObject> _tutorialPanels;
    private int _currentPanelNumber;

    private void Start()
    {
        foreach (var panel in _tutorialPanels)
        {
            panel.SetActive(false);
        }
        if (!_deactivateTutorial)
        {
            StartTutorial();
        }
    }

    public void Next()
    {
        _tutorialPanels[_currentPanelNumber].SetActive(false);
        _currentPanelNumber++;
        if (_currentPanelNumber >= _tutorialPanels.Count)
        {
            return;
        }
        _tutorialPanels[_currentPanelNumber].SetActive(true);
    }

    public void Skip()
    {
        _tutorialPanels[_currentPanelNumber].SetActive(false);
    }

    private void StartTutorial()
    {
        _currentPanelNumber = 0;
        _tutorialPanels[0].SetActive(true);
    }
}
