using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SoundManager is a singleton class that manages all game sounds, including sound effects (SFX) and background music.
/// 
/// Features:
/// - Singleton Pattern: Ensures a single instance of SoundManager exists.
/// - Audio Source Management: Handles the playback of sound effects and background music.
/// - Volume Control: Allows adjusting the volume and muting audio.
/// - SFX Control: Enables toggling sound effects on and off.
/// - Custom Sound Management: Easily add and manage custom sounds through the inspector.
/// 
/// Usage:
/// - Call PlaySound(string audioName, bool loop = false, float volume = 1, float pitch = 1) to play a sound effect.
/// - Call PlayBackgroundMusic() to start playing background music.
/// - Call StopBackgroundMusic() to stop playing background music.
/// - Call MuteVolume(bool active) to mute/unmute the background music.
/// - Call SetSfx(bool active) to enable/disable sound effects.
/// - Call SetVolumeValue(float value) to set the volume for all sounds.
/// </summary>

namespace CleanArchitect
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        [Header("Game Sounds")] [Space(10)] public List<Sound> sounds = new List<Sound>();

        [SerializeField] private AudioSource backgroundMusic;

        public float volume = 1;
        public bool sfxActive = true;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            try
            {
                //Load Values from configs
                MuteVolume(GameConfig.instance.Music);
                SetVolumeValue(GameConfig.instance.Volume);
                sfxActive = GameConfig.instance.Sfx;
            }
            catch
            {
                Debug.LogError("Failed to get music game configurations");
            }
        }

        public void PlaySound(string audioName, bool loop = false, float volume = 1, float pitch = 1)
        {
            if (!sfxActive) return;
            GameObject soundObject = new GameObject("Sound");
            AudioSource audio = soundObject.AddComponent<AudioSource>();
            audio.volume = this.volume;
            audio.pitch = pitch;
            if (!loop)
            {
                audio.PlayOneShot(GetSoundClip(audioName));
                Destroy(soundObject, audio.clip.length);
            }
            else
            {
                audio.clip = GetSoundClip(audioName);
                audio.loop = true;
                audio.volume = volume * this.volume;
                LoopSound loopSoundComponent = soundObject.AddComponent<LoopSound>();
                loopSoundComponent.Initialize(audio, volume);
                audio.Play();
            }
        }

        public AudioClip GetSoundClip(string soundName)
        {
            foreach (Sound sound in sounds)
            {
                if (soundName == sound.soundName)
                {
                    return sound.soundClip;
                }
            }

            return null;
        }

        public void PlayBackgroundMusic()
        {
            backgroundMusic.Play();
        }

        public void StopBackgroundMusic()
        {
            backgroundMusic.Stop();
        }

        public void MuteVolume(bool active)
        {
            backgroundMusic.mute = !active;
            GameConfig.instance.Music = active;
        }

        public void SetSfx(bool active)
        {
            sfxActive = active;
            GameConfig.instance.Sfx = active;
        }

        public void SetVolumeValue(float value)
        {
            volume = value;
            backgroundMusic.volume = volume;
            GameConfig.instance.Volume = volume;
        }
    }

    [System.Serializable]
    public struct Sound
    {
        public string soundName;
        public AudioClip soundClip;
    }
}
