using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UnityEngine;

public class TestColliderTrigger : MonoBehaviour {
    [SerializeField]
    private Transform _transform;
    private void OnTriggerEnter(Collider other) {
        _transform.localPosition = Vector3.zero;
    }
}
