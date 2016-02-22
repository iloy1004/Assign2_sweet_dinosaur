﻿/*----------------------------------------------------------------------------
Source file name: BunnyController.cs
Author's name: Jihee Seo
Last modified by: Jihee Seo
Last modified date: Feb 05, 2016
Program description: alien is a player in this game, and this script is for controlling bunny. Here is controlling of movement, colision.
Revision history: 0.0 - Created document, and made basic methods, Start and Update()
                  0.1 - Added reset method
                  1.0 - Added Trigger Event for destruction
                  1.1 - Added animation of explosion
----------------------------------------------------------------------------*/

using UnityEngine;
using System.Collections;

public class BunnyController : MonoBehaviour {
    //PRIVATE INSTANCE VARIABLES
    
    private float _move;
    private float _jump;
    private bool _facingRight;

    //PRIVATE VARIABLES
    private Transform _transform;
    private Transform _tagGround;
    private Rigidbody2D _myBody;
    private float _hInput;
    private Vector3 _artScaleCache;
    AnimatorControllerJS myAnim = AnimatorControllerJS._instance;
    bool _isGrounded = false;

    //PUBLIC VARIABLES
    public float _speed = 10f;
    public float _jumpVelocity = 10f;
    public bool _canMoveInAir = true;
    public LayerMask _payerMask;
    int layerMask = 1 << 8;

    // Use this for initialization
    void Start () {
        //Set default value for each variables
        this._myBody = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        myAnim = AnimatorControllerJS._instance;
        layerMask = ~layerMask;

        this._move = 0f;
        this._jump = 0f;
        this._facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {

        this._isGrounded = Physics2D.Linecast(_transform.position, this._tagGround.position, _payerMask);
        Debug.Log(this._isGrounded);
        myAnim.UpdateIsGrounded(this._isGrounded);

        //if user play the game via NOT mobile phone, run this code
        #if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
        this._hInput = Input.GetAxisRaw("Horizontal");
        myAnim.UpdateSpeed(this._hInput);
        
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        #endif

        Move(this._hInput);

        
        /*
        this._move = Input.GetAxis("Horizontal");
        this._jump = Input.GetAxis("Vertical");

        if (this._move !=0)
        {
            //Set facingRight when user input Horizontal key
            if(this._move > 0)
            {
                this._facingRight = true;
            }
            if(this._move < 0)
            {
                this._facingRight = false;
            }
            //set animation as walking
            this._animator.SetInteger("Anim_state", 1);
        }
        //set animation as stand
        else
        {
            this._animator.SetInteger("Anim_state", 0);
        }

        if(this._jump>0)
        {
            //set animation as jumping
            this._animator.SetInteger("Anim_state", 2);
        }

        this._flip();
        //this.Movement();

        */
    }



    public void Jump()
    {
        if(this._isGrounded)
        {
            this._myBody.velocity += this._jumpVelocity * Vector2.up;
        }
    }
  
    public void Move(float horizontalInput)
    {
        if(!this._canMoveInAir && !this._isGrounded)
        {
            return;
        }

        Vector2 moveVel = this._myBody.velocity;
        moveVel.x = horizontalInput * this._speed;

        this._myBody.velocity = moveVel;
    }

    public void StartMoving(float horizonalInput)
    {
        this._hInput = horizonalInput;
        myAnim.UpdateSpeed(horizonalInput);
    }
}
