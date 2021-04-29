using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _cc;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;
    private Vector3 _wallSurfaceNormal;
    private float _pushForce = 2f;

    [SerializeField] bool _canWallJump;

    private Vector3 _direction, _velocity;

    public int Coins
    {
        get
        {
            return _coins;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_cc.isGrounded == true)
        {
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;
            _canWallJump = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump && !_canWallJump)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
                else if(_canWallJump)
                {
                    _velocity = _wallSurfaceNormal *_speed;
                    _yVelocity = _jumpHeight;
                }
            }
            _yVelocity -= _gravity;
        }
        _velocity.y = _yVelocity;
        _cc.Move(_velocity * Time.deltaTime);
    }

    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!_cc.isGrounded && hit.transform.tag == "Wall")
        {
            //Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }
        if(_cc.isGrounded && hit.transform.tag =="Moveable")
        {
            Rigidbody rb = hit.rigidbody;
            if(rb == null)
            {
                Debug.LogError("The hitted object I am trying to push does not have a Rigidbody component");
            }
            else
            {
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
                rb.velocity = pushDir * _pushForce; 
            }        
        }
     
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
