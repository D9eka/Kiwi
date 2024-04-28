using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
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
    [Space, SerializeField] private BGM _bgm;

    private enum BGM
    {
        Ambience,
        Boss,
        Level,
        MainHall
    }

    private AudioClip GetBGM()
    {
        return _bgm switch
        {
            BGM.Ambience => _ambienceMusic,
            BGM.Boss => _bossMusic,
            BGM.Level => _levelMusic,
            BGM.MainHall => _mainHallMusic,
            _ => null
        };
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayMusic(GetBGM());
    }

    public void PlaySound(AudioClip audioClip)
    {
        var soundGameObject = new GameObject("Sound");
        var audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        var soundGameObject = new GameObject("Sound");
        var audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.PlayDelayed(0);
    }
}