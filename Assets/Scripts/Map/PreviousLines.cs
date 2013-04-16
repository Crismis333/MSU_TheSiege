using UnityEngine;
using System.Collections;

public class PreviousLines : MonoBehaviour {

    public Material Line_Material;
    private GameObject[] linerenderes;
    private int offset;

	// Use this for initialization
	void Start () {
        offset = 0;
	}

    public void Init()
    {
        linerenderes = new GameObject[CurrentGameState.completedLevelLocations.Count - 1];
        GameObject lr;
        for (int i = 0; i < CurrentGameState.completedLevelLocations.Count - 1; i++)
        {
            lr = new GameObject();
            lr.AddComponent<LineRenderer>();
            SetupLineRenderer(lr.GetComponent<LineRenderer>(), CurrentGameState.completedLevelLocations[i], CurrentGameState.completedLevelLocations[i + 1]);
            linerenderes[i] = lr;
        }
    }

    private void SetupLineRenderer(LineRenderer lr, Vector3 po1, Vector3 po2)
    {
        lr.receiveShadows = false;
        lr.SetWidth(0.02F, 0.02F);
        lr.SetVertexCount(2);
        Vector3 nl = po1;
        nl.y += 0.1f;
        lr.SetPosition(0, nl);
        nl = po2;
        nl.y += 0.1f;
        float diff = Vector3.Distance(po1, po2);
        lr.SetPosition(1, nl);
        lr.material = Line_Material;
        lr.material.mainTextureScale = new Vector2(1F * (diff) * 5f, 1F);
    }
	
	// Update is called once per frame
	void Update () { /*
        if (linerenderes.Length > 0)
        {
            if (Camera.mainCamera.GetComponent<MapGui>().stopped)
            {
                for (int i = 0; i < linerenderes.Length; i++)
                {
                    linerenderes[i].SetActive(false);
                }
                linerenderes = new GameObject[0];
            }
            for (int i = 0; i < linerenderes.Length; i++)
            {
                linerenderes[i].GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(-Time.time * 0.4f, 0);
            }
            offset++;
            if (offset > 1024)
                offset = 0;
        }*/
	}
}
