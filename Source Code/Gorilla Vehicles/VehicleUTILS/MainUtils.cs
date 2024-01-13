using BoingKit;
using ComputerPlusPlus.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using static BoingKit.BoingBones;
using static Gorilla_Vehicles.VehicleUTILS.Json;

namespace Gorilla_Vehicles.VehicleUTILS
{
    // Don't roast my code pls its pretty messy
    // Also hi AHauntedArmy lol if you looking at this
    public static class MainUtils
    {
        public static List<GameObject> LoadedObjects = new List<GameObject>();
        public static List<string> LoadedObjectNames = new List<string>();
        public static List<GameObject> LoadedPresets = new List<GameObject>();
        public static List<GameObject> LoadedPreviews = new List<GameObject>();
        public static List<GameObject> selectedPoints = new List<GameObject>();
        public static List<GameObject> SpawnedVehicles = new List<GameObject>();
        public static Material RedMat;
        public static Material GreenMat;
 
        public static async void SetupBundles()
        {
            List<AssetBundle> bundles = new List<AssetBundle>();
            string folder = Path.GetDirectoryName(typeof(Plugin).Assembly.Location);
            string path = $"{folder}\\CustomVehicles";
            var directoryInfo = new DirectoryInfo(path);
            Debug.Log("SetupBundles");
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.vehicle");
            Debug.Log(fileInfos.Length);
            foreach (var fileInfo in fileInfos)
            {
                string fileDirectory = Path.GetFileNameWithoutExtension(fileInfo.Name);
                string filePath = Path.Combine(path, fileInfo.Name);

                AssetBundle carResourceBundle = null;
                CarJson shirtDataJSON = null;

                Logging.Info($"Opening file '{Path.GetFileName(filePath)}'");
                using var archive = ZipFile.OpenRead(filePath);
                try
                {
                    var packageEntry = archive.Entries.FirstOrDefault(i => i.Name == "CarData.json");
                    if (packageEntry == null) continue;

                    Logging.Info(" > Reading entry");
                    using var stream = new StreamReader(packageEntry.Open(), Encoding.UTF8);

                    string packageReadContents = await stream.ReadToEndAsync();
                    shirtDataJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<CarJson>(packageReadContents);

                    Logging.Info(" > Deserializing contents");
                    var shirtResourceEntry = archive.Entries.FirstOrDefault(i => i.Name == fileInfo.Name.Replace(".vehicle", "") + "_Asset");
                    if (shirtResourceEntry == null) continue;
                LoadedObjectNames.Add(fileInfo.Name.Replace(".vehicle", ""));
                    using var SeekableStream = new MemoryStream();
                    await shirtResourceEntry.Open().CopyToAsync(SeekableStream);

                    Logging.Info(" > Loading resource bundle");
                    carResourceBundle = await LoadFromStream(SeekableStream);
                    bundles.Add(carResourceBundle);
                }
                catch (Exception ex)
                {
                    Logging.Warning($"Failed to parse file '{Path.GetFileName(filePath)}' as a car for GorillaVehicles: {ex}");
                    continue;
                }
            }

                foreach (AssetBundle bundle in bundles)
            {
                List<GameObject> LoadedAssets = new List<GameObject>();
                LoadedAssets = bundle.LoadAllAssets<GameObject>().ToList<GameObject>();
                LoadedObjects.Add(LoadedAssets[0]);

                GameObject Preset = GameObject.Instantiate(LoadedAssets[0]);
                GameObject Preview = GameObject.Instantiate(LoadedAssets[0], Plugin.ObjectParent.transform);
                LoadedPresets.Add(Preset);
                LoadedPreviews.Add(Preview);
                Preview.transform.localPosition = Vector3.zero;
                Debug.Log("Bundle File Loaded: " + LoadedObjectNames[bundles.IndexOf(bundle)]);
                LoadedPresets[bundles.IndexOf(bundle)].transform.DisableAllCollidersInParent();
                LoadedPreviews[bundles.IndexOf(bundle)].transform.DisableAllCollidersInParent();
                Debug.Log("DisabledColliders");
                Debug.Log(LoadedPreviews[bundles.IndexOf(bundle)].transform.childCount);
                LoadedPreviews[bundles.IndexOf(bundle)].GetComponent<Rigidbody>().isKinematic = true;
                LoadedPresets[bundles.IndexOf(bundle)].GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("RigidStuff");

                Preset.transform.SetMaterialInChildren(Plugin.HoloMat);
            }

            LoadedObjects.SetupVehicleScripts($"{folder}\\CustomVehicles");
        }

