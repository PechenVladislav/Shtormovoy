using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 10f;


    private Vector3 cameraChase;

	// Update is called once per frame
	void Update () {


        cameraChase = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, cameraChase, speed * Time.deltaTime);

	}
}
