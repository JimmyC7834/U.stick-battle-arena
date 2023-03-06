#region

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Game
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private AudioDataSetSO _audioDataSet;
        
        // Maps each audio id to its source
        private Dictionary<AudioID, AudioSource> _audioSources;

        private void Awake()
        {
            _audioSources = new Dictionary<AudioID, AudioSource>();
            StartCoroutine(AudioTest());

            _gameService.ProvideAudioManager(this);
        }

        private IEnumerator AudioTest()
        {
            yield return new WaitForSecondsRealtime(2);
            // PlayAudio(AudioID.TestingAudio);
        }

        /**
         * Play audio clip with the default play setting
         */
        public void PlayAudio(AudioID id) => PlayAudio(id, _audioDataSet[id].DefaultSetting); 
        
        /**
         * Play audio clip with the given play setting
         */
        public void PlayAudio(AudioID id, AudioClipPlaySetting setting)
        {
            if (id == AudioID.None) return;
            if (!_audioDataSet.ContainsID(id)) return;
            if (!_audioSources.ContainsKey(id))
            {
                // add source for the id
                AudioSource newSource = new GameObject(
                    $"{id} source").AddComponent<AudioSource>();
                newSource.transform.SetParent(transform);
                _audioSources.Add(id, newSource);
            }

            AudioSource audioSource = _audioSources[id];
            audioSource.clip = _audioDataSet[id].AudioClip;
            audioSource.volume = setting.Volume;
            audioSource.pitch = setting.Pitch;
            audioSource.loop = setting.Loop;
            
            audioSource.Play();
        }

        public void PlayFootstep()
        {
            int rand = Random.Range(0, 4);

            switch (rand)
            {
                case 0:
                    PlayAudio(AudioID.Walk1);
                    break;
                case 1:
                    PlayAudio(AudioID.Walk2);
                    break;
                case 2:
                    PlayAudio(AudioID.Walk3);
                    break;
                case 3:
                    PlayAudio(AudioID.Walk4);
                    break;
            }
        }
    }
}