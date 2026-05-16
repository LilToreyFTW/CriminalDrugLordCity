using UnityEngine;

namespace CriminalDrugLordCity.CDL.Employees
{
    public enum CDL_EmployeeRole { Worker, Driver, Guard, Manager }

    [CreateAssetMenu(menuName = "CDL/Employees/Employee Definition", fileName = "CDL_Employee_")]
    public class CDL_EmployeeDefinition : ScriptableObject
    {
        public string employeeId;
        public string employeeName;
        public CDL_EmployeeRole role;
        public float wage = 100f;
        [Range(0f, 1f)] public float skill = 0.5f;
        [Range(0f, 1f)] public float loyalty = 0.5f;
    }
}
