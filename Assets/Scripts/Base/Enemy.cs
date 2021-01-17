using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationHandler), typeof(EnemyHandler),typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public int Level = 0;
    public int Health;
    public Animator Animator;
    public AnimationHandler AnimationHandler;
    public BoxCollider2D BoxCollider2D;
    
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        AnimationHandler = GetComponent<AnimationHandler>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        GetComponent<EnemyHandler>().Enemy = this;
    }
}
