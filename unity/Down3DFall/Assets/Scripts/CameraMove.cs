using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour {


    private  float maxSpeed =5;
    private  float acceleration =10 ;
    private const float fallspeed = 1f;
    private const float gravity = -50f;
    private const float bulletTime = 0.4f;
    private float lastBullettime =0.0f;
    private const int dragChange = 3;
    public Vector3 velocity;
    private bool left ;
    private bool right;
    private bool forward;
    private bool back;
    private bool mouseHeld;
    private bool reload = false;


    private bool canShoot;
    private int playerHealth;
    private int ammo;
    const int maxAmmo = 8;
    float reloadTime =1.0f;
    float curReloadTime;
    public Text healthText;
    public Text ammoText;

    public Text fallSpeed;

    private float lastPos;
    private float speedTime;
    public float holdSpeed;

    

    private List<GameObject> bullets = new List<GameObject>();




    private const float speedChange =10;
    public bool fall = true;


    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    private bool lockCursor;
    //public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;


    Rigidbody myRigid;
   
    // Use this for initialization
    void Start () {
        Physics.gravity = new Vector3(0, gravity, 0);
        velocity = Vector3.zero;
        left = false;
        right = false;
        forward = false;
        back = false;
        mouseHeld = false;
        lastPos = transform.position.y;
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;
        myRigid = gameObject.GetComponent<Rigidbody>();
        playerHealth = 4;
        speedTime = Time.time;
        ammo = maxAmmo;
        ammoText.text = ammo.ToString();
        healthText.text = playerHealth.ToString();
    }

    // Update is called once per frame
    void Update () {

        CheckButton();
        //Debug.Log(myRigid.velocity);
        moveCamera();
        // if you hold the mous rotate the camera
        moveDirection();

        if (Input.GetMouseButtonDown(0))
        {
            canShoot = true;
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            canShoot = false;
            myRigid.drag = 0;
        }
        if (Input.GetKey(KeyCode.R))
        {
            reload = true;
            curReloadTime = Time.time;
            ammoText.text = "Reloading";
            myRigid.drag = 0;
        }

        

        if (reload)
        {
            if (curReloadTime < Time.time -reloadTime  )
            {
                ammo = maxAmmo;
                ammoText.text = ammo.ToString();
                reload = false;
            }
        }else if (canShoot)
        {
            Shoot();
        }
        if (speedTime < Time.time - 0.1f)
        {
            workSpeed();

        }
        //myRigid.AddRelativeForce(hold2);
    }


    void workSpeed()
    {
        float hold = transform.position.y;
       
        float speedChange = hold - lastPos;
        holdSpeed = -(speedChange / 0.1f);
        fallSpeed.text = holdSpeed.ToString();
        speedTime = Time.time;
        lastPos = transform.position.y;
    }

    void moveCamera()
    {
        if (mouseHeld|| lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            var targetOrientation = Quaternion.Euler(targetDirection);

            //http://forum.unity3d.com/threads/a-free-simple-smooth-mouselook.73117/
            // Get raw mouse input for a cleaner reading on more sensitive mice.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(smoothing.x, smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
            _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

            // Find the absolute mouse movement value from point zero.
            _mouseAbsolute += _smoothMouse;

            // Clamp and apply the local x value first, so as not to be affected by world transforms.
            if (clampInDegrees.x < 360)
                _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

            var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
            transform.localRotation = xRotation;

            // Then clamp and apply the global y value.
            if (clampInDegrees.y < 360)
                _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

            transform.localRotation *= targetOrientation;

            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }



    }

    void moveDirection()
    {

        if (forward && velocity.x < maxSpeed)
        {
            velocity.x += acceleration * Time.deltaTime;
            //Debug.Log("14");
        }
        else if (!forward && velocity.x > 0)
        {
            velocity.x -= (acceleration + velocity.x) * Time.deltaTime;
            //Debug.Log("13");
        }


        if (back && velocity.x > -maxSpeed)
        {
            velocity.x -= acceleration * Time.deltaTime;
            //Debug.Log("12");

        }
        else if (!back && velocity.x < 0)
        {
            //Debug.Log("11");
            velocity.x += (acceleration - velocity.x) * Time.deltaTime;
        }

        if (left && velocity.z > -maxSpeed)
        {
            velocity.z -= acceleration * Time.deltaTime;
        }
        else if (!left && velocity.z < 0)
        {
            velocity.z += (acceleration - velocity.z) * Time.deltaTime;

        }
        if (right && velocity.z < maxSpeed)
        {
            velocity.z += acceleration * Time.deltaTime;
        }
        else if (!right && velocity.z > 0)
        {
            velocity.z -= (acceleration + velocity.z) * Time.deltaTime;

        }


        if (velocity.z < acceleration / 100 && velocity.z > -acceleration / 100)
        {

            velocity.z = 0;
        }

        if (velocity.x < (acceleration / 100) && velocity.x > (-acceleration / 100))
        {

            velocity.x = 0;
        }

        if (fall)
        {
            if (velocity.y >= fallspeed)
            {
                velocity.y = fallspeed;
            }
            else
            {
                velocity.y += fallspeed * Time.deltaTime;
            }
        }

        // move the camera in the diurection its facing 



        Quaternion holdq = transform.localRotation;
        Quaternion holdqa = holdq;
        Vector3 hold2 = Vector3.zero;
        holdq.x = 0;
        transform.localRotation = holdq;

        hold2 += transform.forward * velocity.x;
        hold2 += transform.right * velocity.z;

        transform.localRotation = holdqa;



        hold2 *= 20;
        hold2.y = myRigid.velocity.y;

        if (hold2.y < -100)
        {

            hold2.y = -100;

        }
        if (hold2.y > 100)
        {
            hold2.y = 100;
        }


        // hold2.y = myRigid.velocity.y;

        // hold2.y =  -velocity.y;


        myRigid.velocity = hold2;
        //Debug.Log(myRigid.velocity);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        if (other.tag == "Bounce")
        {
             //myRigid.AddRelativeForce(-Physics.gravity,ForceMode.Acceleration);
            myRigid.velocity += new Vector3(0,200,0);
            //velocity.y = -fallspeed;
            DmgEnemy(other);
           // Debug.Log(myRigid.velocity);
        }

        if (other.tag == "Enemy")
        {
            Hit(other.gameObject.GetComponentInParent<Enemy>());
           
        }

        if (other.tag == "Updraft")
        {
            Physics.gravity = new Vector3(0, -gravity, 0);
            Debug.Log(Physics.gravity);
        }

    }

    void OnTriggerExit(Collider other)
    {
      

        if (other.tag == "Updraft")
        {
            Physics.gravity = new Vector3(0, gravity, 0);
            Debug.Log(Physics.gravity);
        }

    }
    void Shoot()
    {
        if(Time.time -lastBullettime > bulletTime)
        {

            if (ammo > 0)
            {
                myRigid.drag = dragChange;
                ammo--;
                ammoText.text = ammo.ToString();
                lastBullettime = Time.time;

                //remove one ammo 

                //Debug.Log("sdhhot");
                GameObject bullet = Instantiate(Resources.Load("Bullet", typeof(GameObject))) as GameObject;
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * 200;
                bullets.Add(bullet);
            }
            else if(!reload)
            {

                reload = true;
                curReloadTime = Time.time;
                ammoText.text = "Reloading";
                myRigid.drag = 0;
            }


            

        }


    }


    void Hit(Enemy e)
    {
        if (e.dead)
        {
            return;
        }
        playerHealth--;
        
        healthText.text = playerHealth.ToString();
        
        if (playerHealth <= 0)
        {
           // Debug.LogError(SceneManager.GetActiveScene().name);
            // Application.LoadLevel(Application.loadedLevel);

             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }


    void DmgEnemy(Collider other)
    {
        other.GetComponentInParent<Enemy>().takeDmg(100);
    }

    

    public void IncreaseSpeed()
    {
        maxSpeed *= speedChange;
        acceleration *= speedChange;
        //gameObject.GetComponent<SphereCollider>().radius *= speedChange;
    }

    private void CheckButton()
    {
        
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);   
        forward = Input.GetKey(KeyCode.W);
        back = Input.GetKey(KeyCode.S);
        mouseHeld = Input.GetMouseButton(1);
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
    }
    */
}
