using BepInEx;
using Gorilla_Vehicles.VehicleUTILS;
using PlayFab.MultiplayerModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Utilla;
using ComputerPlusPlus;
using GorillaNetworking;
using HarmonyLib;
using Valve.VR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;

namespace Gorilla_Vehicles
{
    // Don't roast my code pls its pretty messy
    // Also hi AHauntedArmy lol if you looking at this
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        public static AssetBundle MainBundle;
        public static GameObject Pad;
        public static GameObject ActivePad = new GameObject();
        public static Material HoloMat;
        public Transform RightHand;
        public static GameObject GorillaPlayer = new GameObject();
        public static TextMeshPro SpawnSpot = new TextMeshPro();
        public static GameObject CurrentSelectedObject = new GameObject();
        public LayerMask ObjectLayerMask = new LayerMask();
        public static LineRenderer lr = new LineRenderer();
        public bool RightButtonLast;
        public bool LastActiveTrigger;
        public static bool FlagBrokenVehicle;
        public bool SelectedVehicle;
        public static List<GameObject> Buttons = new List<GameObject>();
        public GameObject SelectedVehicleObject = new GameObject();
        public List<int> CurrentPageIndexes = new List<int>()
        {
            0,
            0,
            0,
            0,
        };

        public static Plugin Instance { get; private set; }
        public List<int> PageIndexes = new List<int>()
        {
            0,
            0,
            0,
            0,
        };
        public static int CurrentTab = 0;

        public static GameObject ObjectParent = new GameObject();

        void Start()
        {
            Instance = this;
            Utilla.Events.GameInitialized += OnGameInitialized;
            string folder = Path.GetDirectoryName(typeof(Plugin).Assembly.Location);
            Debug.Log(folder);
            if (!Directory.Exists(folder + "\\CustomVehicles"))
            {
                Directory.CreateDirectory(folder + "\\CustomVehicles");
            }
        }

        void OnEnable()
        {
            foreach (var obj in MainUtils.LoadedPresets)
            {
                obj.SetActive(true);
            }
            ActivePad.SetActive(true);
        }

        void OnDisable()
        {
            foreach (var obj in MainUtils.LoadedPresets)
            {
                obj.SetActive(false);
            }

            for (int i = 0; i < MainUtils.SpawnedVehicles.Count; i++)
            {
                MainUtils.SpawnedVehicles.Remove(MainUtils.SpawnedVehicles[i]);
                Destroy(MainUtils.SpawnedVehicles[i]);
            }
            ActivePad.SetActive(false);

            foreach (GameObject button in Buttons)
            {
                button.GetComponent<Button>().Delayed = false;
            }

            foreach (GameObject Preview in MainUtils.LoadedPreviews)
            {
                Preview.SetActive(false);
            }

            CurrentSelectedObject = null;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
           
                RightHand = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R").transform;
            try
            {
                MainUtils.SetupScriptBundle();
            }
            catch
            {
                FlagBrokenVehicle = true;
            }
            ObjectLayerMask = LayerMask.GetMask(new string[] { "Gorilla Object", "Default" });
            GorillaPlayer = GameObject.Find("GorillaPlayer");
            var entry = Plugin.Instance.Config.Bind("EnabledMods", PluginInfo.Name, true);
            if (!Plugin.FlagBrokenVehicle)
            {
                
            if (!entry.Value)
            {
                foreach (var obj in MainUtils.LoadedPresets)
                {
                    obj.SetActive(false);
                }

                ActivePad.SetActive(false);
            }
            }
            else
            {
                Debug.LogError("Broken vehicle file found");
                foreach (var obj in MainUtils.LoadedPresets)
                {
                    obj.SetActive(false);
                }

                ActivePad.SetActive(false);
            }

        }


        public void UpdateCurrentTab(bool Up)
        {
            Debug.Log("ButtonPressed");
                if (Up)
                {
                if (CurrentPageIndexes[CurrentTab] < MainUtils.LoadedPreviews.Count - 1)
                {
                        CurrentPageIndexes[CurrentTab]++;
                }
                else
                {
                        CurrentPageIndexes[CurrentTab] = 0;
                }
                }
                else
                {
                    if (CurrentPageIndexes[CurrentTab] != 0)
                    {
                        CurrentPageIndexes[CurrentTab]--;
                    }
                    else
                    {
                        CurrentPageIndexes[CurrentTab] = MainUtils.LoadedPreviews.Count - 1;
                    }
                }
        
        }

