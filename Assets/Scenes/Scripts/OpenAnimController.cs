using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class OpenAnimController : MonoBehaviour
{
    [SerializeField, Interface(typeof(IHmd))]
    private MonoBehaviour _hmd;
    private IHmd Hmd { get; set; }

    [SerializeField]
    private ActiveStateSelector _pose;

    [SerializeField] private Material _particleIcon;
    [SerializeField] private GameObject _poseActiveParticles;
    private GameObject _particle;
    [Space]
    [SerializeField] private GameObject _breathingSphere;
    [SerializeField] private Vector3 _offset =  new Vector3 (-0.09f, -0.14f, 0f);
    [SerializeField] private Transform _parent;
    private Animator _sphereAnimCntrl;
    private GameObject _sphere;

    private int _trigger = 0;

    protected virtual void Awake()
    {
        Hmd = _hmd as IHmd;
    }


    void Start()
    {
        this.AssertField(Hmd, nameof(Hmd));
        this.AssertField(_poseActiveParticles, nameof(_poseActiveParticles));
        this.AssertField(_breathingSphere, nameof(_breathingSphere));

        _particle = Instantiate(_poseActiveParticles);
        _particle.GetComponentInChildren<ParticleSystemRenderer>().material = _particleIcon;
        _particle.SetActive(false);

        _sphere = Instantiate(_breathingSphere);
        _sphere.transform.localScale = Vector3.zero;
        _sphere.SetActive(false);
        _sphereAnimCntrl = _sphere.GetComponent<Animator>();

    }

    public void ShowVisuals()
    {
        if (!Hmd.TryGetRootPose(out Pose hmdPose))
        {
            return;
        }

        switch (_trigger)
        {
            case 0:
                Vector3 spawnSpot = hmdPose.position + hmdPose.forward;
                _particle.transform.position = spawnSpot;
                _particle.transform.LookAt(2 * _particle.transform.position - hmdPose.position);
                _sphere.transform.position = spawnSpot;

                var hand = _pose.GetComponent<HandRef>();

                Vector3 visualsPos = Vector3.zero;
                hand.GetRootPose(out Pose wristPose);
                Vector3 forward = hand.Handedness == Handedness.Left ? wristPose.right : -wristPose.right;
                visualsPos += wristPose.position + forward * .15f + Vector3.up * .02f;

                _particle.transform.position = visualsPos;
                _particle.gameObject.SetActive(true);

                _sphere.transform.position = visualsPos + _offset;
                _sphere.gameObject.SetActive(true);
                _sphereAnimCntrl.SetBool("PoseActivated", true);
                _sphere.GetComponent<Transform>().SetParent(_parent);

                _trigger = 1;

                break;
            case 1:
                break;

        } 
    }

    public void HideVisuals()
    {
        _particle.gameObject.SetActive(false);
        //_sphereAnimCntrl.SetBool("PoseActivated", false);
    }
}