        public static void SetupVehicleScripts(this List<GameObject> Objects, string path = "")
        {
            foreach (GameObject obj in Objects) 
            {
                VehicleScript vs = obj.AddComponent<VehicleScript>();
                List<WheelCollider> Wheels = new List<WheelCollider>()
          {
                obj.FindInParent("FWheelL").gameObject.GetComponent<WheelCollider>(),
                obj.FindInParent("FWheelR").gameObject.GetComponent<WheelCollider>(),
                obj.FindInParent("BWheelL").gameObject.GetComponent<WheelCollider>(),
                obj.FindInParent("BWheelR").gameObject.GetComponent<WheelCollider>()
          };

            List<Transform> WheelMeshes = new List<Transform>()
            {
                obj.FindInParent("FWheelLM").gameObject.GetComponent<Transform>(),
                obj.FindInParent("FWheelRM").gameObject.GetComponent<Transform>(),
                obj.FindInParent("BWheelLM").gameObject.GetComponent<Transform>(),
                obj.FindInParent("BWheelRM").gameObject.GetComponent<Transform>()
            };

                var directoryInfo = new DirectoryInfo(path);

                FileInfo[] fileInfos = directoryInfo.GetFiles("*.vehicle");

                foreach (var fileInfo in fileInfos)
                {
                    if (fileInfo.Name.Replace(".vehicle", "") == obj.name)
                    {
string fileDirectory = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    string filePath = Path.Combine(path, fileInfo.Name);
                    CarJson carDataJSON = null;

                    Logging.Info($"Opening file '{Path.GetFileName(filePath)}'");
                    using var archive = ZipFile.OpenRead(filePath);
                    try
                    {
                        var packageEntry = archive.Entries.FirstOrDefault(i => i.Name == "CarData.json");
                        if (packageEntry == null) continue;

                        Logging.Info(" > Reading entry");
                        using var stream = new StreamReader(packageEntry.Open(), Encoding.UTF8);

                        string packageReadContents = stream.ReadToEnd();
                        carDataJSON = Newtonsoft.Json.JsonConvert.DeserializeObject<CarJson>(packageReadContents);
  
                vs.maxMotorTorque = carDataJSON.torque;
                vs.maxSteeringAngle = carDataJSON.MaxSteeringAngle;
                vs.VehicleSpeedMutiplyer = carDataJSON.SpeedMultiplyer;
                vs.breakForce = carDataJSON.Breakforce;
                        vs.Autor = carDataJSON.Author;
                            vs.Version = carDataJSON.Version;

                    }
                    catch (Exception ex)
                    {
                        Logging.Warning($"Failed to parse file '{Path.GetFileName(filePath)}' as a car for GorillaVehicles: {ex}");
                        continue;
                    }
                    }
                    

                }

                if (obj.FindInParent("SelectPoint").gameObject != null)
                {
              selectedPoints.Add(obj.FindInParent("SelectPoint").gameObject);
                }
                else
                {
                    Plugin.FlagBrokenVehicle = true;
                }
             if (WheelMeshes.Count == 4 && Wheels.Count == 4 && vs != null) 
                { 
                vs.Wheels = Wheels;
                vs.DrivePoint = obj.FindInParent("DrivePoint");
              vs.WheelMeshes = WheelMeshes;                

                obj.transform.SetupCollidersLayer();
                }
             else
                {
                    Plugin.FlagBrokenVehicle = true;
                }

            }


        }

        private static async Task<AssetBundle> LoadFromStream(Stream str)
        {
            var taskCompletionSource = new TaskCompletionSource<AssetBundle>();
            var request = AssetBundle.LoadFromStreamAsync(str);
            request.completed += operation =>
            {
                var outRequest = operation as AssetBundleCreateRequest;
                taskCompletionSource.SetResult(outRequest.assetBundle);
            };
            return await taskCompletionSource.Task;
        }

