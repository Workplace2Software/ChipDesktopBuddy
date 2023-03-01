#include "dllmain.h"

#define BUFFER_SIZE 1024
char buffer[BUFFER_SIZE];

TIMECAPS t;

BOOL WINAPI DllMain(
		HINSTANCE hInstance, DWORD reason, LPVOID reserved
) {
	switch (reason) {
	case DLL_PROCESS_ATTACH:
		// make Sleep() more precise,
		// increasing framerate because gamemaker sleeps between frames.
		// for more information, look up gmsched, gms_scheduler_fix, etc.
		timeGetDevCaps(&t, sizeof(&t));
		timeBeginPeriod(t.wPeriodMin);
		break;

	case DLL_THREAD_ATTACH:
		break;

	case DLL_THREAD_DETACH:
		break;

	case DLL_PROCESS_DETACH:
		timeEndPeriod(t.wPeriodMin);
		break;
	}
	return TRUE;
}


EXPORT LPCSTR FormatTime(double time) {
	auto ms = dmilliseconds(time);

	// %S automatically includes milliseconds
	std::string str = std::format("{:%M:%S}", ms);
	
	// Necessary
	strncpy(buffer, str.c_str(), BUFFER_SIZE);
	return buffer;
}


EXPORT LPCSTR ScreenshotTimestamp() {
	using namespace std::chrono;
	auto now = floor<seconds>(system_clock::now());

	std::string str = std::format("{:%FT%H%M%S}.png", now);

	strncpy(buffer, str.c_str(), BUFFER_SIZE);
	return buffer;
}


EXPORT LPCSTR ResultsTimestamp() {
	int timeSize = GetTimeFormatEx(
		LOCALE_NAME_USER_DEFAULT,
		0, NULL, NULL,
		NULL, 0
	);
	int dateSize = GetDateFormatEx(
		LOCALE_NAME_USER_DEFAULT,
		0, NULL, NULL,
		NULL, 0,
		NULL
	);

	LPWSTR time = new WCHAR[timeSize];
	LPWSTR date = new WCHAR[dateSize];

	GetTimeFormatEx(
		LOCALE_NAME_USER_DEFAULT,
		0, NULL, NULL,
		time, timeSize
	);
	GetDateFormatEx(
		LOCALE_NAME_USER_DEFAULT,
		0, NULL, NULL,
		date, dateSize,
		NULL
	);

#pragma warning(push)
#pragma warning(disable: 4244)
	std::wstring wstr = std::wstring(date) + L"#" + time;
	std::string str(wstr.begin(), wstr.end());
#pragma warning(pop)

	delete[] time;
	delete[] date;

	strncpy(buffer, str.c_str(), BUFFER_SIZE);
	return buffer;
}