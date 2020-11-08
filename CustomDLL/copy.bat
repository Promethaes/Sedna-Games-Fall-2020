@echo off
for /f "usebackq tokens=*" %%f in (`dir /s /b /a-d "VSProjects" ^| findstr /i /l /e ".dll"`) do xcopy "%%f" %~dp0..\Assets\Plugins\