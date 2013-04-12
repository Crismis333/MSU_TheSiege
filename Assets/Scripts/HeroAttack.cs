using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HeroAttack : MonoBehaviour {

    public List<GameObject> AttackList;

    private int selectedIndex;

    private GameObject selectedEnemy;
    private GUIScript GUI;
    


	// Use this for initialization
	void Start () {
        AttackList = new List<GameObject>();
        GUI = GameObject.Find("GUI").GetComponent<GUIScript>();
      //  Time.timeScale = 0.5f;
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

    bool engaged, firePressed, inEngageBox, inReleaseBox;
    float engagePercent, releasePercent;

    //private void handleAttack()
    //{
    //    if (selectedEnemy != null)
    //    {
    //        float p = gameObject.transform.localPosition.z;
    //        Bounds engageBounds = selectedEnemy.transform.Find("EngageBox").GetComponent<BoxCollider>().bounds;
    //        Bounds releaseBounds = selectedEnemy.transform.Find("ReleaseBox").GetComponent<BoxCollider>().bounds;

    //        float min = engageBounds.min.z;
    //        float max = engageBounds.max.z;

    //        if (p > min && p < max)
    //        {
    //            // In engage box

    //            float range = max - min;

    //            float dist = p - min;         

    //            float percentage = dist / range * 100;
    //            handleEngage(percentage);
    //        }
    //        else
    //        {
                
    //            min = releaseBounds.min.z;
    //            max = releaseBounds.max.z;

    //            if (p > min && p < max)
    //            {
    //                // In release box
    //                print("Release");
    //                float range = max - min;
    //                float dist = p - min;

    //                float percentage = dist / range * 100;
    //                handleRelease(percentage);
    //            }
               
    //        }
    //        if (firePressed && Input.GetButtonUp("Fire1"))
    //        {
    //            GUI.ResetBar();
    //            firePressed = false;
    //        }
    //    }
    //}

    //private void handleEngage(float percentage)
    //{
    //    inEngageBox = true;
    //    GUI.BarActive = true;

    //    GUI.engagePercent = percentage;
    //    engagePercent = percentage;

    //    if (!firePressed)
    //    {
    //        if (Input.GetButtonDown("Fire1"))
    //        {
    //            firePressed = true;
    //            GUI.engageFixed = true;
    //            GUI.fixedEngagePercent = percentage;
    //            engaged = true;
    //        }
    //    }
    //    else
    //    {
    //        // Too quick release - penalty
    //        print("Penalty - too quick");
    //        selectedEnemy.GetComponent<EnemyAttack>().Indicator.renderer.material.color = Color.red;
    //        firePressed = false;
    //    }
    //}

    //private void handleRelease(float percentage)
    //{
    //    if (firePressed)
    //    {
    //        print("Release");
    //        GUI.releasePercent = percentage;
    //        if (Input.GetButtonUp("Fire1"))
    //        {
    //            selectedEnemy.GetComponent<EnemyAttack>().KillSelf();
    //        }
    //    }
    //}

    //private void handleAttack2()
    //{
    //    float p = gameObject.transform.localPosition.z;
    //    Bounds engageBounds = selectedEnemy.transform.Find("EngageBox").GetComponent<BoxCollider>().bounds;
    //    Bounds releaseBounds = selectedEnemy.transform.Find("ReleaseBox").GetComponent<BoxCollider>().bounds;

    //    float eMin = engageBounds.min.z;
    //    float eMax = engageBounds.max.z;
    //    float rMax = releaseBounds.max.z;

    //    int c = 0; // 0: Outside boxes, 1: Engage box, 2: Release box, 3: Outside boxes

    //    if (p > eMin)
    //    {
    //        c++;
    //        if (p > eMax)
    //        {
    //            c++;
    //            if (p > rMax)
    //            {
    //                c++;
    //            }
    //        }            
    //    }

    //    switch (c)
    //    {
    //        case 0:
    //           // print("Before boxes");
    //            break;
    //        case 1:
    //          //  print("In engage box");
    //            float min = engageBounds.min.z;
    //            float max = engageBounds.max.z;

    //            float range = max - min;                                

    //            float dist = p - min;
    //            //   print("Engage: min: " + min + ", p: " + p + ", max: " + max + ", range: " + range + ", dist: " + dist);


    //            float percentage = dist / range * 100;

    //            if (percentage > 0 && percentage < 100)
    //            {
    //                inEngageBox = true;
    //                GUI.BarActive = true;

    //                GUI.engagePercent = percentage;
    //                engagePercent = percentage;
    //                if (!firePressed)
    //                {

    //                    if (Input.GetButtonDown("Fire1"))
    //                    {
    //                        firePressed = true;
    //                        GUI.engageFixed = true;
    //                        GUI.fixedEngagePercent = percentage;
    //                        engaged = true;
    //                    }
    //                }
    //                else
    //                {
    //                    if (Input.GetButtonUp("Fire1"))
    //                    {
    //                        // Too quick release - penalty
    //                        print("Penalty - too quick");
    //                        selectedEnemy.GetComponent<EnemyAttack>().Indicator.renderer.material.color = Color.red;
    //                        firePressed = false;
    //                    }
    //                }

    //            }
    //            break;
    //        case 2:
    //          //  print("In release box");
    //            break;
    //        case 3:
    //          //  print("After boxes");
    //            break;
    //    }
    //}
	
	// Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("EngageBox") && other.transform.parent.gameObject.Equals(selectedEnemy))
        {
            print("Enter engage box");
            inEngageBox = true;
        }
        if (other.gameObject.name.Equals("ReleaseBox") && other.transform.parent.gameObject.Equals(selectedEnemy))
        {
            print("Enter release box");
            inReleaseBox = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("EngageBox") && other.transform.parent.gameObject.Equals(selectedEnemy))
        {
            print("Leaving engage box");
            inEngageBox = false;
        }
        if (other.gameObject.name.Equals("ReleaseBox") && other.transform.parent.gameObject.Equals(selectedEnemy))
        {
            print("Leaving release box");
            inReleaseBox = false;
        }
    }

    void CalculateHit()
    {
        if (engagePercent > 0 && releasePercent > 0)
        {
            float engageAccuracy = Math.Abs(engagePercent - 50);
            float releaseAccuracy = Math.Abs(releasePercent - 50);

            int engageRate = (int)engageAccuracy / 10;
            int releaseRate = (int)releaseAccuracy / 10;

            int hitRate = engageRate + releaseRate;

            float similarity = Math.Abs(engagePercent - (100 - releasePercent));

            print("Hit rate: " + hitRate + ", similarity: " + similarity);
        }
    }

	void Update () {
        if (Input.GetButtonUp("Fire2")) // On keyboard: Alt
        {
            if (AttackList.Count > 1)
            {
                chooseNext();                
            }
        }
       
        else
        {
            if (selectedEnemy != null && !selectedEnemy.GetComponent<EnemyAttack>().AttackDone)
            {
                if (inEngageBox)
                {
                    //   print("Engaging");
                    Bounds bounds = selectedEnemy.transform.Find("EngageBox").GetComponent<BoxCollider>().bounds;


                    float min = bounds.min.z;
                    float max = bounds.max.z;                  

                    //  print("xMin: " + xMin + ", xMax: " + xMax);

                    float range = max - min;

                    float p = gameObject.transform.localPosition.z;

                    float dist = p - min;
                    //   print("Engage: min: " + min + ", p: " + p + ", max: " + max + ", range: " + range + ", dist: " + dist);


                    float percentage = dist / range * 100;

                    if (percentage > 0 && percentage < 100)
                    {

                        GUI.BarActive = true;

                        GUI.engagePercent = percentage;
                     //   engagePercent = percentage;
                        if (!firePressed)
                        {

                            if (Input.GetButtonDown("Fire1"))
                            {
                                firePressed = true;
                                GUI.engageFixed = true;
                                GUI.fixedEngagePercent = percentage;
                                engagePercent = percentage;
                                engaged = true;
                            }
                        }
                        else
                        {
                            if (inEngageBox && Input.GetButtonUp("Fire1"))
                            {
                                // Too quick release - penalty
                                print("Penalty - too quick");
                                selectedEnemy.GetComponent<EnemyAttack>().AttackDone = true;
                                firePressed = false;
                                GUI.ResetBar();
                            }
                        }

                    }
                }

            }



            if (engaged)
            {
                if (inReleaseBox)
                {
                    Bounds bounds = selectedEnemy.transform.Find("ReleaseBox").GetComponent<BoxCollider>().bounds;


                    float min = bounds.min.z;
                    float max = bounds.max.z;

                    float range = max - min;

                    float p = gameObject.transform.localPosition.z;
                    float dist = p - min;
                    //     print("Release: min: " + min + ", p: " + p + ", max: " + max + ", range: " + range + ", dist: " + dist);


                    float percentage = dist / range * 100;

                    releasePercent = percentage;

                    if (firePressed && percentage > 0 && percentage < 100)
                    {
                        inReleaseBox = true;
                        GUI.releasePercent = percentage;
                    }
                    else
                    {
                        inReleaseBox = false;
                    }
                    if (firePressed && Input.GetButtonUp("Fire1"))
                    {
                        GUI.ResetBar();
                        firePressed = false;
                        if (percentage > 0 & percentage < 100)
                        {
							selectedEnemy.GetComponent<EnemyAttack>().
								AddExplosion(ObstacleController.PLAYER.GetComponent<HeroMovement>().CurrentSpeed / 4 * 500, ObstacleController.PLAYER.transform.position + Vector3.up);
                            selectedEnemy.GetComponent<EnemyAttack>().KillSelf();
							
                            engaged = false;
                            releasePercent = percentage;
                            CalculateHit();
                        }
                    }

                }
            }
        }

       
	}
}
