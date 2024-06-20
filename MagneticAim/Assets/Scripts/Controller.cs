using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Controller : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;
    private InputHandler _inputHandler;
    private AimAssist _assist;
    private Vector3 _aimAssistedTarget;
    private Vector3 _aimAssistedDirection;
    private Vector3 _dampingVelocity;
    private Vector3 _lookDirection;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        Assert.IsNotNull(_inputHandler, "Controller warning: No InputHandler found.");
        _assist = GetComponent<AimAssist>();
        Assert.IsNotNull(_assist, "Controller warning: No AimAssist found.");
    }

    private void Update()
    {
        if (!_inputHandler.HasInput) return;
        _aimAssistedDirection = Vector3.SmoothDamp(_aimAssistedDirection, _aimAssistedTarget, ref _dampingVelocity, 0.025f);
        _lookDirection = _aimAssistedDirection;
        _lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(_lookDirection);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Debug.DrawLine(transform.position, transform.position + _inputHandler.AimDirection * 5f, Color.yellow);
        _aimAssistedTarget = _assist.GetAssistedAim(_inputHandler.AimDirection, ref _targets);
        Debug.DrawLine(transform.position, transform.position + _aimAssistedDirection * 5f, Color.green);
    }
}
