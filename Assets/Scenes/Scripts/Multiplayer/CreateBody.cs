using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBody : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _armL;
    [SerializeField] private Transform _armR;
    public float _offset = -0.4f;


    void Start()
    {
        
    }

    void Update()
    {
        transform.position = _head.position - new Vector3(0f, -_offset, 0f);
        float yRot = _head.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, yRot, 0f);
    }
}
