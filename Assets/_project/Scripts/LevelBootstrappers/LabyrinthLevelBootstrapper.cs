using System;
using System.Collections;
using UnityEngine;

public class LabyrinthLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private Chest _chest;
    [SerializeField] private AudioSource _footSteps;

    [SerializeField] private RedChest _chestRed;
    [SerializeField] private AudioSource _laught;

    [SerializeField] private BlueChest _chestBlue;
    [SerializeField] private Screamer _screamer;

    private new void Start()
    {
        base.Start();
        _chest.Construct(_toastService);
        _chestRed.Construct(_toastService);
        _chestBlue.Construct(_toastService);
        _chest.ChestOpened += OnChestOpened;
        _chestRed.ChestOpened += OnRedChestOpened;
        _chestBlue.ChestOpened += OnBlueChestOpened;
    }

    private void OnChestOpened() => StartCoroutine(PlayFootSteps());

    private IEnumerator PlayFootSteps()
    {
        yield return new WaitForSeconds(1);
        _footSteps.Play();
    }

    private void OnRedChestOpened() => StartCoroutine(PlayLaught());

    private IEnumerator PlayLaught()
    {
        yield return new WaitForSeconds(1);
        _laught.Play();

    }

    private void OnBlueChestOpened()
    {
        _screamer.gameObject.SetActive(true);

        _hero.LookAt(_screamer.transform);


        StartCoroutine(PlayScreamer());
    }

    private IEnumerator PlayScreamer()
    {
        yield return new WaitForSeconds(1f);
        _audioService.PlayAudio(SoundEnum.Screamer1);
        _screamer.JumpTo(_hero.transform);
        yield return new WaitForSeconds(0.25f);
        _uiFactory.CreateBlackout();
        _uiFactory.Blackout.Daybreak();
    }
}
