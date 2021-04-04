using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UnityEngine;

public class TestColliderTrigger : MonoBehaviour {
    [SerializeField]
    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        _transform.localPosition = Vector3.zero;
        DebugOnly.Message("ON TRIGGER");
    }
}
