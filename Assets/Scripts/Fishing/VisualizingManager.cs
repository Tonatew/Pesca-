using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizingManager : MonoBehaviour
{
    public bool clicOnClose;

    // Start is called before the first frame update
    public void Start(){
        clicOnClose = false;
    }

    public void Close(){
        clicOnClose = true;
    }
}
