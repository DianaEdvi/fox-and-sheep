using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    // [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color defaultFenceColor;
    [SerializeField] private Color hoverFenceColor;

    /**
     * Change the color of the fence if the player is hovering over it
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the sprite has the tag "Player"
        {
            _spriteRenderer.color = hoverFenceColor; // Change to the desired color
        }
    }

    /**
     * Change the color of the fence back to default if the player leaves
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spriteRenderer.color = defaultFenceColor; // Reset to original color
        }
    }
}