using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElasped = 0f;
    SpriteRenderer spriteRenderer; //set color
    GameObject objRemove;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElasped = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElasped += Time.deltaTime;
        float newAlpha = startColor.a * (1 - timeElasped/fadeTime);

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha); //RGB - red green blue, alpha

        if(timeElasped > fadeTime)
        {
            Destroy(objRemove); 
        }
    }

}
