using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour {

    private GameObject player;
    private bool inRange, isChosen, isDone;

    public float AwareRange;
    public GameObject Indicator;
    private GameObject selectedIndicator;
    public bool AttackDone;

    // Use this for initialization
    void Start()
    {
        player = ObstacleController.PLAYER;
    }

    void enterInRange()
    {
        print("Is in range");
        selectedIndicator = (GameObject)Instantiate(Indicator, gameObject.transform.position + new Vector3(0, 3, 0), gameObject.transform.rotation);
        selectedIndicator.transform.parent = gameObject.transform;
    }

    public void SetChosen(bool value)
    {
        if (inRange)
        {
            isChosen = value;
            if (value)
            {
                selectedIndicator.renderer.material.color = Color.green;
            }
            else
            {
                selectedIndicator.renderer.material.color = Color.white;
            }
        }
    }

    void OnDestroy()
    {
        player.GetComponent<HeroAttack>().RemoveFromList(gameObject, isChosen);
    }

    public void KillSelf()
    {
        inRange = false;
        isDone = true;
        player.GetComponent<HeroAttack>().RemoveFromList(gameObject, isChosen);
        Destroy(selectedIndicator);
        // Add crazy death animation
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isDone)
        {
            if (!inRange)
            {
                if (gameObject.transform.position.z < player.transform.position.z + AwareRange)
                {
                    inRange = true;
                    enterInRange();
                    player.GetComponent<HeroAttack>().AddToList(gameObject);


                }
            }
            else
            {
                if (gameObject.transform.position.z < player.transform.position.z)
                {
                    inRange = false;
                    isDone = true;
                    player.GetComponent<HeroAttack>().RemoveFromList(gameObject, isChosen);
                    Destroy(selectedIndicator);
                }
            }
        }
	}
}
