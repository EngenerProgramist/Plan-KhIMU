using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BuildingCanvasBeh : MonoBehaviour
{
    public Vector3          StartPosition   ;
    public Vector3          Position        ;
    public Vector3          Rotation        ;
    public Text             InfoText        ;
    public Image            InfoImage       ;

    private Canvas          currentCanvas   ;
    private RectTransform   ceryCanvas      ;


    void Awake()
    {
        currentCanvas   =   GetComponent<Canvas>();
        ceryCanvas      =   GetComponent<RectTransform>();
        
    }

    void Start()
    {
        var CompositControl = Observable.EveryLateUpdate()
            .Subscribe(s=> {

                ScaleImage();


            })
            .AddTo(this);
    }
    private void ScaleImage() {
        var sizeOfCanvas    =   ceryCanvas.sizeDelta;
        var xSizeForImage   =   (sizeOfCanvas.x*0.38f)*2;
        
        var ySizeForImage = (sizeOfCanvas.y * 0.38f) * 2;


        //print(sizeOfCanvas.x +" : "+ xSizeForImage);

        InfoImage.rectTransform.sizeDelta = new Vector2(xSizeForImage, InfoImage.rectTransform.sizeDelta.y);  

    }
}
