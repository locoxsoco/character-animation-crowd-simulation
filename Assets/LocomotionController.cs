using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LocomotionController : MonoBehaviour
{
    private Animator _animator;
    private TrackerController _trackerController;
    private float _velocityX = 0.0f;
    private float _velocityZ = 0.0f;
    public float acceleration = 0.5f;
    public float deceleration = 1.0f;
    public float maxWalkVelocity = 1.5935f;
    public float minWalkVelocity = -0.5f;
    public float maxRunVelocity = 4.1249f;
    public Vector3 orientation;
    private int _velocityXHash, _velocityZHash;
    // Start is called before the first frame update
    void Start()
    {
        // set reference for animator
        _animator = GetComponent<Animator>();
        // set reference for tracker
        _trackerController = GetComponent<TrackerController>();
        
        // increases performance
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
        
        orientation = _trackerController.orientation;
        maxWalkVelocity = 1.5935f;
        minWalkVelocity = 0.5f;
        maxRunVelocity = 4.1249f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        orientation = _trackerController.orientation.normalized;
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
                if (_velocityZ < maxRunVelocity)
                {
                    _velocityZ += Time.deltaTime * acceleration;
                }
                else
                {
                    _velocityZ = maxRunVelocity;
                }
            }
            else
            {
                if (_velocityZ < maxWalkVelocity)
                {
                    _velocityZ += Time.deltaTime * acceleration;
                }
                else if (_velocityZ > maxWalkVelocity)
                {
                    _velocityZ -= Time.deltaTime * deceleration;
                }
            }
        }
        else if (backPressed)
        {
            if (runPressed)
            {
                if (_velocityZ > -maxRunVelocity)
                {
                    _velocityZ -= Time.deltaTime * acceleration;
                }
                else
                {
                    _velocityZ = -maxRunVelocity;
                }
            }
            else
            {
                if (_velocityZ > -minWalkVelocity)
                {
                    _velocityZ -= Time.deltaTime * acceleration;
                }
                else if (_velocityZ < -minWalkVelocity)
                {
                    _velocityZ += Time.deltaTime * deceleration;
                }
            }
        }
        else
        {
            if (_velocityZ > 0.08)
            {
                _velocityZ -= Time.deltaTime * deceleration;
            }
            else if(_velocityZ < -0.08)
            {
                _velocityZ += Time.deltaTime * deceleration;
            }
            else
            {
                _velocityZ = 0;
            }
        }
        transform.position += orientation * Time.deltaTime * _velocityZ;

        if (rightPressed)
        {
            if (runPressed)
            {
                if (_velocityX < maxRunVelocity)
                {
                    _velocityX += Time.deltaTime * acceleration;
                }
                else
                {
                    _velocityX = maxRunVelocity;
                }
            }
            else
            {
                if (_velocityX < maxWalkVelocity)
                {
                    _velocityX += Time.deltaTime * acceleration;
                }
                else if (_velocityX > maxWalkVelocity)
                {
                    _velocityX -= Time.deltaTime * deceleration;
                }
            }
        }
        else if (leftPressed)
        {
            if (runPressed)
            {
                if (_velocityX > -maxRunVelocity)
                {
                    _velocityX -= Time.deltaTime * acceleration;
                }
                else
                {
                    _velocityX = -maxRunVelocity;
                }
            }
            else
            {
                if (_velocityX > -maxWalkVelocity)
                {
                    _velocityX -= Time.deltaTime * acceleration;
                }
                else if (_velocityX < -maxWalkVelocity)
                {
                    _velocityX += Time.deltaTime * deceleration;
                }
            }
        }
        else
        {
            if (_velocityX > 0.08)
            {
                _velocityX -= Time.deltaTime * deceleration;
            }
            else if(_velocityX < -0.08)
            {
                _velocityX += Time.deltaTime * deceleration;
            }
            else
            {
                _velocityX = 0;
            }
        }
        transform.position +=  Quaternion.Euler(0, 90, 0) * orientation * Time.deltaTime * _velocityX;
        
        // filtro de smooth para que la interpolacion sea mejor
        // igual que la orientacion, lo mismo un lerp
        // eso es porque los movimiento manuales son bruscos
        _animator.SetFloat(_velocityXHash,_trackerController.local_velocity.x);
        _animator.SetFloat(_velocityZHash,_trackerController.local_velocity.z);
    }
}
