# Kentico Auditor

Auditor module for Kentico CMS tracks actions of users in administration interface and enables super users to verify their activity.

For more information see (Auditor website)[https://ondrabus.github.io/kentico-auditor]

# Installation
If you want to use the provider in your Kentico EMS solution:
* install the provider using [NuGet package](https://github.com/ondrabus/kentico-auditor/blob/master/kentico-auditor.12.0.0.nupkg)
* open your website and configure actions you want to log in `Settings -> System -> Auditor`
* restart your website to let the module to refresh changed settings

# Development installation
If you wish to participate on the implementation, follow these steps:
* install fresh version of Kentico 12
* locate the `.sln` file of your installation and execute following commands to initialize new repository and get all source code of Kentico Auditor:
	* `git init`
	* `git remote add origin https://github.com/ondrabus/kentico-auditor`
	* `git pull origin master`
	* this should merge your Kentico instance with Auditor repository
* open your website and import package `development-import-package.zip`

That's it, you're free to code. `.gitignore` will make sure only relevant files are tracked.

# Contributing
Want to improve Kentico Auditor? Great! Read the [contributing guidelines](https://github.com/ondrabus/kentico-auditor/blob/master/CONTRIBUTING.md).

If anything feels wrong or incomplete, please let us know. Create a new [issue](https://github.com/ondrabus/kentico-auditor/issues/new) or submit a [pull request](https://help.github.com/articles/using-pull-requests/).

# Generate NuGet package
In order to generate NuGet package go into Modules application, open Auditor module and click on `Create installation package`.

![Analytics](https://kentico-ga-beacon.azurewebsites.net/api/UA-69014260-4/ondrabus/kentico-auditor?pixel)
