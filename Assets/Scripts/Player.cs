using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 9.0f, _pushSpeed = 3f;
    [SerializeField]
    private float _gravity = .3f;
    [SerializeField]
    private float _jumpHeight = 25.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;
    private Vector3 velocity, direction, _wallJumpNormal;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
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
        

        if (_controller.isGrounded == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            direction = new Vector3(horizontalInput, 0, 0);
            velocity = direction * _speed;
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
                 if (_canDoubleJump == true)
                {
                    float horizontalInput = Input.GetAxis("Horizontal");
                    direction = new Vector3(horizontalInput, 0, 0);
                    velocity = direction * _speed;
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }

            }

            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
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

    public int CheckCoins()
    { return _coins; }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!_controller.isGrounded && hit.transform.CompareTag("Wall"))
        {
            _wallJumpNormal = hit.normal;
          

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                    direction = hit.normal;
                    velocity = direction * _speed;
                    _yVelocity = _jumpHeight / 1.2f; 
                

            }
        }

        if (_controller.isGrounded && hit.transform.CompareTag("Box"))
        {
            Rigidbody _body = hit.collider.attachedRigidbody;
            if (_body == null || _body.isKinematic)
                return;
            Vector3 _pushdirection = new Vector3(hit.moveDirection.x, 0, 0);
            _body.velocity = _pushdirection * _pushSpeed;
        }
    }
}
