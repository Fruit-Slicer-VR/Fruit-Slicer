using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent (typeof(Rigidbody))]
public class FruitCutter : MonoBehaviour
{   
    public Material capMaterial;
    public GameObject gameManagerObj;
    public GameObject leftHand;
    public GameObject rightHand;

    private GameManager gameManager;
    private ActionBasedController xrLeft;
    private ActionBasedController xrRight;
    
    void Start()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
        xrLeft = leftHand.GetComponent<ActionBasedController>();
        xrRight = rightHand.GetComponent<ActionBasedController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;

        switch (victim.tag)
        {
            case "Small":
                gameManager.SetScore(50);
                break;
            case "Medium":
                gameManager.SetScore(25);
                break;
            case "Large":
                gameManager.SetScore(10);
                break;
            case "Bomb":
                gameManager.Bombed();
                break;


        }

        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

        if(!pieces[1].GetComponent<Rigidbody>())
        {
            pieces[1].AddComponent<Rigidbody>();
            pieces[1].AddComponent<Fruit>();
            MeshCollider temp = pieces[1].AddComponent<MeshCollider>();
            temp.convex = true;
        }
        
        if(this.tag == "Left") {
           xrLeft.SendHapticImpulse(0.5f, 0.5f);
        } else if(this.tag == "Right") {
           xrRight.SendHapticImpulse(0.5f, 0.5f);
        }
    }
}
