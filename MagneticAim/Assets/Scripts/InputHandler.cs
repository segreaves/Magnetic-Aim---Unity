using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, Controls.IPlayerActions
{
    public Vector3 AimDirection { get; private set; }
    private Transform _mainCameraTransform;
    private Controls _controls;
    private Vector3 _targetAimDirection;
    private Vector3 _dampingVelocity;
    public bool HasInput { get; private set; }

    void Start()
    {
        _mainCameraTransform = Camera.main.transform;
        AimDirection = transform.forward;
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
        _controls.Player.Aim.performed += OnAim;
    }

    private void Update()
    {
        if (!HasInput) return;
        AimDirection = Vector3.SmoothDamp(AimDirection, _targetAimDirection, ref _dampingVelocity, 0.025f);
    }

    private void OnDestroy()
    {
        _controls.Player.Aim.performed -= OnAim;
        _controls.Disable();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 aimValueXY = _controls.Player.Aim.ReadValue<Vector2>();
        HasInput = aimValueXY.magnitude > 0.01f ? true : false;
        if (!HasInput) return;
        _targetAimDirection = CalculateDirection(aimValueXY).normalized;
    }

    public Vector3 CalculateDirection(Vector2 xyValue)
    {
        Vector3 forward = _mainCameraTransform.forward;
        Vector3 right = _mainCameraTransform.right;
        forward.y = 0f;
        forward.Normalize();
        right.y = 0f;
        right.Normalize();
        return forward * xyValue.y + right * xyValue.x;
    }
}
