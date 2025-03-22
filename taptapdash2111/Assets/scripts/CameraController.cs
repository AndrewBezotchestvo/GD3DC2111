using UnityEngine;

public class CameraController: MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Vector3 _cameraOffset;

    [SerializeField] private Vector3 _lerpOffset;

    void Update()
    {
        transform.position = Vector3.Lerp(_player.position + _cameraOffset,
            _player.position + _cameraOffset + _lerpOffset, 1);
    }
}
