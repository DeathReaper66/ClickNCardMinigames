using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    [SerializeField] private float _animationDuration;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        Instance = this;

        if (_canvasGroup)
            _canvasGroup = GetComponentInChildren<CanvasGroup>();

        _canvasGroup.DOFade(0f, _animationDuration * 3f);
    }

    public void LoadScene(int sceneIndex)
    {
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1f, _animationDuration))
            .OnComplete(() => SceneManager.LoadScene(sceneIndex));
    }

    public void LoadScene(string sceneName)
    {
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1f, _animationDuration))
            .OnComplete(() => SceneManager.LoadScene(sceneName));
    }

    public void Restart()
    {
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1f, _animationDuration))
            .OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}
