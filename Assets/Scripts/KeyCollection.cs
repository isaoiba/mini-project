using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyCollection : MonoBehaviour
{
    public TMP_Text text;
    public int keysneeded;
    private int _keysCollected;
    
    // Start is called before the first frame update
    void Start()
    {
        _keysCollected = 0;
        text = text.GetComponent<TMP_Text>();
    }

    void Update() {
        if (_keysCollected == keysneeded)
        {
            SceneManager.LoadScene (sceneName:"Win Screen");
            
        }
    }

    public void playerHit(GameObject other){
        Debug.Log(other.gameObject.name);
        ++_keysCollected;
        other.gameObject.SetActive(false);
        text.text = "Objective:\nCollect the keys ("+_keysCollected+"/6)";
    }
}
