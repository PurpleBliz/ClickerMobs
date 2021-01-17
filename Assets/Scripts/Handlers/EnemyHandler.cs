
using System;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Enemy Enemy;
    public int Damage;
   
    public delegate void HitEnemy(int damage);
    public event HitEnemy Notify;
    private Animator _animator => Enemy.Animator;
    private AnimationHandler _animationHandler => Enemy.AnimationHandler;
    private BoxCollider2D _boxCollider2D => Enemy.BoxCollider2D;

    private void OnMouseUpAsButton()
    {
        Hit(Damage);
    }

    private void Hit(int Damage)
    {
        if (Enemy.Health > 0)
        {
            Enemy.Health -= Damage;
            HitAnimation();
        }

        if (Enemy.Health <= 0)
            DeathAnimation();
        
        Notify?.Invoke(Enemy.Health);
    }

    public void ChangeBoxCollider(bool Change)
    {
        _boxCollider2D.enabled = Change;
    }

    public void ShowAnimation()
    {
        _animationHandler.PlayAnimation(_animator, "Show", 0.15f);
        ChangeBoxCollider(true);
    }
    public void HitAnimation()
    {
        _animationHandler.PlayAnimation(_animator, "Hit", 0.15f);
    }
    public void DeathAnimation()
    {
        _animationHandler.PlayAnimation(_animator, "Death", 0.15f);
        ChangeBoxCollider(false);
    }
}