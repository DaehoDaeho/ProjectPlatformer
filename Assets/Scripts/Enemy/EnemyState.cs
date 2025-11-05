using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public Animator animator;

    private bool isAlive = true;

    public void PlayHitAnimation()
    {
        if(animator != null)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }

    public bool GetAlive()
    {
        return isAlive;
    }
}
