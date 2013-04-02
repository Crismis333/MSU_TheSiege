using UnityEngine;
using System.Collections;

public class MapMovementController : MonoBehaviour {

    bool zoomedIn;
    bool mdown;
    bool onguidown;
    int mdowncool;

    private Rect GUI_Area;

    void Start() {
        zoomedIn = true;
        mdown = false;
        onguidown = false;
        mdowncool = 0;
        GUI_Area = new Rect(Screen.width - 400, 50, 350, 450);
    }

    void Update() {

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
                onguidown = false;
            }
        }
        if (mdowncool > 0)
            mdowncool--;
        float xm = Input.GetAxis("Mouse X");
        float ym = Input.GetAxis("Mouse Y");
        float multiplier;
        if (zoomedIn)
            multiplier = 40.0f;
        else
            multiplier = 20.0f;
        Vector3 pos = Camera.main.transform.position;
        if (Input.GetMouseButton(0) && !onguidown)
        {
            if (xm < -0.02f)
                pos.x += xm / multiplier;
            else if (xm > 0.02f)
                pos.x += xm / multiplier;
            if (ym < -0.02f)
                pos.z += ym / multiplier;
            else if (ym > 0.02f)
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
        }
        else {
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
        }


        float posx = Camera.main.transform.position.x;
        float posz = Camera.main.transform.position.y;
        float posy = Camera.main.transform.position.z;
        print(posx + " " + posy + " " + posz);
        //print(Input.mousePosition.x + " " + Input.mousePosition.y);
        Camera.main.transform.position = pos;
    }
}
