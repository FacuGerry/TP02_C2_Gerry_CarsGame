using UnityEngine;

public class WinController : MonoBehaviour
{
    public static WinController Instance;

    [Header("Car")]
    [SerializeField] private SelectionsSO _selection;

    [Header("UI")]
    [SerializeField] private UiPlayerWin _uiWin;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void CheckWin()
    {
        if (_selection.gameMode == GameModes.Competitive)
        {
            int deadEnemies = 0;
            foreach (NpcController enemy in MyPoolManager.Instance.GetPool<NpcController>())
                if (!enemy.IsAlive && enemy.isEnemy)
                    deadEnemies++;

            if (deadEnemies == MyPoolManager.Instance.GetPoolSize<NpcController>())
                Win();
        }
    }

    private void Win()
    {
        PauseGame.Instance.ChangePause();
        _uiWin.OnWin_SetCanvas();
    }
}