msbuild -t:restore TwitterTools.sln
msbuild /p:Configuration=Release 
rd /s/q c:\toolssw\Twitter
mkdir c:\toolssw\Twitter
copy Alert\bin\Release\* c:\toolssw\Twitter
copy Post\bin\Release\* c:\toolssw\Twitter
copy /y C:\Dev\TempProject\credentials\Twitter.config c:\toolssw\Twitter\Alert.exe.config
copy /y C:\Dev\TempProject\credentials\Twitter.config c:\toolssw\Twitter\Post.exe.config
