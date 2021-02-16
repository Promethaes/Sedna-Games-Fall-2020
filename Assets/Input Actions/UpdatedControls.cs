// GENERATED AUTOMATICALLY FROM 'Assets/Input Actions/UpdatedControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UpdatedControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UpdatedControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UpdatedControls"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""c7dcecfa-c0e3-418b-a58f-7b39149b82d9"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""01591a5d-c891-4121-891e-6e7fd8c61a06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability"",
                    ""type"": ""Button"",
                    ""id"": ""732207c6-f34a-4e83-9435-d8cda42b2386"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""182bcb4e-f19f-45fb-ae6d-2d5489b5ad20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""740d9177-11d9-4f24-8359-531afaa83c6c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0f9ef348-5591-43b0-9d70-483d528d592a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""90d02b9e-1d1d-426d-85bf-2e42ebe2ab88"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Revive"",
                    ""type"": ""Button"",
                    ""id"": ""9e8392ea-8a5d-4ace-8d59-ee8b798c3ddd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""f5fafdd6-9530-4af6-8bad-9a931db42fb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CombatAbility"",
                    ""type"": ""Button"",
                    ""id"": ""49fa762c-2d26-4072-be9e-c6f94b0aa8c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1f90dc00-be92-4cbd-8626-75b08a1da628"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d7508ab4-fef4-405f-b73c-9ea08dbcf264"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e94ac9bc-5921-47d0-9694-b93c7a4551dd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5919a8cd-9742-497e-8f09-93daf1cb4df9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""12944c38-e7c0-4533-92be-1e111b10c0ca"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""598a5242-ddbf-402f-a827-bff0d6542a2d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d75584fe-1cc9-4f4b-bd2d-214ccac33543"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MouseInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38c18455-e982-40a3-8859-eceb9cadfd28"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25b2306d-04e8-419a-88e5-9efb457c8ae4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2d71d72-913e-426e-98a6-13b5d2a47de5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b52e89c2-9b89-4df3-846b-6b612eef22bb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48aa9437-0439-42c8-8033-71692ca9c2b7"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d30b2476-bea2-4f30-92f8-37c73931d163"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""deff3e6e-9f02-4065-a9ab-36affe7af6ef"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""431ee30c-ef58-4cd2-8cb1-651af1881dbf"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00153398-a502-43d4-86f4-6ef73dea4378"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b68c9af7-13ae-489f-a5a3-17c13fecc711"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Revive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd596748-fffd-41b2-b0ad-6ec4fb512868"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0823805e-1631-4ef3-8d6b-ece912945be0"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CombatAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""57b855f1-a58f-45b3-aa34-ad11df5912b7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c6ac14bd-34ff-4d2a-b44b-3064241ce310"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""PassThrough"",
                    ""id"": ""94742509-e9a7-4be5-b907-6d7bc8453e08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""58dbd5c2-0b1b-4f32-97d0-5fba9459e08d"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""206d1545-2074-49ee-b627-d818e5e0dbda"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""9c566c20-804e-453d-9301-689238887542"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4a51badc-637f-40a2-9c2e-a40ae66476b2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""89fc4164-1727-4b12-b04e-19c51d0a2554"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3a985963-0ef5-40a3-9e61-c42299a7b39c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fce2ecce-3d05-4f63-a89a-ada32034ee0e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow keys"",
                    ""id"": ""218e3f9e-b35e-4b17-8b90-90687fcda9d7"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f2f3f6ee-c910-4635-b092-0b8bb8f4b649"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ea7a1c20-5c97-43c2-8df2-a84a9ead4a78"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""37903710-2122-4217-b755-054aa48bfff8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e80ad96c-2133-4e79-8ee5-fc1754e4ca06"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c52f3def-9bed-4086-8035-2b69cd15250f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6ca6faf-fe58-414b-8d62-ee7c281b253c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb1ff1cf-19fe-47fc-a5c3-3e284e3b09bc"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CharSelect"",
            ""id"": ""75f1d479-ac92-40c0-b73e-1bf5970bfc42"",
            ""actions"": [
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""50359a77-37c0-4d57-9565-94ec339d6163"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6e3ff01f-7abf-4ec9-bca2-7ac169f90beb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Unconfirm"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1debc90f-2450-42c0-aae2-68b4365bee72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartGame"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4cfe81cb-7e81-4906-bba6-8bf827f9630d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bc938145-742e-48df-9928-ede66113ad44"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c07c1f6-8e75-4454-bcbf-90a5837cd22a"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""6ba0d240-7a94-420d-9460-b9e7b6a3c1fb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ba366e6d-7afe-4634-b596-0c3067cd4d36"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""edb9e209-5893-4e66-90e6-a8b2cdafcfb4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a851d6a5-f344-4a56-ab5b-0454433d169c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""89910441-2631-47ea-a341-2468cd7238e8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow keys"",
                    ""id"": ""684de3c4-6efb-4fd4-9dc2-398f1413e1ac"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""25e59154-ad44-46c7-92cd-a93979e57b1e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""284dc7b9-c6bf-4246-aa1f-d06a2d417c47"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b4b2e414-1720-4b90-bca8-7cce868f6615"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9a0afe2c-47ca-47c1-b1c8-17f5edb1720d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4d7f1154-77d9-4282-957d-c3a8da932164"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c179d15-3455-457a-a4f0-d65bcb271000"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a343822f-ed46-4391-808a-395042766d3a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Unconfirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ae1af90-f32b-4825-8a0b-2a8fed1fd390"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6730ad4a-e9fa-426c-9e55-ed08e1bdc5a3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Unconfirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bb2c941-bd43-40d6-8d9b-ddd8e0b7bf1c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ChatApp"",
            ""id"": ""cbb2bd43-eb15-4f6b-9ac1-567434693cfd"",
            ""actions"": [
                {
                    ""name"": ""SendMessage"",
                    ""type"": ""Button"",
                    ""id"": ""02f3d444-3c1d-4207-a6c0-f4e6904ed896"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2829d588-0edc-414b-b434-0b1af875dc2f"",
                    ""path"": ""*/{Menu}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SendMessage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Attack = m_Game.FindAction("Attack", throwIfNotFound: true);
        m_Game_Ability = m_Game.FindAction("Ability", throwIfNotFound: true);
        m_Game_Dash = m_Game.FindAction("Dash", throwIfNotFound: true);
        m_Game_Jump = m_Game.FindAction("Jump", throwIfNotFound: true);
        m_Game_MouseInput = m_Game.FindAction("MouseInput", throwIfNotFound: true);
        m_Game_Move = m_Game.FindAction("Move", throwIfNotFound: true);
        m_Game_Revive = m_Game.FindAction("Revive", throwIfNotFound: true);
        m_Game_Select = m_Game.FindAction("Select", throwIfNotFound: true);
        m_Game_CombatAbility = m_Game.FindAction("CombatAbility", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
        m_UI_Select = m_UI.FindAction("Select", throwIfNotFound: true);
        // CharSelect
        m_CharSelect = asset.FindActionMap("CharSelect", throwIfNotFound: true);
        m_CharSelect_MouseMove = m_CharSelect.FindAction("MouseMove", throwIfNotFound: true);
        m_CharSelect_Confirm = m_CharSelect.FindAction("Confirm", throwIfNotFound: true);
        m_CharSelect_Unconfirm = m_CharSelect.FindAction("Unconfirm", throwIfNotFound: true);
        m_CharSelect_StartGame = m_CharSelect.FindAction("StartGame", throwIfNotFound: true);
        // ChatApp
        m_ChatApp = asset.FindActionMap("ChatApp", throwIfNotFound: true);
        m_ChatApp_SendMessage = m_ChatApp.FindAction("SendMessage", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Attack;
    private readonly InputAction m_Game_Ability;
    private readonly InputAction m_Game_Dash;
    private readonly InputAction m_Game_Jump;
    private readonly InputAction m_Game_MouseInput;
    private readonly InputAction m_Game_Move;
    private readonly InputAction m_Game_Revive;
    private readonly InputAction m_Game_Select;
    private readonly InputAction m_Game_CombatAbility;
    public struct GameActions
    {
        private @UpdatedControls m_Wrapper;
        public GameActions(@UpdatedControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Game_Attack;
        public InputAction @Ability => m_Wrapper.m_Game_Ability;
        public InputAction @Dash => m_Wrapper.m_Game_Dash;
        public InputAction @Jump => m_Wrapper.m_Game_Jump;
        public InputAction @MouseInput => m_Wrapper.m_Game_MouseInput;
        public InputAction @Move => m_Wrapper.m_Game_Move;
        public InputAction @Revive => m_Wrapper.m_Game_Revive;
        public InputAction @Select => m_Wrapper.m_Game_Select;
        public InputAction @CombatAbility => m_Wrapper.m_Game_CombatAbility;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_GameActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnAttack;
                @Ability.started -= m_Wrapper.m_GameActionsCallbackInterface.OnAbility;
                @Ability.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnAbility;
                @Ability.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnAbility;
                @Dash.started -= m_Wrapper.m_GameActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnDash;
                @Jump.started -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @MouseInput.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMouseInput;
                @MouseInput.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMouseInput;
                @MouseInput.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMouseInput;
                @Move.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @Revive.started -= m_Wrapper.m_GameActionsCallbackInterface.OnRevive;
                @Revive.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnRevive;
                @Revive.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnRevive;
                @Select.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSelect;
                @CombatAbility.started -= m_Wrapper.m_GameActionsCallbackInterface.OnCombatAbility;
                @CombatAbility.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnCombatAbility;
                @CombatAbility.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnCombatAbility;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Ability.started += instance.OnAbility;
                @Ability.performed += instance.OnAbility;
                @Ability.canceled += instance.OnAbility;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MouseInput.started += instance.OnMouseInput;
                @MouseInput.performed += instance.OnMouseInput;
                @MouseInput.canceled += instance.OnMouseInput;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Revive.started += instance.OnRevive;
                @Revive.performed += instance.OnRevive;
                @Revive.canceled += instance.OnRevive;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @CombatAbility.started += instance.OnCombatAbility;
                @CombatAbility.performed += instance.OnCombatAbility;
                @CombatAbility.canceled += instance.OnCombatAbility;
            }
        }
    }
    public GameActions @Game => new GameActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Move;
    private readonly InputAction m_UI_Select;
    public struct UIActions
    {
        private @UpdatedControls m_Wrapper;
        public UIActions(@UpdatedControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_UI_Move;
        public InputAction @Select => m_Wrapper.m_UI_Select;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Select.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // CharSelect
    private readonly InputActionMap m_CharSelect;
    private ICharSelectActions m_CharSelectActionsCallbackInterface;
    private readonly InputAction m_CharSelect_MouseMove;
    private readonly InputAction m_CharSelect_Confirm;
    private readonly InputAction m_CharSelect_Unconfirm;
    private readonly InputAction m_CharSelect_StartGame;
    public struct CharSelectActions
    {
        private @UpdatedControls m_Wrapper;
        public CharSelectActions(@UpdatedControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMove => m_Wrapper.m_CharSelect_MouseMove;
        public InputAction @Confirm => m_Wrapper.m_CharSelect_Confirm;
        public InputAction @Unconfirm => m_Wrapper.m_CharSelect_Unconfirm;
        public InputAction @StartGame => m_Wrapper.m_CharSelect_StartGame;
        public InputActionMap Get() { return m_Wrapper.m_CharSelect; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharSelectActions set) { return set.Get(); }
        public void SetCallbacks(ICharSelectActions instance)
        {
            if (m_Wrapper.m_CharSelectActionsCallbackInterface != null)
            {
                @MouseMove.started -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnMouseMove;
                @MouseMove.performed -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnMouseMove;
                @MouseMove.canceled -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnMouseMove;
                @Confirm.started -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnConfirm;
                @Unconfirm.started -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnUnconfirm;
                @Unconfirm.performed -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnUnconfirm;
                @Unconfirm.canceled -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnUnconfirm;
                @StartGame.started -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnStartGame;
                @StartGame.performed -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnStartGame;
                @StartGame.canceled -= m_Wrapper.m_CharSelectActionsCallbackInterface.OnStartGame;
            }
            m_Wrapper.m_CharSelectActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseMove.started += instance.OnMouseMove;
                @MouseMove.performed += instance.OnMouseMove;
                @MouseMove.canceled += instance.OnMouseMove;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Unconfirm.started += instance.OnUnconfirm;
                @Unconfirm.performed += instance.OnUnconfirm;
                @Unconfirm.canceled += instance.OnUnconfirm;
                @StartGame.started += instance.OnStartGame;
                @StartGame.performed += instance.OnStartGame;
                @StartGame.canceled += instance.OnStartGame;
            }
        }
    }
    public CharSelectActions @CharSelect => new CharSelectActions(this);

    // ChatApp
    private readonly InputActionMap m_ChatApp;
    private IChatAppActions m_ChatAppActionsCallbackInterface;
    private readonly InputAction m_ChatApp_SendMessage;
    public struct ChatAppActions
    {
        private @UpdatedControls m_Wrapper;
        public ChatAppActions(@UpdatedControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SendMessage => m_Wrapper.m_ChatApp_SendMessage;
        public InputActionMap Get() { return m_Wrapper.m_ChatApp; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ChatAppActions set) { return set.Get(); }
        public void SetCallbacks(IChatAppActions instance)
        {
            if (m_Wrapper.m_ChatAppActionsCallbackInterface != null)
            {
                @SendMessage.started -= m_Wrapper.m_ChatAppActionsCallbackInterface.OnSendMessage;
                @SendMessage.performed -= m_Wrapper.m_ChatAppActionsCallbackInterface.OnSendMessage;
                @SendMessage.canceled -= m_Wrapper.m_ChatAppActionsCallbackInterface.OnSendMessage;
            }
            m_Wrapper.m_ChatAppActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SendMessage.started += instance.OnSendMessage;
                @SendMessage.performed += instance.OnSendMessage;
                @SendMessage.canceled += instance.OnSendMessage;
            }
        }
    }
    public ChatAppActions @ChatApp => new ChatAppActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IGameActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnAbility(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseInput(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRevive(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnCombatAbility(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface ICharSelectActions
    {
        void OnMouseMove(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnUnconfirm(InputAction.CallbackContext context);
        void OnStartGame(InputAction.CallbackContext context);
    }
    public interface IChatAppActions
    {
        void OnSendMessage(InputAction.CallbackContext context);
    }
}
