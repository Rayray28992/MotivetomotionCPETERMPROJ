using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] scenePrefabs;
    [SerializeField]
    Transform mainCharacterTransform;

    float[] sceneLenths;
    float offset;
    float lastGenerationZ = 0, currentSceneLenth;
    GameObject previousScene, currentScene;

    int state;
    
    
    void Start() {
        state = 0;
        sceneLenths = new float[3];
        sceneLenths[0] = 300;
        sceneLenths[1] = 200;
        sceneLenths[2] = 200;
        currentSceneLenth = 300;
        offset = 50;
        currentScene = Instantiate(scenePrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
        previousScene = null;
        lastGenerationZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("0 " + mainCharacterTransform.position.z + ", " + lastGenerationZ + ", " + offset);
        //check when to delete the previous scene
        if (state == 0) {
            //Debug.Log("1");
            if (mainCharacterTransform.position.z > lastGenerationZ + offset/4)
            {
                if (previousScene != null)
                {
                    Destroy(previousScene);

                }
                previousScene = currentScene;
                state = 1;
            }
        }

        //check when to generate a new scene
        if(mainCharacterTransform.position.z > lastGenerationZ + currentSceneLenth - 2*offset) {

            int randSceneNumber = Random.Range(0, 3);
            currentScene = Instantiate(scenePrefabs[randSceneNumber], new Vector3(0, 0, lastGenerationZ + currentSceneLenth), Quaternion.identity);
            lastGenerationZ += currentSceneLenth;
            currentSceneLenth = sceneLenths[randSceneNumber];
            state = 0;
        }
    }
}
