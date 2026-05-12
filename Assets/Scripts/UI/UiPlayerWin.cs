using UnityEngine;

public class UiPlayerWin : MonoBehaviour
{
    private CanvasGroup _canvas;

    private void Start()
    {
        ChangeCanvas(false);
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