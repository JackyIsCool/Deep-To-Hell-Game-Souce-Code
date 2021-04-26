using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeeperTransition : MonoBehaviour
{
    [SerializeField] string scene;
    

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LoadScene());
        }
    }
    
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        float cameraY = Camera.main.transform.position.y;
        LeanTween.moveY(Camera.main.gameObject, cameraY - 20, 3f)
            .setEaseInOutSine();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene) ;
    }
    public void LoadScene2()
    {
        StartCoroutine(LoadScene());
    }
}
