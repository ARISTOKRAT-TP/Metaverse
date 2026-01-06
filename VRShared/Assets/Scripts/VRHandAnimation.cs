using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class VRHandAnimation : NetworkBehaviour
{
    public Animator handAnimator;
    public string gripRightParam = "Grip_Right";
    public string gripLeftParam = "Grip_Left";
    public string triggerRightParam = "Trigger_Right";
    public string triggerLeftParam = "Trigger_Left";


    public InputActionProperty rightSelectAction;
    public InputActionProperty leftSelectAction;
    public InputActionProperty rightTriggerAction;
    public InputActionProperty leftTriggerAction;

    private float gripRight;
    private float gripLeft;
    private float triggerRight;
    private float triggerLeft;

    void Update()
    {
        if (Object.HasInputAuthority)
        {
            // Read controller data
            gripRight = rightSelectAction.action.ReadValue<float>();
            gripLeft = leftSelectAction.action.ReadValue<float>();
            triggerRight = rightTriggerAction.action.ReadValue<float>();
            triggerLeft = leftTriggerAction.action.ReadValue<float>();

            // local
            UpdateHandAnimation(gripRight, gripLeft, triggerRight, triggerLeft);

            // network
            RPC_UpdateHandAnimation(gripRight, gripLeft, triggerRight, triggerLeft);
        }
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.All, InvokeLocal = true)]
    private void RPC_UpdateHandAnimation(float gripR, float gripL, float triggerR, float triggerL)
    {
        UpdateHandAnimation(gripR, gripL, triggerR, triggerL);
    }

    private void UpdateHandAnimation(float gripR, float gripL, float triggerR, float triggerL)
    {
        // update anim params
        handAnimator.SetFloat(gripRightParam, gripR);
        handAnimator.SetFloat(gripLeftParam, gripL);
        handAnimator.SetFloat(triggerRightParam, triggerR);
        handAnimator.SetFloat(triggerLeftParam, triggerL);
    }
}