# The Languages Folder
the standardized "Languages" folder (mind spelling and case) contains the **language specific *.xaml files**, such as **en.xaml** with the translated keywords that are going to be displayed to the GUI and used in (error/warning/guidance) messages when communicating with the app-user. 

This folder is required to implement the IPluginI18n interface that provides "internationalisation" to the app. This advices the app to load string-constants from language specific *.xaml files rather than having them hardcoded in the code. 

IPluginI18n is an interface within the Flow Launcher plugin development framework (specifically C#/.NET) designed to enable internationalization (i18n) and localization for plugins. 

By implementing this interface, plugin developers can allow their plugins to support multiple languages, ensuring that the text displayed in Flow Launcher (such as plugin names, descriptions, or search results) matches the user's system language. 

