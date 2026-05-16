using UnityEngine;

namespace CriminalDrugLordCity.CDL.Core
{
    public interface ICDLInteractable
    {
        string GetInteractionLabel();
        void Interact(CDL_PlayerController player);
    }

    public interface ICDLDamageable
    {
        void ApplyDamage(float amount, GameObject source);
    }
}
