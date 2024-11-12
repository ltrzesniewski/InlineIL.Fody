﻿using System;
using System.Collections.Generic;
using System.Linq;
using InlineIL.Tests.InjectedAssembly;
using InlineIL.Tests.InvalidAssemblyToProcess;
using InlineIL.Tests.Support;
using JetBrains.Annotations;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving;

public abstract class TypeRefTestsBase() : ClassTestsBase("TypeRefTestCases");

public class TypeRefTests : TypeRefTestsBase
{
    [Fact]
    public void should_handle_type_arg()
    {
        var result = (RuntimeTypeHandle)GetInstance().ReturnTypeHandle<Guid>();
        result.ShouldEqual(typeof(Guid).TypeHandle);
    }

    [Fact]
    public void should_handle_type_token_load()
    {
        var handle = (RuntimeTypeHandle)GetInstance().ReturnTypeHandle();
        Type.GetTypeFromHandle(handle).ShouldEqual(typeof(Guid));
    }

    [Fact]
    public void should_handle_type_arg_generic()
    {
        var result = (RuntimeTypeHandle)GetInstance().ReturnTypeHandleGeneric<Guid>();
        result.ShouldEqual(typeof(Guid).TypeHandle);
    }

    [Fact]
    public void should_handle_type_token_load_generic()
    {
        var handle = (RuntimeTypeHandle)GetInstance().ReturnTypeHandleGeneric();
        Type.GetTypeFromHandle(handle).ShouldEqual(typeof(Guid));
    }

    [Fact]
    public void should_handle_type_arg_as_generic_method_call()
    {
        var result = (bool)GetInstance().IsString("foo");
        result.ShouldBeTrue();

        result = (bool)GetInstance().IsString((object)Array.Empty<string>());
        result.ShouldBeFalse();
    }

    [Fact]
    public void should_handle_type_arg_as_generic_method_call_with_type_param()
    {
        var result = (bool)GetInstance().GenericIsinst<string>("foo");
        result.ShouldBeTrue();

        result = (bool)GetInstance().GenericIsinst<string[]>("foo");
        result.ShouldBeFalse();
    }

    [Fact]
    public void should_handle_type_arg_different_ways()
    {
        var result = (RuntimeTypeHandle[])GetInstance().LoadTypeDifferentWays();
        result.ShouldAll(i => Equals(i, result[0]));
    }

