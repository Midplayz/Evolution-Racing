using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGameCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            HyperCasual.Runner.GameManager.Instance.Win();
        }
    }
}
