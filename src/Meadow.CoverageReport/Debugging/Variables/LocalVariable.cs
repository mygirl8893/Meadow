﻿using Meadow.CoverageReport.AstTypes;
using Meadow.CoverageReport.AstTypes.Enums;
using Meadow.CoverageReport.Debugging.Variables.Enums;
using Meadow.CoverageReport.Debugging.Variables.UnderlyingTypes;
using SolcNet.DataDescription.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meadow.CoverageReport.Debugging.Variables
{
    /// <summary>
    /// Represents a local variable derived from certain execution state components from execution traces/runtime.
    /// A local variable can be understood as a variable declared within a function of is a parameter given as input
    /// or one returned as output.
    /// </summary>
    public class LocalVariable : BaseVariable
    {
        #region Properties
        /// <summary>
        /// Indicates if this local variable is an input/output parameter to a function.
        /// </summary>
        public bool IsFunctionParameter { get; }
        /// <summary>
        /// Indicates the position on the stack to use as the entry point to resolving our underlying variable value.
        /// </summary>
        public int StackIndex { get; }
        /// <summary>
        /// The source map entry at the point when the local variable was resolved.
        /// </summary>
        public SourceMapEntry SourceMapEntry { get; }
        /// <summary>
        /// Represents the variable's underlying data location in less trivial cases.
        /// In the case of a local variable, the default value refers to memory if this is a function parameter, or storage otherwise.
        /// </summary>
        public override VarLocation VariableLocation
        {
            get
            {
                // Obtain our base value
                AstVariableStorageLocation storageLoc = Declaration.StorageLocation;

                // If our location is stated as the default location, we return our specific values.
                switch (storageLoc)
                {
                    case AstVariableStorageLocation.Memory:
                        return VarLocation.Memory;
                    case AstVariableStorageLocation.Storage:
                        return VarLocation.Storage;
                    case AstVariableStorageLocation.Default:
                    default:
                        return IsFunctionParameter ? VarLocation.Memory : VarLocation.Storage;
                }
            }
        }
        #endregion

        #region Constructor
        public LocalVariable(AstVariableDeclaration declaration, bool isFunctionParameter, int stackIndex, SourceMapEntry sourceMapEntry)
        {
            // Set our extended properties
            StackIndex = stackIndex;
            SourceMapEntry = sourceMapEntry;
            IsFunctionParameter = isFunctionParameter;

            // Initialize by name and type.
            Initialize(declaration);
        }
        #endregion

        #region Functions
        #endregion
    }
}
