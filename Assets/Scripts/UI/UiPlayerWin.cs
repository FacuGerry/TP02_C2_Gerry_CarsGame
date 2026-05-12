using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPlayerWin : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private string _sceneToLoad = "MainMenu";
    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ChangeCanvas(false);

        _btn.onClick.AddListener(GoToMainMenu);
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveAllListeners();
    }

    private void GoToMainMenu()
    {
        PauseGame.Instance.SetTime(false);
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void OnWin_SetCanvas()
    {
        ChangeCanvas(true);
    }

    private void ChangeCanvas(bool isOn)
    {
        _canvas.alpha = isOn ? 1f : 0f;
        _canvas.interactable = isOn;
        _canvas.blocksRaycasts = isOn;
    }

    private void SceneManager_activeSceneChanged(Scene scene, Scene scene2)
    {
        ChangeCanvas(false);
    }
}