    [Fact]
    public void should_handle_pointer_types_using_TypeRef()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadPointerTypeUsingTypeRef();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int**));
    }

    [Fact]
    public void should_handle_pointer_types_usingType()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadPointerTypeUsingType();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int**));
    }

    [Fact]
    public void should_handle_reference_types_using_TypeRef()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadReferenceTypeUsingTypeRef();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int).MakeByRefType());
    }

    [Fact]
    public void should_handle_reference_types_using_Type()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadReferenceTypeUsingType();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int).MakeByRefType());
    }

    [Fact]
    public void should_handle_array_types_using_TypeRef()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeUsingTypeRef();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][]));
    }

    [Fact]
    public void should_handle_array_types_using_Type()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeUsingType();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][]));
    }

    [Fact]
    public void should_handle_array_types_with_rank_using_TypeRef()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeWithRankUsingTypeRef();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][,,]));
    }

    [Fact]
    public void should_handle_array_types_with_rank_using_Type()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeWithRankUsingType();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][,,]));
    }

    [Fact]
    public void should_handle_generic_types_using_TypeRef()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadGenericTypeUsingTypeRef();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(Dictionary<int, string>));
    }

    [Fact]
    public void should_handle_generic_types_using_Type()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadGenericTypeUsingType();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(Dictionary<int, string>));
    }

    [Fact]
    public void should_handle_generic_types_by_name()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadGenericTypeByName();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(Action<int>));
    }

    [Fact]
    public void should_handle_open_generic_types_by_name()
    {
        var result = (RuntimeTypeHandle)GetInstance().LoadOpenGenericTypeByName();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(Action<>));
    }

    [Fact]
    public void should_handle_nested_types_using_runtime_syntax()
    {
        var result = (Type)GetInstance().ReturnNestedTypeUsingRuntimeSyntax();
        result.FullName.ShouldEqual($"{_verifiableAssembly}.TypeRefTestCases+NestedType");
    }

    [Fact]
    public void should_handle_nested_types_using_ecma_syntax()
    {
        var result = (Type)GetInstance().ReturnNestedTypeUsingEcmaSyntax();
        result.FullName.ShouldEqual($"{_verifiableAssembly}.TypeRefTestCases+NestedType");
    }

    [Fact]
    public void should_use_corelib_as_a_standalone_property()
    {
        var result = (string)GetInstance().ReturnCoreLibrary();
        result.ShouldNotBeNull();
    }

    [Fact]
    public void should_report_null_type()
    {
        ShouldHaveError("LoadNullType").ShouldContain("ldnull");
        ShouldHaveError("LoadNullTypeRef").ShouldContain("ldnull");
    }

    [Fact]
    public void should_report_unresolvable_assembly()
    {
        ShouldHaveError("InvalidAssembly").ShouldContain("Could not resolve assembly");
    }

    [Fact]
    public void should_report_unknown_type()
    {
        ShouldHaveError("InvalidType").ShouldContain("Could not find type");
    }

    [Fact]
    public void should_report_invalid_array_rank()
    {
        ShouldHaveError("InvalidArrayRank").ShouldContain("Invalid array rank");
    }

    [Fact]
    public void should_report_unconsumed_reference()
    {
        ShouldHaveError("UnusedInstance");
    }

    [Fact]
    public void should_report_generic_args_on_normal_type()
    {
        ShouldHaveError("NotAGenericType").ShouldContain("Not a generic type");
    }

    [Fact]
    public void should_report_empty_generic_args()
    {
        ShouldHaveError("NoGenericTypeArgs").ShouldContain("No generic arguments supplied");
    }

    [Fact]
    public void should_report_invalid_generic_args_count()
    {
        ShouldHaveError("InvalidGenericArgsCount").ShouldContain("Incorrect number of generic arguments");
    }

    [Fact]
    public void should_report_generic_params_on_generic_instance()
    {
        ShouldHaveError("GenericParamsOnGenericInstance").ShouldContain("Type is already a generic instance");
    }

    [Fact]
    public void should_report_byref_of_byref()
    {
        ShouldHaveError("ByRefOfByRef").ShouldContain("Type is already a ByRef type");
    }

    [Fact]
    public void should_report_pointer_to_byref()
    {
        ShouldHaveError("PointerToByRef").ShouldContain("Cannot make a pointer to a ByRef type");
    }

    [Fact]
    public void should_report_array_of_byref()
    {
        ShouldHaveError("ArrayOfByRef").ShouldContain("Cannot make an array of a ByRef type");
    }

    [Fact]
    public void should_report_incorrect_use_of_generics_and_byref()
    {
        ShouldHaveError("GenericOfByRef").ShouldContain("Cannot make a generic instance");
    }

    [Fact]
    public void should_report_incorrect_usage_of_byref_on_typedbyref()
    {
        ShouldHaveError("ByRefOfTypedReference").ShouldContain("Cannot create an array, pointer or ByRef to TypedReference");
    }

    [Fact]
    public void should_report_incorrect_usage_of_pointer_to_typedbyref()
    {
        ShouldHaveError("PointerToTypedReference").ShouldContain("Cannot create an array, pointer or ByRef to TypedReference");
    }

    [Fact]
    public void should_report_incorrect_usage_of_array_of_typedbyref()
    {
        ShouldHaveError("ArrayOfTypedReference").ShouldContain("Cannot create an array, pointer or ByRef to TypedReference");
    }

    [Fact]
    public void should_report_incorrect_usage_of_type_generic_parameter()
    {
        ShouldHaveError("TypeGenericParameter").ShouldContain("TypeRef.TypeGenericParameters can only be used in MethodRef definitions for overload resolution");
    }

    [Fact]
    public void should_report_incorrect_usage_of_method_generic_parameter()
    {
        ShouldHaveError("MethodGenericParameter").ShouldContain("TypeRef.MethodGenericParameters can only be used in MethodRef definitions for overload resolution");
    }

    [Fact]
    public void should_report_invalid_generic_parameter_index()
    {
        ShouldHaveError("InvalidGenericParameterIndex").ShouldContain("Invalid generic parameter index");
    }

    [Fact]
    public void should_add_injected_assembly_reference()
    {
        var assemblyName = typeof(InjectedType).Assembly.FullName;

        UnverifiableAssemblyToProcessFixture.OriginalModule.AssemblyReferences.ShouldNotContain(i => i.FullName == assemblyName);
        UnverifiableAssemblyToProcessFixture.ResultModule.AssemblyReferences.ShouldContain(i => i.FullName == assemblyName);
    }

    [Fact]
    public void should_not_add_injected_assembly_reference_if_already_exists()
    {
        var assemblyName = typeof(InjectedType).Assembly.FullName;

        InvalidAssemblyToProcessFixture.OriginalModule.AssemblyReferences.Count(i => i.FullName == assemblyName).ShouldEqual(1);
        InvalidAssemblyToProcessFixture.ResultModule.AssemblyReferences.Count(i => i.FullName == assemblyName).ShouldEqual(1);

        InvalidAssemblyToProcessFixture.ResultModule.AssemblyReferences.Count(i => i.FullName.StartsWith("InlineIL")).ShouldEqual(1);
    }

    [Fact]
    public void should_inject_type_from_dll()
    {
        var result = (int)GetUnverifiableInstance().UseInjectedDll();
        result.ShouldEqual(42);
    }

    [Fact]
    public void should_return_type_spec_from_injected_type()
    {
        var result = (RuntimeTypeHandle)GetUnverifiableInstance().ReturnInjectedTypeSpec();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(InjectedType[]));
    }

    [Fact]
    public void should_return_generic_type_spec_from_injected_type()
    {
        var result = (RuntimeTypeHandle)GetUnverifiableInstance().ReturnInjectedGenericTypeSpec();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(InjectedGenericType<>));
    }

    [Fact]
    public void should_return_generic_type_spec_from_injected_type_2()
    {
        var result = (RuntimeTypeHandle)GetUnverifiableInstance().ReturnInjectedGenericTypeSpec2();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(InjectedGenericType<,>));
    }

    [Fact]
    public void should_return_constructed_generic_type_spec_from_injected_type()
    {
        var result = (RuntimeTypeHandle)GetUnverifiableInstance().ReturnInjectedConstructedGenericTypeSpec();
        Type.GetTypeFromHandle(result).ShouldEqual(typeof(InjectedGenericType<InjectedType>));
    }

    [Theory]
    [InlineData(nameof(TypeRefTestCases.UseMethodsFromDifferentVersionsOfDll))]
    [InlineData(nameof(TypeRefTestCases.UseMethodsFromDifferentVersionsOfDllUsingTypeReference))]
    [InlineData(nameof(TypeRefTestCases.UseMethodsFromDifferentVersionsOfDllUsingTypeFullNameProperty))]
    public void should_use_methods_from_injected_type_and_referenced_type(string methodName)
    {
        var calls = InvalidAssemblyToProcessFixture.ResultModule
                                                   .GetType(typeof(TypeRefTestCases).FullName)
                                                   .Methods
                                                   .Single(i => i.Name == methodName)
                                                   .Body
                                                   .Instructions
                                                   .Where(i => i.OpCode.Code == Code.Call)
                                                   .ToArray();

        calls.Length.ShouldEqual(2);

        calls[0].Operand.ShouldBe<MethodReference>().Name.ShouldEqual("AddInt32");
        calls[1].Operand.ShouldBe<MethodReference>().Name.ShouldEqual("MultiplyInt32");
    }

    [Fact]
    public void should_report_dll_file_not_found()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedDllFile)).ShouldContain("Could not read assembly");
    }

    [Fact]
    public void should_report_type_in_dll_file_not_found()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedTypeName)).ShouldContain("Could not find type 'DoesNotExist'");
    }

    [Fact]
    public void should_report_injected_type_spec()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedTypeSpec)).ShouldContain("The provided type does not represent an element type");
    }

    [Fact]
    public void should_report_injected_type_spec_with_full_name()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedTypeSpecWithFullName)).ShouldContain("Could not find type 'InlineIL.Tests.InjectedAssembly.InjectedType[]'");
    }

    [Fact]
    public void should_report_injected_type_spec_2()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedTypeSpec2)).ShouldContain("The provided type does not represent an element type");
    }

    [Fact]
    public void should_report_injected_type_spec_2_with_full_name()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedTypeSpec2WithFullName)).ShouldContain("Could not find type 'InlineIL.Tests.InjectedAssembly.InjectedType&'");
    }

    [Fact]
    public void should_report_injected_fn_ptr()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedFnPtr)).ShouldContain("Function pointer types cannot be used in this context");
    }

    [Fact]
    public void should_report_injected_generic_param()
    {
        ShouldHaveError(nameof(TypeRefTestCases.InvalidInjectedGenericParam)).ShouldContain("Generic parameters cannot be used in this context");
    }
}

