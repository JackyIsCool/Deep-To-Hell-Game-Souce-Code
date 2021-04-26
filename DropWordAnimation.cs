using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DropWordAnimation : MonoBehaviour
{
    TMP_Text textMesh;
    Mesh mesh;
    Vector3[] startVertices;
    Vector3[] toVertices;
    TMP_TextInfo charactersInfo;
    //[SerializeField] LeanTweenType leanTweenType;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] float offset = 1000;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        charactersInfo = textMesh.textInfo;
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;

        startVertices = new Vector3[mesh.vertices.Length];
        toVertices = new Vector3[mesh.vertices.Length];
        mesh.vertices.CopyTo(startVertices, 0);
        mesh.vertices.CopyTo(toVertices, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            DropWordAnimate();
        }
    }

    
    void DropWordAnimate()
    {
        textMesh.ForceMeshUpdate();
        //mesh = textMesh.mesh;
        //vertices = mesh.vertices;
        
        LeanTween.value(gameObject, 0, offset, 20f)
            .setEase(animationCurve)
            .setOnUpdate(SetAnimateDrop);
        
    }
    void SetAnimateDrop(float value)
    {

        StartCoroutine(DropWord(value));
        //set things back
        mesh.vertices = toVertices;
        textMesh.canvasRenderer.SetMesh(mesh);

    }
    IEnumerator DropWord(float value)
    {
        for (int i = 0; i < charactersInfo.characterCount; i++)
        {
            //get the first vertice index of each character
            int vertexIndex = charactersInfo.characterInfo[i].vertexIndex;
            //get the whole character's vertices
            for (int j = 0; j < 4; j++)
            {
                toVertices[vertexIndex + j].y = startVertices[vertexIndex + j].y + value;
            }
            yield return new WaitForSeconds(.08f);
        }
        
    }
}
