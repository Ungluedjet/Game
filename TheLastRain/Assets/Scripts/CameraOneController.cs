using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOneController : MonoBehaviour
{
    [SerializeField] private Transform characterToFollow;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 _offset;

    private void Awake() 
    {
    
    }

    private void LateUpdate() 
    {
        transform.position = characterToFollow.position - _offset;
    }
}
