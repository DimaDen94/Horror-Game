using System;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public Animator Animator => _animator;

}