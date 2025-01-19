using UnityEngine;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;
    [SerializeField] private string _sceneName;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();

        try
        {
            if (_sceneName == "")
                _button.onClick.AddListener(() => SceneTransition.Instance.LoadScene(_sceneIndex));
            else
                _button.onClick.AddListener(() => SceneTransition.Instance.LoadScene(_sceneName));  
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
}
