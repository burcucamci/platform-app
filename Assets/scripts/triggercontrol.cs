using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggercontrol : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void OnTriggerEnter2D(Collider2D collision) // player zemine değdiği an 
    {
        //trigger gerceklesti
        player.GetComponent<playercontroller>().onground = true;

     }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<playercontroller>().onground = false;
        
    }
}
