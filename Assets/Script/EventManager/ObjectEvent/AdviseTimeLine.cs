using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AdviseTimeLine : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&!Operator.Instance.isPlayAdvise)
        {
            playableDirector.Play();
            Operator.Instance.isPlayAdvise=true;
            this.gameObject.SetActive(false);
        }
    }
}
