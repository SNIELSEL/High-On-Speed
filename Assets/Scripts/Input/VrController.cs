//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/VrController.inputactions
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

public partial class @VrController: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @VrController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""VrController"",
    ""maps"": [
        {
            ""name"": ""Oculus"",
            ""id"": ""bbf3e5a8-a09d-4402-858c-11a6c7021284"",
            ""actions"": [
                {
                    ""name"": ""Gas"",
                    ""type"": ""Value"",
                    ""id"": ""4c84cea7-2d9e-4ba6-8b4f-f5f6353d1c19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Value"",
                    ""id"": ""a695d427-6745-4461-a30b-9e3b65813eb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GearBox"",
                    ""type"": ""Button"",
                    ""id"": ""ae4a945f-0ac4-4c2e-b70f-2f3331fd6447"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Steering"",
                    ""type"": ""Value"",
                    ""id"": ""942587d2-e5e4-463c-8482-27d6ab1777c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2b0a71cb-1cc9-4d2b-a3fa-c47f9ea76890"",
                    ""path"": ""<OculusTouchController>{RightHand}/{PrimaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6bba687-37d9-41b6-8c54-2cc35cd30a71"",
                    ""path"": ""<OculusTouchController>{LeftHand}/{PrimaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afc65abb-7535-4ce7-a6ab-709e099dddc5"",
                    ""path"": ""<OculusTouchController>{RightHand}/{SecondaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GearBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b86e7074-8943-4f38-8710-8b15d6f214bb"",
                    ""path"": ""<OculusTouchController>{LeftHand}/{SecondaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GearBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29148ee8-380e-4f3c-9097-2f5cad310882"",
                    ""path"": ""<OculusTouchController>{RightHand}/thumbstickTouched"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""id"": ""bf7c8043-051d-40e1-abeb-2bd712d3d910"",
            ""actions"": [
                {
                    ""name"": ""Gas"",
                    ""type"": ""Value"",
                    ""id"": ""9ec29eda-857c-41d9-8c5d-70e652b3ecf1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Value"",
                    ""id"": ""a9e367d3-444a-4620-abbf-8c0d04403216"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GearBox"",
                    ""type"": ""Button"",
                    ""id"": ""14d6e377-4e20-4d1e-aff4-08190955a03d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Steering"",
                    ""type"": ""Value"",
                    ""id"": ""43d73758-146e-43a4-b5e7-35b029e39013"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ab196f6-cba1-4ab8-8d6e-3302909c0667"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""173ab8b6-0bce-49ce-b781-e0d19f574c49"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12380dfc-d843-44f2-8738-194473f1f331"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GearBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3af95f7-c3f5-4da6-94d7-f333133d84d9"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GearBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feaf9496-c326-47a1-bc2b-55dda21a548b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Oculus
        m_Oculus = asset.FindActionMap("Oculus", throwIfNotFound: true);
        m_Oculus_Gas = m_Oculus.FindAction("Gas", throwIfNotFound: true);
        m_Oculus_Brake = m_Oculus.FindAction("Brake", throwIfNotFound: true);
        m_Oculus_GearBox = m_Oculus.FindAction("GearBox", throwIfNotFound: true);
        m_Oculus_Steering = m_Oculus.FindAction("Steering", throwIfNotFound: true);
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_Gas = m_Controller.FindAction("Gas", throwIfNotFound: true);
        m_Controller_Brake = m_Controller.FindAction("Brake", throwIfNotFound: true);
        m_Controller_GearBox = m_Controller.FindAction("GearBox", throwIfNotFound: true);
        m_Controller_Steering = m_Controller.FindAction("Steering", throwIfNotFound: true);
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

    // Oculus
    private readonly InputActionMap m_Oculus;
    private List<IOculusActions> m_OculusActionsCallbackInterfaces = new List<IOculusActions>();
    private readonly InputAction m_Oculus_Gas;
    private readonly InputAction m_Oculus_Brake;
    private readonly InputAction m_Oculus_GearBox;
    private readonly InputAction m_Oculus_Steering;
    public struct OculusActions
    {
        private @VrController m_Wrapper;
        public OculusActions(@VrController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Gas => m_Wrapper.m_Oculus_Gas;
        public InputAction @Brake => m_Wrapper.m_Oculus_Brake;
        public InputAction @GearBox => m_Wrapper.m_Oculus_GearBox;
        public InputAction @Steering => m_Wrapper.m_Oculus_Steering;
        public InputActionMap Get() { return m_Wrapper.m_Oculus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OculusActions set) { return set.Get(); }
        public void AddCallbacks(IOculusActions instance)
        {
            if (instance == null || m_Wrapper.m_OculusActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_OculusActionsCallbackInterfaces.Add(instance);
            @Gas.started += instance.OnGas;
            @Gas.performed += instance.OnGas;
            @Gas.canceled += instance.OnGas;
            @Brake.started += instance.OnBrake;
            @Brake.performed += instance.OnBrake;
            @Brake.canceled += instance.OnBrake;
            @GearBox.started += instance.OnGearBox;
            @GearBox.performed += instance.OnGearBox;
            @GearBox.canceled += instance.OnGearBox;
            @Steering.started += instance.OnSteering;
            @Steering.performed += instance.OnSteering;
            @Steering.canceled += instance.OnSteering;
        }

        private void UnregisterCallbacks(IOculusActions instance)
        {
            @Gas.started -= instance.OnGas;
            @Gas.performed -= instance.OnGas;
            @Gas.canceled -= instance.OnGas;
            @Brake.started -= instance.OnBrake;
            @Brake.performed -= instance.OnBrake;
            @Brake.canceled -= instance.OnBrake;
            @GearBox.started -= instance.OnGearBox;
            @GearBox.performed -= instance.OnGearBox;
            @GearBox.canceled -= instance.OnGearBox;
            @Steering.started -= instance.OnSteering;
            @Steering.performed -= instance.OnSteering;
            @Steering.canceled -= instance.OnSteering;
        }

        public void RemoveCallbacks(IOculusActions instance)
        {
            if (m_Wrapper.m_OculusActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IOculusActions instance)
        {
            foreach (var item in m_Wrapper.m_OculusActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_OculusActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public OculusActions @Oculus => new OculusActions(this);

    // Controller
    private readonly InputActionMap m_Controller;
    private List<IControllerActions> m_ControllerActionsCallbackInterfaces = new List<IControllerActions>();
    private readonly InputAction m_Controller_Gas;
    private readonly InputAction m_Controller_Brake;
    private readonly InputAction m_Controller_GearBox;
    private readonly InputAction m_Controller_Steering;
    public struct ControllerActions
    {
        private @VrController m_Wrapper;
        public ControllerActions(@VrController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Gas => m_Wrapper.m_Controller_Gas;
        public InputAction @Brake => m_Wrapper.m_Controller_Brake;
        public InputAction @GearBox => m_Wrapper.m_Controller_GearBox;
        public InputAction @Steering => m_Wrapper.m_Controller_Steering;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void AddCallbacks(IControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_ControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ControllerActionsCallbackInterfaces.Add(instance);
            @Gas.started += instance.OnGas;
            @Gas.performed += instance.OnGas;
            @Gas.canceled += instance.OnGas;
            @Brake.started += instance.OnBrake;
            @Brake.performed += instance.OnBrake;
            @Brake.canceled += instance.OnBrake;
            @GearBox.started += instance.OnGearBox;
            @GearBox.performed += instance.OnGearBox;
            @GearBox.canceled += instance.OnGearBox;
            @Steering.started += instance.OnSteering;
            @Steering.performed += instance.OnSteering;
            @Steering.canceled += instance.OnSteering;
        }

        private void UnregisterCallbacks(IControllerActions instance)
        {
            @Gas.started -= instance.OnGas;
            @Gas.performed -= instance.OnGas;
            @Gas.canceled -= instance.OnGas;
            @Brake.started -= instance.OnBrake;
            @Brake.performed -= instance.OnBrake;
            @Brake.canceled -= instance.OnBrake;
            @GearBox.started -= instance.OnGearBox;
            @GearBox.performed -= instance.OnGearBox;
            @GearBox.canceled -= instance.OnGearBox;
            @Steering.started -= instance.OnSteering;
            @Steering.performed -= instance.OnSteering;
            @Steering.canceled -= instance.OnSteering;
        }

        public void RemoveCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_ControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);
    public interface IOculusActions
    {
        void OnGas(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
        void OnGearBox(InputAction.CallbackContext context);
        void OnSteering(InputAction.CallbackContext context);
    }
    public interface IControllerActions
    {
        void OnGas(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
        void OnGearBox(InputAction.CallbackContext context);
        void OnSteering(InputAction.CallbackContext context);
    }
}