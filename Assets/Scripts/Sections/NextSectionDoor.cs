using System;
using System.Collections.Generic;
// using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sections
{
    public class NextSectionDoor : MonoBehaviour
    {
        //Сделал возможность показывать 1, 2 или 3 варианта
        [SerializeField] private bool _isRandom = true;
        [SerializeField, Range(1, 3)] private int _choicesCount = 2;
        [SerializeField] private List<SectionTypeSO> _possibleSectionTypes = new();

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
            if (_isRandom)
            {
                _possibleSectionTypes = SectionManager.Instance.GetRandomSectionTypes(_choicesCount);
            }
        }

        private void ShowChoice()
        {
            NextSectionSelectionUI.Instance.SetTypes(_possibleSectionTypes);
            NextSectionSelectionUI.Instance.ShowChoice();
        }
    }
}