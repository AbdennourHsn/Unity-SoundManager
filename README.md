# 🎵 Sound Manager for Unity

This Unity script provides a reusable and extensible sound management system for handling game audio such as background music and sound effects. It includes functionality for playing, stopping, and looping sounds, as well as managing volume and sound effect (SFX) settings.

## Features

- **🔄 Singleton Pattern**: Ensures a single instance of `SoundManager` exists.
- **🎼 Background Music Control**: Play and stop background music.
- **🔊 Sound Effects**: Play, loop, and manage volume for sound effects.
- **🔧 Easy Configuration**: Simple setup for audio clips and volume control.
- **🔍 Extensibility**: Add your special attributes depending on the game.

## Usage

### Initialization

1. **Ensure a Single Instance**:
    ```csharp
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    ```

### Playing Sounds

- **Play a Sound**:
    ```csharp
    SoundManager.instance.PlaySound("audioName");
    ```

- **Play a Looping Sound**:
    ```csharp
    SoundManager.instance.PlaySound("audioName", true);
    ```

- **Play Background Music**:
    ```csharp
    SoundManager.instance.PlayBackgrounMusic();
    ```

### Managing Volume and SFX

- **Mute/Unmute Background Music**:
    ```csharp
    SoundManager.instance.MuteVolume(true);
    ```

- **Set SFX Active/Inactive**:
    ```csharp
    SoundManager.instance.SetSfx(true);
    ```

- **Set Volume**:
    ```csharp
    SoundManager.instance.setVolumValue(0.8f);
    ```

### Example

```csharp
// To play a sound
SoundManager.instance.PlaySound("jump");

// To play background music
SoundManager.instance.PlayBackgrounMusic();

// To stop background music
SoundManager.instance.StopBackgroundMusic();

// To mute volume
SoundManager.instance.MuteVolume(false);

// To set sound effects active
SoundManager.instance.SetSfx(true);

// To set the volume
SoundManager.instance.setVolumValue(0.5f);

## GameCongig

# Game Configurations Manager for Unity

This Unity script provides a centralized manager for handling game configuration settings such as music, sound effects, visual effects, vibration, and volume. It follows the Singleton pattern to ensure only one instance of the manager exists throughout the game.

## Features

- ** Singleton Pattern**: Ensures a single instance of `GameConfigurations` exists.
- ** Configuration Persistence**: Saves and loads settings from PlayerPrefs.
- ** Easy Access**: Provides properties to access and modify each setting.
- ** Default Settings**: Option to reset all settings to their default values.

