//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Playercontrols.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Playercontrols: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Playercontrols()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Playercontrols"",
    ""maps"": [
        {
            ""name"": ""ground"",
            ""id"": ""f461c4e5-c770-4e98-98d3-766d520d58d7"",
            ""actions"": [
                {
                    ""name"": ""move"",
                    ""type"": ""Value"",
                    ""id"": ""8a62ae01-b743-4f51-8bf5-37c3cbe109b5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Value"",
                    ""id"": ""0e3a5adf-8645-4c19-9a67-7efaf3efdabe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""crouch"",
                    ""type"": ""Button"",
                    ""id"": ""cd67a508-08bc-4265-9378-ddfabfb12ecb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""slide"",
                    ""type"": ""Button"",
                    ""id"": ""77192150-384b-4c1c-a2e5-b14a288e6cb1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""a9f6527c-e7f5-46e4-bb38-6d3098d81bc4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""07caf2ec-eb4a-49d2-92fe-2b9286cddd04"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7995b8b1-1f44-4b27-afee-3fb81cf87107"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3d96c2d7-15f0-44c6-b67d-0df081c9522b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6ae265d5-7d12-4ace-a8d3-707e95525975"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""eaff8d82-7ec3-4049-aee8-d038628df48c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc0c9933-9014-411c-b6ef-3fed01c79232"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4047f3c-ebbb-4f00-b304-ef7f506a3df8"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""interactable"",
            ""id"": ""2640ea27-e37c-462d-af77-3e214a645cd9"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""c763bb48-a9da-4d15-bc18-1dc8da630e28"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c3419e05-33a9-4cfc-966d-9c35db412bf3"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""gun"",
            ""id"": ""a5cdd5e3-be6f-4fae-b4a8-afecbb8d2491"",
            ""actions"": [
                {
                    ""name"": ""shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9063585b-c8fe-4fba-ae2e-1a5ec695e4d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""reload"",
                    ""type"": ""Button"",
                    ""id"": ""35960d94-7cd1-4366-a71a-33dd7df2295d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""drop"",
                    ""type"": ""Button"",
                    ""id"": ""29831715-60f7-48d4-8465-54f8b9d46599"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""13f65313-1850-4d59-a40b-9a7d902ef181"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80e60d12-938a-476f-b8a4-587938e7f21b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb3a6eef-00ee-409c-8067-b3b5c3eea05f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControls"",
                    ""action"": ""drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerControls"",
            ""bindingGroup"": ""PlayerControls"",
            ""devices"": []
        }
    ]
}");
        // ground
        m_ground = asset.FindActionMap("ground", throwIfNotFound: true);
        m_ground_move = m_ground.FindAction("move", throwIfNotFound: true);
        m_ground_jump = m_ground.FindAction("jump", throwIfNotFound: true);
        m_ground_crouch = m_ground.FindAction("crouch", throwIfNotFound: true);
        m_ground_slide = m_ground.FindAction("slide", throwIfNotFound: true);
        // interactable
        m_interactable = asset.FindActionMap("interactable", throwIfNotFound: true);
        m_interactable_Newaction = m_interactable.FindAction("New action", throwIfNotFound: true);
        // gun
        m_gun = asset.FindActionMap("gun", throwIfNotFound: true);
        m_gun_shoot = m_gun.FindAction("shoot", throwIfNotFound: true);
        m_gun_reload = m_gun.FindAction("reload", throwIfNotFound: true);
        m_gun_drop = m_gun.FindAction("drop", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // ground
    private readonly InputActionMap m_ground;
    private List<IGroundActions> m_GroundActionsCallbackInterfaces = new List<IGroundActions>();
    private readonly InputAction m_ground_move;
    private readonly InputAction m_ground_jump;
    private readonly InputAction m_ground_crouch;
    private readonly InputAction m_ground_slide;
    public struct GroundActions
    {
        private @Playercontrols m_Wrapper;
        public GroundActions(@Playercontrols wrapper) { m_Wrapper = wrapper; }
        public InputAction @move => m_Wrapper.m_ground_move;
        public InputAction @jump => m_Wrapper.m_ground_jump;
        public InputAction @crouch => m_Wrapper.m_ground_crouch;
        public InputAction @slide => m_Wrapper.m_ground_slide;
        public InputActionMap Get() { return m_Wrapper.m_ground; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GroundActions set) { return set.Get(); }
        public void AddCallbacks(IGroundActions instance)
        {
            if (instance == null || m_Wrapper.m_GroundActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GroundActionsCallbackInterfaces.Add(instance);
            @move.started += instance.OnMove;
            @move.performed += instance.OnMove;
            @move.canceled += instance.OnMove;
            @jump.started += instance.OnJump;
            @jump.performed += instance.OnJump;
            @jump.canceled += instance.OnJump;
            @crouch.started += instance.OnCrouch;
            @crouch.performed += instance.OnCrouch;
            @crouch.canceled += instance.OnCrouch;
            @slide.started += instance.OnSlide;
            @slide.performed += instance.OnSlide;
            @slide.canceled += instance.OnSlide;
        }

        private void UnregisterCallbacks(IGroundActions instance)
        {
            @move.started -= instance.OnMove;
            @move.performed -= instance.OnMove;
            @move.canceled -= instance.OnMove;
            @jump.started -= instance.OnJump;
            @jump.performed -= instance.OnJump;
            @jump.canceled -= instance.OnJump;
            @crouch.started -= instance.OnCrouch;
            @crouch.performed -= instance.OnCrouch;
            @crouch.canceled -= instance.OnCrouch;
            @slide.started -= instance.OnSlide;
            @slide.performed -= instance.OnSlide;
            @slide.canceled -= instance.OnSlide;
        }

        public void RemoveCallbacks(IGroundActions instance)
        {
            if (m_Wrapper.m_GroundActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGroundActions instance)
        {
            foreach (var item in m_Wrapper.m_GroundActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GroundActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GroundActions @ground => new GroundActions(this);

    // interactable
    private readonly InputActionMap m_interactable;
    private List<IInteractableActions> m_InteractableActionsCallbackInterfaces = new List<IInteractableActions>();
    private readonly InputAction m_interactable_Newaction;
    public struct InteractableActions
    {
        private @Playercontrols m_Wrapper;
        public InteractableActions(@Playercontrols wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_interactable_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_interactable; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractableActions set) { return set.Get(); }
        public void AddCallbacks(IInteractableActions instance)
        {
            if (instance == null || m_Wrapper.m_InteractableActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InteractableActionsCallbackInterfaces.Add(instance);
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(IInteractableActions instance)
        {
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(IInteractableActions instance)
        {
            if (m_Wrapper.m_InteractableActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInteractableActions instance)
        {
            foreach (var item in m_Wrapper.m_InteractableActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InteractableActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InteractableActions @interactable => new InteractableActions(this);

    // gun
    private readonly InputActionMap m_gun;
    private List<IGunActions> m_GunActionsCallbackInterfaces = new List<IGunActions>();
    private readonly InputAction m_gun_shoot;
    private readonly InputAction m_gun_reload;
    private readonly InputAction m_gun_drop;
    public struct GunActions
    {
        private @Playercontrols m_Wrapper;
        public GunActions(@Playercontrols wrapper) { m_Wrapper = wrapper; }
        public InputAction @shoot => m_Wrapper.m_gun_shoot;
        public InputAction @reload => m_Wrapper.m_gun_reload;
        public InputAction @drop => m_Wrapper.m_gun_drop;
        public InputActionMap Get() { return m_Wrapper.m_gun; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GunActions set) { return set.Get(); }
        public void AddCallbacks(IGunActions instance)
        {
            if (instance == null || m_Wrapper.m_GunActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GunActionsCallbackInterfaces.Add(instance);
            @shoot.started += instance.OnShoot;
            @shoot.performed += instance.OnShoot;
            @shoot.canceled += instance.OnShoot;
            @reload.started += instance.OnReload;
            @reload.performed += instance.OnReload;
            @reload.canceled += instance.OnReload;
            @drop.started += instance.OnDrop;
            @drop.performed += instance.OnDrop;
            @drop.canceled += instance.OnDrop;
        }

        private void UnregisterCallbacks(IGunActions instance)
        {
            @shoot.started -= instance.OnShoot;
            @shoot.performed -= instance.OnShoot;
            @shoot.canceled -= instance.OnShoot;
            @reload.started -= instance.OnReload;
            @reload.performed -= instance.OnReload;
            @reload.canceled -= instance.OnReload;
            @drop.started -= instance.OnDrop;
            @drop.performed -= instance.OnDrop;
            @drop.canceled -= instance.OnDrop;
        }

        public void RemoveCallbacks(IGunActions instance)
        {
            if (m_Wrapper.m_GunActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGunActions instance)
        {
            foreach (var item in m_Wrapper.m_GunActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GunActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GunActions @gun => new GunActions(this);
    private int m_PlayerControlsSchemeIndex = -1;
    public InputControlScheme PlayerControlsScheme
    {
        get
        {
            if (m_PlayerControlsSchemeIndex == -1) m_PlayerControlsSchemeIndex = asset.FindControlSchemeIndex("PlayerControls");
            return asset.controlSchemes[m_PlayerControlsSchemeIndex];
        }
    }
    public interface IGroundActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
    }
    public interface IInteractableActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IGunActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
    }
}