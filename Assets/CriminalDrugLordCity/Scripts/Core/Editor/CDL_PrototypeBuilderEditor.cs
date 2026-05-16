#if UNITY_EDITOR
using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Dealers;
using CriminalDrugLordCity.CDL.Districts;
using CriminalDrugLordCity.CDL.Employees;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.NPC;
using CriminalDrugLordCity.CDL.Police;
using CriminalDrugLordCity.CDL.Products;
using CriminalDrugLordCity.CDL.Production;
using CriminalDrugLordCity.CDL.Properties;
using CriminalDrugLordCity.CDL.Rivals;
using CriminalDrugLordCity.CDL.Save;
using CriminalDrugLordCity.CDL.Suppliers;
using CriminalDrugLordCity.CDL.UI;
using CriminalDrugLordCity.CDL.Vehicles;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace CriminalDrugLordCity.CDL.Editor
{
    public static class CDL_PrototypeBuilderEditor
    {
        private const string Root = "Assets/CriminalDrugLordCity";
        private const string ScriptableRoot = Root + "/ScriptableObjects";
        private const string PrefabRoot = Root + "/Prefabs";
        private const string SceneRoot = Root + "/Scenes";

        [MenuItem("CDL/Build Prototype Vertical Slice")]
        public static void BuildPrototype()
        {
            EnsureFolders();
            var data = CreateScriptableObjects();
            CreatePrefabs(data);
            CreateScene(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("CDL prototype generated.");
        }

        public static void BuildPrototypeBatchMode()
        {
            BuildPrototype();
            EditorApplication.Exit(0);
        }

        private static void EnsureFolders()
        {
            string[] folders =
            {
                ScriptableRoot,
                ScriptableRoot + "/Items",
                ScriptableRoot + "/Products",
                ScriptableRoot + "/Recipes",
                ScriptableRoot + "/Employees",
                ScriptableRoot + "/Properties",
                ScriptableRoot + "/Districts",
                ScriptableRoot + "/Suppliers",
                ScriptableRoot + "/Dealers",
                ScriptableRoot + "/Rivals",
                PrefabRoot,
                SceneRoot
            };

            foreach (var folder in folders)
            {
                if (!AssetDatabase.IsValidFolder(folder))
                {
                    string parent = folder[..folder.LastIndexOf('/')];
                    string name = folder[(folder.LastIndexOf('/') + 1)..];
                    AssetDatabase.CreateFolder(parent, name);
                }
            }
        }

        private static CDL_DataBundle CreateScriptableObjects()
        {
            CDL_DataBundle data = new CDL_DataBundle();

            data.supplyCrate = CreateItem("CDL_Item_SupplyCrate", "supply_crate", "Supply Crate");
            data.catalyst = CreateItem("CDL_Item_Catalyst", "catalyst_item", "Catalyst Item");
            data.binder = CreateItem("CDL_Item_Binder", "binder_item", "Binder Item");
            data.stabilizer = CreateItem("CDL_Item_Stabilizer", "stabilizer_item", "Stabilizer Item");
            data.greenPack = CreateItem("CDL_Item_GreenPack", "green_pack", "Green Pack");
            data.crystalPack = CreateItem("CDL_Item_CrystalPack", "crystal_pack", "Crystal Pack");
            data.streetCargo = CreateItem("CDL_Item_StreetCargo", "street_cargo", "Street Cargo");
            data.premiumBatchItem = CreateItem("CDL_Item_PremiumBatch", "premium_batch", "Premium Batch");
            data.productAItem = CreateItem("CDL_Item_ProductA", "product_a", "Product A");
            data.productBItem = CreateItem("CDL_Item_ProductB", "product_b", "Product B");
            data.meleeWeapon = CreateItem("CDL_Item_MeleeWeapon", "melee_weapon", "Melee Weapon", false, true);
            data.firearm = CreateItem("CDL_Item_Firearm", "firearm_placeholder", "Firearm Placeholder", false, true);

            data.products = new[]
            {
                CreateProduct("CDL_Product_ProductA", "product_a", "Product A", CDL_ProductRarity.Common, CDL_ProductQuality.Standard, 95f, 2f, 1f),
                CreateProduct("CDL_Product_ProductB", "product_b", "Product B", CDL_ProductRarity.Uncommon, CDL_ProductQuality.High, 130f, 2.5f, 1.2f),
                CreateProduct("CDL_Product_CrystalPack", "crystal_pack", "Crystal Pack", CDL_ProductRarity.Rare, CDL_ProductQuality.High, 175f, 3.5f, 1.5f),
                CreateProduct("CDL_Product_GreenPack", "green_pack", "Green Pack", CDL_ProductRarity.Common, CDL_ProductQuality.Standard, 80f, 1.5f, 0.9f),
                CreateProduct("CDL_Product_PremiumBatch", "premium_batch", "Premium Batch", CDL_ProductRarity.Elite, CDL_ProductQuality.Premium, 260f, 5f, 1.8f)
            };

            data.recipes = new[]
            {
                CreateRecipe("CDL_Recipe_ProductA", "recipe_a", "Product A Batch", new[]{(data.supplyCrate,1),(data.catalyst,1)}, data.productAItem, data.products[0], 2, 12f),
                CreateRecipe("CDL_Recipe_ProductB", "recipe_b", "Product B Batch", new[]{(data.greenPack,1),(data.binder,1)}, data.productBItem, data.products[1], 2, 16f),
                CreateRecipe("CDL_Recipe_PremiumBatch", "recipe_premium", "Premium Batch", new[]{(data.crystalPack,1),(data.stabilizer,1)}, data.premiumBatchItem, data.products[4], 1, 22f)
            };

            data.employees = new[]
            {
                CreateEmployee("CDL_Employee_Worker", "worker_1", "Rae Torque", CDL_EmployeeRole.Worker, 120f, 0.8f, 0.55f),
                CreateEmployee("CDL_Employee_Guard", "guard_1", "Moss Vale", CDL_EmployeeRole.Guard, 140f, 0.6f, 0.7f),
                CreateEmployee("CDL_Employee_Manager", "manager_1", "Juno March", CDL_EmployeeRole.Manager, 180f, 0.75f, 0.65f)
            };

            data.properties = new[]
            {
                CreateProperty("CDL_Property_Apartment", "apartment_hideout", "Apartment Hideout", 0f, 16, 1, 1),
                CreateProperty("CDL_Property_Warehouse", "warehouse", "Warehouse", 1800f, 48, 2, 2),
                CreateProperty("CDL_Property_Storefront", "storefront", "Storefront", 1200f, 24, 1, 1)
            };

            data.districts = new[]
            {
                CreateDistrict("CDL_District_Breakwater", "breakwater", "Breakwater Yard", 1.1f, 0.25f, 0.35f, 0.7f, 2, 0.1f),
                CreateDistrict("CDL_District_Cinder", "cinder", "Cinder Market", 1.3f, 0.45f, 0.4f, 0.9f, 3, 0.2f),
                CreateDistrict("CDL_District_Heights", "heights", "Saint Vela Heights", 1.6f, 0.7f, 0.2f, 0.5f, 1, 0f)
            };

            data.suppliers = new[]
            {
                CreateSupplier("CDL_Supplier_Dock", "supplier_dock", "Dock Broker"),
                CreateSupplier("CDL_Supplier_Chem", "supplier_chem", "Backroom Vendor")
            };

            data.suppliers[0].stock = new[]
            {
                new CDL_SupplierStockEntry { item = data.supplyCrate, stock = 12, price = 45f },
                new CDL_SupplierStockEntry { item = data.greenPack, stock = 10, price = 60f },
                new CDL_SupplierStockEntry { item = data.meleeWeapon, stock = 2, price = 150f }
            };

            data.suppliers[1].stock = new[]
            {
                new CDL_SupplierStockEntry { item = data.catalyst, stock = 12, price = 35f },
                new CDL_SupplierStockEntry { item = data.binder, stock = 12, price = 30f },
                new CDL_SupplierStockEntry { item = data.stabilizer, stock = 8, price = 55f }
            };

            data.dealers = new[]
            {
                CreateDealer("CDL_Dealer_Brick", "dealer_brick", "Brick"),
                CreateDealer("CDL_Dealer_Sable", "dealer_sable", "Sable")
            };

            data.rivals = new[]
            {
                CreateRival("CDL_Rival_DockWolves", "dock_wolves", "Dock Wolves", 0.6f, 0.5f, 0.25f),
                CreateRival("CDL_Rival_GlassSaints", "glass_saints", "Glass Saints", 0.45f, 0.65f, 0.15f)
            };

            return data;
        }

        private static CDL_ItemDefinition CreateItem(string assetName, string id, string displayName, bool stackable = true, bool isWeapon = false)
        {
            string path = $"{ScriptableRoot}/Items/{assetName}.asset";
            var item = AssetDatabase.LoadAssetAtPath<CDL_ItemDefinition>(path);
            if (item == null)
            {
                item = ScriptableObject.CreateInstance<CDL_ItemDefinition>();
                AssetDatabase.CreateAsset(item, path);
            }
            item.itemId = id;
            item.itemName = displayName;
            item.stackable = stackable;
            item.maxStack = stackable ? 99 : 1;
            item.isWeapon = isWeapon;
            EditorUtility.SetDirty(item);
            return item;
        }

        private static CDL_ProductDefinition CreateProduct(string assetName, string id, string displayName, CDL_ProductRarity rarity, CDL_ProductQuality quality, float basePrice, float heatRisk, float demand)
        {
            string path = $"{ScriptableRoot}/Products/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_ProductDefinition>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_ProductDefinition>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.productId = id;
            asset.productName = displayName;
            asset.rarity = rarity;
            asset.quality = quality;
            asset.basePrice = basePrice;
            asset.heatRisk = heatRisk;
            asset.demandScore = demand;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static CDL_RecipeDefinition CreateRecipe(string assetName, string id, string displayName, (CDL_ItemDefinition item, int amount)[] inputs, CDL_ItemDefinition output, CDL_ProductDefinition product, int outputAmount, float duration)
        {
            string path = $"{ScriptableRoot}/Recipes/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_RecipeDefinition>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_RecipeDefinition>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.recipeId = id;
            asset.recipeName = displayName;
            asset.inputs = new CDL_RecipeIngredient[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                asset.inputs[i] = new CDL_RecipeIngredient { item = inputs[i].item, amount = inputs[i].amount };
            }
            asset.outputItem = output;
            asset.outputProduct = product;
            asset.outputAmount = outputAmount;
            asset.duration = duration;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static CDL_EmployeeDefinition CreateEmployee(string assetName, string id, string name, CDL_EmployeeRole role, float wage, float skill, float loyalty)
        {
            string path = $"{ScriptableRoot}/Employees/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_EmployeeDefinition>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_EmployeeDefinition>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.employeeId = id;
            asset.employeeName = name;
            asset.role = role;
            asset.wage = wage;
            asset.skill = skill;
            asset.loyalty = loyalty;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static CDL_PropertyDefinition CreateProperty(string assetName, string id, string name, float price, int storage, int security, int slots)
        {
            string path = $"{ScriptableRoot}/Properties/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_PropertyDefinition>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_PropertyDefinition>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.propertyId = id;
            asset.propertyName = name;
            asset.purchasePrice = price;
            asset.storageCapacity = storage;
            asset.securityLevel = security;
            asset.productionSlots = slots;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static CDL_DistrictDefinition CreateDistrict(string assetName, string id, string name, float demand, float police, float rival, float density, int slots, float control)
        {
            string path = $"{ScriptableRoot}/Districts/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_DistrictDefinition>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_DistrictDefinition>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.districtId = id;
            asset.districtName = name;
            asset.demandMultiplier = demand;
            asset.policePresence = police;
            asset.rivalPresence = rival;
            asset.customerDensity = density;
            asset.dealerSlots = slots;
            asset.districtControlValue = control;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static CDL_SupplierDefinition CreateSupplier(string assetName, string id, string name)
        {
            string path = $"{ScriptableRoot}/Suppliers/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_SupplierDefinition>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_SupplierDefinition>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.supplierId = id;
            asset.supplierName = name;
            asset.reputation = 0.5f;
            asset.bulkDiscountThreshold = 5f;
            asset.bulkDiscountPercent = 0.15f;
            asset.restockSeconds = 45f;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static CDL_DealerProfile CreateDealer(string assetName, string id, string name)
        {
            string path = $"{ScriptableRoot}/Dealers/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_DealerProfile>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_DealerProfile>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.dealerId = id;
            asset.dealerName = name;
            asset.loyalty = 0.6f;
            asset.risk = 0.4f;
            asset.skill = 0.6f;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static CDL_RivalGangDefinition CreateRival(string assetName, string id, string name, float aggression, float grip, float ambush)
        {
            string path = $"{ScriptableRoot}/Rivals/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<CDL_RivalGangDefinition>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CDL_RivalGangDefinition>();
                AssetDatabase.CreateAsset(asset, path);
            }
            asset.rivalId = id;
            asset.gangName = name;
            asset.aggression = aggression;
            asset.territoryGrip = grip;
            asset.ambushChance = ambush;
            EditorUtility.SetDirty(asset);
            return asset;
        }

        private static void CreatePrefabs(CDL_DataBundle data)
        {
            CreateOrUpdatePrefab($"{PrefabRoot}/CDL_StorageContainer.prefab", go =>
            {
                var collider = go.GetComponent<BoxCollider>() ?? go.AddComponent<BoxCollider>();
                collider.isTrigger = false;
                go.AddComponent<CDL_InventoryComponent>();
            });

            CreateOrUpdatePrefab($"{PrefabRoot}/CDL_ProductionStation.prefab", go =>
            {
                var storage = go.GetComponent<CDL_InventoryComponent>() ?? go.AddComponent<CDL_InventoryComponent>();
                var station = go.GetComponent<CDL_ProductionStation>() ?? go.AddComponent<CDL_ProductionStation>();
                station.recipe = data.recipes[0];
                station.storage = storage;
                station.assignedWorker = data.employees[0];
                if (go.GetComponent<BoxCollider>() == null) go.AddComponent<BoxCollider>();
                });

                CreateOrUpdatePrefab($"{PrefabRoot}/CDL_MissionBoard.prefab", go =>
                {
                var mission = go.GetComponent<CDL_DeliveryMission>() ?? go.AddComponent<CDL_DeliveryMission>();
                mission.requiredItem = data.productAItem;
                mission.requiredAmount = 2;
                mission.reward = 450f;
                mission.timeLimit = 120f;
                if (go.GetComponent<BoxCollider>() == null) go.AddComponent<BoxCollider>();
                });
}

        private static void CreateScene(CDL_DataBundle data)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            scene.name = "CDL_PrototypeCity";

            GameObject gm = new GameObject("CDL_GameManager");
            gm.AddComponent<CDL_GameManager>();
            gm.AddComponent<CDL_RaidEventSystem>();
            gm.AddComponent<CDL_SaveSystem>();

            GameObject player = new GameObject("CDL_Player");
            player.transform.position = new Vector3(0f, 1f, 0f);
            var controller = player.AddComponent<CharacterController>();
            controller.center = new Vector3(0f, 0.9f, 0f);
            controller.height = 1.8f;
            controller.radius = 0.35f;
            var inventory = player.AddComponent<CDL_InventoryComponent>();
            inventory.AddItem(data.productAItem, 4);
            inventory.AddItem(data.meleeWeapon, 1);
            var playerController = player.AddComponent<CDL_PlayerController>();
            playerController.inventory = inventory;
            player.AddComponent<CDL_PlayerCombat>().player = playerController;

            GameObject fpPivot = new GameObject("FirstPersonPivot");
            fpPivot.transform.SetParent(player.transform, false);
            fpPivot.transform.localPosition = new Vector3(0f, 1.5f, 0f);
            playerController.firstPersonCameraPivot = fpPivot.transform;

            GameObject tpPivot = new GameObject("ThirdPersonPivot");
            tpPivot.transform.SetParent(player.transform, false);
            tpPivot.transform.localPosition = new Vector3(0f, 1.6f, -4f);
            playerController.thirdPersonCameraPivot = tpPivot.transform;

            Camera cam = Camera.main;
            cam.transform.SetParent(fpPivot.transform, false);
            playerController.playerCamera = cam;

            CreateGround();
            CreateBuilding("Apartment Hideout", new Vector3(-10f, 1.5f, 0f), new Vector3(8f, 3f, 8f), data.properties[0], true);
            CreateBuilding("Warehouse", new Vector3(18f, 2.5f, 6f), new Vector3(12f, 5f, 10f), data.properties[1], false);
            CreateBuilding("Garage", new Vector3(6f, 2f, -14f), new Vector3(8f, 4f, 8f), data.properties[1], false);
            CreateBuilding("Storefront", new Vector3(-18f, 2f, 12f), new Vector3(10f, 4f, 6f), data.properties[2], false);

            var district1 = CreateDistrictZone("Breakwater Yard", new Vector3(0f, 0f, 0f), new Vector3(30f, 0.2f, 30f), data.districts[0]);
            var district2 = CreateDistrictZone("Cinder Market", new Vector3(-22f, 0f, 8f), new Vector3(18f, 0.2f, 18f), data.districts[1]);
            CreateDistrictZone("Saint Vela Heights", new Vector3(24f, 0f, 18f), new Vector3(18f, 0.2f, 18f), data.districts[2]);

            var stationPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{PrefabRoot}/CDL_ProductionStation.prefab");
            var station = PrefabUtility.InstantiatePrefab(stationPrefab) as GameObject;
            station.name = "CDL_ProductionStation";
            station.transform.position = new Vector3(-10f, 0.75f, 2f);

            var storagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{PrefabRoot}/CDL_StorageContainer.prefab");
            var storage = PrefabUtility.InstantiatePrefab(storagePrefab) as GameObject;
            storage.name = "CDL_StorageContainer";
            storage.transform.position = new Vector3(-7f, 0.75f, 2f);
            storage.GetComponent<CDL_InventoryComponent>().AddItem(data.supplyCrate, 8);
            storage.GetComponent<CDL_InventoryComponent>().AddItem(data.catalyst, 4);
            storage.GetComponent<CDL_InventoryComponent>().AddItem(data.greenPack, 4);
            storage.GetComponent<CDL_InventoryComponent>().AddItem(data.binder, 4);
            station.GetComponent<CDL_ProductionStation>().storage = storage.GetComponent<CDL_InventoryComponent>();

            CreateCustomer("CDL_Customer_1", new Vector3(-14f, 1f, 8f), data.productAItem, data.products[0]);
            CreateCustomer("CDL_Customer_2", new Vector3(-19f, 1f, 14f), data.productBItem, data.products[1]);
            CreateCustomer("CDL_Customer_3", new Vector3(8f, 1f, 11f), data.premiumBatchItem, data.products[4]);

            CreateSupplier("CDL_Supplier_1", new Vector3(-2f, 1f, -10f), data.suppliers[0]);
            CreateSupplier("CDL_Supplier_2", new Vector3(12f, 1f, -10f), data.suppliers[1]);

            CreateDealer("CDL_Dealer_1", new Vector3(0f, 1f, 8f), data.dealers[0], district1);
            CreateDealer("CDL_Dealer_2", new Vector3(15f, 1f, 10f), data.dealers[1], district2);

            CreatePoliceRoute(new Vector3[] { new(-5f, 0f, 18f), new(5f, 0f, 18f), new(5f, 0f, 6f), new(-5f, 0f, 6f) });
            CreateRivalZone(new Vector3(20f, 0f, -6f), data.rivals[0], district1);
            CreateVehicle(new Vector3(3f, 0.6f, -6f));

            var missionBoardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{PrefabRoot}/CDL_MissionBoard.prefab");
            var board = PrefabUtility.InstantiatePrefab(missionBoardPrefab) as GameObject;
            board.name = "CDL_MissionBoard";
            board.transform.position = new Vector3(-12f, 1f, -3f);
            board.GetComponent<CDL_DeliveryMission>().dropoff = CreateMarker("DeliveryDropoff", new Vector3(22f, 0.2f, 18f)).transform;

            CreateWeaponPickup("CDL_Weapon_Melee", new Vector3(-8f, 0.8f, -2f), data.meleeWeapon, Color.gray);
            CreateWeaponPickup("CDL_Weapon_Firearm", new Vector3(16f, 0.8f, 3f), data.firearm, Color.black);
            CreateCanvas();

            EditorSceneManager.SaveScene(scene, $"{SceneRoot}/CDL_PrototypeCity.unity");
        }

        private static void CreateCanvas()
        {
            GameObject canvasGo = new GameObject("CDL_Canvas");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();

            var ui = canvasGo.AddComponent<CDL_UIController>();
            ui.hudText = CreateText("HUD", canvasGo.transform, new Vector2(10f, -10f), new Vector2(500f, 220f), TextAnchor.UpperLeft, 18);
            ui.inventoryPanel = CreatePanel("InventoryPanel", canvasGo.transform, new Vector2(10f, -240f), new Vector2(250f, 240f), new Color(0f, 0f, 0f, 0.65f));
            ui.inventoryText = CreateText("InventoryText", ui.inventoryPanel.transform, new Vector2(10f, -10f), new Vector2(230f, 220f), TextAnchor.UpperLeft, 16);
            ui.phonePanel = CreatePanel("PhonePanel", canvasGo.transform, new Vector2(-260f, -10f), new Vector2(250f, 220f), new Color(0.05f, 0.05f, 0.08f, 0.75f));
            ui.phoneText = CreateText("PhoneText", ui.phonePanel.transform, new Vector2(10f, -10f), new Vector2(230f, 200f), TextAnchor.UpperLeft, 16);
            ui.pausePanel = CreatePanel("PausePanel", canvasGo.transform, new Vector2(-150f, -120f), new Vector2(300f, 160f), new Color(0f, 0f, 0f, 0.8f));
            CreateText("PauseText", ui.pausePanel.transform, new Vector2(15f, -15f), new Vector2(270f, 120f), TextAnchor.UpperLeft, 20).text = "Pause Menu\nESC Resume\nF5 Save\nF9 Load";
            ui.mainMenuPanel = CreatePanel("MainMenuPanel", canvasGo.transform, new Vector2(-220f, -80f), new Vector2(440f, 220f), new Color(0f, 0f, 0f, 0.85f));
            CreateText("MainMenuText", ui.mainMenuPanel.transform, new Vector2(20f, -20f), new Vector2(400f, 180f), TextAnchor.UpperLeft, 22).text = "Criminal Drug Lord City\nPrototype Vertical Slice\nEnter to Start\nTab Inventory\nP Phone";
            ui.inventoryPanel.SetActive(false);
            ui.phonePanel.SetActive(false);
            ui.pausePanel.SetActive(false);
        }

        private static Text CreateText(string name, Transform parent, Vector2 anchoredPos, Vector2 size, TextAnchor anchor, int fontSize)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = anchoredPos;
            rect.sizeDelta = size;
            Text text = go.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = fontSize;
            text.color = Color.white;
            text.alignment = anchor;
            return text;
        }

        private static GameObject CreatePanel(string name, Transform parent, Vector2 anchoredPos, Vector2 size, Color color)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = anchoredPos;
            rect.sizeDelta = size;
            Image image = go.AddComponent<Image>();
            image.color = color;
            return go;
        }

        private static void CreateGround()
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "CDL_Ground";
            ground.transform.localScale = new Vector3(6f, 1f, 6f);
        }

        private static GameObject CreateBuilding(string name, Vector3 pos, Vector3 scale, CDL_PropertyDefinition property, bool owned)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = pos;
            go.transform.localScale = scale;
            var propertyInstance = go.AddComponent<CDL_PropertyInstance>();
            propertyInstance.definition = property;
            propertyInstance.IsOwned = owned;
            var storage = new GameObject("Storage");
            storage.transform.SetParent(go.transform, false);
            storage.AddComponent<CDL_InventoryComponent>();
            propertyInstance.storage = storage.GetComponent<CDL_InventoryComponent>();
            return go;
        }

        private static CDL_DistrictRuntime CreateDistrictZone(string name, Vector3 pos, Vector3 scale, CDL_DistrictDefinition definition)
        {
            var zone = GameObject.CreatePrimitive(PrimitiveType.Cube);
            zone.name = name;
            zone.transform.position = pos;
            zone.transform.localScale = scale;
            zone.GetComponent<MeshRenderer>().material.color = new Color(0.2f, 0.2f, 0.25f, 0.15f);
            var runtime = zone.AddComponent<CDL_DistrictRuntime>();
            runtime.definition = definition;
            return runtime;
        }

        private static void CreateCustomer(string name, Vector3 pos, CDL_ItemDefinition desiredItem, CDL_ProductDefinition product)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.name = name;
            go.transform.position = pos;
            var customer = go.AddComponent<CDL_CustomerNPC>();
            customer.desiredItem = desiredItem;
            customer.desiredProduct = product;
            customer.offerPrice = product.basePrice;
        }

        private static void CreateSupplier(string name, Vector3 pos, CDL_SupplierDefinition definition)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.name = name;
            go.transform.position = pos;
            go.GetComponent<MeshRenderer>().material.color = Color.yellow;
            go.AddComponent<CDL_SupplierNPC>().definition = definition;
        }

        private static void CreateDealer(string name, Vector3 pos, CDL_DealerProfile profile, CDL_DistrictRuntime district)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.name = name;
            go.transform.position = pos;
            go.GetComponent<MeshRenderer>().material.color = Color.cyan;
            var dealer = go.AddComponent<CDL_DealerNPC>();
            dealer.profile = profile;
            dealer.assignedDistrict = district;
            dealer.inventory = go.AddComponent<CDL_InventoryComponent>();
        }

        private static void CreatePoliceRoute(Vector3[] points)
        {
            var police = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            police.name = "CDL_PolicePatrol";
            police.transform.position = points[0] + Vector3.up;
            police.GetComponent<MeshRenderer>().material.color = Color.blue;
            var patrol = police.AddComponent<CDL_PolicePatrol>();
            var route = new List<Transform>();
            for (int i = 0; i < points.Length; i++)
            {
                route.Add(CreateMarker($"PolicePoint_{i}", points[i]).transform);
            }
            patrol.patrolPoints = route.ToArray();
        }

        private static void CreateRivalZone(Vector3 pos, CDL_RivalGangDefinition rival, CDL_DistrictRuntime district)
        {
            var zone = CreateMarker("CDL_RivalZone", pos);
            zone.transform.localScale = new Vector3(6f, 1f, 6f);
            zone.AddComponent<CDL_RivalZone>().rivalGang = rival;
            zone.GetComponent<CDL_RivalZone>().district = district;

            var rivalNpc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            rivalNpc.name = "CDL_RivalNPC";
            rivalNpc.transform.position = pos + Vector3.up;
            rivalNpc.GetComponent<MeshRenderer>().material.color = Color.red;
            rivalNpc.AddComponent<CDL_RivalNPC>().gang = rival;
        }

        private static void CreateVehicle(Vector3 pos)
        {
            var vehicle = GameObject.CreatePrimitive(PrimitiveType.Cube);
            vehicle.name = "CDL_DeliveryVehicle";
            vehicle.transform.position = pos;
            vehicle.transform.localScale = new Vector3(2f, 1.2f, 4f);
            var controller = vehicle.AddComponent<CDL_VehicleController>();
            controller.trunkInventory = vehicle.AddComponent<CDL_InventoryComponent>();
        }

        private static void CreateWeaponPickup(string name, Vector3 pos, CDL_ItemDefinition weapon, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.position = pos;
            go.transform.localScale = new Vector3(0.4f, 0.2f, 1f);
            go.GetComponent<MeshRenderer>().material.color = color;
            go.AddComponent<CDL_WeaponPickup>().weaponItem = weapon;
        }

        private static GameObject CreateMarker(string name, Vector3 pos)
        {
            var marker = new GameObject(name);
            marker.transform.position = pos;
            return marker;
        }

        private static void CreateOrUpdatePrefab(string path, System.Action<GameObject> configure)
        {
            GameObject temp = new GameObject(System.IO.Path.GetFileNameWithoutExtension(path));
            configure(temp);
            PrefabUtility.SaveAsPrefabAsset(temp, path);
            Object.DestroyImmediate(temp);
        }

        private sealed class CDL_DataBundle
        {
            public CDL_ItemDefinition supplyCrate;
            public CDL_ItemDefinition catalyst;
            public CDL_ItemDefinition binder;
            public CDL_ItemDefinition stabilizer;
            public CDL_ItemDefinition greenPack;
            public CDL_ItemDefinition crystalPack;
            public CDL_ItemDefinition streetCargo;
            public CDL_ItemDefinition premiumBatchItem;
            public CDL_ItemDefinition productAItem;
            public CDL_ItemDefinition productBItem;
            public CDL_ItemDefinition meleeWeapon;
            public CDL_ItemDefinition firearm;
            public CDL_ProductDefinition[] products;
            public CDL_RecipeDefinition[] recipes;
            public CDL_EmployeeDefinition[] employees;
            public CDL_PropertyDefinition[] properties;
            public CDL_DistrictDefinition[] districts;
            public CDL_SupplierDefinition[] suppliers;
            public CDL_DealerProfile[] dealers;
            public CDL_RivalGangDefinition[] rivals;
        }
    }
}
#endif
