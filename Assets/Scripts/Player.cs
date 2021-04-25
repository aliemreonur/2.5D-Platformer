using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private float _speed = 5f;
    private float _horizontalInput;
    [SerializeField] private float _gravity = 1.3f;
    [SerializeField] private float _jumpHeight = 25f;
    private float _yVelocity;
    private bool _canDooubleJump;

    private int coins;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();        
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(_horizontalInput, 0, 0) ;
        Vector3 velocity = direction * _speed;
        
        if(_characterController.isGrounded)
        { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDooubleJump = true;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(_canDooubleJump)
                {
                    _yVelocity += _jumpHeight;
                    _canDooubleJump = false;
                }
            }
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _characterController.Move(velocity * Time.deltaTime);

        if(transform.position.y < -5)
        {
            Debug.Log("Game Over");
        }

    }

    public void CoinCollected()
    {
        coins++;
        UIManager.Instance.UpdateScore(coins);
    }
}
