using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _camera;

    InputAction Touch;
    InputAction TouchPosition;

    public delegate void TouchStart(Vector3 position);
    public delegate void TouchEnd(Vector3 position);
    public static event TouchStart onTouchStart;
    public static event TouchEnd onTouchEnd;
    private void Awake()
    {
        Touch = _playerInput.actions["Primary"];
        TouchPosition = _playerInput.actions["PrimaryPosition"];
        Touch.started += TouchStartData;
        Touch.canceled += TouchEndData;
    }

    public void TouchStartData(InputAction.CallbackContext ctx)
    {
        onTouchStart?.Invoke(TouchPosition.ReadValue<Vector2>());
    }

    public void TouchEndData(InputAction.CallbackContext ctx)
    {    
        onTouchEnd?.Invoke(TouchPosition.ReadValue<Vector2>());
    }
    private void OnDisable()
    {
        Touch.started -= TouchStartData;
        Touch.canceled -= TouchEndData;
    }

}
