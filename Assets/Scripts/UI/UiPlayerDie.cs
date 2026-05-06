using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPlayerDie : MonoBehaviour
{
    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnMainMenu;
    [SerializeField] private string _sceneMainMenu;
    [SerializeField] private SelectionsSO _selection;
    private HealthSystem _healthSystem;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _healthSystem = _selection.spawnedCar.GetComponent<HealthSystem>();

        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _btnReplay.onClick.AddListener(ReplayPressed);
        _btnMainMenu.onClick.AddListener(MainMenuPressed);
    }

    private void OnEnable()
    {
        _healthSystem.OnPlayerDie += OnPlayerDie_ShowText;
    }

    private void OnDisable()
    {
        _healthSystem.OnPlayerDie -= OnPlayerDie_ShowText;
    }

    private void OnDestroy()
    {
        _btnReplay.onClick.RemoveAllListeners();
        _btnMainMenu.onClick.RemoveAllListeners();
    }

    private void OnPlayerDie_ShowText()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void ReplayPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenuPressed()
    {
        SceneManager.LoadScene(_sceneMainMenu);
    }
}
