using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Fusion;

namespace UnityEngine.XR.Content.Interaction
{
    public class NWKnob : NetworkBehaviour, ISpawned
    {
        const float k_ModeSwitchDeadZone = 0.1f;

        struct TrackedRotation
        {
            float m_BaseAngle;
            float m_CurrentOffset;
            float m_AccumulatedAngle;

            public float totalOffset => m_AccumulatedAngle + m_CurrentOffset;

            public void Reset()
            {
                m_BaseAngle = 0.0f;
                m_CurrentOffset = 0.0f;
                m_AccumulatedAngle = 0.0f;
            }

            public void SetBaseFromVector(Vector3 direction)
            {
                m_AccumulatedAngle += m_CurrentOffset;
                m_BaseAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                m_CurrentOffset = 0.0f;
            }

            public void UpdateOffsetFromVector(Vector3 direction)
            {
                var currentAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                m_CurrentOffset = Mathf.DeltaAngle(m_BaseAngle, currentAngle);
            }
        }

        [SerializeField]
        private float m_Value;

        [SerializeField]
        private float m_MinAngle;
        [SerializeField]
        private float m_MaxAngle;

        [SerializeField]
        private Transform m_Handle;

        [SerializeField]
        private bool m_ClampedMotion = true;

        [Networked]
        public float Value { get; set; }

        TrackedRotation m_TrackedRotation;
        XRBaseInteractor m_Interactor;
        private bool isSpawned = false;

        public override void Spawned()
        {
            isSpawned = true;
            SetKnobRotation(ValueToRotation());
        }

        void Start()
        {
            if (isSpawned)
            {
                SetKnobRotation(ValueToRotation());
            }
        }

        void OnEnable()
        {
            if (m_Handle != null && isSpawned)
            {
                m_Handle.localEulerAngles = new Vector3(0.0f, 0.0f, ValueToRotation());
            }
        }

        void Update()
        {
            if (!isSpawned) return;

            if (m_Interactor != null)
            {
                Vector3 direction = m_Interactor.transform.position - transform.position;
                m_TrackedRotation.UpdateOffsetFromVector(direction);

                if (Object.HasStateAuthority)
                {
                    float newValue = RotationToValue(m_TrackedRotation.totalOffset);
                    if (Mathf.Abs(newValue - Value) > Mathf.Epsilon)
                    {
                        Value = newValue;
                        RpcSetValue(newValue);
                    }
                }
            }

            if (!Object.HasStateAuthority)
            {
                UpdateKnobRotation();
            }
        }

        public void SetValue(float newValue)
        {
            if (!isSpawned) return;

            if (Object.HasStateAuthority)
            {
                Value = newValue;
                RpcSetValue(newValue);
            }
            else if (isSpawned) // Добавлено условие isSpawned
            {
                RpcRequestSetValue(newValue);
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RpcRequestSetValue(float newValue)
        {
            SetValue(newValue);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RpcSetValue(float newValue)
        {
            Value = newValue;
            SetKnobRotation(ValueToRotation());
        }

        private void UpdateKnobRotation()
        {
            SetKnobRotation(ValueToRotation());
        }

        private void SetKnobRotation(float rotation)
        {
            if (m_Handle != null)
                m_Handle.localEulerAngles = new Vector3(0.0f, 0.0f, rotation);
        }

        float ValueToRotation()
        {
            if (!isSpawned) return 0.0f; // Добавлена проверка на isSpawned
            return Mathf.Lerp(m_MinAngle, m_MaxAngle, Value);
        }

        float RotationToValue(float rotation)
        {
            return Mathf.InverseLerp(m_MinAngle, m_MaxAngle, rotation);
        }

        void OnDrawGizmosSelected()
        {
            const int k_CircleSegments = 16;
            const float k_SegmentRatio = 1.0f / k_CircleSegments;

            if (m_Handle != null)
            {
                var circleCenter = m_Handle.position;
                var circleX = transform.right;
                var circleY = transform.forward;

                Gizmos.color = Color.green;
                for (int segmentCounter = 0; segmentCounter < k_CircleSegments; segmentCounter++)
                {
                    var startAngle = segmentCounter * k_SegmentRatio * 2.0f * Mathf.PI;
                    var endAngle = (segmentCounter + 1) * k_SegmentRatio * 2.0f * Mathf.PI;

                    // Gizmos.DrawLine(circleCenter + (Mathf.Cos(startAngle) * circleX + Mathf.Sin(startAngle) * circleY) * m_PositionTrackedRadius,
                    //                 circleCenter + (Mathf.Cos(endAngle) * circleX + Mathf.Sin(endAngle) * circleY) * m_PositionTrackedRadius);
                }
            }
        }

        void OnValidate()
        {
            if (m_ClampedMotion)
                m_Value = Mathf.Clamp01(m_Value);

            if (m_MinAngle > m_MaxAngle)
                m_MinAngle = m_MaxAngle;

            if (isSpawned) // Добавлена проверка на isSpawned
                SetKnobRotation(ValueToRotation());
        }
    }
}