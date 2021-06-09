using System;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Behaviours
{
    public sealed class LerpVector3 : AwaitableBehaviour<Vector3>
    {
        [SerializeField] AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);

        [SerializeField] private Vector3 StartValue;
        [SerializeField] private Vector3 TargetValue;
        [SerializeField] private float LerpTimeInSeconds;

        [SerializeField] private float _valueForTick;
        [SerializeField] private float _currentTime;
        [SerializeField] private float _remainingTimeInSeconds;

        protected override void OnAwake(params object[] parameters)
        {
            StartValue = (Vector3) parameters[0];
            TargetValue = (Vector3) parameters[1];
            LerpTimeInSeconds = (float) parameters[2];
            _curve = (AnimationCurve) parameters[3];

            _valueForTick = 0;
            _currentTime = 0;
            _remainingTimeInSeconds = LerpTimeInSeconds;
        }


        private void Update()
        {
            Lerp();
        }


        private void Lerp()
        {
            if (Vector3.Distance(Result, TargetValue) < 0.001f)
            {
                SetComplete(true);
                return;
            }

            _valueForTick = Time.deltaTime / LerpTimeInSeconds;

            _currentTime += _valueForTick;

            Result = Vector3.Lerp(StartValue, TargetValue, _curve.Evaluate(_currentTime));

            _remainingTimeInSeconds -= Time.deltaTime;
        }
    }
}