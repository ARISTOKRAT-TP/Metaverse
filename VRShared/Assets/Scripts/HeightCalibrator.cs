using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.InputSystem;

public class HeightCalibrator : MonoBehaviour
    {
        private const float maxAllowedHeight = 2.2f;
        private const float minAllowedHeight = 1.35f;

        [SerializeField] private VRIK ik;
        [SerializeField] private InputActionProperty trackedHeadPosition;
        private float lastCalibratedHeight;
        private float scale;

        public void CalibrateHeight()
        {
            // get the player height, by reading it's tracked head position
            var headPosition = trackedHeadPosition.action.ReadValue<Vector3>();

            // Store the tracked head position 
            lastCalibratedHeight = Mathf.Min(maxAllowedHeight, Mathf.Max(minAllowedHeight, headPosition.y));

            CalibrateBody();
        }
        
        public void CalibrateBody()
        {
            scale = lastCalibratedHeight / 1.83f;
            // Scale the avatar game object via VRIK
            ik.references.root.localScale = new Vector3(scale, scale, scale);
            CalibrateHead();
            CalibrateHands();
        }

        // Calibrate the avatar head to the correct size (a bigger avatar 
        // has a slightly smaller head and vice versa).
        private void CalibrateHead()
        {
            const float scaleDivisionConstant = 2f;

            var headScale = 1f + (1f - scale) / scaleDivisionConstant;
            ik.references.head.localScale = new Vector3(headScale, headScale, headScale);
        }
        
        // This method calibrates avatar hands back to one. This means, that the interactions
        // with hands don't need to change, since the hands are bigger/smaller.
        private void CalibrateHands()
        {
            ScaleBoneToOne(ik.references.leftHand);
            ScaleBoneToOne(ik.references.rightHand);
        }

        private void ScaleBoneToOne(Transform hand)
        {
            hand.localScale = Vector3.one;
            var lossyScale = hand.lossyScale;

            hand.localScale = new Vector3(1f / lossyScale.x,
                1f / lossyScale.y, 1f / lossyScale.z);
        }
    }