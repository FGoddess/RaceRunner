using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioClip[] _music;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = _music[Random.Range(0, _music.Length)];
            _audioSource.Play();
        }
    }

    public void PlayRandomJumpSFX()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
    }

    public void Mute()
    {
        _audioSource.mute = !_audioSource.mute;
    }
}
