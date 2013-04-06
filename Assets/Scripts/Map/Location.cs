using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {

    public int levelID;
    public string name, gains, losses;
    [Multiline]
    public string description;
    public float difficulty_soldier, difficulty_length, difficulty_pits, difficulty_obstacles, difficulty_catapults;
    public Location[] locations;
    public Material Line_Material;
    private GameObject[] linerenderes;
    private int offset;

    void Update()
    {
        if (linerenderes.Length > 0)
        {
            for (int i = 0; i < linerenderes.Length; i++)
            {
                linerenderes[i].GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(-Time.time * 0.4f, 0);
            }
            offset++;
            if (offset > 1024)
                offset = 0;
        }
    }

    void Start()
    {
        offset = 0;

        if (CurrentLocation.JustStarted && levelID == 0)
        {
            CurrentLocation.JustStarted = false;
            CurrentLocation.locID = this.levelID;
        }
        if (CurrentLocation.locID == this.levelID)
        {
            CurrentLocation.loc = this;
            GameObject lr;
            linerenderes = new GameObject[locations.Length];
            for (int i = 0; i < locations.Length; i++)
            {
                lr = new GameObject();
                lr.AddComponent<LineRenderer>();
                SetupLineRenderer(lr.GetComponent<LineRenderer>(), locations[i]);
                linerenderes[i] = lr;
            }
        }
        else 
        {
            linerenderes = new GameObject[0];
        }
    }

    private void SetupLineRenderer(LineRenderer lr, Location lo)
    {
        //Material m = new Material(Shader.Find("Self-Illumin/Diffuse"));
        //m.color = Color.white;
        //lr.material = m;

        lr.receiveShadows = false;
        lr.SetWidth(0.01F, 0.01F);
        lr.SetVertexCount(2);
        Vector3 nl = this.gameObject.transform.position;
        nl.y += 0.1f;
        lr.SetPosition(0, nl);
        nl = lo.gameObject.transform.position;
        nl.y += 0.1f;
        float diff = Vector3.Distance(this.gameObject.transform.position,lo.gameObject.transform.position);
        lr.SetPosition(1, nl);
        lr.material = Line_Material;
        lr.material.mainTextureScale = new Vector2(1F*(diff)*2f, 1F);
    }

    public bool isChildOfCurrent()
    {
        bool found = false;
        Location l = CurrentLocation.loc;
        int il = l.levelID;
        for (int i = 0; i < CurrentLocation.loc.locations.Length; i++)
        {
            if (CurrentLocation.loc.locations[i] == this)
            {
                found = true;
                break;
            }
        }
        return found;
    }
}
