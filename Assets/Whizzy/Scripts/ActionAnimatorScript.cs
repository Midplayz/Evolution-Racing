using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimatorScript : MonoBehaviour
{
    [field: Header("Animation Randomizer")]
    [SerializeField] private int delayInterval = 1;
    [SerializeField][Range(0, 100)] private int probabilityOfOccurance;
    [SerializeField] private string triggerName;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isActive;
    private int randomNum = 0;

    private IEnumerator AnimationRandomizer()
    {
        while(isActive)
        {
            randomNum = Random.Range(0, 100);
            if(randomNum < probabilityOfOccurance) 
            {
                animator.SetTrigger(triggerName);
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                yield return new WaitForSeconds(delayInterval);
            }
        }
    }
    private void OnEnable()
    {
        if (TryGetComponent<Animator>(out animator) == false)
        {
            Debug.Log("Animator on Player not found!"); //Debug is Required, Do not REMOVE
            return;
        }
        isActive = true;
        StartCoroutine(AnimationRandomizer());
    }
    private void OnDisable()
    {
        isActive= false;
    }
}
