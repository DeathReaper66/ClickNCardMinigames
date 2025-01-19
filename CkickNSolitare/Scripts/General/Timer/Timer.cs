using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private string TimerKey;
    [SerializeField] private bool _eraseProgressAfterStart = false;

    private TMP_Text _TMP;
    private float _timer = 0f;

    private void Awake()
    {
        _TMP = GetComponent<TMP_Text>();
        _timer = PlayerPrefs.GetFloat(TimerKey, 0);
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        float minutes = Mathf.FloorToInt(_timer / 60);
        float seconds = Mathf.FloorToInt(_timer % 60);
        _TMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SaveTimer()
    {
        PlayerPrefs.SetFloat(TimerKey, _timer);
    }

    public void RemoveTimer()
    {
        PlayerPrefs.DeleteKey(TimerKey);
    }
}
