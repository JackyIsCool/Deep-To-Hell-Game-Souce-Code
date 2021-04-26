using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidLava : MonoBehaviour
{
    [SerializeField] ParticleSystem dropIntoLava;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dropIntoLava.transform.position = collision.collider.transform.position;
        dropIntoLava.Play();
    }
}
