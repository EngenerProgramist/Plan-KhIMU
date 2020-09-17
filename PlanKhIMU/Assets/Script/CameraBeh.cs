using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class CameraBeh : MonoBehaviour
{
    public static CameraBeh Instance { get; private set; }
    public float speedmov;
    public float mouseSpeed;
    public Vector3 Debug;
    public Vector3 Debug2;
    public Camera camera;
    public Moving movi;
    [Range(-90, 0)]
    public float minViewAngle = -60f; // How much can the user look down (in degrees)
    [Range(0, 90)]
    public float maxViewAngle = 60f;

    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //camera = GetComponent<Camera>();
        movi = Moving.Instance;
        movi.Movement
            .Where(v => v!= Vector2.zero)
            .Subscribe(input => {
                var inputVelocity = input * speedmov;
                var x = inputVelocity.x;
                var z = inputVelocity.y*Vector3.forward;

                var playerVelocity = //new Vector3(x, 0, z.z);
                inputVelocity.x * this.gameObject.transform.right + // x (+/-) corresponds to strafe right/left
                inputVelocity.y * this.gameObject.transform.forward; // y (+/-) corresponds to forward/back
                

                var distance = playerVelocity * Time.fixedDeltaTime;
                Debug = input;
                Debug2 = distance;
                transform.Translate(distance);
            }).AddTo(this);
        movi.Mouselook
            .Where(v => v != Vector2.zero) // We can ignore this if mouse look is zero.
            .Subscribe(inputLook => {
                
                
                    // inputLook.x rotates the character around the vertical axis (with + being right)
                    var horzLook = inputLook.x * Time.deltaTime * Vector3.up * mouseSpeed;
                    transform.localRotation *= Quaternion.Euler(horzLook);

                    // inputLook.y rotates the camera around the horizontal axis (with + being up)
                    var vertLook = inputLook.y * Time.deltaTime * Vector3.left * mouseSpeed;

                    var newQ = this.transform.localRotation * Quaternion.Euler(vertLook);


                /*
                //print(view.transform.localRotation.eulerAngles.ToString());
                var x = view.transform.localRotation.eulerAngles.x; 
                if (x>maxViewAngle) {
                    view.transform.localRotation = Quaternion.Euler(new Vector3(maxViewAngle, view.transform.localRotation.eulerAngles.y,0));
                }
                if (x < minViewAngle) {
                    view.transform.localRotation = Quaternion.Euler(new Vector3(minViewAngle, view.transform.localRotation.eulerAngles.y, 0));
                }
                view.transform.localRotation *= Quaternion.Euler(vertLook);
                // We have to flip the signs and positions of min/max view angle here because the math
                // uses the contradictory interpretation of our angles (+/- is down/up).
                //view.transform.localRotation = Quaternion.Euler(view.transform.localRotation.eulerAngles.x, view.transform.localRotation.eulerAngles.y, 0);
                 */
                this.transform.localRotation = ClampRotationAroundXAxis(newQ, -maxViewAngle, -minViewAngle);
                    //hit.transform.localRotation = ClampRotationAroundXAxis(newQ, -maxViewAngle, -minViewAngle);
                    //cameraQuan = hit.transform.localRotation.eulerAngles;
                
            }).AddTo(this);
    
    }
    private Quaternion ClampRotationAroundXAxis(Quaternion q, float minAngle, float maxAngle)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, minAngle, maxAngle);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        //q.x = Mathf.Clamp(q.x, minAngle, maxAngle);
        //q.z = 0;
        //q.w = 0f;

        Vector3 rotation = q.eulerAngles;
        //rotation.y = 90;
        rotation.z = 0;
        q = Quaternion.Euler(rotation);
        ////rotation.x = (rotation.x>minAngle)&()?
        //
        ////rotation.x = Mathf.Clamp(rotation.x, Mathf.Abs(360-minAngle), maxAngle);
        //
        //	Quaternion qua = new Quaternion(q.x, q.y, q.z,1);
        return q;// ua;						
    }

    void Update()
    {
        
    }
}
