using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    [SerializeField] private AudioSource shotFXObject;

    void Awake()
    {
        //Singleton mimarisi
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySoundFX(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Instantiate
        AudioSource audioSource = Instantiate(shotFXObject, spawnTransform.position, Quaternion.identity);
        //Spawn
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        //Destroy On Finish
        Destroy(audioSource.gameObject, clipLength);
    }



}