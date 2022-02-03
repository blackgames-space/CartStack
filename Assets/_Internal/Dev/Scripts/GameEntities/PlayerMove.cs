using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    CharacterController _characterController;
    CollisionFlags _collisionFlags;

    [SerializeField]
    float _maxOffset = 1f;

    [SerializeField]
    public float g = 9.8f;

    //GroundChecker _groundChecker;

    float _deltaX = 0f;

    public float originSpeed = 2f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //_groundChecker = GetComponentInChildren<GroundChecker>();

        _deltaX = 0f;
    }

    public void Move(float deltaX = 0)
    {
        //_collisionFlags = _characterController.Move(new Vector3(deltaX * Time.fixedDeltaTime * bouquetSpeed, 0f, Time.fixedDeltaTime * bouquetSpeed));
        //Debug.Log(_deltaX);
        //Debug.Log(deltaX);
        _collisionFlags = _characterController.Move(new Vector3((deltaX + _deltaX) * Time.fixedDeltaTime, -g, originSpeed * Time.fixedDeltaTime));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_maxOffset, _maxOffset), transform.position.y, transform.position.z);
        //transform.position += new Vector3(deltaX * Time.fixedDeltaTime, 0f, 0f);
    }

    public float GetOffset()
    {
        return _maxOffset;
    }
}