        public static void SetupCollidersLayer(this Transform transform)
        {
            List<Transform> Children = new List<Transform>();
            FindAllChildrenRecursive(transform, ref Children);

            foreach (Transform child in Children) 
            {
                child.gameObject.layer = 8;
            }
        }

        public static void DisableAllCollidersInParent(this Transform currentTransform)
        {
            if (currentTransform.gameObject.GetComponent<Collider>())
            {
                currentTransform.gameObject.GetComponent<Collider>().enabled = false;
            }
            for (int i = 0; i < currentTransform.childCount; i++)
            {
               
                Transform child = currentTransform.GetChild(i);

                child.DisableAllCollidersInParent();
            }
        }

         public static void SetMaterialOverrideInParent(this Transform currentTransform, int MatIndex)
        {
            if (currentTransform.GetComponent<Collider>())
            {
                currentTransform.gameObject.AddComponent<GorillaSurfaceOverride>().overrideIndex = MatIndex;
            }
            for (int i = 0; i < currentTransform.childCount; i++)
            {
                Transform child = currentTransform.GetChild(i);
                
                child.SetMaterialOverrideInParent(MatIndex);
            }
        }

        public static void SetMaterialInChildren(this Transform currentTransform, Material NewMat)
        {
            if (currentTransform.GetComponent<Renderer>())
            {
                Material[] childrenmats = currentTransform.GetComponent<Renderer>().materials;
                for (int ii = 0; ii < childrenmats.Length; ii++)
                {
                    childrenmats[ii] = NewMat;
                }
                currentTransform.GetComponent<Renderer>().materials = childrenmats;
            }
            for (int i = 0; i < currentTransform.childCount; i++)
            {
                Transform child = currentTransform.GetChild(i);

                child.SetMaterialInChildren(NewMat);
            }
        }

        public static void SetupScriptBundle()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Gorilla_Vehicles.Resources.gorillavehicles");
            Plugin.MainBundle = AssetBundle.LoadFromStream(stream);
            Plugin.HoloMat = Plugin.MainBundle.LoadAsset<Material>("Hologram");
            RedMat = Plugin.MainBundle.LoadAsset<Material>("redclicked");
            GreenMat = Plugin.MainBundle.LoadAsset<Material>("TestMat 2");
            Plugin.Pad = Plugin.MainBundle.LoadAsset<GameObject>("Pad");
            GameObject Insobj = GameObject.Instantiate(Plugin.MainBundle.LoadAsset<GameObject>("Line"));
            Plugin.lr = Insobj.GetComponent<LineRenderer>();

            Plugin.ActivePad = GameObject.Instantiate(Plugin.Pad, GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L").transform);
            Plugin.SpawnSpot = Plugin.ActivePad.FindInParent("Name").GetComponent<TextMeshPro>();
            Plugin.ObjectParent = Plugin.ActivePad.FindInParent("ObjectParent").gameObject;
            SetupButtons();
            SetupBundles();

            Plugin.ActivePad.SetActive(false);
        }


        public static void SetupButtons()
        {
            List<GameObject> buttons = new List<GameObject>()
           {
               Plugin.ActivePad.FindInParent("Left").gameObject,
               Plugin.ActivePad.FindInParent("Right").gameObject,
               Plugin.ActivePad.FindInParent("Equip").gameObject
           };

            Plugin.Buttons = buttons;

            foreach (GameObject button in buttons)
            {
                Button btn = button.AddComponent<Button>();

                if (button.name != "Equip")
                {
                    btn.Up = button.name == "Right"? true : false;
                }
                else
                {
                    btn.EnterKey = true;
                }
            }
        }

        public static Transform FindInParent(this GameObject Parent, string ChildName)
        {
            List<Transform> Children = new List<Transform>();
            FindAllChildrenRecursive(Parent.transform, ref Children);
            foreach (Transform child in Children)
            {
                if (child.name == ChildName)
                {
                    return child;
                }
            }

            return null;
        }

        public static void FindAllChildrenRecursive(Transform parent, ref List<Transform> childrenList)
        {
            foreach (Transform child in parent)
            {
                childrenList.Add(child);
                FindAllChildrenRecursive(child, ref childrenList);
            }
        }

    }
}
