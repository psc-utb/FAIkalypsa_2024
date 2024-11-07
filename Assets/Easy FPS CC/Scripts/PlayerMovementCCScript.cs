using CodeMonkey.HealthSystemCM;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementCCScript : MonoBehaviour
{
    CharacterController characterController;

    [Tooltip("Current player's speed")]
    [SerializeField]
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }


    [Tooltip("The maximum Player's speed  during moving")]
    [SerializeField]
    private int maxMoveSpeed = 5;
    public int MaxMoveSpeed
    {
        get { return maxMoveSpeed; }
        set { maxMoveSpeed = value; }
    }

    [Tooltip("gravity acceleration")]
    [SerializeField]
    private float gravity = 9.81f;

    [Tooltip("Maximum jump height")]
    [SerializeField]
    private float jumpHeight = 1.0f;

    [Tooltip("Jump duration in seconds")]
    [Min(0.1f)]
    [SerializeField]
    private float jumpDuration = 0.25f;


    [Tooltip("Assign player's camera here")]
    private Transform cameraMain;

    [Tooltip("Position of the camera inside the player")]
    public Vector3 CameraPosition { get; set; }



    [Tooltip("Tells us whether the player is grounded or not (by using another script)")]
    [SerializeField]
    private bool grounded;

    [Tooltip("Tells us whether the player is grounded or not (by using raycast from another script)")]
    [SerializeField]
    private bool rayCastGrounded;


    /*
	 * Getting the Players CharacterController component.
	 * And grabbing the mainCamera from Players child transform.
	 */
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraMain = transform.Find("Main Camera").transform;
        bulletSpawn = cameraMain.Find("BulletSpawn").transform;
        ignoreLayer = 1 << LayerMask.NameToLayer("Player");

    }


    /*
	* Raycasting for meele attacks and input movement handling here.
	*/
    void FixedUpdate()
    {
        RaycastForMeleeAttacks();
    }


    /*
	* Update loop calling other stuff
	*/
    void Update()
    {

        PlayerMovementLogic();

        Jumping();

        Crouching();

        WalkingSound();


    }//end update

    /*
	* Accordingly to input adds force and if magnitude is bigger it will clamp it.
	* If player leaves keys it will deaccelerate
	*/
    bool jumpContinue = false;
    Vector3 jumpMax = new Vector3();
    Vector3 jumpStep = new Vector3();
    Vector3 jumpCumulative = new Vector3();
    Vector3 previousJumpPosition;
    Vector3 previousjumpStep = new Vector3();
    bool jumpStarted = false;
    void PlayerMovementLogic()
    {
        currentSpeed = characterController.velocity.magnitude;

        Vector3 velocity = new Vector3();

        // Determine how much should move in the x-direction
        Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right * maxMoveSpeed * Time.deltaTime;

        // Determine how much should move in the z-direction
        Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward * maxMoveSpeed * Time.deltaTime;

        // Determine total movement
        Vector3 movement = transform.TransformDirection(movementZ + movementX);
        // move
        characterController.Move(movement);


        if (grounded)
        {
            bool jumpPressed = Input.GetButtonDown("Jump");
            if (jumpPressed)
            {
                // Apply velocity for jump
                jumpMax.y = jumpHeight;
                jumpStep.y = jumpMax.y * Time.deltaTime / jumpDuration;
                previousjumpStep = new Vector3();
                jumpCumulative = new Vector3();
                jumpContinue = true;
                jumpStarted = true;

                previousJumpPosition = characterController.transform.position;
                GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(10);
            }
        }

        if (jumpContinue)
        {
            velocity.y = jumpStep.y;
            jumpCumulative.y += jumpStep.y;

            if (jumpCumulative.y >= jumpMax.y || jumpStarted == false && Mathf.Abs(characterController.transform.position.y - previousJumpPosition.y) < previousjumpStep.y - 0.0001f)
            {
                jumpContinue = false;
            }
            jumpStarted = false;

            previousJumpPosition = characterController.transform.position;
            previousjumpStep.y = jumpStep.y;
        }
        else
        {
            // Apply gravity (so the object will fall if not grounded)
            velocity.y += -gravity * Time.deltaTime;
        }
        // Actually move the character controller in the movement direction
        characterController.Move(velocity);

    }

    /*
	* Handles jumping and ads the force and sounds.
	*/
    void Jumping()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            if (_jumpSound)
                _jumpSound.Play();
            else
                print("Missig jump sound.");
            _walkSound.Stop();
            _runSound.Stop();

            //Debug.Log("Position during jump: " + characterController.gameObject.transform.position);
        }
    }


    public void SetGround(bool grounded)
    {
        this.grounded = grounded;
    }


    public void SetRayCastGrounded(bool rayCastGrounded)
    {
        this.rayCastGrounded = rayCastGrounded;
    }


    /*
	* Checks if player is grounded and plays the sound accorindlgy to his speed
	*/
    void WalkingSound()
    {
        if (_walkSound && _runSound)
        {
            if (rayCastGrounded)
            { //for walk sounsd using this because suraface is not straigh			
                if (currentSpeed > 1)
                {
                    //				print ("unutra sam");
                    if (maxMoveSpeed == 3)
                    {
                        //	print ("tu sem");
                        if (!_walkSound.isPlaying)
                        {
                            //	print ("playam hod");
                            _walkSound.Play();
                            _runSound.Stop();
                        }
                    }
                    else if (maxMoveSpeed == 5)
                    {
                        //	print ("NE tu sem");

                        if (!_runSound.isPlaying)
                        {
                            _walkSound.Stop();
                            _runSound.Play();
                        }
                    }
                }
                else
                {
                    _walkSound.Stop();
                    _runSound.Stop();
                }
            }
            else
            {
                _walkSound.Stop();
                _runSound.Stop();
            }
        }
        else
        {
            print("Missing walk and running sounds.");
        }

    }


    /*
	* If player toggle the crouch it will scale the player to appear that is crouching
	*/
    void Crouching()
    {
        if (Input.GetKey(KeyCode.C))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 0.6f, 1), Time.deltaTime * 15);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 15);
        }
    }






    RaycastHit hitInfo;
    private float meleeAttack_cooldown;
    private string currentWeapo;
    [Tooltip("Put 'Player' layer here")]
    [Header("Shooting Properties")]
    private LayerMask ignoreLayer;//to ignore player layer
    Ray ray1, ray2, ray3, ray4, ray5, ray6, ray7, ray8, ray9;
    private float rayDetectorMeeleSpace = 0.15f;
    private float offsetStart = 0.05f;
    [Tooltip("Put BulletSpawn gameobject here, palce from where bullets are created.")]
    [HideInInspector]
    public Transform bulletSpawn; //from here we shoot a ray to check where we hit him;
    /*
	* This method casts 9 rays in different directions. ( SEE scene tab and you will see 9 rays differently coloured).
	* Used to widley detect enemy infront and increase meele hit detectivity.
	* Checks for cooldown after last preformed meele attack.
	*/


    public bool been_to_meele_anim = false;
    private void RaycastForMeleeAttacks()
    {



        if (meleeAttack_cooldown > -5)
        {
            meleeAttack_cooldown -= 1 * Time.deltaTime;
        }


        if (GetComponent<GunInventoryCC>().currentGun)
        {
            if (GetComponent<GunInventoryCC>().currentGun.GetComponent<GunCCScript>())
                currentWeapo = "gun";
        }

        //middle row
        ray1 = new Ray(bulletSpawn.position + (bulletSpawn.right * offsetStart), bulletSpawn.forward + (bulletSpawn.right * rayDetectorMeeleSpace));
        ray2 = new Ray(bulletSpawn.position - (bulletSpawn.right * offsetStart), bulletSpawn.forward - (bulletSpawn.right * rayDetectorMeeleSpace));
        ray3 = new Ray(bulletSpawn.position, bulletSpawn.forward);
        //upper row
        ray4 = new Ray(bulletSpawn.position + (bulletSpawn.right * offsetStart) + (bulletSpawn.up * offsetStart), bulletSpawn.forward + (bulletSpawn.right * rayDetectorMeeleSpace) + (bulletSpawn.up * rayDetectorMeeleSpace));
        ray5 = new Ray(bulletSpawn.position - (bulletSpawn.right * offsetStart) + (bulletSpawn.up * offsetStart), bulletSpawn.forward - (bulletSpawn.right * rayDetectorMeeleSpace) + (bulletSpawn.up * rayDetectorMeeleSpace));
        ray6 = new Ray(bulletSpawn.position + (bulletSpawn.up * offsetStart), bulletSpawn.forward + (bulletSpawn.up * rayDetectorMeeleSpace));
        //bottom row
        ray7 = new Ray(bulletSpawn.position + (bulletSpawn.right * offsetStart) - (bulletSpawn.up * offsetStart), bulletSpawn.forward + (bulletSpawn.right * rayDetectorMeeleSpace) - (bulletSpawn.up * rayDetectorMeeleSpace));
        ray8 = new Ray(bulletSpawn.position - (bulletSpawn.right * offsetStart) - (bulletSpawn.up * offsetStart), bulletSpawn.forward - (bulletSpawn.right * rayDetectorMeeleSpace) - (bulletSpawn.up * rayDetectorMeeleSpace));
        ray9 = new Ray(bulletSpawn.position - (bulletSpawn.up * offsetStart), bulletSpawn.forward - (bulletSpawn.up * rayDetectorMeeleSpace));

        Debug.DrawRay(ray1.origin, ray1.direction, Color.cyan);
        Debug.DrawRay(ray2.origin, ray2.direction, Color.cyan);
        Debug.DrawRay(ray3.origin, ray3.direction, Color.cyan);
        Debug.DrawRay(ray4.origin, ray4.direction, Color.red);
        Debug.DrawRay(ray5.origin, ray5.direction, Color.red);
        Debug.DrawRay(ray6.origin, ray6.direction, Color.red);
        Debug.DrawRay(ray7.origin, ray7.direction, Color.yellow);
        Debug.DrawRay(ray8.origin, ray8.direction, Color.yellow);
        Debug.DrawRay(ray9.origin, ray9.direction, Color.yellow);

        if (GetComponent<GunInventoryCC>().currentGun)
        {
            if (GetComponent<GunInventoryCC>().currentGun.GetComponent<GunCCScript>().meeleAttack == false)
            {
                been_to_meele_anim = false;
            }
            if (GetComponent<GunInventoryCC>().currentGun.GetComponent<GunCCScript>().meeleAttack == true && been_to_meele_anim == false)
            {
                been_to_meele_anim = true;
                //	if (isRunning == false) {
                StartCoroutine("MeeleAttackWeaponHit");
                //	}
            }
        }

    }

    /*
	 *Method that is called if the waepon hit animation has been triggered the first time via Q input
	 *and if is, it will search for target and make damage
	 */
    IEnumerator MeeleAttackWeaponHit()
    {
        if (Physics.Raycast(ray1, out hitInfo, 2f, ~ignoreLayer) || Physics.Raycast(ray2, out hitInfo, 2f, ~ignoreLayer) || Physics.Raycast(ray3, out hitInfo, 2f, ~ignoreLayer)
            || Physics.Raycast(ray4, out hitInfo, 2f, ~ignoreLayer) || Physics.Raycast(ray5, out hitInfo, 2f, ~ignoreLayer) || Physics.Raycast(ray6, out hitInfo, 2f, ~ignoreLayer)
            || Physics.Raycast(ray7, out hitInfo, 2f, ~ignoreLayer) || Physics.Raycast(ray8, out hitInfo, 2f, ~ignoreLayer) || Physics.Raycast(ray9, out hitInfo, 2f, ~ignoreLayer))
        {
            //Debug.DrawRay (bulletSpawn.position, bulletSpawn.forward + (bulletSpawn.right*0.2f), Color.green, 0.0f);
            if (hitInfo.transform.tag == "Dummie")
            {
                Transform _other = hitInfo.transform.root.transform;
                if (_other.transform.tag == "Dummie")
                {
                    print("hit a dummie");
                }
                InstantiateBlood(hitInfo, false);
            }
        }
        yield return new WaitForEndOfFrame();
    }

    [Header("BloodForMeeleAttacks")]
    RaycastHit hit;//stores info of hit;
    [Tooltip("Put your particle blood effect here.")]
    public GameObject bloodEffect;//blod effect prefab;
    /*
	* Upon hitting enemy it calls this method, gives it raycast hit info 
	* and at that position it creates our blood prefab.
	*/
    void InstantiateBlood(RaycastHit _hitPos, bool swordHitWithGunOrNot)
    {

        if (currentWeapo == "gun")
        {
            GunCCScript.HitMarkerSound();

            if (_hitSound)
                _hitSound.Play();
            else
                print("Missing hit sound");

            if (!swordHitWithGunOrNot)
            {
                if (bloodEffect)
                    Instantiate(bloodEffect, _hitPos.point, Quaternion.identity);
                else
                    print("Missing blood effect prefab in the inspector.");
            }
        }
    }
    private GameObject myBloodEffect;


    [Header("Player SOUNDS")]
    [Tooltip("Jump sound when player jumps.")]
    public AudioSource _jumpSound;
    [Tooltip("Sound while player makes when successfully reloads weapon.")]
    public AudioSource _freakingZombiesSound;
    [Tooltip("Sound Bullet makes when hits target.")]
    public AudioSource _hitSound;
    [Tooltip("Walk sound player makes.")]
    public AudioSource _walkSound;
    [Tooltip("Run Sound player makes.")]
    public AudioSource _runSound;
}
