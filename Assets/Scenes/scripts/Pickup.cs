using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private inventory inventory;
    public GameObject itemButton;
    public Transform itemPosition;

    public void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<inventory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided with something");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    gameObject.transform.position = itemPosition.position;
                    transform.parent = itemPosition.transform;
                    if (gameObject.GetComponent<weapon>())
                    {
                        gameObject.GetComponent<weapon>().enabled = true;

                    }
                  
                    gameObject.SetActive(false);

                    break;
                }
            }
        }
    }
}
