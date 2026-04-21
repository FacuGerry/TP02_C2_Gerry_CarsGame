using System.Collections;
using TMPro;
using UnityEngine;

public class UiLoseLife : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _life;
    [SerializeField] private int _numberOfChanges;

    private IEnumerator _coroutine;

    private void Start()
    {
        _life.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CollisionController.OnPlayerCrashed += ShowText;
    }

    private void OnDisable()
    {
        CollisionController.OnPlayerCrashed -= ShowText;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator ShowingText()
    {
        _life.gameObject.SetActive(true);
        int changes = 0;
        while (changes < _numberOfChanges)
        {
            float rand = Random.value;
            _life.color = Color.HSVToRGB(rand, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
            changes++;
            yield return null;
        }
        _life.gameObject.SetActive(false);
        yield return null;
    }

    private void ShowText(int life)
    {
        _life.text = "-" + life.ToString("0");

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = ShowingText();
        StartCoroutine(_coroutine);
    }
}
