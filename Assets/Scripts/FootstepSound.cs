using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] private Foot _leftFoot;
    [SerializeField] private Foot _rightFoot;
    [SerializeField] private AudioClip[] _grassSounds;
    [SerializeField] private AudioClip[] _groundSounds;
    [SerializeField] private AudioClip[] _rockSounds;
    private AudioSource _audioSource;
    private readonly List<AudioClip[]> _sounds=new List<AudioClip[]>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _leftFoot.FootStep.AddListener(LeftFootstep);
        _rightFoot.FootStep.AddListener(RightFootstep);
        _sounds.Add(_grassSounds);
        _sounds.Add(_groundSounds);
        _sounds.Add(_rockSounds);
    }

    private void LeftFootstep()
    {
        FootstepSoundPlay(_leftFoot.transform.position);
    }

    private void RightFootstep()
    {
        FootstepSoundPlay(_rightFoot.transform.position);
    }
    

    private void FootstepSoundPlay(Vector3 pozition)
    {
        var terrainLayer= TerrainSurface.GetMainTexture(pozition);
        _audioSource.PlayOneShot(_sounds[terrainLayer][Random.Range(0, _sounds[terrainLayer].Length)]);
    }
}
