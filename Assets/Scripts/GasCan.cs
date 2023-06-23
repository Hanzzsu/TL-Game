using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GasCan : MonoBehaviour
{
	
    private void OnTriggerEnter(Collider other)
   {
		PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
		if(playerInventory != null) 
		{
			playerInventory.GasCanCollected();
			gameObject.SetActive(false);
		}
    }
}
