using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.EntryPoint
{
    public class EndGame : MonoBehaviour
    {
        private void Start()
        {
            SceneChangerSingleton.Instance.FadeOut();
        }
    }
}