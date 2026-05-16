using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Products;
using System;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Production
{
    [Serializable]
    public class CDL_RecipeIngredient
    {
        public CDL_ItemDefinition item;
        public int amount;
    }

    [CreateAssetMenu(menuName = "CDL/Production/Recipe Definition", fileName = "CDL_Recipe_")]
    public class CDL_RecipeDefinition : ScriptableObject
    {
        public string recipeId;
        public string recipeName;
        public CDL_RecipeIngredient[] inputs;
        public CDL_ItemDefinition outputItem;
        public CDL_ProductDefinition outputProduct;
        public int outputAmount = 1;
        public float duration = 10f;
        [TextArea] public string notes = "Fictional prototype recipe using abstract inputs only.";
    }
}
