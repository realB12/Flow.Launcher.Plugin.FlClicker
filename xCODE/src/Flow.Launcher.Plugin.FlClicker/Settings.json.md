# Settings.jsons
* [Settings.cs](Settings.cs.md)

Other than the [Settings.cs](Settings.cs.md) (that is just a FlowLauncher standard Helperfile that must remain "empty" and unchanged and must never contain the Token or ListID), this Settings.JSON file is ment to contain **the ClickUp provided (default) values that allows the Plugin to connect and interact with the ClickUp API.** 

<span style="color:red; font-weight:bold">Attention</span>: Insert actual access-Token values only for internal testing (to prevent you to insert the Token manually every time you compile/test a new version, but make sure, you have it deleted/modified befor a new [a new release is published](../../../../../DEV_MAN/07%20DEPLOY/_FlClicker_Deployment%20Guide.md)


<span style="color:red; font-weight:bold">Attention</span> **Do not put comments into the JSON file** as the JSON format is not ment to include a comments. Using "_comments" keywords might do the trick, but will not be removed automatically when compiled for production and therefore might negatively impact the overall performance at startup resp. initial loadtime of the plugin.  

## Getting the ClickUpToken for ClickUp API access

> **pk_2694106_9BXEGR4DICA1CNOS527NPD9RJQLY6I8I**

### Getting the ListID for the "Inbox" named Tasklist
To get the List ID in ClickUp, open the specific list (in our case "Inbox) in your browser and copy the long numerical string located at the end of the URL (typically found after */l/*, */li/*, or a list name. 

The List ID is the unique sequence of numbers following the last forward slash in the web address such as:

> **6-901522152984-1**

## Current Values
{
  "ClickUpToken": "pk_2694106_9BXEGR4DICA1CNOS527NPD9RJQLY6I8I",
  "ListId": "901522152984"
}