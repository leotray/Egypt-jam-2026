using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weaponSwitch : MonoBehaviour
{
    GameObject weapon1;
    GameObject weapon2;
   public GameObject weaponHolder;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {


            weapon1 = weaponHolder.transform.GetChild(0).gameObject;
            weapon1.SetActive(true);
            if (weaponHolder.transform.childCount > 1f)
            {
                weapon2 = weaponHolder.transform.GetChild(1).gameObject;
                if (weapon2.activeSelf)
                {
                    weapon2.SetActive(false);
                }
            }



        }
    }
    public void selectWeapon1(InputAction.CallbackContext context)
    {
        if(context.performed && transform.childCount > 0)
        {
            
            
                weapon1 = weaponHolder.transform.GetChild(0).gameObject;
                weapon1.SetActive(true);
                if(weaponHolder.transform.childCount >1f)
                {
                    weapon2 = weaponHolder.transform.GetChild(1).gameObject;
                    if(weapon2.activeSelf)
                    {
                        weapon2.SetActive(false);
                    }
                }

            
            
        }
        
    }
    public void SelectWeapon2(InputAction.CallbackContext context)
    {
        if(context.performed && transform.childCount >=2)
        {
            
            
                weapon1 = weaponHolder.transform.GetChild(0).gameObject;
                weapon2 = weaponHolder.transform.GetChild(1).gameObject;
                weapon1.SetActive(false);
                weapon2.SetActive(true);
            
            
        }
        

    }
}
