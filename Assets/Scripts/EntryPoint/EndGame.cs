using Assets.Scripts.General;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void Start()
    {
        SceneChangerSingleton.Instance.FadeOut();
    }
}
