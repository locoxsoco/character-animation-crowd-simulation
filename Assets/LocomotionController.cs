using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionController : MonoBehaviour
{
    private Animator _animator;
    private float _velocityX = 0.0f;
    private float _velocityZ = 0.0f;
    public float acceleration = 0.5f;
    public float deceleration = 1.0f;
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
        _velocityZHash = Animator.StringToHash("Velocity Z");
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

        if (forwardPressed)
        {
            if (runPressed)
            {
                if (_velocityZ < maximumRunVelocity)
                {
                    _velocityZ += Time.deltaTime * acceleration;
                }
                else
                {
                    _velocityZ = maximumRunVelocity;
                }
            }
            else
            {
                if (_velocityZ < maximumWalkVelocity)
                {
                    _velocityZ += Time.deltaTime * acceleration;
                }
                else if (_velocityZ > maximumWalkVelocity)
                {
                    _velocityZ -= Time.deltaTime * deceleration;
                }
            }
        }
        else
        {
            if (_velocityZ > 0)
            {
                _velocityZ -= Time.deltaTime * deceleration;
            }
            else
            {
                _velocityZ = 0;
            }
        }
        if (backPressed)
        {
            if (runPressed)
            {
                if (_velocityZ > -maximumRunVelocity)
                {
                    _velocityZ -= Time.deltaTime * acceleration;
                }
                else
                {
                    _velocityZ = maximumRunVelocity;
                }
            }
            else
            {
                if (_velocityZ > maximumWalkVelocity)
                {
                    _velocityZ -= Time.deltaTime * acceleration;
                }
                else if (_velocityZ < maximumWalkVelocity)
                {
                    _velocityZ += Time.deltaTime * deceleration;
                }
            }
        }
        else
        {
            if (_velocityZ < 0)
            {
                _velocityZ += Time.deltaTime * deceleration;
            }
            else
            {
                _velocityZ = 0;
            }
        }
        _animator.SetFloat(_velocityXHash,_velocityX);
        _animator.SetFloat(_velocityZHash,_velocityZ);
    }
}