        public void EnterCurrentFunction()
        {
            Debug.Log("ButtonPressed");
            foreach (GameObject obj in MainUtils.LoadedPresets)
            {
                obj.SetActive(false);
            }
            PageIndexes[CurrentTab] = CurrentPageIndexes[CurrentTab];
        }

        void Update()
        {
          if (MainUtils.LoadedObjects.Count != 0 && !FlagBrokenVehicle)
            {
         if (inRoom)
                {
                    List<GameObject> list = new List<GameObject>();
                    foreach (GameObject obj in MainUtils.SpawnedVehicles)
                    {
                     if (obj != null)
                        {
                        if (obj.GetComponent<VehicleScript>().SelectedVehicle)
                        {
                            list.Add(obj);
                        }
                        }

                    }

                    if (list.Count != 0)
                    {
                        SelectedVehicleObject = list[0];
                    }
                    else
                    {
                        SelectedVehicleObject = null;
                    }

                    if (SelectedVehicleObject != null)
                    {
                        GorillaPlayer.GetComponent<GorillaSnapTurn>().enabled = false;
                    }
                    else
                    {
                        GorillaPlayer.GetComponent<GorillaSnapTurn>().enabled = true;
                    }
            if (MainUtils.LoadedPreviews.Count != 0)
            {
                if (CurrentSelectedObject != MainUtils.LoadedPreviews[CurrentPageIndexes[CurrentTab]] && CurrentTab == 0 && ActivePad.activeSelf)
                {
                    for (int i = 0; i < MainUtils.LoadedPreviews.Count; i++)
                    {
                        if (i == CurrentPageIndexes[CurrentTab])
                        {
                            MainUtils.LoadedPreviews[i].SetActive(true);

                            CurrentSelectedObject = MainUtils.LoadedPreviews[i];
                            SpawnSpot.text = MainUtils.LoadedObjectNames[i];
                        }
                        else
                        {
                            MainUtils.LoadedPreviews[i].SetActive(false);

                        }

                        if (i == PageIndexes[CurrentTab])
                        {
                            MainUtils.LoadedPresets[i].SetActive(true);
                        }
                        else
                        {
                            MainUtils.LoadedPresets[i].SetActive(false);
                        }
                    }


                }
                else
                {
                    foreach (var obj in MainUtils.LoadedPresets)
                    {
                        obj.SetActive(false);
                    }
                }




                if (CurrentTab == 0 && ActivePad.activeSelf)
                {
                    if (!MainUtils.LoadedPresets[PageIndexes[0]].activeSelf)
                    {
                        MainUtils.LoadedPresets[PageIndexes[0]].SetActive(true);
                    }

                    HandleRaycasting(MainUtils.LoadedPresets[PageIndexes[0]]);
                    HandleInput();

                }
                else
                {
                    if (MainUtils.LoadedPresets[PageIndexes[0]].activeSelf)
                    {
                        MainUtils.LoadedPresets[PageIndexes[0]].SetActive(false);
                    }
                }
                }
            HandleVehicleSelectRaycasting();
            }
            }


                if (ControllerInputPoller.instance.leftControllerPrimaryButton && inRoom)
                {
                    ActivePad.SetActive(true);
                }
                else
                {
                    ActivePad.SetActive(false);
                }
        }

