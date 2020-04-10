using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class cameraOptions 
{
    static public void cursorLock() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    static public void cursorUnlock() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
