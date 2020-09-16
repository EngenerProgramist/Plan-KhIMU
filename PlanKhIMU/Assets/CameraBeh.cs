using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class CameraBeh : MonoBehaviour
{

    private Camera camera;
    private Moving moving;
    // Start is called before the first frame update
    void Start()
    {
        var moving = Moving.Instance;
        camera = GetComponent<Camera>();

        //moving.Movement
        //    .Where(v => v != Vector2.zero)
        //    .Subscribe(t => { }).AddTo(this);           

    }

 
    void Update()
    {
        
    }
}
