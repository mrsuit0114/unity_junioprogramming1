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
        playerRb.centerOfMass = centerOfMass.transform.position;  //rb�� �ִ� �ʵ��ΰ�����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        brake = 0;
        horizontalInput = Input.GetAxis("Horizontal"); //�� ������Ʈ�� �δ°��� �𸣰ڴµ� --> �ǽð����� ������ �ޱ����ؼ�
        verticalInput = Input.GetAxis("Vertical");  //�������� �η��ϸ� ���� ���ٰ� �����߳�
        //Move the vehicle forward
        /*transform.Translate(0, 0, 1); �̹���� deltaTime ���ϱⰡ �ѹ����ȵ�  */
        /*transform.Translate(Vector3.forward *Time.deltaTime*speed * forwardInput);*/
        /*transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);*/
        if (IsOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * turnSpeed * horizontalInput); // up�� (0,1,0)
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
