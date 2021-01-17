using UnityEngine;

public class Notyfy : MonoBehaviour
{
    public void Send(EnemyHandler boxCollider2D, Animator animator)
    {
        boxCollider2D.ChangeBoxCollider(false);
        animator.SetBool("Close", false);
    }
}