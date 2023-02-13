using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   
        {
            var charControl = other.gameObject.GetComponent<CharacterController>();
            charControl.detectable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var charControl = other.gameObject.GetComponent<CharacterController>();
        charControl.detectable = true;
    }
}