        public void HandleInput()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat >= 0.5f && !LastActiveTrigger)
            {
                GameObject insobj = GameObject.Instantiate(MainUtils.LoadedObjects[PageIndexes[0]]);
                MainUtils.SpawnedVehicles.Add(insobj);
                insobj.transform.SetMaterialOverrideInParent(0);
                Debug.Log(insobj.layer);
                Vector3 correctPos = new Vector3(MainUtils.LoadedPresets[PageIndexes[0]].transform.position.x, MainUtils.LoadedPresets[PageIndexes[0]].transform.position.y + 2f, MainUtils.LoadedPresets[PageIndexes[0]].transform.position.z);
                insobj.transform.position = correctPos;
                insobj.transform.rotation = MainUtils.LoadedPresets[PageIndexes[0]].transform.rotation;
                insobj.transform.localScale = MainUtils.LoadedPresets[PageIndexes[0]].transform.localScale;
                LastActiveTrigger = true;
            }

            if (ControllerInputPoller.instance.rightControllerIndexFloat < 0.5f)
            {
                LastActiveTrigger = false;
            }

            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                foreach (GameObject obj in MainUtils.LoadedPresets)
                {
                    float Rotationoffset = 1f;
                    Quaternion currentRotation = obj.transform.rotation;


                    Quaternion rotationOffset = Quaternion.Euler(0f, Rotationoffset, 0f);

                    obj.transform.rotation = currentRotation * rotationOffset;
                }
            }

            if (ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                foreach (GameObject obj in MainUtils.LoadedPresets)
                {
                    float Rotationoffset = 1f;
                    Quaternion currentRotation = obj.transform.rotation;


                    Quaternion rotationOffset = Quaternion.Euler(Rotationoffset, 0f, 0f);

                    obj.transform.rotation = currentRotation * rotationOffset;
                }
            }


        }

        public void HandleRaycasting(GameObject CurrentRaycastObject)
        {
            Ray ray = new Ray(RightHand.position, RightHand.up);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ObjectLayerMask))
            {
                Vector3 hitPoint = hit.point;
                CurrentRaycastObject.transform.position = hitPoint;
            }
        }

        public void HandleVehicleSelectRaycasting()
        {
            if (!ActivePad.activeSelf)
            {
                Ray ray = new Ray(RightHand.position, RightHand.up);
               if (ControllerInputPoller.instance.rightControllerPrimaryButton && !RightButtonLast)
                {
                    if (CurrentSelectedObject != null && !SelectedVehicle)
                    {
                        CurrentSelectedObject.GetComponent<VehicleScript>().SelectedVehicle = CurrentSelectedObject.GetComponent<VehicleScript>().SelectedVehicle == false ? true : false;
                        RightButtonLast = true;
                    }
                    else if (SelectedVehicle && CurrentSelectedObject != null)
                    {
                        foreach (GameObject vehicle in MainUtils.SpawnedVehicles)
                        {
                            if (vehicle != CurrentSelectedObject)
                            {
                            vehicle.GetComponent<VehicleScript>().SelectedVehicle = false;                                
                            }

                        }
                        CurrentSelectedObject.GetComponent<VehicleScript>().SelectedVehicle = CurrentSelectedObject.GetComponent<VehicleScript>().SelectedVehicle == false ? true : false;
                        RightButtonLast = true;
                    }
                }

               if (ControllerInputPoller.instance.rightControllerSecondaryButton && ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                {
                    if (CurrentSelectedObject != null)
                    {
                        foreach (GameObject obj in MainUtils.SpawnedVehicles)
                        {
                            if (obj == CurrentSelectedObject)
                            {
                               MainUtils.SpawnedVehicles.Remove(obj);
                            }
                        }
                        Destroy(CurrentSelectedObject);
                    }
                }

               if (!ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    RightButtonLast= false;
                }
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "SelectPoint")
                    {

                        if (lr != null)
                        {
                            lr.gameObject.SetActive(true);
                            CurrentSelectedObject = hit.collider.gameObject.transform.parent.gameObject;
                            lr.SetPosition(0, RightHand.position);
                            lr.SetPosition(1, hit.collider.transform.position);
                        }
                    }
                    else
                    {
                        lr.gameObject.SetActive(false);
                        CurrentSelectedObject = null;
                    }
                }
                else
                {
                    CurrentSelectedObject = null;
                    if (lr != null)
                    {
                        lr.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                CurrentSelectedObject = null;
                lr.gameObject.SetActive(false);
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            foreach (var obj in MainUtils.LoadedPresets)
            {
                obj.SetActive(false);
            }

            for (int i = 0; i < MainUtils.SpawnedVehicles.Count; i++)
            {
                MainUtils.SpawnedVehicles.Remove(MainUtils.SpawnedVehicles[i]);
                Destroy(MainUtils.SpawnedVehicles[i]);
            }
            ActivePad.SetActive(false);

            foreach (GameObject button in Buttons)
            {
                button.GetComponent<Button>().Delayed = false;
            }

            foreach (GameObject Preview in MainUtils.LoadedPreviews)
            {
                Preview.SetActive(false);
            }
            CurrentSelectedObject = null;
            lr.gameObject.SetActive(false);
            inRoom = false;
        }
    }
}
