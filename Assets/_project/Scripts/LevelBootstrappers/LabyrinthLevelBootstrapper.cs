using System;
using System.Collections;
using UnityEngine;

public class LabyrinthLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private ChestKey _chestKey;
    [SerializeField] private RedChestKey _redChestKey;
    [SerializeField] private BlueChestKye _blueChestKey;
    [SerializeField] private ExitKey _exitKey;

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

        _chestKey.Lifted += OnFirstChestKeyLifted;

        _chest.ChestOpened += OnFirstChestOpened;

        _redChestKey.Lifted += OnRedKeyLifted;

        _chestRed.ChestOpened += OnRedChestOpened;

        _blueChestKey.Lifted += OnBlueKeyLifted;

        _chestBlue.ChestOpened += OnBlueChestOpened;

        _exitKey.Lifted += OnExitKeyLifted;
    }


    private void OnFirstChestKeyLifted() => _analyticService.LabyrinthLevelFirstKeyLifted();

    private void OnFirstChestOpened()
    {
        StartCoroutine(PlayFootSteps());
        _analyticService.LabyrinthLevelFirstChestOpened();
    }

    private void OnRedKeyLifted() => _analyticService.LabyrinthLevelRedKeyLifted();


    private void OnRedChestOpened()
    {
        StartCoroutine(PlayLaught());
        _analyticService.LabyrinthLevelRedChestOpened();
    }

    private void OnBlueKeyLifted() => _analyticService.LabyrinthLevelBlueKeyLifted();

    private void OnBlueChestOpened()
    {
        _screamer.gameObject.SetActive(true);

        _hero.LookAt(_screamer.transform);


        StartCoroutine(PlayScreamer());
    }

    private void OnExitKeyLifted() => _analyticService.LabyrinthLevelExitKeyLifted();


    private IEnumerator PlayFootSteps()
    {
        yield return new WaitForSeconds(1);
        _footSteps.Play();
    }

    private IEnumerator PlayLaught()
    {
        yield return new WaitForSeconds(1);
        _laught.Play();

    }

    private IEnumerator PlayScreamer()
    {
        yield return new WaitForSeconds(1f);
        _audioService.PlayAudio(SoundEnum.Screamer);
        _screamer.JumpTo(_hero.transform);
        yield return new WaitForSeconds(0.25f);
        _uiFactory.CreateBlackout();
        _uiFactory.Blackout.Daybreak();
    }
}
