using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToHead : MonoBehaviour
{

    private Reference _reference;

    [Space]
    [SerializeField] private Transform _translocator;

    [SerializeField] private float _lerpSpeedPosition = 0.05f;
    [SerializeField] private float _lerpSpeedRotation = 0.1f;

    [SerializeField] private float _lerpSpeedDistance = 0.01f;

    private float _distance;

    private void Awake()
    {
        _reference = FindObjectOfType<Reference>();
    }

    public void SetDistance(float distance)
    {
        _distance = distance;
    }

    private void Update()
    {
        var currentPos = transform.position;
        var newPos = _reference.GetHead().position;
        newPos = Vector3.Lerp(currentPos, new Vector3(newPos.x, currentPos.y, newPos.z), _lerpSpeedPosition);

        Quaternion newRot = Quaternion.Euler(0, _reference.GetHead().rotation.eulerAngles.y, 0);
        newRot = Quaternion.Lerp(transform.rotation, newRot, _lerpSpeedRotation);

        transform.SetPositionAndRotation(newPos, newRot);

        Vector3 pos = _translocator.localPosition;
        _translocator.localPosition = Vector3.Lerp(pos, new Vector3(0, 0, _distance), _lerpSpeedDistance);
    }
}
