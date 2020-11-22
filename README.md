# DemoExercise
.Net Core 3.1 MVC app

## Requirements
Visual Studio 2017/2019 <br/>
.Net Core 3.1 <br/>
Sql Server Express 2016 LocalDB <br/>

## Setup
Unless you clone the repo to C:\git folder, you will have to update the DemoExample connection string in DemoExercise.Spa\appsettings.json to match your file structure.

You will also have to mount the mdf file to MSSqlLocalDb. Easiest way to do that is through Server Explorer in VS. 
* Right Click "Data Connections"
* Click "Add Connection"
* Click Browse button and navigate to `<GitFolderLocation>\DemoExercise\DemoExercise\DemoExample.mdf`
