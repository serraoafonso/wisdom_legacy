using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandleBack : MonoBehaviour
{

    public GameObject InitialCanvas;
    public GameObject CurrentCanvas;
    public GameObject UpCanvas;
    public GameObject WarningCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onBackClicked()
    {
        if(UpCanvas == CurrentCanvas)
        {
            WarningCanvas.SetActive(true);
        }
        else
        {
            CurrentCanvas.SetActive(false);
            InitialCanvas.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
