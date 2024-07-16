using UnityEngine;

/// <summary>
/// LoopSound is a helper class that ensures looped sounds are properly managed and cleaned up.
/// </summary>
public class LoopSound : MonoBehaviour
{
    private AudioSource audioSource;
    private float volume;

    public void Initialize(AudioSource source, float vol)
    {
        audioSource = source;
        volume = vol;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}