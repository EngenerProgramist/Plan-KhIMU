﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;


public class CameraMoving : MonoBehaviour
{
    private Camera camera;
    public IObservable <Vector2> Movement { get; private set; }
    public IObservable <Vector2> Mouselook { get; private set; }

    public IObservable <float> zoomScroll { get; private set; }
    void Start()
    {
        camera = GetComponent<Camera>();


        zoomScroll = this.FixedUpdateAsObservable()
            .Select(_ => {
                return Input.GetAxis("Mouse ScrollWheel");
            });
        zoomScroll = this.FixedUpdateAsObservable()
            .Select(_ => {
                float x = 0;
                if (Input.GetKey(KeyCode.Q)) {
                    x = 1;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    x = -1;
                }
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

        var click = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButton(0));


       // var role = Observable.EveryUpdate()
       //     .Where(_ => Input.GetAxis("Mouse ScrollWheel"));
    }

    


    void Update()
    {
        
    }
}
