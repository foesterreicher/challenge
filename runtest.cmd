@echo off

dotnet test "E:\prj\cgmchallenge\tests\CGMCodingChallengeTests.dll"

if [%1] neq [/nopause] pause