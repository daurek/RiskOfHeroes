using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    [Header("Camera Attributes")]
    [SerializeField] private float cameraOffsetY = 2.5f;
    [Header("Smooth Follow")]
    [SerializeField] private bool smoothFollow;
    [SerializeField] private float damp = 1.5f;
    

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void FixedUpdate ()
    {
        if (player)
        {
            Vector3 playerVector = new Vector3(player.transform.position.x, player.transform.position.y + cameraOffsetY, transform.position.z);
            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(transform.position, playerVector, damp * Time.deltaTime);
            }
            else
            {
                transform.position = playerVector;
            }
            
        }
	}
}
