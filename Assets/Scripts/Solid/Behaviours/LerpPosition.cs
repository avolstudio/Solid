using System;
using UnityEngine;

namespace Solid.Behaviours
{
    public class LerpPosition : Awaitable<Vector3>
    {
        public event Action Changed;
        
        [SerializeField] private Vector3 StartValue;
        [SerializeField] private Vector3 _currentValue;
        [SerializeField] private Vector3 TargetValue;
        [SerializeField] private float LerpTimeInSeconds;

        [SerializeField] private float _valueForTick;
        [SerializeField] private float _currentTime;
        [SerializeField] private float _remainingTimeInSeconds;
        
        private void Update()
        {
            Lerp();
        }

        protected override void OnAwake(params object[] parameters)
        {
            StartValue = (Vector3) parameters[0];
            TargetValue = (Vector3) parameters[1];
            LerpTimeInSeconds = (float) parameters[2];

            Result = StartValue;
            _valueForTick = 0;
            _currentTime = 0;
            _remainingTimeInSeconds = LerpTimeInSeconds;
        }
        
        private void Lerp()
        {
            if (Vector3.Distance(TargetValue,Result) < 0.0001)
            {
                SetComplete();
                return;
            }


            _valueForTick = Time.deltaTime / LerpTimeInSeconds;

            _currentTime += _valueForTick;

            Result = Vector3.Lerp(StartValue, TargetValue, _currentTime);

            transform.position = Result;
            
            Changed?.Invoke();

            _remainingTimeInSeconds -= Time.deltaTime;
        }
    }
}
