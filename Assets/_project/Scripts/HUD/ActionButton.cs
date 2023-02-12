using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);
    }
}
