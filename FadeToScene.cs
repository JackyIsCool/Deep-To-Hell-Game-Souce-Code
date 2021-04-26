using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeToScene : MonoBehaviour
{
    [SerializeField] string toSceneName;
    [SerializeField] bool anyKeyActivation;
    private void Update()
    {
        if (anyKeyActivation && Input.anyKeyDown && !Input.GetMouseButton(0))
        {
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        GetComponent<Animator>().SetTrigger("ChangeScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(toSceneName);
    }
    public void LoadScene2()
    {
        StartCoroutine(LoadScene());
    }
}
