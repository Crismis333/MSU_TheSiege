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

    public void CenterCamera(Transform t)
    {
        /*
        Camera.mainCamera.transform.Rotate(new Vector3(30, 0, 0));
        Vector3 pos = Camera.mainCamera.transform.position;
        Vector3 newpos = Camera.mainCamera.WorldToScreenPoint(t.position);
        newpos.x += Screen.width / 2f;
        newpos.y -= Screen.height / 2f;
        pos.x = Camera.mainCamera.ScreenToWorldPoint(newpos).x;
        pos.z = Camera.mainCamera.ScreenToWorldPoint(newpos).z;
        Camera.mainCamera.transform.position = pos;// ClampToMap(pos);
        Camera.mainCamera.transform.Rotate(new Vector3(-30, 0, 0));
        print("moved: " + t.position + " " + pos);
         */
        this.transform.position = new Vector3(t.position.x,this.transform.position.y,t.position.z);
    }

    private Vector3 ClampToMap(Vector3 pos)
    {
        Vector2 topleftpos = Camera.mainCamera.WorldToScreenPoint(topLeftLimit.transform.position);
        Vector2 botrightpos = Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position);
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
        return pos;
    }

    void Update() {
        Rect GUI_Area = new Rect(Screen.width - 400, 50, 350, 450);
        if (Screen.lockCursor)
            return;
        if (GetComponentInChildren<MapGui>().started || GetComponentInChildren<MapGui>().stopped)
            return;
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

        if (Input.GetMouseButton(1))
        {
            if (!GUI_Area.Contains(Input.mousePosition))
            {
                Camera.mainCamera.GetComponent<MapGui>().current_location = null;
                Camera.mainCamera.GetComponent<MapGui>().ResetScroll();
            }
        }

        Camera.mainCamera.transform.Rotate(new Vector3(30, 0, 0));
        Vector3 pos = this.transform.position;
        bool left, right, up, down;
        left = right = up = down = false;

        Vector2 topleftpos = Camera.mainCamera.WorldToScreenPoint(topLeftLimit.transform.position);
        Vector2 botrightpos = Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position);
        Vector2 newposright = Camera.mainCamera.ScreenToWorldPoint(Camera.mainCamera.WorldToScreenPoint(bottomRightLimit.transform.position));

        float multiplier = 40.0f;

        if (topleftpos.x >= -(3 * multiplier + 20) && botrightpos.x <= Screen.width + 3 * multiplier + 20)
        { }
        else if (topleftpos.x >= -(3*multiplier + 20))
        {
            left = true;
        }
        else if (botrightpos.x <= Screen.width + 3* multiplier + 20)
        {
            right = true;
        }
        if (topleftpos.y <= Screen.height + 3 * multiplier + 20 && botrightpos.y >= -(3 * multiplier + 20))
        { }
        else if (topleftpos.y <= Screen.height + 3 * multiplier + 20)
        {
            up = true;
        }
        else if (botrightpos.y >= -(3 * multiplier + 20))
        {
            down = true;
        }

        pos = ClampToMap(pos);

        if (mdowncool > 0)
            mdowncool--;
        float xm = Input.GetAxis("Mouse X");
        float ym = Input.GetAxis("Mouse Y");

        if (xm > 3)
            xm = 4;
        if (ym > 3)
            ym = 4;
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

        pos = ClampToMap(pos);
        

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

        this.transform.position = pos;
        Camera.mainCamera.transform.Rotate(new Vector3(-30, 0, 0));
    }
}
