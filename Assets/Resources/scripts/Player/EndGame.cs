using System;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private PlayerInterface interfaceController;

    private void Start()
    {
        interfaceController = GetComponent<PlayerInterface>();
    }

    public void GameOver()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerInventory>().itemsContainer.SetActive(false);
        interfaceController.disableInterface();
        GetComponent<PlayerInventory>().enabled = false;
        GetComponent<Animator>().SetBool("die", true);
        interfaceController.GameOver();
    }
}