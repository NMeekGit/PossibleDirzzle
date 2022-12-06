using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopUpUI : MonoBehaviour
{
    public GameObject _uiPanel;
    public TextMeshProUGUI _popupText;
    public Image _itemImage;

    // Start is called before the first frame update
    void Start()
    {
        _uiPanel.SetActive(false);
    }

    public bool IsDisplayed = false;

    public void SetUp(string promptText, Sprite itemSprite) {
        _popupText.text = promptText;
        _uiPanel.SetActive(true);
        _itemImage.sprite = itemSprite;
        IsDisplayed = true;
    }

    public void Close() {
        IsDisplayed = false;
        _uiPanel.SetActive(false);
    }
}
