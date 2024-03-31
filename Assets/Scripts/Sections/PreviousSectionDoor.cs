using System;
using System.Collections.Generic;
// using NaughtyAttributes;
using UnityEngine;

namespace Sections
{
    public class PreviousSectionDoor : MonoBehaviour
    {
        private void Start()
        {
            TryHideDoor();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SectionManager.Instance.EnterPreviousOpenedSection();
            }
        }

        private void TryHideDoor()
        {
            // if (
            //     // Невыполнение условия прохождения секцции
            //     false
            // )
            // {
            //     gameObject.SetActive(false);
            // }
        }
    }
}