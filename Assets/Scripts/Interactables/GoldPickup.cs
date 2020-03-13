using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    class GoldPickup : InteractableObject
    {

        public override void Interact()
        {            
            base.Interact();
            var n = FindObjectOfType<AudioManager>();
            n.Play("Coins");
        }
    }
}
