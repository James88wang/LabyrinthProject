using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]

    private GameObject ImWin;

    public void WinGame()
    {
        ImWin.SetActive(true);
    }

}
