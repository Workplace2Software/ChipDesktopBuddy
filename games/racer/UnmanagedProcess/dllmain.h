#ifndef UNICODE
#define UNICODE
#endif

#define NOGDI
#include <Windows.h>

#include <chrono>
#include <string>
#include <format>

#define EXPORT extern "C" __declspec(dllexport)

// https://philippegroarke.com/posts/2018/chrono_for_humans/
using dmilliseconds = std::chrono::duration<double, std::milli>;
using dseconds = std::chrono::duration<double>;
using dminutes = std::chrono::duration<double, std::ratio<60>>;
using dhours = std::chrono::duration<double, std::ratio<3600>>;
using ddays = std::chrono::duration<double, std::ratio<86400>>;
using dweeks = std::chrono::duration<double, std::ratio<604800>>;
using dmonths = std::chrono::duration<double, std::ratio<2629746>>;
using dyears = std::chrono::duration<double, std::ratio<31556952>>;