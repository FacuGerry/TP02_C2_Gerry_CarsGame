using UnityEngine;

[CreateAssetMenu(fileName = "KeyBindings", menuName = "GameSettings/KeyBindings")]

public class KeyBindingsSO : ScriptableObject
{
    [Header("Restart Car")]
    public KeyCode restartCar;

    [Header("Camera Control")]
    public KeyCode changePOV;

    [Header("Pause")]
    public KeyCode pause;
    public KeyCode pause2;
    public KeyCode pause3;

    [Header("Shoot")]
    public KeyCode shoot;
    public KeyCode secondShoot;
    public KeyCode showCheat;
}
