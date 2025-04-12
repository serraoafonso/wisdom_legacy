using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWarning : MonoBehaviour
{
    public GameObject initialScreen;
    public GameObject WarningCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onButtonStayClicked()
    {
        WarningCanvas.SetActive(false);
    }

    public void onButtonLeaveClicked()
    {
        WarningCanvas.SetActive(false);
        initialScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
