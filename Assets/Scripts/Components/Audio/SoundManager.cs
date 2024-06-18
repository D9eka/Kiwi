using Components.Audio;
using Creatures.Player;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject _soundPrefab;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private BGMType _backgroundMusic;
    [Space]
    public AudioClip _ambienceMusic;
    public AudioClip _bossMusic;
    public AudioClip _levelMusic;
    public AudioClip _mainHallMusic;
    public AudioClip _clickGameSound;
    public AudioClip _clickUISound;
    public AudioClip _rechargeHealthSound;
    public AudioClip _rechargeOxygenSound;
    public AudioClip _startWaveSound;
    public AudioClip _takeDamageSound;
    public AudioClip _upgradeWeaponSound;
    public AudioClip _upgradeChipSound;

    public const string SOUND_VOLUME_KEY = "SoundVolume";
    public const string MUSIC_VOLUME_KEY = "MusicVolume";

    public static SoundManager Instance { get; private set; }

    public enum BGMType
    {
        Ambience,
        Boss,
        Level,
        MainHall
    }

    private AudioClip GetBGM(BGMType type)
    {
        return type switch
        {
            BGMType.Ambience => _ambienceMusic,
            BGMType.Boss => _bossMusic,
            BGMType.Level => _levelMusic,
            BGMType.MainHall => _mainHallMusic,
            _ => null
        };
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _musicAudioSource.loop = true;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void Start()
    {
        _musicAudioSource.loop = true;
        ChangeVolume();
        PlayMusic(GetBGM(_backgroundMusic));
    }

    public void PlaySound(AudioClip clip)
    {
        SoundObject soundGO = Instantiate(_soundPrefab, transform).GetComponent<SoundObject>();
        soundGO.Initialize(clip, PlayerPrefsController.GetFloat(SOUND_VOLUME_KEY, 1f));
    }

    public void PlayMusic(BGMType type)
    {
        PlayMusic(GetBGM(type));
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicAudioSource.clip == clip)
            return;
        _musicAudioSource.Stop();
        _musicAudioSource.clip = clip;
        _musicAudioSource.Play();
    }

    public void ChangeVolume()
    {
        float musicVolume = PlayerPrefsController.GetFloat(MUSIC_VOLUME_KEY, 1f);
        _musicAudioSource.volume = musicVolume;
    }
}