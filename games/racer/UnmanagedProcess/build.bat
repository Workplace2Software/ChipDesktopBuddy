cl /EHsc /std:c++20 /O2 /LD ^
dllmain.cpp ^
winmm.lib ^
/o UnmanagedProcess.dll

copy UnmanagedProcess.dll ..\
pause
