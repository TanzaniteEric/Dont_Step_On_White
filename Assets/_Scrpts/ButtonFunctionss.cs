using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionss : MonoBehaviour
{
    public GenerateEnemy m_generatenemy;
    public Canvas thisCanvas;
    public GameObject player;

    public void normalStart()
    {
        m_generatenemy.enabled = true;
        thisCanvas.gameObject.SetActive(false);
    }
}
