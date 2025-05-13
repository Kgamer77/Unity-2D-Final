using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioSource musicObject;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Creates an audio source game object to play one off sound effects and destroys it afterwards.
    public void PlaySoundEffect(AudioClip audioClip, Transform transform, float volume, float pitchRange = 0f)
    {
        AudioSource audioSource = Instantiate(sfxObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        if (pitchRange > 0f)
        {
            audioSource.pitch *= 1f + Random.Range(-pitchRange, pitchRange);
        }

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
    
    // Gets an audio source for playing level music to be attached as a component of the player
    public AudioSource GetMusicPlayer(AudioClip audioClip, float volume, float time = 0f)
    {
        AudioSource audioSource = Instantiate(musicObject, Vector3.zero, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = true;
 
        if (time > 0f)
        {
            //audioSource.time = time;
        }
        

        audioSource.Play();

        return audioSource;
    }

}
