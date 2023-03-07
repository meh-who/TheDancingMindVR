using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class MPHandManager : MonoBehaviour
{
    // Reference to Realtime to use to instantiate brush strokes
    [SerializeField] private Realtime _realtime;
    
    // to generate
    [Space]
    [SerializeField] private GameObject _handLPrefab;
    [SerializeField] private GameObject _handRPrefab;
    private GameObject MPHandL;
    private GameObject MPHandR;

    // to track
    [Space]
    [SerializeField] SkinnedMeshRenderer _handLMesh;
    [SerializeField] SkinnedMeshRenderer _handRMesh;

    private Transform _localHandL;
    private Transform _localHandR;
    public Transform[] _localFingerRotsL;
    public Transform[] _localFingerRotsR;

    public bool ActiveL => _handLMesh.isVisible;
    private bool LGenerated = false;
    public bool ActiveR => _handRMesh.isVisible;
    private bool RGenerated = false;

    private void Awake()
    {

    }

    void Start()
    {
        _localHandL = GameObject.Find("OculusHand_L").GetComponent<Transform>();
        _localHandR = GameObject.Find("OculusHand_R").GetComponent<Transform>();
        
    }


    void Update()
    {

        if (!_realtime.connected)
            return;

        if (ActiveL && !LGenerated)
        {
            // generate hand L
            MPHandL = Realtime.Instantiate(_handLPrefab.name, new Realtime.InstantiateOptions
            {
                ownedByClient = true,
                preventOwnershipTakeover = true,
                destroyWhenOwnerLeaves = true,
                destroyWhenLastClientLeaves = true,
                useInstance = _realtime,
            });
            LGenerated = true;

            // initialize finger rotation model
            MPHandL.GetComponent<MPHand>().CreateFingerModelRotations();
        }
        
        if (LGenerated)
        {
            // update hand pos & rot
            MPHandL.GetComponent<MPHand>().StoreHandData(_localHandL.position, _localHandL.localRotation);
            // update finger rots
            OVRPlugin.HandState handStateL = default(OVRPlugin.HandState);
            if (OVRPlugin.GetHandState(OVRPlugin.Step.Render, OVRPlugin.Hand.HandLeft, ref handStateL))
            {
                for (int i = 0; i < MPHandL.GetComponent<MPHand>()._FingerRots.Length; i++)
                {
                    //MPHandL.GetComponent<MPHand>().StoreFingerRotations(i, FromQuatf(handStateL.BoneRotations[i]));
                    MPHandL.GetComponent<MPHand>().StoreFingerRotations(i, _localFingerRotsL[i].localRotation);
                }
            }
        }
       
        if (!ActiveL && LGenerated)
        {
            Realtime.Destroy(MPHandL);
            LGenerated = false;
        }
        

        if (ActiveR && !RGenerated)
        {
            // generate hand R
            MPHandR = Realtime.Instantiate(_handRPrefab.name, new Realtime.InstantiateOptions
            {
                ownedByClient = true,
                preventOwnershipTakeover = true,
                destroyWhenOwnerLeaves = true,
                destroyWhenLastClientLeaves = true,
                useInstance = _realtime,
            });
            RGenerated = true;

            // initialize finger rotation model
            MPHandR.GetComponent<MPHand>().CreateFingerModelRotations();
        }
        if (RGenerated)
        {
            // update hand pos & rot
            MPHandR.GetComponent<MPHand>().StoreHandData(_localHandR.position, _localHandR.localRotation);
            // update finger rots
            OVRPlugin.HandState handStateR = default(OVRPlugin.HandState);
            if (OVRPlugin.GetHandState(OVRPlugin.Step.Render, OVRPlugin.Hand.HandRight, ref handStateR))
            {
                for (int i = 0; i < MPHandR.GetComponent<MPHand>()._FingerRots.Length; i++)
                {
                    //MPHandR.GetComponent<MPHand>().StoreFingerRotations(i, FromQuatf(handStateR.BoneRotations[i]));
                    MPHandR.GetComponent<MPHand>().StoreFingerRotations(i, _localFingerRotsR[i].localRotation);
                }
            }
        }

        if (!ActiveR && RGenerated)
        {
            Realtime.Destroy(MPHandR);
            RGenerated = false;
        }

    }


    internal static Quaternion FromQuatf(OVRPlugin.Quatf q)
    {
        return new Quaternion() { x = q.x, y = q.y, z = q.z, w = q.w };
    }
    internal static Vector3 FromVector3f(OVRPlugin.Vector3f v)
    {
        return new Vector3() { x = v.x, y = v.y, z = v.z };
    }


}
