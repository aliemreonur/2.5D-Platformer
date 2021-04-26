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

    Vector3 _spawnPoint;

    private int _coins;
    private int _lives = 3;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _spawnPoint = new Vector3(-7f, 2.43f, 0);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(_horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDooubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDooubleJump)
                {
                    _yVelocity += _jumpHeight;
                    _canDooubleJump = false;
                }
            }
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void Death()
    {

    }

    public void CoinCollected()
    {
        _coins++;
        UIManager.Instance.UpdateScore(_coins);
    }

    IEnumerator DeathRoutine()
    {
        for(int i = 0; i<2; i++)
        {
            transform.position = new Vector3(-7.5f, 2.4f, 0);
            _lives--;
            UIManager.Instance.UpdateLives(_lives);
            yield return new WaitForSeconds(0.5f);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathZone")
        {
            _characterController.enabled = false;
            _lives--;
            UIManager.Instance.UpdateLives(_lives);
            transform.position = _spawnPoint;
            _characterController.enabled = true;
            if (_lives <= 0)
            {
                UIManager.Instance.GameOver(_coins);
                Debug.Log("Game Over");
            }
            
        }
    }

}
