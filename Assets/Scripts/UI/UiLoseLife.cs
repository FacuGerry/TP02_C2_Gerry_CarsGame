using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiLoseLife : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _life = new List<TextMeshProUGUI>();
    [SerializeField] private int _numberOfChanges;

    private Dictionary<TextMeshProUGUI, Coroutine> _coroutines = new Dictionary<TextMeshProUGUI, Coroutine>();

    private void Start()
    {
        foreach (TextMeshProUGUI text in _life)
            text.gameObject.SetActive(false);
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

    private IEnumerator ShowingText(TextMeshProUGUI life)
    {
        life.gameObject.SetActive(true);
        int changes = 0;
        while (changes < _numberOfChanges)
        {
            float rand = Random.value;
            life.color = Color.HSVToRGB(rand, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
            changes++;
            yield return null;
        }
        life.gameObject.SetActive(false);
        yield return null;
    }

    private void ShowText(int life)
    {
        foreach (TextMeshProUGUI text in _life)
        {
            if (!text.gameObject.activeInHierarchy)
            {
                text.text = "-" + life.ToString("0");

                if (_coroutines.TryGetValue(text, out Coroutine currentCoroutine))
                    StopCoroutine(currentCoroutine);

                Coroutine newCoroutine = StartCoroutine(ShowingText(text));
                _coroutines[text] = newCoroutine;
                return;
            }
        }
    }
}
