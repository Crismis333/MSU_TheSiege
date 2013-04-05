using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroAttack : MonoBehaviour {

    public List<GameObject> AttackList;

    private int selectedIndex;

    private GameObject selectedEnemy;


	// Use this for initialization
	void Start () {
        AttackList = new List<GameObject>();
     //   Time.timeScale = 0.5f;
	}

    public void AddToList(GameObject go)
    {
        AttackList.Add(go);
        if (AttackList.Count == 1)
        {
            // This is the only element in the list
            setChosen(go);
          //  go.GetComponent<EnemyAttack>().SetChosen(true);
        }
        else
        {
            AttackList.Sort((x, y) => x.transform.position.z.CompareTo(y.transform.position.z));
        }
    }

    public void RemoveFromList(GameObject go, bool selected)
    {
        
        AttackList.Remove(go);
        selectedIndex--;

      //  if (selected && AttackList.Count > 0)
        if (go.Equals(selectedEnemy) && AttackList.Count > 0)
        {
            GameObject best = null;
            float dist = float.MaxValue;
            foreach (GameObject obj in AttackList)
            {
                float tDist = obj.transform.position.z;
                if (tDist < dist)
                {
                    best = obj;
                    dist = tDist;
                }
            }
            if (best != null)
            {
             //   best.GetComponent<EnemyAttack>().SetChosen(true);
                selectedIndex = 0;
                setChosen(best);
            }
            AttackList.Sort((x, y) => x.transform.position.z.CompareTo(y.transform.position.z));
        }
    }

    void setChosen(GameObject obj)
    {
        this.selectedEnemy = obj;
        obj.GetComponent<EnemyAttack>().SetChosen(true);
    }

    void chooseNext()
    {
        int selectedIndex = AttackList.IndexOf(selectedEnemy);
        if (selectedIndex >= 0)
        {
         //   AttackList[selectedIndex].GetComponent<EnemyAttack>().SetChosen(false);
            this.selectedEnemy.GetComponent<EnemyAttack>().SetChosen(false);

        }
        selectedIndex++;
        selectedIndex %= AttackList.Count;
        setChosen(AttackList[selectedIndex]);       
    }

    void KillEnemy(GameObject enemy)
    {
        RemoveFromList(enemy, true);
        Destroy(enemy);
    }

    bool engaged;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Fire2")) // On keyboard: Alt
        {
            if (AttackList.Count > 1)
            {
                chooseNext();                
            }
        }
        if (!engaged && Input.GetButtonDown("Fire1")) // On keyboard: Left Ctrl
        {
            if (selectedEnemy != null)
            {
                print("Engaging");
                Bounds bounds = selectedEnemy.transform.Find("EngageBox").GetComponent<BoxCollider>().bounds;


                float min = bounds.min.z;
                float max = bounds.max.z;

                float range = max - min;

                float p = gameObject.transform.localPosition.z;

                if (p > min && p < max)
                {
                    engaged = true;
                }

              //  KillEnemy(selectedEnemy);
            }
        }
        if (engaged && Input.GetButtonUp("Fire1"))
        {
            if (selectedEnemy != null)
            {
                Bounds bounds = selectedEnemy.transform.Find("ReleaseBox").GetComponent<BoxCollider>().bounds;

                print("Releasing");
                float min = bounds.min.z;
                float max = bounds.max.z;

                float range = max - min;

                float p = gameObject.transform.localPosition.z;

                if (p > min && p < max)
                {
                    KillEnemy(selectedEnemy);
                }

                //  
            }
            engaged = false;
        }
	}
}
