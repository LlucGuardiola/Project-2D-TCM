using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWireCube : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;

    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.green;

            Vector3 center = new Vector3(boxCollider.offset.x, boxCollider.offset.y, transform.position.z);
            Vector3 size = new Vector3(boxCollider.size.x, boxCollider.size.y, 0f);

            Gizmos.DrawWireCube(center + transform.position, size);
        }
    }
}
