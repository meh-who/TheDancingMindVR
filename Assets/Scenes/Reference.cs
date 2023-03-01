using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    [SerializeField] private Transform _head;

    public Transform _centerEyeAnchor;

    private void Start()
    {

    }

    private void Update()
    {
        _head.position = new Vector3(_centerEyeAnchor.position.x, 0.001f, _centerEyeAnchor.position.z);
        _head.rotation = Quaternion.Euler(0, _centerEyeAnchor.rotation.eulerAngles.y, 0);
    }

    public Transform GetHead()
    {
        return _head;
    }
}
