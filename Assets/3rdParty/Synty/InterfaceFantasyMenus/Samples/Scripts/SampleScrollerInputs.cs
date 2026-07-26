// Copyright (c) 2024 Synty Studios Limited. All rights reserved.
//
// Use of this software is subject to the terms and conditions of the End User Licence Agreement (EULA) 
// of the store at which you purchased this asset. 
//
// Synty assets are available at:
// http://www.syntystore.com
// http://www.unityassetstore.com
// http://www.fab.com
//
// Sample scripts are included only as examples and are not intended as production-ready.

using UnityEngine;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Sample credits screen controller
    /// </summary>
    public class SampleScrollerInputs : MonoBehaviour
    {
        [Header("References")]
        public RectTransform viewport;
        public RectTransform content;

        [Header("Parameters")]
        public float speed;
        public float speedChangeIncrement;
        public float slowDownDistance = 200;
        public float maxSpeed;

        Vector3 startPosition;
        float startSpeed;

        private void Start()
        {
            startSpeed = speed;
            startPosition = content.localPosition;
        }

        private void Reset()
        {
            content.localPosition = startPosition;
            speed = startSpeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                speed = Mathf.Clamp( speed - speedChangeIncrement, -maxSpeed, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                speed = Mathf.Clamp( speed + speedChangeIncrement, 0, maxSpeed);
            }

            float length = content.sizeDelta.y - viewport.rect.height;
            float distanceTraveled = Vector3.Distance(content.localPosition, startPosition);

            float distanceLeft = length - distanceTraveled;
            if(speed < 0)
            {
                distanceLeft = distanceTraveled;
            }

            Vector3 delta = Vector3.up * Mathf.Lerp(speed, 0, Mathf.InverseLerp(slowDownDistance, 0, distanceLeft)) * Time.deltaTime;

            content.localPosition = content.localPosition + delta;
        }
    }
}