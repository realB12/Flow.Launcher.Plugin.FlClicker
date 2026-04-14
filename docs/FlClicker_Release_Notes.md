# FlClicker Release Notes

## Scope of Document
Before we will publish an official release, we will share here some notes about its purpose, about what is new and has changed, where it evenutally breaks with what we already had, why and for which date next release are scheduled. 

The version numbers below are identical with the GitHub Release Tags you want to use when installing a specific (older) version. Version-Namings follow [SemVer best practices](#semver-releasenumbers). 

---

**Current, still UNSTABLE Test-Relase is [v0.0.6](#v006-initial-testversion)**. 

---


## v0.0.6: Initial round-trip framework testing: do not use!

## v0.0.5: Initial round-trip framework testing: do not use!
<span style="color:red; font-weight:bold">Attention</span>: This release is **for internal end2end round-trip framework testing only**. 

**NEVER install this version** as it contains draft,  preliminary code that is neither complete nor documented and will undergo major changes in the future. 

This relase is **only for TECHNICAL end2end integration testing**. Goal is to check wether the Plugin-Project's Framework automation works, is accepted by the FlowLauncher-authorities and therefore "goes through", before we are adding a bulk of functionality.  

This **functionality is neither complete**, nor can users modify their own settings. The current version runs again a dedicated ClickUp Test-Account!

### Features
The only features we have publicly opened for testing is the display of the FlClicker's internal version number. Currently, during preliminary framework testing phase, "click version" displays the CreationTime and Date of the currently compiled code base. This will change to [SemVer-Versioning](#semver-releasenumbers) with the first productive release. 

## Appendix
### SemVer ReleaseNumbers 
Our version numbers for software realeases follow the industry standard for **Semantic Versioning (SemVer) 2.0.0**, that is structured as MAJOR.MINOR.PATCH (e.g., 1.5.2). 

* **MAJOR**: Incompatible API changes.
* **MINOR**: Functionality added in a backward-compatible manner.
* **PATCH**: The industry standard for : Backward-compatible bug fixes