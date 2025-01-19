using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberOfClicksTMP;
    [SerializeField] private int _valuePerCkick;
    public static readonly string ClickKey = "Clicks";
    private int _totalClicks;
    private Button _clickButton;

    private void Awake()
    {
        _totalClicks = PlayerPrefs.GetInt(ClickKey, 0);
        _numberOfClicksTMP.text = _totalClicks.ToString();

        _clickButton = GetComponent<Button>();
        _clickButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _totalClicks += _valuePerCkick;
        _numberOfClicksTMP.text = _totalClicks.ToString();
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(ClickKey, _totalClicks);
    }

    public void RemoveProgress() 
    {
        PlayerPrefs.DeleteKey(ClickKey);
    }
}
