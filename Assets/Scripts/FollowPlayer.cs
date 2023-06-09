using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);
    public GameObject player;

    // Update is called once per frame
    void LateUpdate()  //Update() 보다 이후 호출됨 --> 차량이 움직이고나서 호출됨
    {
        transform.position = player.transform.position+offset;
    }
}
