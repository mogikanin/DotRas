@ECHO OFF
ECHO DotRas (http://dotras.codeplex.com) Release Builder
ECHO Copyright (c) Jeff Winn. All rights reserved.
ECHO.

IF /I "%1"=="/?" GOTO ShowSyntax
IF /I "%1"=="/help" GOTO ShowSyntax
IF /I "%1"=="-?" GOTO ShowSyntax

REM *****************************************************************
REM * CONFIGURATION
REM *****************************************************************

SET SKIPTEST=false
SET SKIPDOCS=false
SET SKIPPACK=false

REM *****************************************************************
REM DO NOT MODIFY THE CONTENTS OF THIS FILE BELOW THIS POINT!!
REM *****************************************************************

SET KEYFILE=.\Source\DotRas\Public.snk
SET CERTFILE=
SET RELEASEKEYFILE=G:\PrivateKeys\Winnster.snk
SET RELEASECERTFILE=
SET FXFOLDER=%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319

REM *****************************************************************
REM * INITIALIZE
REM *****************************************************************

IF "%DEVENVDIR%"=="" GOTO NeedSDKPrompt

:Initialize

IF /I "%1"=="/testonly" (
	SET SKIPTEST=false
	SET SKIPDOCS=true
	SET SKIPPACK=true
)

IF /I "%1"=="/skiptest" (SET SKIPTEST=true)
IF /I "%1"=="/skiptests" (SET SKIPTEST=true)
IF /I "%1"=="/skipinteg" (SET SKIPINTEG=true)
IF /I "%1"=="/skipdocs" (SET SKIPDOCS=true)
IF /I "%1"=="/skipdoc" (SET SKIPDOCS=true)
IF /I "%1"=="/skippack" (SET SKIPPACK=true)
IF /I "%1"=="" GOTO EndInit
SHIFT

GOTO Initialize

:EndInit

ECHO ATTENTION! For your safety, please understand this build project does use 
ECHO custom MSBuild tasks during the build process.
ECHO.
ECHO 1. NON-OFFICIAL release build.
ECHO 2. OFFICIAL release build. This option requires a specific strong key file.
ECHO 3. ABORT!
ECHO.
CHOICE /C:123 /N /D:3 /T:30 /M "Please enter a build option (1/2/3):"

IF "%ERRORLEVEL%" == "2" (
	SET KEYFILE=%RELEASEKEYFILE%
	SET CERTFILE=%RELEASECERTFILE%
)

IF "%ERRORLEVEL%" == "3" (
	GOTO AbortRequested
)



:PerformBuild
IF NOT "%KEYFILE%"=="" (IF NOT EXIST "%KEYFILE%" GOTO MissingSNK)
IF NOT "%CERTFILE%"=="" (IF NOT EXIST "%CERTFILE%" GOTO MissingCert)
	

"%FXFOLDER%\MSBuild.exe" .\build.proj /p:CertificateKeyFile="%CERTFILE%" /p:StrongNameKeyFile="%KEYFILE%" /p:SkipIntegrationTests=%SKIPINTEG% /p:SkipTests=%SKIPTEST% /p:SkipDocumentation=%SKIPDOCS% /p:SkipPackage=%SKIPPACK% /fl /v:diag

GOTO ExitBatch




:ShowSyntax

ECHO Syntax: make.bat [options]
ECHO.
ECHO Options:
ECHO      /skiptest	- Forces the assembly testing process to be skipped.
ECHO      /skipinteg = Forces the integration testing process to be skipped.
ECHO      /skipdocs	- Forces the documentation generation to be skipped.
ECHO      /skippack	- Forces the packaging process to be skipped.
ECHO.
GOTO ExitBatch




:AbortRequested

ECHO.
ECHO Aborting the build process as requested.
ECHO.

GOTO ExitBatch




:NeedSDKPrompt

ECHO.
ECHO *******************************************
ECHO * ABORTING! VISUAL STUDIO SDK PROMPT
ECHO *******************************************
ECHO.
ECHO Aborting the build process. This batch file must be executed within a 
ECHO Visual Studio 2008 command prompt.
ECHO.

GOTO ExitBatch




:MissingCert

ECHO.
ECHO *******************************************
ECHO * ABORTING! MISSING CERTIFICATE FILE
ECHO *******************************************
ECHO.
ECHO Aborting the build process. The certificate file could not be found. Please
ECHO verify the USB key is inserted in the build machine or choose the non-official 
ECHO release build.
ECHO.
ECHO CERTFILE: '%CERTFILE%'
ECHO.

GOTO ExitBatch




:MissingSNK

ECHO.
ECHO *******************************************
ECHO * ABORTING! MISSING SNK FILE
ECHO *******************************************
ECHO.
ECHO Aborting the build process. The private key could not be found. Please verify
ECHO the USB key is inserted in the build machine or choose the non-official 
ECHO release build.
ECHO.
ECHO KEYFILE: '%KEYFILE%'
ECHO.

GOTO ExitBatch




:ExitBatch