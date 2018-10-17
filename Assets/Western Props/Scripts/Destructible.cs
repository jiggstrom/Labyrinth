// --------------------------------------
// This script is totally optional. It is an example of how you can use the
// destructible versions of the objects as demonstrated in my tutorial.
// Watch the tutorial over at http://youtube.com/brackeys/.
// --------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : Interactable {

	public GameObject destroyedVersion; // Reference to the shattered version of the object
    public Loot loot;

    // If the player clicks on the object
    public override void Interact()
    {
        if (!IsCloseEnough()) return;
        base.Interact();
        var gm = FindObjectOfType<GameManager>();

		// Spawn a shattered object
		var n = Instantiate(destroyedVersion, transform.position, transform.rotation);
        n.GetComponent<ObjectVanish>().VanishInSeconds(3);

		// Remove the current object
		Destroy(gameObject);

        if (loot != null)
        {
            if (gm.LootFound(loot, this)) loot = null;
        }
    }
}
