using Solid.Core;
using UnityEngine;

namespace Solid.Examples
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayAudio : Awaitable<bool>
    {
        private AudioSource _source;

        private AudioClip _clip;

        protected override void OnAwake(params object[] parameters)
        {
            _clip = (AudioClip)parameters[0];
            
            _source = GetComponent<AudioSource>();
        }

        protected override void OnStart()
        {
            _source.clip = _clip;
            
            _source.Play();
        }

        private void Update()
        {
            if (_source.isPlaying)
                        return;
            
            SetComplete(true);
        }

        protected override void OnFinish(bool finishedWithSuccess)
        {
            _source.Stop();

            Destroy(_source);
        }
    }
}
