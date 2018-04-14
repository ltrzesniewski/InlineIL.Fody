namespace InlineIL
{
    /// <summary>
    /// Injects IL code to the calling method, using InlineIL.Fody.
    /// All method calls are replaced at weaving time.
    /// </summary>
    public static class ILEmit
    {

        /// <summary>
        /// <c>nop</c> - Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.
        /// </summary>
        public static void Nop()
            => IL.Throw();

        /// <summary>
        /// <c>break</c> - Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.
        /// </summary>
        public static void Break()
            => IL.Throw();

        /// <summary>
        /// <c>ldarg.0</c> - Loads the argument at index 0 onto the evaluation stack.
        /// </summary>
        public static void Ldarg_0()
            => IL.Throw();

        /// <summary>
        /// <c>ldarg.1</c> - Loads the argument at index 1 onto the evaluation stack.
        /// </summary>
        public static void Ldarg_1()
            => IL.Throw();

        /// <summary>
        /// <c>ldarg.2</c> - Loads the argument at index 2 onto the evaluation stack.
        /// </summary>
        public static void Ldarg_2()
            => IL.Throw();

        /// <summary>
        /// <c>ldarg.3</c> - Loads the argument at index 3 onto the evaluation stack.
        /// </summary>
        public static void Ldarg_3()
            => IL.Throw();

        /// <summary>
        /// <c>ldloc.0</c> - Loads the local variable at index 0 onto the evaluation stack.
        /// </summary>
        public static void Ldloc_0()
            => IL.Throw();

        /// <summary>
        /// <c>ldloc.1</c> - Loads the local variable at index 1 onto the evaluation stack.
        /// </summary>
        public static void Ldloc_1()
            => IL.Throw();

        /// <summary>
        /// <c>ldloc.2</c> - Loads the local variable at index 2 onto the evaluation stack.
        /// </summary>
        public static void Ldloc_2()
            => IL.Throw();

        /// <summary>
        /// <c>ldloc.3</c> - Loads the local variable at index 3 onto the evaluation stack.
        /// </summary>
        public static void Ldloc_3()
            => IL.Throw();

        /// <summary>
        /// <c>stloc.0</c> - Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 0.
        /// </summary>
        public static void Stloc_0()
            => IL.Throw();

        /// <summary>
        /// <c>stloc.1</c> - Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 1.
        /// </summary>
        public static void Stloc_1()
            => IL.Throw();

        /// <summary>
        /// <c>stloc.2</c> - Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 2.
        /// </summary>
        public static void Stloc_2()
            => IL.Throw();

        /// <summary>
        /// <c>stloc.3</c> - Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 3.
        /// </summary>
        public static void Stloc_3()
            => IL.Throw();

        /// <summary>
        /// <c>ldarg.s</c> - Loads the argument (referenced by a specified short form index) onto the evaluation stack.
        /// </summary>
        public static void Ldarg_S()
            => IL.Throw();

        /// <summary>
        /// <c>ldarga.s</c> - Load an argument address, in short form, onto the evaluation stack.
        /// </summary>
        public static void Ldarga_S()
            => IL.Throw();

        /// <summary>
        /// <c>starg.s</c> - Stores the value on top of the evaluation stack in the argument slot at a specified index, short form.
        /// </summary>
        public static void Starg_S()
            => IL.Throw();

        /// <summary>
        /// <c>ldloc.s</c> - Loads the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        public static void Ldloc_S()
            => IL.Throw();

        /// <summary>
        /// <c>ldloca.s</c> - Loads the address of the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        public static void Ldloca_S()
            => IL.Throw();

        /// <summary>
        /// <c>stloc.s</c> - Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index (short form).
        /// </summary>
        public static void Stloc_S()
            => IL.Throw();

        /// <summary>
        /// <c>ldnull</c> - Pushes a null reference (type O) onto the evaluation stack.
        /// </summary>
        public static void Ldnull()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.m1</c> - Pushes the integer value of -1 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_M1()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.0</c> - Pushes the integer value of 0 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_0()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.1</c> - Pushes the integer value of 1 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_1()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.2</c> - Pushes the integer value of 2 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_2()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.3</c> - Pushes the integer value of 3 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_3()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.4</c> - Pushes the integer value of 4 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_4()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.5</c> - Pushes the integer value of 5 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_5()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.6</c> - Pushes the integer value of 6 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_6()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.7</c> - Pushes the integer value of 7 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_7()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.8</c> - Pushes the integer value of 8 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4_8()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4.s</c> - Pushes the supplied int8 value onto the evaluation stack as an int32, short form.
        /// </summary>
        public static void Ldc_I4_S()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i4</c> - Pushes a supplied value of type int32 onto the evaluation stack as an int32.
        /// </summary>
        public static void Ldc_I4()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.i8</c> - Pushes a supplied value of type int64 onto the evaluation stack as an int64.
        /// </summary>
        public static void Ldc_I8()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.r4</c> - Pushes a supplied value of type float32 onto the evaluation stack as type F (float).
        /// </summary>
        public static void Ldc_R4()
            => IL.Throw();

        /// <summary>
        /// <c>ldc.r8</c> - Pushes a supplied value of type float64 onto the evaluation stack as type F (float).
        /// </summary>
        public static void Ldc_R8()
            => IL.Throw();

        /// <summary>
        /// <c>dup</c> - Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.
        /// </summary>
        public static void Dup()
            => IL.Throw();

        /// <summary>
        /// <c>pop</c> - Removes the value currently on top of the evaluation stack.
        /// </summary>
        public static void Pop()
            => IL.Throw();

        /// <summary>
        /// <c>jmp</c> - Exits current method and jumps to specified method.
        /// </summary>
        public static void Jmp()
            => IL.Throw();

        /// <summary>
        /// <c>call</c> - Calls the method indicated by the passed method descriptor.
        /// </summary>
        public static void Call()
            => IL.Throw();

        /// <summary>
        /// <c>calli</c> - Calls the method indicated on the evaluation stack (as a pointer to an entry point) with arguments described by a calling convention.
        /// </summary>
        public static void Calli()
            => IL.Throw();

        /// <summary>
        /// <c>ret</c> - Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.
        /// </summary>
        public static void Ret()
            => IL.Throw();

        /// <summary>
        /// <c>br.s</c> - Unconditionally transfers control to a target instruction (short form).
        /// </summary>
        public static void Br_S()
            => IL.Throw();

        /// <summary>
        /// <c>brfalse.s</c> - Transfers control to a target instruction if value is false, a null reference, or zero.
        /// </summary>
        public static void Brfalse_S()
            => IL.Throw();

        /// <summary>
        /// <c>brtrue.s</c> - Transfers control to a target instruction (short form) if value is true, not null, or non-zero.
        /// </summary>
        public static void Brtrue_S()
            => IL.Throw();

        /// <summary>
        /// <c>beq.s</c> - Transfers control to a target instruction (short form) if two values are equal.
        /// </summary>
        public static void Beq_S()
            => IL.Throw();

        /// <summary>
        /// <c>bge.s</c> - Transfers control to a target instruction (short form) if the first value is greater than or equal to the second value.
        /// </summary>
        public static void Bge_S()
            => IL.Throw();

        /// <summary>
        /// <c>bgt.s</c> - Transfers control to a target instruction (short form) if the first value is greater than the second value.
        /// </summary>
        public static void Bgt_S()
            => IL.Throw();

        /// <summary>
        /// <c>ble.s</c> - Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.
        /// </summary>
        public static void Ble_S()
            => IL.Throw();

        /// <summary>
        /// <c>blt.s</c> - Transfers control to a target instruction (short form) if the first value is less than the second value.
        /// </summary>
        public static void Blt_S()
            => IL.Throw();

        /// <summary>
        /// <c>bne.un.s</c> - Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        public static void Bne_Un_S()
            => IL.Throw();

        /// <summary>
        /// <c>bge.un.s</c> - Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Bge_Un_S()
            => IL.Throw();

        /// <summary>
        /// <c>bgt.un.s</c> - Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Bgt_Un_S()
            => IL.Throw();

        /// <summary>
        /// <c>ble.un.s</c> - Transfers control to a target instruction (short form) if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Ble_Un_S()
            => IL.Throw();

        /// <summary>
        /// <c>blt.un.s</c> - Transfers control to a target instruction (short form) if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Blt_Un_S()
            => IL.Throw();

        /// <summary>
        /// <c>br</c> - Unconditionally transfers control to a target instruction.
        /// </summary>
        public static void Br()
            => IL.Throw();

        /// <summary>
        /// <c>brfalse</c> - Transfers control to a target instruction if value is false, a null reference (Nothing in Visual Basic), or zero.
        /// </summary>
        public static void Brfalse()
            => IL.Throw();

        /// <summary>
        /// <c>brtrue</c> - Transfers control to a target instruction if value is true, not null, or non-zero.
        /// </summary>
        public static void Brtrue()
            => IL.Throw();

        /// <summary>
        /// <c>beq</c> - Transfers control to a target instruction if two values are equal.
        /// </summary>
        public static void Beq()
            => IL.Throw();

        /// <summary>
        /// <c>bge</c> - Transfers control to a target instruction if the first value is greater than or equal to the second value.
        /// </summary>
        public static void Bge()
            => IL.Throw();

        /// <summary>
        /// <c>bgt</c> - Transfers control to a target instruction if the first value is greater than the second value.
        /// </summary>
        public static void Bgt()
            => IL.Throw();

        /// <summary>
        /// <c>ble</c> - Transfers control to a target instruction if the first value is less than or equal to the second value.
        /// </summary>
        public static void Ble()
            => IL.Throw();

        /// <summary>
        /// <c>blt</c> - Transfers control to a target instruction if the first value is less than the second value.
        /// </summary>
        public static void Blt()
            => IL.Throw();

        /// <summary>
        /// <c>bne.un</c> - Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        public static void Bne_Un()
            => IL.Throw();

        /// <summary>
        /// <c>bge.un</c> - Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Bge_Un()
            => IL.Throw();

        /// <summary>
        /// <c>bgt.un</c> - Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Bgt_Un()
            => IL.Throw();

        /// <summary>
        /// <c>ble.un</c> - Transfers control to a target instruction if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Ble_Un()
            => IL.Throw();

        /// <summary>
        /// <c>blt.un</c> - Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static void Blt_Un()
            => IL.Throw();

        /// <summary>
        /// <c>switch</c> - Implements a jump table.
        /// </summary>
        public static void Switch()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.i1</c> - Loads a value of type int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_I1()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.u1</c> - Loads a value of type unsigned int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_U1()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.i2</c> - Loads a value of type int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_I2()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.u2</c> - Loads a value of type unsigned int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_U2()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.i4</c> - Loads a value of type int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_I4()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.u4</c> - Loads a value of type unsigned int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_U4()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.i8</c> - Loads a value of type int64 as an int64 onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_I8()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.i</c> - Loads a value of type native int as a native int onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_I()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.r4</c> - Loads a value of type float32 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_R4()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.r8</c> - Loads a value of type float64 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_R8()
            => IL.Throw();

        /// <summary>
        /// <c>ldind.ref</c> - Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.
        /// </summary>
        public static void Ldind_Ref()
            => IL.Throw();

        /// <summary>
        /// <c>stind.ref</c> - Stores a object reference value at a supplied address.
        /// </summary>
        public static void Stind_Ref()
            => IL.Throw();

        /// <summary>
        /// <c>stind.i1</c> - Stores a value of type int8 at a supplied address.
        /// </summary>
        public static void Stind_I1()
            => IL.Throw();

        /// <summary>
        /// <c>stind.i2</c> - Stores a value of type int16 at a supplied address.
        /// </summary>
        public static void Stind_I2()
            => IL.Throw();

        /// <summary>
        /// <c>stind.i4</c> - Stores a value of type int32 at a supplied address.
        /// </summary>
        public static void Stind_I4()
            => IL.Throw();

        /// <summary>
        /// <c>stind.i8</c> - Stores a value of type int64 at a supplied address.
        /// </summary>
        public static void Stind_I8()
            => IL.Throw();

        /// <summary>
        /// <c>stind.r4</c> - Stores a value of type float32 at a supplied address.
        /// </summary>
        public static void Stind_R4()
            => IL.Throw();

        /// <summary>
        /// <c>stind.r8</c> - Stores a value of type float64 at a supplied address.
        /// </summary>
        public static void Stind_R8()
            => IL.Throw();

        /// <summary>
        /// <c>add</c> - Adds two values and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Add()
            => IL.Throw();

        /// <summary>
        /// <c>sub</c> - Subtracts one value from another and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Sub()
            => IL.Throw();

        /// <summary>
        /// <c>mul</c> - Multiplies two values and pushes the result on the evaluation stack.
        /// </summary>
        public static void Mul()
            => IL.Throw();

        /// <summary>
        /// <c>div</c> - Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.
        /// </summary>
        public static void Div()
            => IL.Throw();

        /// <summary>
        /// <c>div.un</c> - Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.
        /// </summary>
        public static void Div_Un()
            => IL.Throw();

        /// <summary>
        /// <c>rem</c> - Divides two values and pushes the remainder onto the evaluation stack.
        /// </summary>
        public static void Rem()
            => IL.Throw();

        /// <summary>
        /// <c>rem.un</c> - Divides two unsigned values and pushes the remainder onto the evaluation stack.
        /// </summary>
        public static void Rem_Un()
            => IL.Throw();

        /// <summary>
        /// <c>and</c> - Computes the bitwise AND of two values and pushes the result onto the evaluation stack.
        /// </summary>
        public static void And()
            => IL.Throw();

        /// <summary>
        /// <c>or</c> - Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Or()
            => IL.Throw();

        /// <summary>
        /// <c>xor</c> - Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.
        /// </summary>
        public static void Xor()
            => IL.Throw();

        /// <summary>
        /// <c>shl</c> - Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        public static void Shl()
            => IL.Throw();

        /// <summary>
        /// <c>shr</c> - Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        public static void Shr()
            => IL.Throw();

        /// <summary>
        /// <c>shr.un</c> - Shifts an unsigned integer value (in zeroes) to the right by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        public static void Shr_Un()
            => IL.Throw();

        /// <summary>
        /// <c>neg</c> - Negates a value and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Neg()
            => IL.Throw();

        /// <summary>
        /// <c>not</c> - Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.
        /// </summary>
        public static void Not()
            => IL.Throw();

        /// <summary>
        /// <c>conv.i1</c> - Converts the value on top of the evaluation stack to int8, then extends (pads) it to int32.
        /// </summary>
        public static void Conv_I1()
            => IL.Throw();

        /// <summary>
        /// <c>conv.i2</c> - Converts the value on top of the evaluation stack to int16, then extends (pads) it to int32.
        /// </summary>
        public static void Conv_I2()
            => IL.Throw();

        /// <summary>
        /// <c>conv.i4</c> - Converts the value on top of the evaluation stack to int32.
        /// </summary>
        public static void Conv_I4()
            => IL.Throw();

        /// <summary>
        /// <c>conv.i8</c> - Converts the value on top of the evaluation stack to int64.
        /// </summary>
        public static void Conv_I8()
            => IL.Throw();

        /// <summary>
        /// <c>conv.r4</c> - Converts the value on top of the evaluation stack to float32.
        /// </summary>
        public static void Conv_R4()
            => IL.Throw();

        /// <summary>
        /// <c>conv.r8</c> - Converts the value on top of the evaluation stack to float64.
        /// </summary>
        public static void Conv_R8()
            => IL.Throw();

        /// <summary>
        /// <c>conv.u4</c> - Converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.
        /// </summary>
        public static void Conv_U4()
            => IL.Throw();

        /// <summary>
        /// <c>conv.u8</c> - Converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.
        /// </summary>
        public static void Conv_U8()
            => IL.Throw();

        /// <summary>
        /// <c>callvirt</c> - Calls a late-bound method on an object, pushing the return value onto the evaluation stack.
        /// </summary>
        public static void Callvirt()
            => IL.Throw();

        /// <summary>
        /// <c>cpobj</c> - Copies the value type located at the address of an object (type &amp;, * or native int) to the address of the destination object (type &amp;, * or native int).
        /// </summary>
        public static void Cpobj()
            => IL.Throw();

        /// <summary>
        /// <c>ldobj</c> - Copies the value type object pointed to by an address to the top of the evaluation stack.
        /// </summary>
        public static void Ldobj()
            => IL.Throw();

        /// <summary>
        /// <c>ldstr</c> - Pushes a new object reference to a string literal stored in the metadata.
        /// </summary>
        public static void Ldstr()
            => IL.Throw();

        /// <summary>
        /// <c>newobj</c> - Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        public static void Newobj()
            => IL.Throw();

        /// <summary>
        /// <c>castclass</c> - Attempts to cast an object passed by reference to the specified class.
        /// </summary>
        public static void Castclass()
            => IL.Throw();

        /// <summary>
        /// <c>isinst</c> - Tests whether an object reference (type O) is an instance of a particular class.
        /// </summary>
        public static void Isinst()
            => IL.Throw();

        /// <summary>
        /// <c>conv.r.un</c> - Converts the unsigned integer value on top of the evaluation stack to float32.
        /// </summary>
        public static void Conv_R_Un()
            => IL.Throw();

        /// <summary>
        /// <c>unbox</c> - Converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        public static void Unbox()
            => IL.Throw();

        /// <summary>
        /// <c>throw</c> - Throws the exception object currently on the evaluation stack.
        /// </summary>
        public static void Throw()
            => IL.Throw();

        /// <summary>
        /// <c>ldfld</c> - Finds the value of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        public static void Ldfld()
            => IL.Throw();

        /// <summary>
        /// <c>ldflda</c> - Finds the address of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        public static void Ldflda()
            => IL.Throw();

        /// <summary>
        /// <c>stfld</c> - Replaces the value stored in the field of an object reference or pointer with a new value.
        /// </summary>
        public static void Stfld()
            => IL.Throw();

        /// <summary>
        /// <c>ldsfld</c> - Pushes the value of a static field onto the evaluation stack.
        /// </summary>
        public static void Ldsfld()
            => IL.Throw();

        /// <summary>
        /// <c>ldsflda</c> - Pushes the address of a static field onto the evaluation stack.
        /// </summary>
        public static void Ldsflda()
            => IL.Throw();

        /// <summary>
        /// <c>stsfld</c> - Replaces the value of a static field with a value from the evaluation stack.
        /// </summary>
        public static void Stsfld()
            => IL.Throw();

        /// <summary>
        /// <c>stobj</c> - Copies a value of a specified type from the evaluation stack into a supplied memory address.
        /// </summary>
        public static void Stobj()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i1.un</c> - Converts the unsigned value on top of the evaluation stack to signed int8 and extends it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I1_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i2.un</c> - Converts the unsigned value on top of the evaluation stack to signed int16 and extends it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I2_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i4.un</c> - Converts the unsigned value on top of the evaluation stack to signed int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I4_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i8.un</c> - Converts the unsigned value on top of the evaluation stack to signed int64, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I8_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u1.un</c> - Converts the unsigned value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U1_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u2.un</c> - Converts the unsigned value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U2_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u4.un</c> - Converts the unsigned value on top of the evaluation stack to unsigned int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U4_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u8.un</c> - Converts the unsigned value on top of the evaluation stack to unsigned int64, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U8_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i.un</c> - Converts the unsigned value on top of the evaluation stack to signed native int, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I_Un()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u.un</c> - Converts the unsigned value on top of the evaluation stack to unsigned native int, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U_Un()
            => IL.Throw();

        /// <summary>
        /// <c>box</c> - Converts a value type to an object reference (type O).
        /// </summary>
        public static void Box()
            => IL.Throw();

        /// <summary>
        /// <c>newarr</c> - Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
        /// </summary>
        public static void Newarr()
            => IL.Throw();

        /// <summary>
        /// <c>ldlen</c> - Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.
        /// </summary>
        public static void Ldlen()
            => IL.Throw();

        /// <summary>
        /// <c>ldelema</c> - Loads the address of the array element at a specified array index onto the top of the evaluation stack as type &amp; (managed pointer).
        /// </summary>
        public static void Ldelema()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.i1</c> - Loads the element with type int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static void Ldelem_I1()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.u1</c> - Loads the element with type unsigned int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static void Ldelem_U1()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.i2</c> - Loads the element with type int16 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static void Ldelem_I2()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.u2</c> - Loads the element with type unsigned int16 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static void Ldelem_U2()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.i4</c> - Loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static void Ldelem_I4()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.u4</c> - Loads the element with type unsigned int32 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static void Ldelem_U4()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.i8</c> - Loads the element with type int64 at a specified array index onto the top of the evaluation stack as an int64.
        /// </summary>
        public static void Ldelem_I8()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.i</c> - Loads the element with type native int at a specified array index onto the top of the evaluation stack as a native int.
        /// </summary>
        public static void Ldelem_I()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.r4</c> - Loads the element with type float32 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        public static void Ldelem_R4()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.r8</c> - Loads the element with type float64 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        public static void Ldelem_R8()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.ref</c> - Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).
        /// </summary>
        public static void Ldelem_Ref()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.i</c> - Replaces the array element at a given index with the native int value on the evaluation stack.
        /// </summary>
        public static void Stelem_I()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.i1</c> - Replaces the array element at a given index with the int8 value on the evaluation stack.
        /// </summary>
        public static void Stelem_I1()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.i2</c> - Replaces the array element at a given index with the int16 value on the evaluation stack.
        /// </summary>
        public static void Stelem_I2()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.i4</c> - Replaces the array element at a given index with the int32 value on the evaluation stack.
        /// </summary>
        public static void Stelem_I4()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.i8</c> - Replaces the array element at a given index with the int64 value on the evaluation stack.
        /// </summary>
        public static void Stelem_I8()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.r4</c> - Replaces the array element at a given index with the float32 value on the evaluation stack.
        /// </summary>
        public static void Stelem_R4()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.r8</c> - Replaces the array element at a given index with the float64 value on the evaluation stack.
        /// </summary>
        public static void Stelem_R8()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.ref</c> - Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.
        /// </summary>
        public static void Stelem_Ref()
            => IL.Throw();

        /// <summary>
        /// <c>ldelem.any</c> - Loads the element at a specified array index onto the top of the evaluation stack as the type specified in the instruction. 
        /// </summary>
        public static void Ldelem_Any()
            => IL.Throw();

        /// <summary>
        /// <c>stelem.any</c> - Replaces the array element at a given index with the value on the evaluation stack, whose type is specified in the instruction.
        /// </summary>
        public static void Stelem_Any()
            => IL.Throw();

        /// <summary>
        /// <c>unbox.any</c> - Converts the boxed representation of a type specified in the instruction to its unboxed form. 
        /// </summary>
        public static void Unbox_Any()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i1</c> - Converts the signed value on top of the evaluation stack to signed int8 and extends it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I1()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u1</c> - Converts the signed value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U1()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i2</c> - Converts the signed value on top of the evaluation stack to signed int16 and extending it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I2()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u2</c> - Converts the signed value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U2()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i4</c> - Converts the signed value on top of the evaluation stack to signed int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I4()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u4</c> - Converts the signed value on top of the evaluation stack to unsigned int32, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U4()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i8</c> - Converts the signed value on top of the evaluation stack to signed int64, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I8()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u8</c> - Converts the signed value on top of the evaluation stack to unsigned int64, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U8()
            => IL.Throw();

        /// <summary>
        /// <c>refanyval</c> - Retrieves the address (type &amp;) embedded in a typed reference.
        /// </summary>
        public static void Refanyval()
            => IL.Throw();

        /// <summary>
        /// <c>ckfinite</c> - Throws <see cref="System.ArithmeticException" /> if value is not a finite number.
        /// </summary>
        public static void Ckfinite()
            => IL.Throw();

        /// <summary>
        /// <c>mkrefany</c> - Pushes a typed reference to an instance of a specific type onto the evaluation stack.
        /// </summary>
        public static void Mkrefany()
            => IL.Throw();

        /// <summary>
        /// <c>ldtoken</c> - Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
        /// </summary>
        public static void Ldtoken()
            => IL.Throw();

        /// <summary>
        /// <c>conv.u2</c> - Converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.
        /// </summary>
        public static void Conv_U2()
            => IL.Throw();

        /// <summary>
        /// <c>conv.u1</c> - Converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.
        /// </summary>
        public static void Conv_U1()
            => IL.Throw();

        /// <summary>
        /// <c>conv.i</c> - Converts the value on top of the evaluation stack to native int.
        /// </summary>
        public static void Conv_I()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.i</c> - Converts the signed value on top of the evaluation stack to signed native int, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_I()
            => IL.Throw();

        /// <summary>
        /// <c>conv.ovf.u</c> - Converts the signed value on top of the evaluation stack to unsigned native int, throwing <see cref="System.OverflowException" /> on overflow.
        /// </summary>
        public static void Conv_Ovf_U()
            => IL.Throw();

        /// <summary>
        /// <c>add.ovf</c> - Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Add_Ovf()
            => IL.Throw();

        /// <summary>
        /// <c>add.ovf.un</c> - Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Add_Ovf_Un()
            => IL.Throw();

        /// <summary>
        /// <c>mul.ovf</c> - Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Mul_Ovf()
            => IL.Throw();

        /// <summary>
        /// <c>mul.ovf.un</c> - Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Mul_Ovf_Un()
            => IL.Throw();

        /// <summary>
        /// <c>sub.ovf</c> - Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Sub_Ovf()
            => IL.Throw();

        /// <summary>
        /// <c>sub.ovf.un</c> - Subtracts one unsigned integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static void Sub_Ovf_Un()
            => IL.Throw();

        /// <summary>
        /// <c>endfinally</c> - Transfers control from the fault or finally clause of an exception block back to the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        public static void Endfinally()
            => IL.Throw();

        /// <summary>
        /// <c>leave</c> - Exits a protected region of code, unconditionally transferring control to a specific target instruction.
        /// </summary>
        public static void Leave()
            => IL.Throw();

        /// <summary>
        /// <c>leave.s</c> - Exits a protected region of code, unconditionally transferring control to a target instruction (short form).
        /// </summary>
        public static void Leave_S()
            => IL.Throw();

        /// <summary>
        /// <c>stind.i</c> - Stores a value of type native int at a supplied address.
        /// </summary>
        public static void Stind_I()
            => IL.Throw();

        /// <summary>
        /// <c>conv.u</c> - Converts the value on top of the evaluation stack to unsigned native int, and extends it to native int.
        /// </summary>
        public static void Conv_U()
            => IL.Throw();

        /// <summary>
        /// <c>arglist</c> - Returns an unmanaged pointer to the argument list of the current method.
        /// </summary>
        public static void Arglist()
            => IL.Throw();

        /// <summary>
        /// <c>ceq</c> - Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static void Ceq()
            => IL.Throw();

        /// <summary>
        /// <c>cgt</c> - Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static void Cgt()
            => IL.Throw();

        /// <summary>
        /// <c>cgt.un</c> - Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static void Cgt_Un()
            => IL.Throw();

        /// <summary>
        /// <c>clt</c> - Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static void Clt()
            => IL.Throw();

        /// <summary>
        /// <c>clt.un</c> - Compares the unsigned or unordered values value1 and value2. If value1 is less than value2, then the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static void Clt_Un()
            => IL.Throw();

        /// <summary>
        /// <c>ldftn</c> - Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.
        /// </summary>
        public static void Ldftn()
            => IL.Throw();

        /// <summary>
        /// <c>ldvirtftn</c> - Pushes an unmanaged pointer (type native int) to the native code implementing a particular virtual method associated with a specified object onto the evaluation stack.
        /// </summary>
        public static void Ldvirtftn()
            => IL.Throw();

        /// <summary>
        /// <c>ldarg</c> - Loads an argument (referenced by a specified index value) onto the stack.
        /// </summary>
        public static void Ldarg()
            => IL.Throw();

        /// <summary>
        /// <c>ldarga</c> - Load an argument address onto the evaluation stack.
        /// </summary>
        public static void Ldarga()
            => IL.Throw();

        /// <summary>
        /// <c>starg</c> - Stores the value on top of the evaluation stack in the argument slot at a specified index.
        /// </summary>
        public static void Starg()
            => IL.Throw();

        /// <summary>
        /// <c>ldloc</c> - Loads the local variable at a specific index onto the evaluation stack.
        /// </summary>
        public static void Ldloc()
            => IL.Throw();

        /// <summary>
        /// <c>ldloca</c> - Loads the address of the local variable at a specific index onto the evaluation stack.
        /// </summary>
        public static void Ldloca()
            => IL.Throw();

        /// <summary>
        /// <c>stloc</c> - Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.
        /// </summary>
        public static void Stloc()
            => IL.Throw();

        /// <summary>
        /// <c>localloc</c> - Allocates a certain number of bytes from the local dynamic memory pool and pushes the address (a transient pointer, type *) of the first allocated byte onto the evaluation stack.
        /// </summary>
        public static void Localloc()
            => IL.Throw();

        /// <summary>
        /// <c>endfilter</c> - Transfers control from the filter clause of an exception back to the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        public static void Endfilter()
            => IL.Throw();

        /// <summary>
        /// <c>unaligned.</c> - Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, initblk, or cpblk instruction.
        /// </summary>
        public static void Unaligned()
            => IL.Throw();

        /// <summary>
        /// <c>volatile.</c> - Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.
        /// </summary>
        public static void Volatile()
            => IL.Throw();

        /// <summary>
        /// <c>tail.</c> - Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.
        /// </summary>
        public static void Tail()
            => IL.Throw();

        /// <summary>
        /// <c>initobj</c> - Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
        /// </summary>
        public static void Initobj()
            => IL.Throw();

        /// <summary>
        /// <c>constrained.</c> - Constrains the type on which a virtual method call is made.
        /// </summary>
        public static void Constrained()
            => IL.Throw();

        /// <summary>
        /// <c>cpblk</c> - Copies a specified number bytes from a source address to a destination address.
        /// </summary>
        public static void Cpblk()
            => IL.Throw();

        /// <summary>
        /// <c>initblk</c> - Initializes a specified block of memory at a specific address to a given size and initial value.
        /// </summary>
        public static void Initblk()
            => IL.Throw();

        /// <summary>
        /// <c>no.</c>
        /// </summary>
        public static void No()
            => IL.Throw();

        /// <summary>
        /// <c>rethrow</c> - Rethrows the current exception.
        /// </summary>
        public static void Rethrow()
            => IL.Throw();

        /// <summary>
        /// <c>sizeof</c> - Pushes the size, in bytes, of a supplied value type onto the evaluation stack.
        /// </summary>
        public static void Sizeof()
            => IL.Throw();

        /// <summary>
        /// <c>refanytype</c> - Retrieves the type token embedded in a typed reference.
        /// </summary>
        public static void Refanytype()
            => IL.Throw();

        /// <summary>
        /// <c>readonly.</c> - Specifies that the subsequent array address operation performs no type check at run time, and that it returns a managed pointer whose mutability is restricted.
        /// </summary>
        public static void Readonly()
            => IL.Throw();
    }
}
