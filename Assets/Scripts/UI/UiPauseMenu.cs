using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPauseMenu : MonoBehaviour
{
    private CanvasGroup _canvas;
    [SerializeField] private Button _btnBack;
    [SerializeField] private Button _btnMainMenu;
    [SerializeField] private string _sceneToLoad;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _btnBack.onClick.AddListener(BackClicked);
        _btnMainMenu.onClick.AddListener(MainMenuClicked);

        EnableCanvas(false);

        if (PauseGame.Instance != null)
            PauseGame.Instance.OnChangePause += OnPause_ShowSettings;
    }

    private void OnDestroy()
    {
        _btnBack.onClick.RemoveAllListeners();
        _btnMainMenu.onClick.RemoveAllListeners();

        if (PauseGame.Instance != null)
            PauseGame.Instance.OnChangePause -= OnPause_ShowSettings;
    }

    private void EnableCanvas(bool isOn)
    {
        _canvas.alpha = isOn ? 1f : 0f;
        _canvas.interactable = isOn;
        _canvas.blocksRaycasts = isOn;
    }

    private void BackClicked()
    {
        if (PauseGame.Instance != null)
            PauseGame.Instance.ChangePause();
    }

    private void MainMenuClicked()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    private void OnPause_ShowSettings(bool isPause)
    {
        EnableCanvas(isPause);
    }
}
