﻿// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
    public partial class TestComponent : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Test.ComponentWithEditorRequiredParameters>(0);
            __builder.AddComponentParameter(1, "Property1", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.String>(
#nullable restore
#line 1 "x:\dir\subdir\Test\TestComponent.cshtml"
                                                        myField

#line default
#line hidden
#nullable disable
            ));
            __builder.AddComponentParameter(2, "Property1Changed", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => myField = __value, myField));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 3 "x:\dir\subdir\Test\TestComponent.cshtml"
       
    private string myField = "Some Value";

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
