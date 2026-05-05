using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButtonHoverSFXEvent : MonoBehaviour, IPointerEnterHandler
{
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void Start()
    {
        _btn.onClick.AddListener(ButtonClicked);
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        return;
        SfxManager.Instance.OnButtonHover_PlayClip();
    }

    public void ButtonClicked()
    {
        return;
        SfxManager.Instance.OnButtonClick_PlayClip();
    }
}