using System;
using UnityEngine;

public class LightPoint : MonoBehaviour
{
    private GameObject _light;

    public void SetLight(GameObject light)
    {
        _light = light;
    }
}