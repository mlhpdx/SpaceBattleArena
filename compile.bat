@echo off
echo CLEAN
del /Q bin\SBAServer.zip
del /Q bin\SpaceBattle.jar
rmdir /S /Q doc\client\java_doc

echo BUILDING SERVER
mkdir bin
cd SBA_Serv
python compile.py py2exe
cd ..

echo BUILDING CLIENT
cd java_client_src

rmdir /S /Q bin
mkdir bin
javac -cp ..\gson-2.2.jar -d bin src\ihs\apcs\spacebattle\*.java src\ihs\apcs\spacebattle\commands\*.java src\ihs\apcs\spacebattle\games\*.java src\ihs\apcs\spacebattle\networking\*.java src\ihs\apcs\spacebattle\util\*.java
cd bin
jar cf ..\..\bin\SpaceBattle.jar ihs
cd..

echo GENERATE DOCS
javadoc -public -sourcepath src -classpath "*;bin" -d ..\doc\client\java_doc -windowtitle "IHS AP CS Space Battle" -doctitle "IHS AP CS Space Battle" ihs.apcs.spacebattle ihs.apcs.spacebattle.commands ihs.apcs.spacebattle.games

cd..

echo BUILDING DOTNET CLIENT PACKAGE
cd cs_client_src

rmdir /S /Q bin
mkdir bin
dotnet build -c Release -nowarn:0649
dotnet pack -c Release
cd..
