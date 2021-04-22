using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform targetPoint;

    public float moveSpeed = 8f, rotateSpeed = 3f;
    public float xAway = -5, yAway = -5, zAway = -5;

    private Vector3 vXYZAway;

    private void Awake()
    {
        instance = this;
        vXYZAway = new Vector3(xAway, yAway , zAway);


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetPoint.rotation, rotateSpeed * Time.deltaTime);
        
    }
}
