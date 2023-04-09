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


    [SerializeField] private Transform _visualL;
    [SerializeField] private Transform _visualR;

    private int count = 0;
    private int totAvatars;

    public Vector3 camPos => _camera.position;

    void Start()
    {
        totAvatars = _visualL.childCount;
    }

    void Update()
    {

    }

    public void magicSpawn()
    {
        //Debug.Log(camPos);
        Realtime.Instantiate(_magicParticles.name, camPos, transform.rotation, new Realtime.InstantiateOptions
        {
            ownedByClient = true,
            preventOwnershipTakeover = true,
            destroyWhenOwnerLeaves = true,
            destroyWhenLastClientLeaves = true,
            useInstance = _realtime,
        });
    }

    public void nextAvatar()
    {
        _visualL.GetChild(count).gameObject.SetActive(false);
        _visualR.GetChild(count).gameObject.SetActive(false);

        //Ternary Operator, similiar to if else statement
        count = count < totAvatars - 1 ? count += 1 : count = 0;
        
        _visualL.GetChild(count).gameObject.SetActive(true);
        _visualR.GetChild(count).gameObject.SetActive(true);

    }

    public void lastAvatar()
    {
        _visualL.GetChild(count).gameObject.SetActive(false);
        _visualR.GetChild(count).gameObject.SetActive(false);

        //Ternary Operator, similiar to if else statement
        count = count > 0 ? count -= 1 : count = totAvatars - 1;

        _visualL.GetChild(count).gameObject.SetActive(true);
        _visualR.GetChild(count).gameObject.SetActive(true);
    }

}