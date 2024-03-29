﻿using System;
using Solid.Core;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Examples
{
    public sealed class LerpFloat : Awaitable<float>
    {
        public event Action Changed;
        
        [SerializeField] private float StartValue;
        [SerializeField] private float _currentValue;
        [SerializeField] private float TargetValue;
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
            StartValue = (float) parameters[0];
            TargetValue = (float) parameters[1];
            LerpTimeInSeconds = (float) parameters[2];

            Result = StartValue;
            _valueForTick = 0;
            _currentTime = 0;
            _remainingTimeInSeconds = LerpTimeInSeconds;
        }


        private void Lerp()
        {
            if (Math.Abs(TargetValue - Result) < 0.0001)
            {
                SetComplete();
                return;
            }
            
            _valueForTick = Time.deltaTime / LerpTimeInSeconds;

            _currentTime += _valueForTick;

            Result = Mathf.Lerp(StartValue, TargetValue, _currentTime);
            
            Changed?.Invoke();

            _remainingTimeInSeconds -= Time.deltaTime;
        }

        protected override void OnStart()
        {
            
        }
    }
}