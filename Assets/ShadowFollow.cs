using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    [SerializeField] private Transform _head;
    public Vector3 _offset = new Vector3(.01f, 0.04f, .01f);


    void Update()
    {
        transform.localPosition = new Vector3(_head.position.x, 0f, _head.position.z) + _offset;
        float yRot = _head.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, yRot, 0f);
    }
}