#if NET
public class TypeRefTestsCore : TypeRefTestsBase
{
    [Fact]
    public void should_handle_nested_forwarded_types_using_runtime_syntax()
    {
        var result = (Type)GetInstance().ReturnNestedForwardedTypeUsingRuntimeSyntax();
        result.ShouldEqual(typeof(Span<>.Enumerator));
    }

    [Fact]
    public void should_handle_nested_forwarded_types_using_ecma_syntax()
    {
        var result = (Type)GetInstance().ReturnNestedForwardedTypeUsingEcmaSyntax();
        result.ShouldEqual(typeof(Span<>.Enumerator));
    }

    [Fact]
    public void should_add_reference_to_forwarding_assembly()
    {
        const string refName = "System.Runtime.Extensions";
        AssemblyToProcessFixture.OriginalModule.AssemblyReferences.ShouldNotContain(i => i.Name == refName);
        AssemblyToProcessFixture.ResultModule.AssemblyReferences.ShouldContain(i => i.Name == refName);
    }

    [Fact]
    public void should_return_forwarded_type()
    {
        var result = (Type)GetInstance().ReturnForwardedType();
        result.ShouldEqual(typeof(Math));
    }

    [Fact]
    public void should_map_forwarded_type_to_forwarder()
    {
        var typeRef = (TypeReference)GetMethodDefinition("ReturnForwardedType").Body.Instructions.ShouldContainSingle(i => i.OpCode == OpCodes.Ldtoken).Operand;
        var assemblyName = typeRef.Scope.ShouldBe<AssemblyNameReference>();
        assemblyName.Name.ShouldEqual("System.Runtime.Extensions");
    }

#if CSHARP_13_OR_GREATER
    [Fact]
    public void should_handle_type_token_load_generic_ref_struct()
    {
        var handle = (RuntimeTypeHandle)GetInstance().ReturnTypeHandleGenericRefStruct();
        Type.GetTypeFromHandle(handle).ShouldNotBeNull().FullName.ShouldEqual($"{_verifiableAssembly}.TypeRefTestCases+RefStruct");
    }
#endif
}
#endif

[UsedImplicitly]
public class TypeRefTestsStandard : TypeRefTests
{
    public TypeRefTestsStandard()
        => NetStandard = true;
}
