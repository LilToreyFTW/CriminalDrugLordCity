using System.Reflection;

namespace CriminalDrugLordCity.Gameplay
{
    public static class SerializedPickupName
    {
        public static void Apply(PickupWeapon target, string weaponName)
        {
            FieldInfo field = typeof(PickupWeapon).GetField("weaponName", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(target, weaponName);
            }
        }
    }

    public static class SerializedVehicleName
    {
        public static void Apply(VehicleInteractable target, string vehicleName)
        {
            FieldInfo field = typeof(VehicleInteractable).GetField("vehicleName", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(target, vehicleName);
            }
        }
    }
}
