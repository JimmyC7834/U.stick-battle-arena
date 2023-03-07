#region

using System;
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
            _gameService.ProvideAudioManager(this);
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
        
        /**
         * Play audio clip with the given play setting
         */
        public void StopAudio(AudioID id)
        {
            if (id == AudioID.None) return;
            if (!_audioDataSet.ContainsID(id)) return;
            if (!_audioSources.ContainsKey(id)) return;

            AudioSource audioSource = _audioSources[id];
            StartCoroutine(StopAudio(audioSource));
        }
        
        private IEnumerator StopAudio(AudioSource source)
        {
            float startV = source.volume;
            while (source.volume > 0)
            {
                source.volume -= startV * Time.deltaTime / 0.5f;
                yield return null;
            }
            
            source.Stop();
            source.volume = startV;
        }

        /**
         * Return if the given audio is playing or not
         */
        public bool AudioIsPlaying(AudioID id)
        {
            if (!_audioSources.ContainsKey(id))
                return false;
            
            return _audioSources[id].isPlaying;
        }

        public static AudioID SceneIDToAudioID(SceneID sceneID)
        {
            // ugly conversion between the enums
            return (AudioID) (Convert.ToInt32(sceneID) + 92);
        }

        private void OnValidate()
        {
            
        }
        
    }
}