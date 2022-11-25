using UnityEngine;

public partial class Character
{
    private void OnValidate()
    {
        var collider = GetComponent<BoxCollider2D>();

        if (collider)
        {
            collider.isTrigger = true;
        }


        var rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody)
        {
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}