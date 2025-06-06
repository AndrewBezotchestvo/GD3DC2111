﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;

    private bool _isGround;
    private bool _isJump;

    private Vector3 _movement; //смещение в мировых кординатах

    private Vector3 _previosPosition;

    private float _velosity;

    private float _rotationX;

    private void Start()
    {
        _previosPosition = transform.position;
        _rotationX = transform.rotation.x;
        _isJump = false;
        _rb = GetComponent<Rigidbody>();
        _movement = Vector3.forward;  //настройка смещения для движения вперед
    }

    private void FixedUpdate()
    {
        _velosity = (_previosPosition - transform.position).magnitude / Time.fixedDeltaTime;
        
        Debug.Log(_velosity);
        
        _previosPosition = transform.position;

        //установить позицию героя  т.е. текущее положение + смещение умноженное на скорость и на изменение времени
        _rb.MovePosition(transform.position + _movement * _speed * Time.fixedDeltaTime); 

        if (_isJump)
        {
            _isJump = false;
            _movement.y = _jumpForce;
        }

        if (!_isGround)
        {
            _movement.y -= _gravityScale * Time.fixedDeltaTime;
        }
        
        if (_isGround)
        {
            _movement.y = 0;
        }

        if (_velosity < 1 && transform.position.z >= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (_velosity > 40 && transform.position.z >= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _isGround)
        {
            _isGround = false;
            _isJump = true;
            StartCoroutine(rotateCube());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            _isGround = true;
        }

        if (collision.gameObject.tag == "kill")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator rotateCube()
    {
        for (int i = 0; i < 90; i++)
        {
            _rotationX += 1;
            transform.rotation = Quaternion.Euler(_rotationX, 0, 0);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        /*float rotationStep = 1f; // Шаг вращения за кадр
        float targetRotation = _rotationX + 90f; // Целевой угол

        while (_rotationX < targetRotation)
        {
            _rotationX += rotationStep;
            transform.rotation = Quaternion.Euler(_rotationX, 0, 0);
            yield return null; // Ждём 1 кадр (не зависит от FPS)
        }*/
    }
}
