using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPauseMenu : MonoBehaviour
{
    public static event Action OnBackClicked;

    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private Button _btnBack;
    [SerializeField] private Button _btnMainMenu;
    [SerializeField] private string _sceneToLoad;

    private void Start()
    {
        _btnBack.onClick.AddListener(BackClicked);
        _btnMainMenu.onClick.AddListener(MainMenuClicked);
    }

    private void OnEnable()
    {
        PauseGame.OnPause += OnPause_ShowSettings;
    }

    private void OnDisable()
    {
        PauseGame.OnPause -= OnPause_ShowSettings;
    }

    private void OnDestroy()
    {
        _btnBack.onClick.RemoveAllListeners();
        _btnMainMenu.onClick.RemoveAllListeners();
    }

    private void EnableCanvas(bool isOn)
    {
        _canvas.alpha = isOn ? 1f : 0f;
        _canvas.interactable = isOn;
        _canvas.blocksRaycasts = isOn;
    }

    private void BackClicked()
    {
        OnBackClicked?.Invoke();
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
