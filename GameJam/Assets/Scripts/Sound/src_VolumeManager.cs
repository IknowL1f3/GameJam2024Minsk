using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class src_VolumeManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private int firstPlayInt;
    public Slider musicSlider, soundEffectsSlider;
    private float musicFloat, soundEffectsFloat;
    public AudioSource musicAudio;
    public AudioSource[] soundEffectsAudio;

    public AudioClip mainMenuMusic; // Аудиоклип для главного меню
    public AudioClip battleMusic; // Аудиоклип для боя

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            musicFloat = 0.25f;
            soundEffectsFloat = 0.75f;
            musicSlider.value = musicFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicSlider.value = musicFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;
        }

        UpdateSound();
        CheckCurrentScene(); // Проверка текущей сцены при запуске
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        musicAudio.volume = musicSlider.value;

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }
    }

    // Метод для переключения музыки в зависимости от ситуации
    public void ChangeMusic(AudioClip newMusic)
    {
        musicAudio.clip = newMusic;
        musicAudio.Play();
    }

    // Пример вызова метода для главного меню
    public void PlayMainMenuMusic()
    {
        ChangeMusic(mainMenuMusic);
    }

    // Пример вызова метода для боя
    public void PlayBattleMusic()
    {
        ChangeMusic(battleMusic);
    }

    // Проверка текущей сцены при запуске
    private void CheckCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Main_menu")
        {
            PlayMainMenuMusic();
        }
        else if (currentScene.name == "SampleScene")
        {
            PlayBattleMusic();
        }
        else if (currentScene.name == "Education_scene")
        {
            PlayBattleMusic();
        }
    }
}
