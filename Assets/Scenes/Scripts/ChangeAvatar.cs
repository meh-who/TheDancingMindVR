using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ChangeAvatar : MonoBehaviour
{
    // Reference to Realtime to use to instantiate brush strokes
    [SerializeField] private Realtime _realtime;

    [SerializeField] private GameObject _magicParticles;
    [SerializeField] private Transform _camera;
    public Vector3 camPos => _camera.position;

    void Start()
    {

    }

    void Update()
    {

    }

    public void magicSpawn()
    {
        Realtime.Instantiate(_magicParticles.name, new Vector3(camPos.x, camPos.y, camPos.z), transform.rotation, new Realtime.InstantiateOptions
        {
            ownedByClient = true,
            preventOwnershipTakeover = true,
            destroyWhenOwnerLeaves = true,
            destroyWhenLastClientLeaves = true,
            useInstance = _realtime,
        });
    }


}