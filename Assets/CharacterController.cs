using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator _animator;
    private float _velocityX = 0.0f;
    private float _velocityZ = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    private int _velocityXHash, _velocityZHash;
    
    // Start is called before the first frame update
    void Start()
    {
        // set reference for animator
        _animator = GetComponent<Animator>();
        
        // increases performance
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityXHash = Animator.StringToHash("Velocity Z");
    }
    
    // Handles acceleration and deceleration
    void changeVelocity(bool forwardPressed, bool leftPressed, bool backPressed, bool rightPressed,
        bool runPressed, float currentMaxVelocity)
    {
        
    }
    
    // Handles reset and locking velocity
    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool backPressed, bool rightPressed,
        bool runPressed, float currentMaxVelocity)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get key input from player
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool backPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        
        // set current maxVelocity
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;
        
        // handles changes in velocity
        changeVelocity(forwardPressed, leftPressed, backPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, backPressed, rightPressed, runPressed, currentMaxVelocity);

        _animator.SetFloat(_velocityXHash,_velocityX);
        _animator.SetFloat(_velocityZHash,_velocityZ);
    }
}
