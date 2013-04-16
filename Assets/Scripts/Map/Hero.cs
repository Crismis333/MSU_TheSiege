using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    Vector3 startLocation;
    Vector3 endLocation;
    float move;
    bool canmove;

    public void LookAtLoc(Location targetLoc)
    {
        Vector3 target = targetLoc.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    public void MoveToLoc(Location targetLoc)
    {
        endLocation = targetLoc.transform.position;
        canmove = true;
        move = 0f;
    }

    void Start()
    {
        startLocation = this.transform.position;
        move = 0f;
    }

    void Update()
    {
        if (canmove)
        {
            this.transform.position = Vector3.Lerp(startLocation, endLocation, move);
            move += Time.deltaTime/2;
        }
    }
}
