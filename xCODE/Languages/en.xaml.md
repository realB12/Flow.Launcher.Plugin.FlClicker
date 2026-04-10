# en.xmal

This files  contains the translated ENGLISH keywords that are going to be presented in the GUI and used in (error/warning/guidance) messages when communicating with the user. This file must reside in the Languages folder beneath the project's root folder. 

## Example Code

<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:system="clr-namespace:System;assembly=mscorlib">

    <system:String x:Key="plugin_helloworldcsharp_greet_title">Greetings</system:String>
    <system:String x:Key="plugin_helloworldcsharp_greet_subtitle">Hi and welcome to C Sharp Hello World project</system:String>
    <system:String x:Key="plugin_helloworldcsharp_plugin_name">Hello World CSharp</system:String>
    <system:String x:Key="plugin_helloworldcsharp_plugin_description">Hello World CSharp</system:String>
</ResourceDictionary>