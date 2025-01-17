using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color defaultFenceColor;
    [SerializeField] private Color hoverFenceColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the sprite has the tag "Player"
        {
            _spriteRenderer.color = hoverFenceColor; // Change to the desired color
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spriteRenderer.color = defaultFenceColor; // Reset to original color
        }
    }
}