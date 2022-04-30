using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

class MenuController : MonoBehaviour
{

    public void StartBtn(){
        SceneManager.LoadScene("proto-map");
    }
}