using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    [SerializeField] private Transform pointOne;
    [SerializeField] private Transform pointTwo;
    [SerializeField] private Transform lookOne;
    [SerializeField] private Transform lookTwo;

    [SerializeField] private float contLimit = 10;
    [SerializeField] private float cont = 0;
    [SerializeField] private bool caseOneBool = true;
    [SerializeField] private bool walking = false;

    [SerializeField] private float movementSpeed = 4;
    [SerializeField] private float rotationSpeed = 10;

    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Transform SignOne = transform.Find("SignOne");
        Transform SignTwo = transform.Find("SignTwo");

    }

    // Update is called once per frame
    void Update()
    {
        selectObject();
        LookForPlayer();
    }

    private void selectObject() 
    {
        cont += Time.deltaTime;
        var characterToCube = pointOne.position - transform.position;
        if (!caseOneBool)
        {
            moveToCube(pointTwo, lookTwo);
        }
        else
        {
            moveToCube(pointOne, lookOne);
        }
        if (cont >= contLimit) 
        {
            cont = 0;
            walking = true;
            if (!caseOneBool)
            {
                caseOneBool = true;
            }
            else {
                caseOneBool = false;
            }
        }
    }
    private void moveToCube(Transform cube, Transform look)
    {
        var characterToCube = cube.position - transform.position;
        var distance = characterToCube.magnitude;
        if (distance >= 0.3)
        {
            anim.SetBool("Walk", true);
            // move
            transform.position += characterToCube.normalized * (Time.deltaTime * movementSpeed);
            // rotate
            var newRotation = Quaternion.LookRotation(characterToCube);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }
        else {
            anim.SetBool("Walk", false);
            var characterToLook = look.position - transform.position;
            var newRotation = Quaternion.LookRotation(characterToLook);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

        }
    }
    private CharacterController LookForPlayer()
    {
        if (CharacterController.Instance == null)
        {
            return null;
        }

        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = CharacterController.Instance.transform.position - enemyPosition;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                if (CharacterController.Instance.detectable == true) 
                {
                    Debug.Log("Player has been detected!");
                    return CharacterController.Instance;
                }
            }
        }


        return null;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f, 0, 0, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius);

    }
#endif
}
