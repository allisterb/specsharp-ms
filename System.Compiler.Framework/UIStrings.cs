﻿using System;
using System.Resources;
using System.Globalization;

namespace System.Compiler { 


  public enum UIStringNames {
    DebugCaption, // Debugging
    BuildCaption, // Build
    GeneralCaption, // General
    Application, // Application
    Project , // Project 
    WrapperAssembly, // Wrapper Assembly for ActiveX/COM Objects
    CodeGeneration, // Code Generation
    ErrorsAndWarnings, // Errors and Warnings
    Outputs, // Outputs
    StartAction, // Start Action
    StartOptions, // Start Options
    Advanced, // Advanced
    Misc, // Misc
    AssemblyName, // Assembly Name
    AssemblyNameDescription, // The name of the output file that will hold assembly metadata.
    OutputType, // Output Type
    OutputTypeDescription, // The type of application to build.
    DefaultNamespace, // Default Namespace
    DefaultNamespaceDescription, // Specifies the default namespace for added items, such as classes, that are added via the Add New Item Dialog Box.
    StartupObject, // Startup Object
    StartupObjectDescription, // The name of the class that contains the static Main method that you want called when you launch your application.
    ApplicationIcon, // Application Icon
    ApplicationIconDescription, // Sets the .ico file to use as your application icon.
    ProjectFile, // Project File
    ProjectFileDescription, // The name of the file containing build, configuration, and other information about the project.
    ProjectFolder, // Project Folder
    ProjectFolderDescription, // The absolute location of the project.
    OutputFile, // Output File
    OutputFileDescription, // The name of the project's primary output file.
    WrapperAssemblyKeyFile, // Wrapper Assembly Key File
    WrapperAssemblyKeyFileDescription, // Specifies the key file to use for ActiveX/COM object wrappers.
    WrapperAssemblyKeyName, // Wrapper Assembly Key Name
    WrapperAssemblyKeyNameDescription, // Specifies the Wrapper Assembly Key Name for ActiveX/COM objects.
    DefineConstants, // Define Constants
    DefineConstantsDescription, // Specify symbols on which to perform conditional compilation.
    OptimizeCode, // Optimize Code
    OptimizeCodeDescription, // Enables or disables optimizations performed by the compiler to make your output file smaller, faster and more efficient.
    CheckArithmeticOverflow, // Check Arithmetic Overflow
    CheckArithmeticOverflowDescription, // Check all integer arithmetic for out of bounds results.
    AllowUnsafeCode, // Allow Unsafe Code
    AllowUnsafeCodeDescription, // Enable use of the unsafe keyword.
    WarningLevel, // Warning Level
    WarningLevelDescription, // Specify the level of detail for compiler warnings.
    TreatWarningsAsErrors, // Treat Warnings As Errors
    TreatWarningsAsErrorsDescription, // Indicates whether to treat all compiler warnings as errors.
    OutputPath, // Output Path
    OutputPathDescription, // Specifies the location of the output files for this project's configuration.
    XMLDocumentationFile, // XML Documentation File
    XMLDocumentationFileDescription, // Specifies the name of the file that is to contain the XML documentation generated by the compiler.
    GenerateDebuggingInformation, // Generate Debugging Information
    GenerateDebuggingInformationDescription, // Specifies whether to generate debugging information.
    RegisterForCOMInterop, // Register For COM Interop
    RegisterForCOMInteropDescription, // Register the project's output file for use with COM components.  The output file must contain assembly metadata with a strong name.
    DebugMode, // Debug Mode
    DebugModeDescription, // Determins whether an appliation, a URL, or the project output will be launched when you start debugging.
    StartApplication, // Start Application
    StartApplicationDescription, // Indicates the program that should be started when the project is debugged. 
    StartURL, // Start URL
    StartURLDescription, // Indicates the URL that should be launched when the application is debugged.
    StartPage, // Start Page
    StartPageDescription, // Specifies the page that will launch within the browser when you begin debugging a web project or URL.  
    CommandLineArguments, // Command Line Arguments
    CommandLineArgumentsDescription, // Indicates command line arguments to be passed to the program on startup.
    WorkingDirectory, // Working Directory
    WorkingDirectoryDescription, // Indicates the working directory from which the project or custom application is launched.
    UseInternetExplorer, // Use Internet Explorer
    UseInternetExplorerDescription, // Indicates that Internet Explorer should be used to start the project during debugging.
    EnableRemoteDebugging, // Enable Remote Debugging
    EnableRemoteDebuggingDescription, // Enable debugging of the project from a remote machine.
    RemoteDebugMachine, // Remote Debug Machine
    RemoteDebugMachineDescription, // Name of the machine to be used for remote debugging.
    IncrementalBuild, // Incremental Build
    IncrementalBuildDescription, // Enable the incremental compiler.
    BaseAddress, // Base Address
    BaseAddressDescription, // Specify the preferred address at which to load a .dll at run time.
    BuildAction, // Build Action
    BuildActionDescription, // How the file relates to the build and deployment process
    CustomTool, // Custom Tool
    CustomToolDescription, // Specifies a tool that transforms the file at design time and generating another file that is then included in the build
    CustomToolNamespace, // Custom Tool Namespace
    CustomToolNamespaceDescription, // The namespace into which the output of the custom tool is placed
    FileName, // File Name
    FileNameDescription, // The name of the file or folder
    FullPath, // Full Path
    FullPathDescription, // Location of the file
    FileAlignment, // File Alignment
    FileAlignmentDescription, // This advanced property is used for optimizing memory access.
    v1, // Microsoft .Net Framework v1
    v11, // Microsoft .Net Framework v1.1
    v12, // Microsoft .Net Framework v1.2
    cli1, // ECMA-335 CLI compatible framework (location must be provided)
    Exe, // Console Application
    Library, // Class Library
    WinExe, // Windows Application
    Compile, // Compile
    Content, // Content
    EmbeddedResource, // Embedded Resource
    None, // None
    Program, // Program
    URL, // URL
    MaxErrorsReached, // sorry, maximal number of error messages reached
    BraceMatchStatus, // Matches: {0}
    TargetPlatform, // Target Platform
    TargetPlatformDescription, // The version of the Common Language Runtime to use for output assembly
    TargetPlatformLocation, // Target Platform Location
    TargetPlatformLocationDescription, // The location of the target platform

  }    
  

  public class UIStrings {
    static ResourceManager rm;
    public static string GetString(UIStringNames name) {
      if (rm == null) {
        rm = new ResourceManager("System.Compiler.UIStrings", typeof(UIStrings).Assembly); 
      }
      return rm.GetString(name.ToString());
    }
    public static string GetString(UIStringNames name, CultureInfo culture) {
      if (rm == null) {
        rm = new ResourceManager("System.Compiler.UIStrings", typeof(UIStrings).Assembly); 
      }
      return rm.GetString(name.ToString(), culture);
    }
    public static string GetString(string name, CultureInfo culture) {
      if (rm == null) {
        rm = new ResourceManager("System.Compiler.UIStrings", typeof(UIStrings).Assembly); 
      }
      return rm.GetString(name, culture);
    }    
  }
}