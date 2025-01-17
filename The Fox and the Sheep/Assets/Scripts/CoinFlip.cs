using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinFlip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _isEntering = true;
    }

    [SerializeField] private Vector3 enterTargetScale = new Vector3(0.5f, 0.5f, 0.5f); // target size for entering
    [SerializeField] private Vector3 exitTargetScale = new Vector3(0, 0, 0); // target size for exiting size
    [SerializeField] private Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f); // Final size
    private bool _isEntering;
    public float scaleSpeed = 3f; // Speed of scaling

    private void Update()
    {
        ScaleCoin(_isEntering);
        // Every 3-10 seconds, flip a coin 
        // The coin stays active for 5 seconds 
    }

    /**
     * Set the animation to out if the coin is triggered
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // And animation is complete 
        {
            _isEntering = false;
        }
    }

    /**
     * Animate the coin in and out by scaling it
     */
    private void ScaleCoin(bool entering)
    {
        // Set target size 
        targetScale = entering ? enterTargetScale : exitTargetScale;

        // Smoothly scale towards the target size
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        // Stop scaling when close enough to target size
        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            transform.localScale = targetScale; // Snap to target size
        }
    }
}