using System;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private ActionButton _actionButton;
    [SerializeField] private ActionButton _dropButton;

    public void ShowActionButton() => _actionButton.Show();
    public void HideActionButton() => _actionButton.Hide();

    public void ShowDropButton() => _dropButton.Show();
    public void HideDropButton() => _dropButton.Hide();
}
