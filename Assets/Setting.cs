using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [SerializeField] GameObject escOption;
    [SerializeField] PlayerMovementScript script;
    [SerializeField] MouseLookScript mouseLookScript;
    [SerializeField] GunScript gunScript;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(escOption.activeSelf)
            {
                Time.timeScale = 1.0f;
                escOption.SetActive(false);
                script.enabled = true;
                mouseLookScript.enabled = true;
                GunScript gun = FindFirstObjectByType<GunScript>();
                if (gun != null)
                    gun.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0.0f;
                escOption.SetActive(true);
                script.enabled = false;
                GunScript gun = FindFirstObjectByType<GunScript>();
                if (gun != null)
                    gun.enabled = false; 
                mouseLookScript.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void OnResumeHandler()
    {
        Time.timeScale = 1.0f;
        escOption.SetActive(false);
        GunScript gun = FindFirstObjectByType<GunScript>();
        if (gun != null)
            gun.enabled = true; script.enabled = true;
        mouseLookScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnQuitHandler()
    {
        Application.Quit();
    }
}
