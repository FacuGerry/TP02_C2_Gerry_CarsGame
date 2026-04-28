using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private StatsDataSO _npcData;
    [SerializeField] private GameDataSO _gameData;
    [SerializeField] private string _sceneToLoad = "Gameplay";
    [SerializeField] private List<NpcController> _npcList = new List<NpcController>();

    public int enemies = 0;

    private IEnumerator _corroutineCreating;
    private void Start()
    {
        if (_corroutineCreating != null)
            StopCoroutine(_corroutineCreating);

        _corroutineCreating = CreatingEnemies();
        StartCoroutine(_corroutineCreating);
    }

    private void OnEnable()
    {
        NpcHealthSystem.OnNpcDie += OnNpcDie_CheckForWin;
    }

    private void OnDisable()
    {
        NpcHealthSystem.OnNpcDie -= OnNpcDie_CheckForWin;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator CreatingEnemies()
    {
        int numberOfEnemies = Random.Range(_gameData.minEnemies, (_gameData.maxEnemies + 1));

        while (enemies < numberOfEnemies)
        {
            int randomEnemy = Random.Range(0, _npcList.Count);
            if (!_npcList[randomEnemy].isEnemy)
            {
                _npcList[randomEnemy].isEnemy = true;
                enemies++;
            }
            yield return null;
        }
        yield return null;
    }

    private void OnNpcDie_CheckForWin(bool isEnemy)
    {
        if (isEnemy)
            enemies--;

        if (enemies <= 0)
            BuffEnemiesAndReload();
    }

    private void BuffEnemiesAndReload()
    {
        _npcData.level++;

        _gameData.minEnemies++;
        _gameData.maxEnemies++;

        if (_gameData.minEnemies > _npcList.Count)
            _gameData.minEnemies = _npcList.Count;

        if (_gameData.maxEnemies > _npcList.Count)
            _gameData.maxEnemies = _npcList.Count;

        SceneManager.LoadScene(_sceneToLoad);
    }
}