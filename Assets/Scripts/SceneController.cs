using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public GameObject turtPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(turtPrefab==null){
            Debug.Log("SceneController: Ay caramba, no hay tortugas para tí (no se ha establecido el prefab)");
        }

        StartCoroutine("corutinaSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator corutinaSpawn(){
        yield return new WaitForSeconds(1.5f);
        spawn(turtPrefab, turtPrefab.GetComponent<Turtle>().RightSpawnPoint);

        yield return new WaitForSeconds(2f);
        spawn(turtPrefab, turtPrefab.GetComponent<Turtle>().LeftSpawnPoint);

        yield return new WaitForSeconds(6f);
        spawn(turtPrefab, turtPrefab.GetComponent<Turtle>().RightSpawnPoint);
        
        yield return new WaitForSeconds(2f);
        spawn(turtPrefab, turtPrefab.GetComponent<Turtle>().LeftSpawnPoint);
    }

    private void spawn(GameObject prefab, Vector3 spawnPoint){

        Instantiate(prefab, spawnPoint, Quaternion.identity);

    }
}
