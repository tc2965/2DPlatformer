using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCode : MonoBehaviour
{
   public GameManager _gameManger;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Trynig to load next level!");
            _gameManger.LoadNextLevel();
        }
    }
}
