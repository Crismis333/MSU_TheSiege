using UnityEngine;
using System.Collections;

public class MapMovementController : MonoBehaviour {

    public Transform topLeftLimit;
    public Transform bottomRightLimit;

    bool zoomedIn;
    bool mdown;
    bool onguidown;
    int mdowncool;

    void Start() {
        zoomedIn = true;
        mdown = false;
        onguidown = false;
        mdowncool = 0;
        //Camera.mainCamera.transform.Rotate(new Vector3(-40, 0, 0));
    }

    void Update() {
        Rect GUI_Area = new Rect(Screen.width - 400, 50, 350, 450);
        if (Input.GetMouseButtonDown(0))
        {
            if (GUI_Area.Contains(Input.mousePosition))
            {
                onguidown = true;
                return;
            }
        }
        else
        {
            if (!GUI_Area.Contains(Input.mousePosition))
            {
                //onguidown = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            onguidown = false;
        }

        Camera.mainCamera.transform.Rotate(new Vector3(30, 0, 0));
        Vector3 pos = Camera.mainCamera.transform.position;
        bool left, right, up, down;
        left = right = up = down = false;

        Vector2 topleftpos = Camera.mainCamera.WorldToScreenPoint(topLeftLimit.transform.position);
        Vector2 botrightpos = Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position);
        Vector2 newposright = Camera.mainCamera.ScreenToWorldPoint(Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position));

        float multiplier;
        if (zoomedIn)
            multiplier = 40.0f;
        else
            multiplier = 20.0f;

        if (topleftpos.x >= -multiplier && botrightpos.x <= Screen.width + multiplier)
        { }
        else if (topleftpos.x >= -multiplier)
        {
            left = true;
        }
        else if (botrightpos.x <= Screen.width + multiplier)
        {
            right = true;
        }
        if (topleftpos.y <= Screen.height + multiplier && botrightpos.y >= -multiplier)
        { }
        else if (topleftpos.y <= Screen.height + multiplier)
        {
            up = true;
        }
        else if (botrightpos.y >= -multiplier)
        {
            down = true;
        }

        if (topleftpos.x >= 0 && botrightpos.x <= Screen.width)
        {
            Vector3 newpos = Camera.mainCamera.WorldToScreenPoint(topLeftLimit.transform.position);
            Vector3 newpos2 = Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position);
            newpos.x += Screen.width / 2f;
            newpos2.x -= Screen.width / 2f;
            newpos += newpos2;
            newpos /= 2;
            pos.x = Camera.mainCamera.ScreenToWorldPoint(newpos).x;
        }
        else if (topleftpos.x >= 0)
        {
            Vector3 newpos = Camera.mainCamera.WorldToScreenPoint(topLeftLimit.transform.position);
            newpos.x += Screen.width / 2f;
            pos.x = Camera.mainCamera.ScreenToWorldPoint(newpos).x;
        }
        else if (botrightpos.x <= Screen.width)
        {
            Vector3 newpos = Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position);
            newpos.x -= Screen.width / 2f;
            pos.x = Camera.mainCamera.ScreenToWorldPoint(newpos).x;
        }
        if (topleftpos.y <= Screen.height && botrightpos.y >= 0)
        {
            Vector3 newpos = Camera.mainCamera.WorldToScreenPoint(topLeftLimit.transform.position);
            Vector3 newpos2 = Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position);
            newpos.y -= Screen.height / 2f;
            newpos2.y += Screen.height / 2f;
            newpos += newpos2;
            newpos /= 2;
            pos.z = Camera.mainCamera.ScreenToWorldPoint(newpos).z;
        }
        else if (topleftpos.y <= Screen.height)
        {
            Vector3 newpos = Camera.mainCamera.WorldToScreenPoint(topLeftLimit.transform.position);
            newpos.y -= Screen.height / 2f;
            pos.z = Camera.mainCamera.ScreenToWorldPoint(newpos).z;
        }
        else if (botrightpos.y >= 0)
        {
            Vector3 newpos = Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position);
            newpos.y += Screen.height / 2f;
            pos.z = Camera.mainCamera.ScreenToWorldPoint(newpos).z;
        }

        if (mdowncool > 0)
            mdowncool--;
        float xm = Input.GetAxis("Mouse X");
        float ym = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(0) && !onguidown)
        {
            if (xm < -0.02f && !right)
                pos.x += xm / multiplier;
            else if (xm > 0.02f && !left)
                pos.x += xm / multiplier;
            if (ym < -0.02f && !up)
                pos.z += ym / multiplier;
            else if (ym > 0.02f && !down)
                pos.z += ym / multiplier;
        }

        if (!mdown)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomedIn = !zoomedIn;
                mdown = true;
                mdowncool = 5;
            }
        }
        else {
            if (mdowncool == 0)
                mdown = false;
        }

        if (zoomedIn) {
            //Camera.mainCamera.fieldOfView = 40;
            //pos.y = 1.361f;
            /*
            float upper = -2f;
            float lower = 2f;
            pos.y = 1.361f;
            if (pos.x < upper)
                pos.x = upper;
            else if (pos.x > lower)
                pos.x = lower;
            if (pos.z < -1.79f)
                pos.z = -1.79f;
            else if (pos.z > 1.9f)
                pos.z = 1.9f;
             */
        }
        else {
            //Camera.mainCamera.fieldOfView = 80;
            //pos.y = 5f;
            /*
            float upper = -0.58f;
            float lower = 0.4f;
            pos.y = 5f;
            if (pos.x < upper)
                pos.x = upper;
            else if (pos.x > lower)
                pos.x = lower;
            if (pos.z < -1.05f)
                pos.z = -1.05f;
            else if (pos.z > 1.28f)
                pos.z = 1.28f;
             */

        }


        float posx = Camera.main.transform.position.x;
        float posz = Camera.main.transform.position.y;
        float posy = Camera.main.transform.position.z;
        print(topleftpos.x + " " + topleftpos.y + " " + botrightpos.x + " " + botrightpos.y);
        //print(Input.mousePosition.x + " " + Input.mousePosition.y);
        Camera.main.transform.position = pos;
        Camera.mainCamera.transform.Rotate(new Vector3(-30, 0, 0));
    }
}
