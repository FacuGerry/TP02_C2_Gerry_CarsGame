using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPlayerWin : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private string _sceneToLoad = "MainMenu";
    private CanvasGroup _canvas;

    private void Start()
    {
        ChangeCanvas(false);

        _btn.onClick.AddListener(GoToMainMenu);
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveAllListeners();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void OnWin_SetCanvas()
    {
        ChangeCanvas(true);
    }

    private void ChangeCanvas(bool isOn)
    {
        _canvas.alpha = isOn ? 1 : 0;
        _canvas.interactable = isOn;
        _canvas.blocksRaycasts = isOn;
    }
}