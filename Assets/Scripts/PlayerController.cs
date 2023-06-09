using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    /*[SerializeField] private float speed = 20;*/
    [SerializeField] private float horsePower = 0;
    [SerializeField] private float turnSpeed = 5;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] float speed;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;
    private float brake;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;  //rb에 있는 필드인가보네
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        brake = 0;
        horizontalInput = Input.GetAxis("Horizontal"); //왜 업데이트에 두는건지 모르겠는데 --> 실시간으로 방향을 받기위해서
        verticalInput = Input.GetAxis("Vertical");  //전역으로 두려하면 아직 없다고 에러뜨네
        //Move the vehicle forward
        /*transform.Translate(0, 0, 1); 이방식은 deltaTime 곱하기가 한번에안돼  */
        /*transform.Translate(Vector3.forward *Time.deltaTime*speed * forwardInput);*/
        /*transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);*/
        if (IsOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * turnSpeed * horizontalInput); // up는 (0,1,0)
        }

        if (Input.GetKey(KeyCode.Space))
        {
            brake = 1000000;
        }
        foreach (WheelCollider wheel in allWheels)
        {
            wheel.brakeTorque = brake;
        }
    }

    private void Update()
    {
        speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f);  //  3.6 for kmh 
        speedometerText.text = "Speed: " + speed + "km/h";
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
                wheelsOnGround++;
        }
        return wheelsOnGround != 0;
    }
}
