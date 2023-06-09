using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);
    public GameObject player;

    // Update is called once per frame
    void LateUpdate()  //Update() ���� ���� ȣ��� --> ������ �����̰��� ȣ���
    {
        transform.position = player.transform.position+offset;
    }
}
