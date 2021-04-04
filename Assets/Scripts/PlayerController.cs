using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200000E RID: 14
public class PlayerController : MonoBehaviour
{
	// Token: 0x0600003B RID: 59 RVA: 0x000030D3 File Offset: 0x000012D3
	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.maxHealth = this.health;
		this.baseSpeed = this.startBaseSpeed;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000030F9 File Offset: 0x000012F9
	private void Start()
	{
		this.playerScale = base.transform.localScale;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		this.allowDrag = true;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000311F File Offset: 0x0000131F
	private void FixedUpdate()
	{
		this.Movement();
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003128 File Offset: 0x00001328
	private void Update()
	{
		this.MyInput();
		if (!this.lockLook)
		{
			this.Look();
		}
		this.CheckForWall();
		if (Input.GetKeyDown(KeyCode.L))
		{
			SceneManager.LoadScene(1);
		}
		if (this.currentVelocityDisplay != null)
		{
			this.currentVelocityDisplay.SetText("currVel: " + Mathf.RoundToInt(this.rb.velocity.magnitude));
		}
		if (this.baseVelocityDisplay != null)
		{
			this.baseVelocityDisplay.SetText("baseVel: " + this.baseSpeed);
		}
		if (this.recordVelocityDisplay != null)
		{
			this.recordVelocityDisplay.SetText("recordVel: " + this.speedRecord);
		}
		if (this.speedRecord < this.rb.velocity.magnitude)
		{
			this.speedRecord = (float)Mathf.RoundToInt(this.rb.velocity.magnitude);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			this.speedRecord = 0f;
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003248 File Offset: 0x00001448
	private void MyInput()
	{
		this.x = Input.GetAxisRaw("Horizontal");
		this.y = Input.GetAxisRaw("Vertical");
		this.jumping = Input.GetButton("Jump");
		this.crouching = Input.GetKey(KeyCode.LeftShift);
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			this.StartCrouch();
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			this.StopCrouch();
		}
		if (this.readyToJump && this.jumping && this.grounded)
		{
			this.Jump();
		}
		if (Input.GetButtonDown("Jump") && !this.grounded && this.doubleJumpsLeft >= 1)
		{
			this.Jump();
		}
		if (Input.GetButtonDown("Jump") && this.wallJumpsLeft >= 1 && this.isWallRunning)
		{
			this.Jump();
		}
		if (Input.GetKeyDown(KeyCode.W) && this.wTapTimes <= 1)
		{
			this.wTapTimes++;
			base.Invoke("ResetTapTimes", 0.3f);
		}
		if (this.wTapTimes == 2 && this.readyToDash)
		{
			this.Dash();
		}
		if (this.isWallRight && !this.grounded && this.readyToWallrun)
		{
			this.StartWallrun();
		}
		if (this.isWallLeft && !this.grounded && this.readyToWallrun)
		{
			this.StartWallrun();
		}
		if (!this.isWallRight && !this.isWallLeft && !this.readyToWallrun)
		{
			this.readyToWallrun = true;
		}
		if (Physics.Raycast(base.transform.position, this.orientation.forward, 1f, this.whatIsLadder) && this.y > 0.9f)
		{
			this.Climb();
		}
		else
		{
			this.alreadyStoppedAtLadder = false;
		}
		if (Input.GetKeyDown(KeyCode.LeftControl) && this.readyForSlowMo)
		{
			this.StartSlowMo();
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003422 File Offset: 0x00001622
	private void ResetTapTimes()
	{
		this.wTapTimes = 0;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000342C File Offset: 0x0000162C
	private void Movement()
	{
		float d = 10f;
		if (this.crouching)
		{
			d = this.crouchGravityMultiplier;
		}
		this.rb.AddForce(Vector3.down * Time.deltaTime * d);
		Vector2 vector = this.FindVelRelativeToLook();
		float num = vector.x;
		float num2 = vector.y;
		this.CounterMovement(this.x, this.y, vector);
		if (this.grounded)
		{
			this.readyToDash = true;
			this.doubleJumpsLeft = this.startDoubleJumps;
			this.wallJumpsLeft = this.wallJumps;
		}
		float num3 = this.baseSpeed;
		if (this.crouching && this.grounded && this.readyToJump)
		{
			this.rb.AddForce(Vector3.down * Time.deltaTime * 3000f);
			return;
		}
		if (this.crouching && this.onSlope)
		{
			this.rb.AddForce(Vector3.down * Time.deltaTime * this.slopeDownwardForce);
		}
		if (this.x > 0f && num > num3)
		{
			this.x = 0f;
		}
		if (this.x < 0f && num < -num3)
		{
			this.x = 0f;
		}
		if (this.y > 0f && num2 > num3)
		{
			this.y = 0f;
		}
		if (this.y < 0f && num2 < -num3)
		{
			this.y = 0f;
		}
		float d2 = 1f;
		float d3 = 1f;
		if (!this.grounded && !this.fullAirControl)
		{
			d2 = 0.5f;
			d3 = 0.5f;
		}
		if (this.fullAirControl)
		{
			d2 = 0.35f;
		}
		if (this.grounded && this.crouching)
		{
			d3 = 0f;
		}
		this.rb.AddForce(this.orientation.transform.forward * this.y * this.moveSpeed * Time.deltaTime * d2 * d3);
		this.rb.AddForce(this.orientation.transform.right * this.x * this.moveSpeed * Time.deltaTime * d2);
		if (this.rb.velocity.magnitude > this.baseSpeed + this.bSAccelPoint)
		{
			this.IncreaseBaseSpeed();
		}
		if (this.rb.velocity.magnitude < this.baseSpeed - this.bSDeccelPoint)
		{
			this.DecreaseBaseSpeed();
		}
		if (this.rb.velocity.magnitude > this.baseSpeed + this.slowDownPoint)
		{
			this.SlowDown();
			return;
		}
		this.rb.drag = 0f;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003718 File Offset: 0x00001918
	private void StartCrouch()
	{
		base.transform.localScale = this.crouchScale;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.5f, base.transform.position.z);
		if (this.rb.velocity.magnitude > 0.5f && this.grounded)
		{
			this.rb.AddForce(this.orientation.transform.forward * this.slideForce);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000037C4 File Offset: 0x000019C4
	private void StopCrouch()
	{
		base.transform.localScale = this.playerScale;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, base.transform.position.z);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003828 File Offset: 0x00001A28
	private void Jump()
	{
		if (this.grounded)
		{
			this.readyToJump = false;
			this.rb.AddForce(Vector3.up * this.jumpForce * 1.5f);
			this.rb.AddForce(this.normalVector * this.jumpForce * 0.5f);
			Vector3 velocity = this.rb.velocity;
			if (this.rb.velocity.y < 0.5f)
			{
				this.rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
			}
			else if (this.rb.velocity.y > 0f)
			{
				this.rb.velocity = new Vector3(velocity.x, velocity.y / 2f, velocity.z);
			}
			base.Invoke("ResetJump", this.jumpCooldown);
		}
		if (!this.grounded && !this.isWallRunning && this.doubleJumpsLeft >= 1)
		{
			this.readyToJump = false;
			this.doubleJumpsLeft--;
			this.rb.AddForce(this.orientation.forward * this.jumpForce * 1f);
			this.rb.AddForce(Vector2.up * this.jumpForce * 1.7f);
			this.rb.AddForce(this.normalVector * this.jumpForce * 0.7f);
			this.rb.velocity = new Vector3(this.rb.velocity.x, this.rb.velocity.y * 0.4f, this.rb.velocity.z);
			base.Invoke("ResetJump", this.jumpCooldown);
		}
		if (this.isWallRunning && this.wallJumpsLeft >= 1)
		{
			this.readyToJump = false;
			this.wallJumpsLeft--;
			this.rb.AddForce(Vector2.up * this.jumpForce * 0.85f);
			this.rb.AddForce(this.normalVector * this.jumpForce * 0.5f);
			this.rb.AddForce(this.orientation.forward * this.jumpForce * 0.5f);
			if (this.isWallRight)
			{
				this.rb.AddForce(-this.orientation.right * this.jumpForce * 1.5f);
			}
			if (this.isWallLeft)
			{
				this.rb.AddForce(this.orientation.right * this.jumpForce * 1.5f);
			}
			this.rb.AddForce(this.orientation.forward * this.jumpForce * 1f);
			this.rb.velocity = Vector3.zero;
			base.Invoke("ResetJump", this.jumpCooldown);
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003B8F File Offset: 0x00001D8F
	private void ResetJump()
	{
		this.readyToJump = true;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003B98 File Offset: 0x00001D98
	private void Dash()
	{
		this.readyToDash = false;
		this.wTapTimes = 0;
		this.rb.AddForce(this.orientation.forward * this.dashForce);
		this.rb.AddForce(this.orientation.up * this.dashForce * 0.5f);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003C00 File Offset: 0x00001E00
	private void StartWallrun()
	{
		Debug.Log("Wallrunning");
		if (this.grounded)
		{
			this.StopWallRun();
		}
		this.elapsedWallTime += Time.deltaTime;
		float num = this.elapsedWallTime;
		float num2 = this.maxWallrunTime;
		this.isWallRunning = true;
		this.rb.AddForce(this.orientation.up * this.wallrunUpwardForce * Time.deltaTime);
		if (this.rb.velocity.magnitude <= this.baseSpeed + this.wallSpeedAdd)
		{
			this.rb.AddForce(this.orientation.forward * this.wallrunForce * Time.deltaTime);
			if (this.isWallRight)
			{
				this.rb.AddForce(this.orientation.right * this.wallrunForce / 5f * Time.deltaTime);
				return;
			}
			this.rb.AddForce(-this.orientation.right * this.wallrunForce / 5f * Time.deltaTime);
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003D3F File Offset: 0x00001F3F
	private void StopWallRun()
	{
		this.isWallRunning = false;
		this.readyToWallrun = false;
		this.elapsedWallTime = 0f;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003D5C File Offset: 0x00001F5C
	private void CheckForWall()
	{
		this.isWallRight = Physics.Raycast(base.transform.position, this.orientation.right, out this.wallHitR, 1f, this.whatIsGround);
		this.isWallLeft = Physics.Raycast(base.transform.position, -this.orientation.right, out this.wallHitL, 1f, this.whatIsGround);
		if (!this.isWallLeft && !this.isWallRight && this.isWallRunning)
		{
			this.StopWallRun();
		}
		if ((this.isWallLeft || this.isWallRight) && this.resetDoubleJumpsOnWall)
		{
			this.doubleJumpsLeft = this.startDoubleJumps;
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003E20 File Offset: 0x00002020
	private void Climb()
	{
		Vector3 velocity = this.rb.velocity;
		if (this.rb.velocity.y < 0.5f && !this.alreadyStoppedAtLadder)
		{
			this.rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
			this.alreadyStoppedAtLadder = true;
			this.rb.AddForce(this.orientation.forward * 500f * Time.deltaTime);
		}
		if (this.rb.velocity.magnitude < this.baseSpeed + this.climbSpeedAdd)
		{
			this.rb.AddForce(this.orientation.up * this.climbForce * Time.deltaTime);
		}
		if (!Input.GetKey(KeyCode.S))
		{
			this.y = 0f;
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003F0B File Offset: 0x0000210B
	private void StartSlowMo()
	{
		this.readyForSlowMo = false;
		this.slowMoPlane.SetActive(true);
		Time.timeScale = this.slowMoStrength;
		base.Invoke("StopSlowMo", this.slowMoTime * this.slowMoStrength);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003F43 File Offset: 0x00002143
	private void StopSlowMo()
	{
		this.slowMoPlane.SetActive(false);
		Time.timeScale = 1f;
		base.Invoke("ResetSlowMo", this.slowMoCooldown);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003F6C File Offset: 0x0000216C
	private void ResetSlowMo()
	{
		this.readyForSlowMo = true;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003F78 File Offset: 0x00002178
	private void Look()
	{
		float num = Input.GetAxis("Mouse X") * this.sensitivity * Time.fixedDeltaTime * this.sensMultiplier;
		float num2 = Input.GetAxis("Mouse Y") * this.sensitivity * Time.fixedDeltaTime * this.sensMultiplier;
		Vector3 eulerAngles = this.playerCam.transform.localRotation.eulerAngles;
		this.desiredX = eulerAngles.y + num;
		this.xRotation -= num2;
		this.xRotation = Mathf.Clamp(this.xRotation, -90f, 90f);
		this.playerCam.transform.localRotation = Quaternion.Euler(this.xRotation, this.desiredX, this.wallRunCameraTilt);
		this.orientation.transform.localRotation = Quaternion.Euler(0f, this.desiredX, 0f);
		if (Math.Abs(this.wallRunCameraTilt) < this.maxWallRunCameraTilt && this.isWallRunning && this.isWallRight)
		{
			this.wallRunCameraTilt += Time.deltaTime * this.maxWallRunCameraTilt * 2f;
		}
		if (Math.Abs(this.wallRunCameraTilt) < this.maxWallRunCameraTilt && this.isWallRunning && this.isWallLeft)
		{
			this.wallRunCameraTilt -= Time.deltaTime * this.maxWallRunCameraTilt * 2f;
		}
		if (this.wallRunCameraTilt > 0f && !this.isWallRight && !this.isWallLeft)
		{
			this.wallRunCameraTilt -= Time.deltaTime * this.maxWallRunCameraTilt * 2f;
		}
		if (this.wallRunCameraTilt < 0f && !this.isWallRight && !this.isWallLeft)
		{
			this.wallRunCameraTilt += Time.deltaTime * this.maxWallRunCameraTilt * 2f;
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0000415C File Offset: 0x0000235C
	private void IncreaseBaseSpeed()
	{
		if (this.baseSpeed >= this.maxBaseSpeed)
		{
			return;
		}
		this.timer1 += Time.deltaTime * this.baseSpeedAccel;
		this.extraBaseDeccel = 0f;
		if (this.timer1 > 1f)
		{
			this.baseSpeed += 0.1f;
			this.timer1 = 0f;
		}
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000041C8 File Offset: 0x000023C8
	private void DecreaseBaseSpeed()
	{
		if (this.baseSpeed <= this.startBaseSpeed)
		{
			return;
		}
		this.timer2 += Time.deltaTime * this.baseSpeedDeccel * this.extraBaseDeccel;
		this.extraBaseDeccel += Time.deltaTime * 0.5f;
		if (this.timer2 > 1f)
		{
			this.baseSpeed -= 0.1f;
			this.timer2 = 0f;
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00004246 File Offset: 0x00002446
	private void SlowDown()
	{
		if (this.allowDrag)
		{
			this.rb.drag = 1f;
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00004260 File Offset: 0x00002460
	private void CounterMovement(float x, float y, Vector2 mag)
	{
		if (!this.grounded || this.jumping || this.isWallRunning)
		{
			return;
		}
		if (this.crouching)
		{
			this.rb.AddForce(this.moveSpeed * Time.deltaTime * -this.rb.velocity.normalized * this.slideCounterMovement);
			return;
		}
		if ((Math.Abs(mag.x) > this.threshold && Math.Abs(x) < 0.05f) || (mag.x < -this.threshold && x > 0f) || (mag.x > this.threshold && x < 0f))
		{
			this.rb.AddForce(this.moveSpeed * this.orientation.transform.right * Time.deltaTime * -mag.x * this.counterMovement);
		}
		if ((Math.Abs(mag.y) > this.threshold && Math.Abs(y) < 0.05f) || (mag.y < -this.threshold && y > 0f) || (mag.y > this.threshold && y < 0f))
		{
			this.rb.AddForce(this.moveSpeed * this.orientation.transform.forward * Time.deltaTime * -mag.y * this.counterMovement);
		}
		if (Mathf.Sqrt(Mathf.Pow(this.rb.velocity.x, 2f) + Mathf.Pow(this.rb.velocity.z, 2f)) > this.baseSpeed)
		{
			float num = this.rb.velocity.y;
			Vector3 vector = this.rb.velocity.normalized * this.baseSpeed;
			this.rb.velocity = new Vector3(vector.x, num, vector.z);
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004488 File Offset: 0x00002688
	public Vector2 FindVelRelativeToLook()
	{
		float current = this.orientation.transform.eulerAngles.y;
		float target = Mathf.Atan2(this.rb.velocity.x, this.rb.velocity.z) * 57.29578f;
		float num = Mathf.DeltaAngle(current, target);
		float num2 = 90f - num;
		float magnitude = this.rb.velocity.magnitude;
		float num3 = magnitude * Mathf.Cos(num * 0.017453292f);
		return new Vector2(magnitude * Mathf.Cos(num2 * 0.017453292f), num3);
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000451B File Offset: 0x0000271B
	private bool IsFloor(Vector3 v)
	{
		return Vector3.Angle(Vector3.up, v) < this.maxSlopeAngle;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00004530 File Offset: 0x00002730
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(Vector3.Angle(base.transform.up, collision.contacts[0].normal));
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00004560 File Offset: 0x00002760
	private void OnCollisionStay(Collision other)
	{
		int layer = other.gameObject.layer;
		if (this.whatIsGround != (this.whatIsGround | 1 << layer))
		{
			return;
		}
		for (int i = 0; i < other.contactCount; i++)
		{
			Vector3 normal = other.contacts[i].normal;
			if (this.IsFloor(normal))
			{
				this.onSlope = false;
				this.grounded = true;
				this.cancellingGrounded = false;
				this.normalVector = normal;
				base.CancelInvoke("StopGrounded");
			}
			else
			{
				this.onSlope = true;
			}
			if (this.isWallRunning && this.lastWall != other.gameObject)
			{
				Debug.Log("WallChanged!");
				this.lastWall = other.gameObject;
				this.wallJumpsLeft = this.wallJumps;
			}
		}
		float num = 3f;
		if (!this.cancellingGrounded)
		{
			this.cancellingGrounded = true;
			base.Invoke("StopGrounded", Time.deltaTime * num);
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000465F File Offset: 0x0000285F
	private void StopGrounded()
	{
		this.grounded = false;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00004668 File Offset: 0x00002868
	public void DashInDirection(Vector3 dir, float force)
	{
		this.rb.AddForce(dir * force, ForceMode.Impulse);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000467D File Offset: 0x0000287D
	public void PreventDrag(float time)
	{
		this.allowDrag = false;
		base.Invoke("ResetAllowDrag", time);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004692 File Offset: 0x00002892
	private void ResetAllowDrag()
	{
		this.allowDrag = true;
	}

	// Token: 0x04000066 RID: 102
	public Transform playerCam;

	// Token: 0x04000067 RID: 103
	public Transform orientation;

	// Token: 0x04000068 RID: 104
	private Rigidbody rb;

	// Token: 0x04000069 RID: 105
	public bool lockLook;

	// Token: 0x0400006A RID: 106
	private float xRotation;

	// Token: 0x0400006B RID: 107
	private float sensitivity = 50f;

	// Token: 0x0400006C RID: 108
	private float sensMultiplier = 1f;

	// Token: 0x0400006D RID: 109
	public int health;

	// Token: 0x0400006E RID: 110
	public int regen;

	// Token: 0x0400006F RID: 111
	private int maxHealth;

	// Token: 0x04000070 RID: 112
	public Vector3 inputVector;

	// Token: 0x04000071 RID: 113
	public float moveSpeed = 4500f;

	// Token: 0x04000072 RID: 114
	public bool grounded;

	// Token: 0x04000073 RID: 115
	public bool onSlope;

	// Token: 0x04000074 RID: 116
	public bool fullAirControl;

	// Token: 0x04000075 RID: 117
	public LayerMask whatIsGround;

	// Token: 0x04000076 RID: 118
	public float counterMovement = 0.175f;

	// Token: 0x04000077 RID: 119
	private float threshold = 0.01f;

	// Token: 0x04000078 RID: 120
	public float maxSlopeAngle = 35f;

	// Token: 0x04000079 RID: 121
	public float startBaseSpeed = 15f;

	// Token: 0x0400007A RID: 122
	public float baseSpeed;

	// Token: 0x0400007B RID: 123
	public float maxBaseSpeed;

	// Token: 0x0400007C RID: 124
	public float baseSpeedAccel;

	// Token: 0x0400007D RID: 125
	public float baseSpeedDeccel;

	// Token: 0x0400007E RID: 126
	public float bSAccelPoint;

	// Token: 0x0400007F RID: 127
	public float bSDeccelPoint;

	// Token: 0x04000080 RID: 128
	public float slowDownPoint;

	// Token: 0x04000081 RID: 129
	public float dragToSlowDown;

	// Token: 0x04000082 RID: 130
	private Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);

	// Token: 0x04000083 RID: 131
	private Vector3 playerScale;

	// Token: 0x04000084 RID: 132
	public float slideForce = 400f;

	// Token: 0x04000085 RID: 133
	public float slideCounterMovement = 0.2f;

	// Token: 0x04000086 RID: 134
	public float crouchGravityMultiplier;

	// Token: 0x04000087 RID: 135
	private bool readyToJump = true;

	// Token: 0x04000088 RID: 136
	private float jumpCooldown = 0.25f;

	// Token: 0x04000089 RID: 137
	public float jumpForce = 550f;

	// Token: 0x0400008A RID: 138
	public int startDoubleJumps = 1;

	// Token: 0x0400008B RID: 139
	private int doubleJumpsLeft;

	// Token: 0x0400008C RID: 140
	public float x;

	// Token: 0x0400008D RID: 141
	public float y;

	// Token: 0x0400008E RID: 142
	private bool jumping;

	// Token: 0x0400008F RID: 143
	private bool sprinting;

	// Token: 0x04000090 RID: 144
	private bool crouching;

	// Token: 0x04000091 RID: 145
	public float airForwardForce;

	// Token: 0x04000092 RID: 146
	public float dashForce;

	// Token: 0x04000093 RID: 147
	public float dashTime;

	// Token: 0x04000094 RID: 148
	private bool readyToDash;

	// Token: 0x04000095 RID: 149
	private int wTapTimes;

	// Token: 0x04000096 RID: 150
	private Vector3 normalVector = Vector3.up;

	// Token: 0x04000097 RID: 151
	private Vector3 wallNormalVector;

	// Token: 0x04000098 RID: 152
	public float slopeDownwardForce;

	// Token: 0x04000099 RID: 153
	public LayerMask whatIsWall;

	// Token: 0x0400009A RID: 154
	private RaycastHit wallHitR;

	// Token: 0x0400009B RID: 155
	private RaycastHit wallHitL;

	// Token: 0x0400009C RID: 156
	public bool isWallRight;

	// Token: 0x0400009D RID: 157
	public bool isWallLeft;

	// Token: 0x0400009E RID: 158
	public float maxWallrunTime;

	// Token: 0x0400009F RID: 159
	public float wallrunForce;

	// Token: 0x040000A0 RID: 160
	public float wallrunUpwardForce;

	// Token: 0x040000A1 RID: 161
	public float wallSpeedAdd;

	// Token: 0x040000A2 RID: 162
	public int wallJumps;

	// Token: 0x040000A3 RID: 163
	public int wallJumpsLeft;

	// Token: 0x040000A4 RID: 164
	public bool readyToWallrun;

	// Token: 0x040000A5 RID: 165
	public bool isWallRunning;

	// Token: 0x040000A6 RID: 166
	public bool resetDoubleJumpsOnWall;

	// Token: 0x040000A7 RID: 167
	public GameObject lastWall;

	// Token: 0x040000A8 RID: 168
	public float maxWallRunCameraTilt;

	// Token: 0x040000A9 RID: 169
	public float wallRunCameraTilt;

	// Token: 0x040000AA RID: 170
	public float climbForce;

	// Token: 0x040000AB RID: 171
	public float climbSpeedAdd;

	// Token: 0x040000AC RID: 172
	public LayerMask whatIsLadder;

	// Token: 0x040000AD RID: 173
	private bool alreadyStoppedAtLadder;

	// Token: 0x040000AE RID: 174
	public GameObject slowMoPlane;

	// Token: 0x040000AF RID: 175
	public float slowMoCooldown;

	// Token: 0x040000B0 RID: 176
	public float slowMoTime;

	// Token: 0x040000B1 RID: 177
	[Range(0f, 1f)]
	public float slowMoStrength;

	// Token: 0x040000B2 RID: 178
	private bool readyForSlowMo = true;

	// Token: 0x040000B3 RID: 179
	public float speedRecord;

	// Token: 0x040000B4 RID: 180
	public TextMeshProUGUI baseVelocityDisplay;

	// Token: 0x040000B5 RID: 181
	public TextMeshProUGUI currentVelocityDisplay;

	// Token: 0x040000B6 RID: 182
	public TextMeshProUGUI recordVelocityDisplay;

	// Token: 0x040000B7 RID: 183
	private float elapsedWallTime;

	// Token: 0x040000B8 RID: 184
	private float desiredX;

	// Token: 0x040000B9 RID: 185
	private float timer1;

	// Token: 0x040000BA RID: 186
	private float timer2;

	// Token: 0x040000BB RID: 187
	private float extraBaseDeccel;

	// Token: 0x040000BC RID: 188
	private bool allowDrag = true;

	// Token: 0x040000BD RID: 189
	private bool cancellingGrounded;
}
