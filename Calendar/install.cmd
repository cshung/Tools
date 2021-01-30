dotnet publish -c release -r win-x64 
rd /s/q c:\toolssw\Calendar
mkdir c:\toolssw\Calendar
copy C:\dev\Tools\Calendar\Calendar.UserInterface\bin\Release\netcoreapp3.1\win-x64\publish\* c:\toolssw\Calendar
copy /y C:\Dev\TempProject\credentials\schedule.json c:\toolssw\Calendar

