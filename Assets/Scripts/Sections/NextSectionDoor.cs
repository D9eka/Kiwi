using System;
using System.Collections.Generic;
// using NaughtyAttributes;
using UnityEngine;

namespace Sections
{
    public class NextSectionDoor : MonoBehaviour
    {
        //Сделал возможность показывать 1, 2 или 3 варианта
        [SerializeField] private bool isRandom;
        [SerializeField, Range(1, 3)] private int choicesCount;
        [SerializeField] private List<SectionTypeSO> possibleSectionTypes = new();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (SectionManager.Instance.CurrentSectionIndex + 1 < SectionManager.Instance.OpenedSectionsCount)
                {
                    SectionManager.Instance.EnterNextOpenedSection();
                }
                else
                {
                    SetChoice();
                    ShowChoice();
                }
            }
        }

        private void SetChoice()
        {
            if (isRandom)
            {
                possibleSectionTypes = SectionManager.Instance.GetRandomSectionTypes(choicesCount);
            }
        }

        private void ShowChoice()
        {
            NextSectionSelectionUI.Instance.SetTypes(possibleSectionTypes);
            NextSectionSelectionUI.Instance.ShowChoice();
        }
    }
}