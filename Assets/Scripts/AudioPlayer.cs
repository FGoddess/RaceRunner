using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioClip[] _music;

    [SerializeField] private PlayerMover _player;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _player.Jumped += PlayRandomJumpSFX;
    }

    private void OnDisable()
    {
        _player.Jumped -= PlayRandomJumpSFX;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
        _audioSource.clip = _music[Random.Range(0, _music.Length)];
        _audioSource.Play();
    }

    public void PlayRandomJumpSFX()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
    }
}
