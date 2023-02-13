using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    public enum typeOfCrate {
        one,
        two,
        three,
    }

    public typeOfCrate currentCrate;

    [SerializeField] private float contLimitUD = 1;
    [SerializeField] private float contUDown = 0;
    [SerializeField] private float speedUD = 0.1f;
    [SerializeField] private bool boolUD = true;

    [SerializeField] private Transform pointOne;
    [SerializeField] private Transform pointTwo;
    [SerializeField] private float contLimit = 2;
    [SerializeField] private float cont = 0;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private bool boolMoveToFirst = true;
    [SerializeField] private bool startMoving = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        contUDown += Time.deltaTime;
        switch (currentCrate)
        {
            case typeOfCrate.one:
                moveCrateUpDown();
                break;
            case typeOfCrate.two:
                cont += Time.deltaTime;
                moveCrateUpDown();
                if (boolMoveToFirst)
                {
                    moveToCube(pointOne);
                }
                else {
                    moveToCube(pointTwo);
                }
                break;
            case typeOfCrate.three:
                cont += Time.deltaTime;
                moveCrateUpDown();
                if (startMoving) 
                {
                    if (boolMoveToFirst)
                    {
                        moveToCube(pointOne);
                    }
                    else
                    {
                        moveToCube(pointTwo);
                    }
                }
                break;
            default:
                break;
        }

        if (contUDown >= contLimitUD) {
            contUDown = 0;
            if (boolUD)
            {
                boolUD = false;
            }
            else {
                boolUD = true;
            }

        }
        if (cont >= contLimit)
        {
            cont = 0;
            if (boolMoveToFirst)
            {
                boolMoveToFirst = false;
            }
            else
            {
                boolMoveToFirst = true;
            }

        }
    }

    void moveCrateUpDown() 
    {
        var nextPosition = new Vector3(0f, 0f, 0f);
        if (boolUD)
        {
            nextPosition = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        }
        else
        {
            nextPosition = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }
        var positionCrate = nextPosition - transform.position;
        // move
        transform.position += positionCrate.normalized * (Time.deltaTime * speedUD);
    }

    private void moveToCube(Transform cube)
    {
        var characterToCube = cube.position - transform.position;
        var distance = characterToCube.magnitude;
        if (distance >= 0.3)
        {
            // move
            transform.position += characterToCube.normalized * (Time.deltaTime * movementSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            startMoving = true;
        }
    }
}
