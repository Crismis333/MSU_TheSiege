using UnityEngine;
using System.Collections;

public class MapMovementController : MonoBehaviour {

    bool zoomedIn;
    bool mdown;
    int mdowncool;

    void Start() {
        zoomedIn = true;
        mdown = false;
        mdowncool = 0;
    }

    void Update() {
        if (mdowncool > 0)
            mdowncool--;
        float xm = Input.GetAxis("Mouse X");
        float ym = Input.GetAxis("Mouse Y");
        float multiplier;
        if (zoomedIn)
            multiplier = 20.0f;
        else
            multiplier = 10.0f;
        Vector3 pos = Camera.main.transform.position;
        if (Input.GetMouseButton(0))
        {
            if (xm < -0.2f)
                pos.x += xm / multiplier;
            else if (xm > 0.2f)
                pos.x += xm / multiplier;
            if (ym < -0.2f)
                pos.z += ym / multiplier;
            else if (ym > 0.2f)
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
            pos.y = 1.361f;
            if (pos.x < -1.71f)
                pos.x = -1.71f;
            else if (pos.x > 1.73f)
                pos.x = 1.73f;
            if (pos.z < -1.79f)
                pos.z = -1.79f;
            else if (pos.z > 1.78f)
                pos.z = 1.78f;
        }
        else {
            pos.y = 5f;
            if (pos.x < -1.58f)
                pos.x = -1.58f;
            else if (pos.x > 0f)
                pos.x = 0f;
            if (pos.z < -1.05f)
                pos.z = -1.05f;
            else if (pos.z > 1.48f)
                pos.z = 1.48f;
        }


        float posx = Camera.main.transform.position.x;
        float posz = Camera.main.transform.position.y;
        float posy = Camera.main.transform.position.z;
        print(posx + " " + posy + " " + posz);
        Camera.main.transform.position = pos;
    }
}
