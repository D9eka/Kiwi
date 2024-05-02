using System.Collections;
using UnityEngine;

namespace Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundObject : MonoBehaviour
    {
        private AudioSource _source;
        private AudioClip _sound;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Initialize(AudioClip clip, float volume)
        {
            _sound = clip;
            _source.volume = volume;
            StartCoroutine(PlaySound());
        }

        private IEnumerator PlaySound()
        {
            _source.PlayOneShot(_sound);
            yield return new WaitForSecondsRealtime(_sound.length);
            Destroy(gameObject);
        }
    }
}
