using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weapon : MonoBehaviour
{
    public float Offset;
    public GameObject bullet;
    public Transform shootPosition;
    public weapon weaponScript;
    float TimebtwShots;
    public float startTimeBtwShots;

    // Update is called once per frame
    public void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);

        if (TimebtwShots <= 0f)
        {
            if (Input.GetMouseButton(0) && weaponScript.enabled)
            {
                Instantiate(bullet, shootPosition.position, transform.rotation);
                TimebtwShots = startTimeBtwShots;
            }
        }
        else
        {
            TimebtwShots -= Time.deltaTime;
        }

    }


}
