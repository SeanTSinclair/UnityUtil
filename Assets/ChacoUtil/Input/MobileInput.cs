using ChacoUtil.Input;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    [SerializeField] private bool isMobileEnabled = true;
    [SerializeField] private GameObject mobileUIPanel;

    private InputManager _inputManager;
    private Camera _camera;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _camera = Camera.main;
    }

    private void Start()
    {
        if(!isMobileEnabled) Destroy(gameObject);
    }

    private void Move(Vector2 screenPosition, float time)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _camera.nearClipPlane);
        Vector3 worldCoordinates = _camera.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= Move;
    }
}
