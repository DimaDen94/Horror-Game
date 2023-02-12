using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private ActionButton _actionButton;

    public void ShowActionButton() => _actionButton.Show();
    public void HideActionButton() => _actionButton.Hide();
}
