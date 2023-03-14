using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseState : MonoBehaviour
{

    public OVRHand Hand { get; private set; } = null;
    public Transform _handTrans;
    private bool _isPointing;
    private List<AudioClip> _randomAudioClipPool = new List<AudioClip>();
    private AudioSource _audioSource = null;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxVolume = 1f;

    private Vector3 prevPos;

    private AudioClip _previousAudioClip = null;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private GameObject musicObject;

    // Start is called before the first frame update
    void Start()
    {
        _isPointing = false;
        Hand = GetComponent<OVRHand>();
        prevPos = _handTrans.position;
        // Add all audio clips in the populated array into an audio clip list for randomization purposes
        for (int i = 0; i < _audioClips.Length; i++)
        {
            _randomAudioClipPool.Add(_audioClips[i]);
        }
    }

    private void Update()
    {
        if (_isPointing == true)
        {
            // Debug.Log("ispointing");
            // Calculate the speed of the GameObject
            float speed = (_handTrans.position - prevPos).magnitude / Time.deltaTime;
            speed = Mathf.Clamp(speed, 0f, maxSpeed);
            prevPos = _handTrans.position;

            // Map the speed to a volume value
            float volume = speed / maxSpeed * maxVolume;

            // Set the volume of the AudioSource
            _audioSource.volume = volume;
        }
        else
        {

        }

    }

    public void StateActivated()
    {
        _isPointing = true;
        // Play the audio
        GameObject obj = Instantiate(musicObject);
        int randomIndex = Random.Range(0, _randomAudioClipPool.Count);
        _audioSource = obj.GetComponent<AudioSource>();
        _audioSource.clip = RandomClipWithoutRepeat();
        _audioSource.loop = true;
        _audioSource.Play();
    }
    public void StateDeactivated()
    {
        _isPointing = false;
    }

    private AudioClip RandomClipWithoutRepeat()
    {
        int randomIndex = Random.Range(0, _randomAudioClipPool.Count);
        AudioClip randomClip = _randomAudioClipPool[randomIndex];
        _randomAudioClipPool.RemoveAt(randomIndex);
        if (_previousAudioClip != null)
        {
            _randomAudioClipPool.Add(_previousAudioClip);
        }
        _previousAudioClip = randomClip;
        return randomClip;
    }
}
