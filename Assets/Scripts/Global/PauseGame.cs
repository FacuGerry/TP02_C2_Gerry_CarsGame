using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public static PauseGame Instance;
    public event Action<bool> OnChangePause;

    [SerializeField] private KeyBindingsSO _keys;
    [SerializeField] private string _mainMenuScene = "MainMenu";
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        IsPaused = false;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != _mainMenuScene &&
           (Input.GetKeyDown(_keys.pause) || Input.GetKeyDown(_keys.pause2) || Input.GetKeyDown(_keys.pause3)))
            ChangePause();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void ChangePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0.0f : 1.0f;
        OnChangePause?.Invoke(IsPaused);
    }

    public void SetTime(bool isZero)
    {
        IsPaused = isZero;
        Time.timeScale = IsPaused ? 0.0f : 1.0f;
    }
}