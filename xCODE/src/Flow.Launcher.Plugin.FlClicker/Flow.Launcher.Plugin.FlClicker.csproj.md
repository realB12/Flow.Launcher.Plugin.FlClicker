# The Project's Configuration File

Besides VSC specific configuration options for how to deal with this C# sourcecode it contains the following important sections as well: 

## Language Resoure Files
To make sure that resource files are not automatically (standard) compiled into the binary output (*.exe or *.dll) you have to plicitely exclude such files or folders by specifying/marking them as follows. 

### Exluding the Languages folder 
For the implementation of i18n Internalization concept one has to define a "Languages"-folder (mind the spellign, case is relevent) that will include the various en.xaml language package files and which, herin must be mentioned as follows


  <ItemGroup>
    <Content Include="Languages\*.*">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup> 

### Exluding the Images folder 
Foldes with images must be excluced as follows

----

## Sample Source Code

<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net7.0-windows</TargetFramework>
    <AssemblyName>Flow.Launcher.Plugin.FlClicker</AssemblyName>
    <PackageId>Flow.Launcher.Plugin.FlClicker</PackageId>
    <Authors>rene.baron@baronsolutions.ch</Authors>
    <PackageProjectUrl>https://github.com/rene.baron@baronsolutions.ch/Flow.Launcher.Plugin.FlClicker</PackageProjectUrl>
    <RepositoryUrl>https://github.com/rene.baron@baronsolutions.ch/Flow.Launcher.Plugin.FlClicker</RepositoryUrl>
    <PackageTags>flow-launcher flow-plugin</PackageTags>
    <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <DebugSymbols>false</DebugSymbols>
    <DebugType>None</DebugType>
  </PropertyGroup>


  <ItemGroup>
    <PackageReference Include="Flow.Launcher.Plugin" Version="4.4.0" />
  </ItemGroup>
  
  <ItemGroup>
    <Content Include="plugin.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <ItemGroup>
    <Content Include="Languages\*.*">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup> 

</Project>
