using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public GameObject miniGameCanvas;

    void Start()
    { 
       miniGameCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            miniGameCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            miniGameCanvas.SetActive(false);
        }
    }
}