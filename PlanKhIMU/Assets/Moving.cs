using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;


public class Moving : MonoBehaviour
{
    public static Moving Instance { get; private set; }
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    
    float camSens = 0.25f;
    public IObservable <Vector2> Movement { get; private set; }
    public IObservable <Vector2> Mouselook { get; private set; }

    public IObservable <Vector2> MouseClick { get; private set; }

    public IObservable <float> zoomScroll { get; private set; }
    void Start()
    {
        //camera = GetComponent<Camera>();
        Instance = this;

        //zoomScroll = this.FixedUpdateAsObservable()
        //    .Select(_ => {
        //    });
        zoomScroll = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                float x = 0;
                x = Input.GetAxis("Mouse ScrollWheel");
                if (Input.GetKey(KeyCode.E))
                {
                    x = 1;
                }
                if (Input.GetKey(KeyCode.F))
                {
                    x = -1;
                }
                //print(x);

                return x;//  ("Mouse ScrollWheel");

            });
          
        Movement = this.FixedUpdateAsObservable()
            .Select(_ => {
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxis("Vertical");
                return new Vector2(y, -x).normalized;
            });

        Mouselook = this.UpdateAsObservable()
                .Select(_ => {
                    var x = Input.GetAxis("Mouse X");
                    var y = Input.GetAxis("Mouse Y");
                    return new Vector2(x, y);
                });

        MouseClick = this.FixedUpdateAsObservable()
            .Select(_ =>
            { Vector3 click = Vector3.zero;
               
                if (Input.GetMouseButtonDown(0)) 
                {
                    click = Input.mousePosition;
                }
                //print(clickPlace.position);
                Vector2 clickV2 = new Vector2(click.x, click.y);
                return clickV2;
            });
        //MouseClick.Subscribe(s=> { print(s); }).AddTo(this);

    }
                
                
       // var role = Observable.EveryUpdate()
       //     .Where(_ => Input.GetAxis("Mouse ScrollWheel"));

    


    void Update()
    {
       // lastMouse = Input.mousePosition - lastMouse;
       // lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
       // lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
       // transform.eulerAngles = lastMouse;
       // lastMouse = Input.mousePosition;
    }
}
