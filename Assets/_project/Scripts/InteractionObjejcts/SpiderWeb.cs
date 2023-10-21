using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpiderWeb : InteractionObject
{
    private const string ProgressShaderkey = "_Progress";

    [SerializeField] private List<Renderer> _spiderWebs;
    [SerializeField] private Material _spiderMaterial;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _fire;

    private float _maxProgress = 1;
    private float _fadeDurotation = 2;


    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _spiderMaterial.SetFloat(ProgressShaderkey, _maxProgress);
    }

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is Torch && ((Torch)slot.Thing).IsBurning)
        {
            FadeWeb();
            _fire.SetActive(true);
        }
        return false;
    }

    [Button]
    private void FadeWeb()
    {
        _spiderMaterial.DOFloat(0, ProgressShaderkey, _fadeDurotation).OnComplete(DisableCollider);
    }

    private void DisableCollider()
    {
        _collider.enabled = false;
    }
}
