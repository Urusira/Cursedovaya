using System;
using UnityEngine;

public class PlayerSightHandler : MonoBehaviour
{
    public static bool IsMouseSight = false;
    public static bool lookLock = false;
    public static Vector2 lookDir;
    public static float lookAngle;
    public static Vector2 mousePos;

    private static float playerCursorOffsetY;

    public GameObject playerCursor;
    public GameObject firePoint;

    public Texture2D cursorSecondModedTexture;

    private void Start()
    {
        lookDir = new Vector2(1.0f, 0.0f);
        playerCursorOffsetY = playerCursor.transform.position.y;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0f)
        {
            switchAimMode();
        }
        if (Input.GetButtonDown("LookLock"))
        {
            lookLock = true;
        }
        if (Input.GetButtonUp("LookLock"))
        {
            lookLock = false;
        }

        if (!lookLock)
        {
            updateLookDirection();
        }
        
        if (!IsMouseSight)
        {
            try
            {
                playerCursor.LeanRotateZ(lookAngle, 0.1f);
            }
            catch (Exception e)
            {
                
            }
        }
        else
        {
            playerCursor.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }
        
        firePoint.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }
    
    public void switchAimMode()
    {
        if(IsMouseSight)
        {
            IsMouseSight = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            IsMouseSight = true;
            Cursor.SetCursor(cursorSecondModedTexture, new Vector2(cursorSecondModedTexture.width/2, cursorSecondModedTexture.height/2), CursorMode.ForceSoftware);
        }
    }

    private void updateLookDirection()
    {
        if (IsMouseSight)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y -= playerCursorOffsetY;
            lookDir = mousePos-PlayerMovement.PlayerPosition;
        }
        else
        {
            if (PlayerMovement.MoveVector != Vector2.zero)
            {
                lookDir = PlayerMovement.MoveVector;
            }
        }

        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }

    public void resetCursor()
    {
        if (IsMouseSight)
        {
            Cursor.SetCursor(cursorSecondModedTexture, new Vector2(cursorSecondModedTexture.width/2, cursorSecondModedTexture.height/2), CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public void reset()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        IsMouseSight = false;
    }
}