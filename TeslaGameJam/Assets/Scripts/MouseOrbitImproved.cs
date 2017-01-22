using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{
    public bool stop = true; 


    public AudioSource _aSource;


    public Transform target;
    public  float distance;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float wheelSpeed = 1f;


    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;



    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        distance = distanceMax;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    float lastDistance = 0;
    Vector2 lastPos = Vector2.zero;
    void LateUpdate()
    {
        
        if (stop)
            return;

        
        if (target)
        {
            x += OptionManager.instance._InverseXAxis?1:-1* InputController.instance.leftRaw * xSpeed * Mathf.Clamp((distance - distanceMin) / distanceMax, 0.05f, 0.15f) ;
            y += OptionManager.instance._InverseYAxis ? 1 : -1 * InputController.instance.forwardRaw * ySpeed * Mathf.Clamp((distance - distanceMin) / distanceMax, 0.05f, 0.15f);
            /*
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            */
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - InputController.instance.RLTrigger * wheelSpeed * distance, distanceMin, distanceMax);
            /*
            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            */
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            if (lastPos.x != x || lastPos.y != y || lastDistance != distance)
            {
                if(_aSource.isPlaying)
                _aSource.PlayOneShot(_aSource.clip);
            }

            lastDistance = distance;
            lastPos.Set(x, y);

        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
