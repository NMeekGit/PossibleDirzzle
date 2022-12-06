using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    public GameObject _uiPanel;
    public TextMeshProUGUI _promptText;

    // Start is called before the first frame update
    void Start()
    {
        _uiPanel.SetActive(false);
    }

    public bool IsDisplayed = false;

    public void SetUp(string promptText) {
        _promptText.text = promptText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close() {
        IsDisplayed = false;
        _uiPanel.SetActive(false);
    }
}
