using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerJoystickController : MonoBehaviour
{
    public float _moveSpeed;

    [SerializeField] private FixedJoystick _joystick;
    private Rigidbody2D _rigidbody;
    public Animator anim;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed);
    }

}