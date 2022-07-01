using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class Player_Controller : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;
    private string actualState;
    private string activeForces;
    private string propertiesEdit;
    private string buttonStatus;
    private string forcesLabel;
    string[] activeForceAndEffect;

    public Button test;
    public float testA;

    /// Audio ///////////////////////////////////////////////////////////////////////////////////////////
    public AudioSource audio_source;
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /// Live telemetry displays /////////////////////////////////////////////////////////////////////////
    public float speed; //alter speed without recompiling, edit from editor
    public Text speeddisplay; //Speed display variable
    public Text superspeeddisplay; //Speed display variable (supervisor)
    private float velocitydisp; //Velocity Display
    public Slider steeringinputdisp; //steering axis slider
    public Slider throttleinputdisp;
    public Slider brakeinputdisp;
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /// Car initialisation //////////////////////////////////////////////////////////////////////////////
    public Transform[] wheels; //Array for the 4 wheels
    public float motorPower = 1500f; //motor power
    public float maxTurn = 25f; //max rotation of front wheels
    float instantPower = 0f;
    float brake = 0f;
    float WheelTurn = 0f;
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /// On-screen Steering Wheel ////////////////////////////////////////////////////////////////////////
    public GameObject wheel; //steering sprite
    float rotations = 0;
    public float turnSpeed; //wheel sprite turning rate
    Quaternion defaultRotation; //starting wheel sprite rotation
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /// Supervisor interference //////////////////////////////////////////////////////////////////////////
    public GameObject player_camera;
    public GameObject car;
    Vector3 CarPos;
    Vector3 standard_camera_orientation;
    Vector3 camera_orientation;
    //////////////////////////////////////////////////////////////////////////////////////////////////////

    Rigidbody rb; //sets name to rb

   

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        velocitydisp = 0; //Initial Velocity Display value
        SetSpeedDisplay();
        string start = "Time;X_Pos;Y_Pos;Z_Pos;Velocity;Wheel angle;Power;Brake;\n\n";  //write column headings for Excel
        System.IO.File.WriteAllText(@"..\\Output_Files\\Driver_Analysis_Telemetry.txt", start); //Clear text file

        defaultRotation = wheel.transform.localRotation;  //Set current wheel sprite rotation as default rotation

    }

   

    void FixedUpdate() //Update calls before rendering frame (FPS dependent), FixedUpdate is dependent on physics calculations
    {
        WheelTurn = Input.GetAxis("Horizontal") * maxTurn;
        //Debug.Log("Steering= " + Input.GetAxis("Horizontal") + " Throttle= " + Input.GetAxis("Throttle") + " Brake =" + Input.GetAxis("Brake"));  for debugging
        instantPower = (Input.GetAxis("Throttle")+1f) * motorPower; // +1 so that G27 throttle doesn't decelerate the car on its own (axis range -1 to 1)
        brake = (Input.GetAxis("Brake")+1f) * rb.mass * 10f; // brake input

        //turn collider
        getCollider(0).steerAngle = WheelTurn;
        getCollider(1).steerAngle = WheelTurn;

        //turn steering wheel////////////////////////////////////////////////////////////////////////////
        Vector3 steeringwheelturn;
        steeringwheelturn.x = 0;
        steeringwheelturn.y = 0;
        steeringwheelturn.z = -Input.GetAxis("Horizontal"); //Wheel angle matches real wheel
        wheel.transform.Rotate(steeringwheelturn * Time.deltaTime * turnSpeed);
        rotations = steeringwheelturn.z;
        defaultRotation.x = rb.transform.rotation.x; //keep default x axis as car direction
        defaultRotation.y = rb.transform.rotation.y; //keep default y axis as car direction

        if (Input.GetAxis("Horizontal") == 0)
        {
                wheel.transform.rotation = defaultRotation; //reset wheel back to 0 if no input
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////

        //turn wheels
        wheels[0].localEulerAngles = new Vector3(wheels[0].localEulerAngles.x, getCollider(0).steerAngle - wheels[0].localEulerAngles.z + 90, wheels[0].localEulerAngles.z);
        wheels[1].localEulerAngles = new Vector3(wheels[1].localEulerAngles.x, getCollider(1).steerAngle - wheels[1].localEulerAngles.z + 90, wheels[1].localEulerAngles.z);

        //spin wheels
        wheels[0].Rotate(0, -getCollider(0).rpm / 60 * 360 * Time.deltaTime, 0); //0-3 are colliders, 4-7 are models
        wheels[1].Rotate(0, -getCollider(1).rpm / 60 * 360 * Time.deltaTime, 0);
        wheels[2].Rotate(0, -getCollider(2).rpm / 60 * 360 * Time.deltaTime, 0);
        wheels[3].Rotate(0, -getCollider(3).rpm / 60 * 360 * Time.deltaTime, 0);
        wheels[4].Rotate(0, getCollider(0).rpm / 60 * 360 * Time.deltaTime, 0);
        wheels[5].Rotate(0, getCollider(1).rpm / 60 * 360 * Time.deltaTime, 0);
        wheels[6].Rotate(0, getCollider(2).rpm / 60 * 360 * Time.deltaTime, 0);
        wheels[7].Rotate(0, getCollider(3).rpm / 60 * 360 * Time.deltaTime, 0);

        //brakes
        if (brake > 0.0f)
        {
            getCollider(0).brakeTorque = brake; //apply brake force
            getCollider(1).brakeTorque = brake;
            getCollider(2).brakeTorque = brake;
            getCollider(3).brakeTorque = brake;
            getCollider(2).motorTorque = 0.0f;
            getCollider(3).motorTorque = 0.0f;
        }
        else
        {
            getCollider(0).brakeTorque = 0.0f; //remove brake force
            getCollider(1).brakeTorque = 0f;
            getCollider(2).brakeTorque = 0f;
            getCollider(3).brakeTorque = 0f;
            getCollider(2).motorTorque = instantPower;
            getCollider(3).motorTorque = instantPower;
        }
        velocitydisp = rb.velocity.magnitude * 3.6f;
        SetSpeedDisplay();
    }

    WheelCollider getCollider(int n)
    {
        return wheels[n].gameObject.GetComponent<WheelCollider>(); //get access to wheel colliders
    }

        

    void SetSpeedDisplay()
    {
        speeddisplay.text = "Speed: " + velocitydisp.ToString("0") + "km/h"; //Speed display
        superspeeddisplay.text = "Speed: " + velocitydisp.ToString("0") + "km/h";
        float steeringinput = Input.GetAxis("Horizontal");
        steeringinputdisp.value = steeringinput;
        float throttleinput = Input.GetAxis("Throttle");
        throttleinputdisp.value = throttleinput;
        float brakeinput = Input.GetAxis("Brake");
        brakeinputdisp.value = brakeinput;
    }

    void Update()
    {
        //Audio modulation////////////////////////////////////////////////////////////////////////////
        float currentPitch = 0.00f; //audio pitch

        currentPitch = (Mathf.Abs(rb.velocity.magnitude)/25) + 0.8f; //+0.8f so sound is audible when idling
        audio_source.pitch = currentPitch;
        //////////////////////////////////////////////////////////////////////////////////////////////

        //Graph telemetry/////////////////////////////////////////////////////////////////////////////
        //normalise power, brakes and turn angle to value between 0 and 1 (-1 and 1 for turn angle)
        float normalised_power = (Input.GetAxis("Throttle")) / 2;
        float normalised_brakes = ((Input.GetAxis("Brake")) / 2);
        float normalised_turn = WheelTurn / maxTurn;
        float current_time = Time.time;
        float old_time = 0f;
        do
        {

            string lines = Time.time.ToString("0.000") + ";" + rb.transform.position.x.ToString("0.000") + ";" + rb.transform.position.y.ToString("0.000") + ";" + rb.transform.position.z.ToString("0.000") + ";" + velocitydisp.ToString("0.00") + ";" + normalised_turn.ToString("0.000") + ";" + normalised_power.ToString("0.000") + ";" + normalised_brakes.ToString("0.000") + ";";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\Output_Files\\Driver_Analysis_Telemetry.txt", true))
            {
                file.WriteLine(lines);
            }
            old_time = current_time;
        }
        while (current_time > (old_time + 1)); //write to file at an interval
        ////////////////////////////////////////////////////////////////////////////////////////////////

        //Interference//////////////////////////////////////////////////////////////////////////////////

        //shake car
        if (Input.GetKey(KeyCode.J))
        {
            CarPos = car.transform.position;
            CarPos.y = car.transform.position.y + 0.1f;
            car.transform.position = CarPos;
        }

        //Move camera (broken)
        if (Input.GetKey(KeyCode.PageUp))
        {
            camera_orientation = player_camera.transform.position;
            camera_orientation.y = camera_orientation.y + 100;
        }

        //Reset camera (broken)
        if (Input.GetKey(KeyCode.E))
        {
            camera_orientation = standard_camera_orientation;
        }

        //vibrate controller (XBOX controller only)
        if (Input.GetKey(KeyCode.V))
        {
            GamePad.SetVibration(0, testA, testA);
        }

        //
        /////////////////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); //quit to main menu on ESC
                                        //Application.Quit(); //quit program on escape
        }
        if (Input.GetKey(KeyCode.End))
        {
            Application.Quit(); //quit application on end key
        }  
    }
}


